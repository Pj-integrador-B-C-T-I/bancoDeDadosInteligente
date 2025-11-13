# app/services/embedding_service.py
from sqlalchemy import text
from app.db import engine
import requests
import json
import numpy as np


OLLAMA_URL = "http://ollama:11434"

def generate_embedding(texto: str, modelo: str = "nomic-embed-text"):
    """
    Gera um embedding usando o Ollama.
    """
    response = requests.post(
        f"{OLLAMA_URL}/api/embeddings",
        json={"model": modelo, "prompt": texto}
    )
    if response.status_code != 200:
        print("Erro ao gerar embedding:", response.text)
        return None
    
    data = response.json()
    return data.get("embedding", [])


def salvar_embedding(article_id: int, chunk: str, vector, source="article", source_id=None):
    """
    Salva o embedding gerado no banco.
    """
    vector_json = json.dumps(vector)
    with engine.begin() as conn:
        conn.execute(
            text("""
                INSERT INTO "Embeddings" ("ArticleId", "Chunk", "VectorJson", "Source", "SourceId", "CreatedAt")
                VALUES (:article_id, :chunk, :vector_json, :source, :source_id, NOW())
            """),
            {
                "article_id": article_id,
                "chunk": chunk,
                "vector_json": vector_json,
                "source": source,
                "source_id": source_id
            }
        )
    print(f"✅ Embedding salvo para artigo {article_id}")


def gerar_embeddings_para_artigos():
    """
    Busca artigos e gera embeddings automaticamente.
    """
    with engine.begin() as conn:
        artigos = conn.execute(text('SELECT "Id", "Content" FROM "Articles"')).fetchall()

        for artigo in artigos:
            print(f"🧠 Gerando embedding para artigo {artigo.Id}...")
            embedding = generate_embedding(artigo.Content)
            if embedding:
                salvar_embedding(artigo.Id, artigo.Content[:500], embedding)
            else:
                print(f"❌ Falha ao gerar embedding para artigo {artigo.Id}")


def get_all_embeddings():
    """
    Retorna todos os embeddings salvos no banco.
    """
    with engine.begin() as conn:
        rows = conn.execute(text("""
            SELECT "Id", "ArticleId", "Chunk", "VectorJson", "Source", "SourceId", "CreatedAt"
            FROM "Embeddings"
            ORDER BY "CreatedAt" DESC
        """)).fetchall()

    # Converte para lista de dicionários
    embeddings = [
        {
            "id": row.Id,
            "article_id": row.ArticleId,
            "chunk": row.Chunk,
            "vector": row.VectorJson,
            "source": row.Source,
            "source_id": row.SourceId,
            "created_at": str(row.CreatedAt)
        }
        for row in rows
    ]

    return embeddings

# ----------------------
# Busca semântica
# ----------------------

def cosine_similarity(vec1, vec2):
    vec1 = np.array(vec1)
    vec2 = np.array(vec2)
    return np.dot(vec1, vec2) / (np.linalg.norm(vec1) * np.linalg.norm(vec2))


def buscar_artigos_semanticos(query_embedding, top_k=5):
    """Retorna os top_k artigos mais relevantes para um embedding."""
    with engine.begin() as conn:
        embeddings = conn.execute(text('SELECT "ArticleId", "Chunk", "VectorJson" FROM "Embeddings"')).fetchall()

    scores = []
    for e in embeddings:
        vector = json.loads(e.VectorJson)
        score = cosine_similarity(query_embedding, vector)
        scores.append((score, e.ArticleId, e.Chunk))

    scores.sort(reverse=True, key=lambda x: x[0])
    return scores[:top_k]