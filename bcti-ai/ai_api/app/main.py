# main.py
from fastapi import FastAPI, HTTPException, Depends
from fastapi.security import HTTPBearer, HTTPAuthorizationCredentials
from pydantic import BaseModel
import requests
import jwt

from app.services.embedding_service import generate_embedding, gerar_embeddings_para_artigos, get_all_embeddings
from app.services.chat_service import generate_response, chat_with_semantic

app = FastAPI(title="BCTI AI Chat")
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



# ✅ Nova rota GET para listar todos embeddings
@app.get("/api/embedding")
def listar_embeddings():
    embeddings = get_all_embeddings()
    return {"count": len(embeddings), "embeddings": embeddings}