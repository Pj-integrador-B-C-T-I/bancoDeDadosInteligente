import os
import requests
from app.services.embedding_service import generate_embedding, buscar_artigos_semanticos

OLLAMA_API_URL = os.getenv("OLLAMA_API_URL", "http://ollama:11434")


def generate_response(user_input: str, context: str = "", model: str = "llama2:latest", temperature: float = 0.3):
    prompt = f"""
Você é um assistente corporativo.
Contexto da empresa: {context}
Pergunta do usuário: {user_input}
Responda de forma clara e útil. Se não houver informação, improvise com base no contexto geral.
"""
    url = f"{OLLAMA_API_URL}/v1/completions"
    payload = {
        "model": model,
        "prompt": prompt,
        "temperature": temperature,
        "max_tokens": 512
    }

    try:
        resp = requests.post(url, json=payload)
        resp.raise_for_status()
        data = resp.json()
        return data["choices"][0]["text"].strip()
    except Exception as e:
        print("Chat error:", e, resp.text if 'resp' in locals() else "")
        return "Desculpe, ocorreu um erro ao gerar a resposta."


def chat_with_semantic(user_input: str, top_k: int = 5):
    """Gera resposta usando contexto semântico dos artigos mais relevantes."""
    query_emb = generate_embedding(user_input)
    if not query_emb:
        return "Erro ao gerar embedding para a pergunta."

    top_articles = buscar_artigos_semanticos(query_emb, top_k=top_k)
    context = "\n".join([a[2] for a in top_articles])
    return generate_response(user_input, context=context)
