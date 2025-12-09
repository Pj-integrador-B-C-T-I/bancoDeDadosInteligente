# main.py
from fastapi import FastAPI, HTTPException, Depends
from fastapi.security import HTTPBearer, HTTPAuthorizationCredentials
from pydantic import BaseModel
import requests
import jwt
import json
from fastapi.responses import StreamingResponse

from fastapi.middleware.cors import CORSMiddleware

from app.services.embedding_service import generate_embedding, gerar_embeddings_para_artigos, get_all_embeddings
from app.services.chat_service import generate_response, chat_with_semantic

app = FastAPI(title="BCTI AI Chat")

# ⚡ Configuração CORS
origins = [
    "http://localhost:3000",  # front-end React
    "http://127.0.0.1:3000",
    "http://localhost:8000",  # se for acessar de outra rota
]

app.add_middleware(
    CORSMiddleware,
    allow_origins=origins,       # permitir front-end
    allow_credentials=True,
    allow_methods=["*"],         # permite GET, POST, OPTIONS, PATCH, etc
    allow_headers=["*"],         # permite Authorization, Content-Type, etc
)

DOTNET_API_URL = "http://bcti_api/api"  # URL do backend .NET

security = HTTPBearer()

class EmbeddingRequest(BaseModel):
    text: str
    model: str = "llama2"


class ChatRequest(BaseModel):
    question: str
    context: str = ""  # contexto opcional


@app.post("/api/embedding/generate")
def embedding(req: EmbeddingRequest):
    emb = generate_embedding(req.text, req.model)
    return {"model": req.model, "embedding": emb}


@app.post("/api/embedding/generate/all")
def gerar_todos_embeddings():
    gerar_embeddings_para_artigos()
    return {"status": "ok", "message": "Embeddings gerados e salvos com sucesso!"}


@app.post("/api/chat")
def chat(req: ChatRequest):
    resp = generate_response(req.question, req.context)
    return {"answer": resp}



# ✅ Nova rota GET para listar todos embeddings
@app.get("/api/embedding")
def listar_embeddings():
    embeddings = get_all_embeddings()
    return {"count": len(embeddings), "embeddings": embeddings}


@app.post("/api/chat/semantic")
def chat_semantic(req: ChatRequest, credentials: HTTPAuthorizationCredentials = Depends(security)):
    """
    Chat semântico que cria chat, pergunta no backend .NET e atualiza a resposta gerada.
    """

    # Extrair userId do JWT
    token = credentials.credentials
    try:
        payload = jwt.decode(token, options={"verify_signature": False})
        user_id = int(payload.get(
            "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"
        ))
    except Exception as e:
        raise HTTPException(status_code=401, detail=f"Token inválido: {e}")

    headers = {"Authorization": f"Bearer {token}"}

    # 1️⃣ Criar chat no backend .NET (título = primeira pergunta)
    chat_payload = {"userId": user_id, "title": req.question}
    chat_resp = requests.post(f"{DOTNET_API_URL}/Chat", json=chat_payload, headers=headers)
    chat_resp.raise_for_status()
    chat_data = chat_resp.json()
    chat_id = chat_data["id"]

    # 2️⃣ Criar a pergunta no backend .NET
    question_payload = {"chatId": chat_id, "content": req.question}
    question_resp = requests.post(f"{DOTNET_API_URL}/Question", json=question_payload, headers=headers)
    question_resp.raise_for_status()
    question_data = question_resp.json()
    question_id = question_data["id"]

    # 3️⃣ Gerar resposta semântica local
    answer_content = chat_with_semantic(req.question)

    # 4️⃣ Atualizar a resposta no backend .NET via PATCH
    patch_payload = {"content": answer_content}
    patch_resp = requests.patch(f"{DOTNET_API_URL}/Answer/by-question/{question_id}", json=patch_payload, headers=headers)
    patch_resp.raise_for_status()
    patched_answer = patch_resp.json()

    # Retornar dados completos
    return {
        "chat": chat_data,
        "question": question_data,
        "patched_answer": patched_answer,
        "answer_generated": answer_content
    }


@app.post("/api/chat/semantic/{chat_id}")
def continue_chat_semantic(chat_id: int, req: ChatRequest, credentials: HTTPAuthorizationCredentials = Depends(security)):
    """
    Continua uma conversa existente (usa chat_id existente).
    Cria apenas nova pergunta e resposta.
    """
    token = credentials.credentials
    try:
        payload = jwt.decode(token, options={"verify_signature": False})
    except Exception as e:
        raise HTTPException(status_code=401, detail=f"Token inválido: {e}")

    headers = {"Authorization": f"Bearer {token}"}

    # Criar nova pergunta para o chat existente
    question_payload = {"chatId": chat_id, "content": req.question}
    question_resp = requests.post(f"{DOTNET_API_URL}/Question", json=question_payload, headers=headers)
    question_resp.raise_for_status()
    question_data = question_resp.json()
    question_id = question_data["id"]

    # Gerar resposta
    answer_content = chat_with_semantic(req.question)

    # Atualizar resposta
    patch_payload = {"content": answer_content}
    patch_resp = requests.patch(f"{DOTNET_API_URL}/Answer/by-question/{question_id}", json=patch_payload, headers=headers)
    patch_resp.raise_for_status()
    patched_answer = patch_resp.json()

    return {
        "chat_id": chat_id,
        "question": question_data,
        "patched_answer": patched_answer,
        "answer_generated": answer_content
        
    }


def stream_response(user_input: str, context: str = "", model: str = "llama2"):
    url = "http://ollama:11434/api/generate"
    prompt = f"""
Você é um assistente corporativo da CTI...
Pergunta: {user_input}
Contexto: {context}
"""
    payload = {
        "model": model,
        "prompt": prompt,
        "stream": True
    }

    with requests.post(url, json=payload, stream=True) as r:
        for line in r.iter_lines():
            if line:
                data = json.loads(line.decode("utf-8"))
                if "response" in data:
                    yield data["response"]  # envia o token
                if data.get("done"):
                    break



@app.post("/api/chat/semantic/stream/{chat_id}")
def chat_semantic_stream(chat_id: int, req: ChatRequest, credentials: HTTPAuthorizationCredentials = Depends(security)):
    token = credentials.credentials
    payload = jwt.decode(token, options={"verify_signature": False})
    headers = {"Authorization": f"Bearer {token}"}

    # cria pergunta no .NET
    question_payload = {"chatId": chat_id, "content": req.question}
    question_resp = requests.post(f"{DOTNET_API_URL}/Question", json=question_payload, headers=headers)
    question_resp.raise_for_status()
    question_data = question_resp.json()
    question_id = question_data["id"]

    def event_stream():
        collected = ""
        for token_text in stream_response(req.question):
            collected += token_text
            # envia cada token imediatamente
            yield token_text
            yield ""  # força o envio do chunk ao cliente
        # após concluir, atualiza no .NET
        patch_payload = {"content": collected}
        requests.patch(f"{DOTNET_API_URL}/Answer/by-question/{question_id}", json=patch_payload, headers=headers)


    return StreamingResponse(event_stream(), media_type="application/octet-stream")


