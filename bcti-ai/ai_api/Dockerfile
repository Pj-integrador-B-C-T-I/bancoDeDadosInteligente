# ai_api/Dockerfile
FROM python:3.11-slim

# Define diretório de trabalho
WORKDIR /app

# Copia o arquivo de dependências
COPY requirements.txt .

# Instala as dependências
RUN pip install --no-cache-dir -r requirements.txt

# Copia o restante do código
COPY ./app ./app

# Expõe a porta do FastAPI
EXPOSE 8000

# Comando de inicialização (sem --reload em produção)
CMD ["uvicorn", "app.main:app", "--host", "0.0.0.0", "--port", "8000"]
