import os
import requests
from app.services.embedding_service import generate_embedding, buscar_artigos_semanticos

OLLAMA_API_URL = os.getenv("OLLAMA_API_URL", "http://ollama:11434")

# 🔹 Contexto corporativo fixo da CTI
CTI_CONTEXT = """
A CTI (Computer Technology Industry) é uma empresa brasileira de tecnologia especializada em fornecer soluções para automação de processos, segurança e infraestrutura de TI.
Ela oferece produtos e serviços voltados para automação industrial, controle, monitoramento, consultoria e integração de sistemas.
A CTI atua principalmente no mercado B2B, com foco em inovação, segurança e eficiência operacional.
"""

def generate_response(
    user_input: str,
    context: str = "",
    model: str = "llama2:latest",
    temperature: float = 0.3
):
    # 🔹 Se não houver contexto, ainda assim o modelo saberá o que é a CTI
    prompt = f"""
Você é um assistente corporativo da CTI (Computer Technology Industry), 
uma empresa brasileira de tecnologia que atua com automação, segurança e infraestrutura de TI.

Seu papel:
- Responda de forma clara, estruturada e técnica.
- Use **Markdown válido** para formatar o texto.
- **Títulos:** use apenas #, ##, ### para H1, H2, H3. Evite ===== ou -----.
- **Negrito e itálico:** use **texto** e *texto*, nunca HTML ou outros símbolos.
- **Listas:** use * item ou - item. Evite indentação com 4 espaços.
- **Blocos de código:** use ``` para delimitar código.
- Evite parágrafos longos — mantenha frases curtas e bem divididas.
- Organize a resposta com títulos, listas, tabelas, blocos de código ou exemplos práticos quando fizer sentido.
- Destaque termos importantes em **negrito** ou *itálico*.


Contexto fixo da empresa:
{CTI_CONTEXT}

Contexto adicional relacionado à pergunta (extraído do banco de conhecimento):
{context or "Nenhum dado adicional foi encontrado."}

Pergunta do usuário:
{user_input}

Responda sempre em **português do Brasil**, de forma técnica, clara, direta e resumida, contextualizada com as áreas de atuação da CTI e formatação em Markdown.
Se não houver informações no contexto, explique o conceito de forma geral, mas mantendo a coerência com o domínio tecnológico da empresa.
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
    context = "\n".join([a[2] for a in top_articles]) if top_articles else ""

    return generate_response(user_input, context=context)
