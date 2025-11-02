from sqlalchemy import create_engine
import os

DB_USER = os.getenv("DB_USER", "postgres")
DB_PASSWORD = os.getenv("DB_PASSWORD", "your_secure_password")
DB_NAME = os.getenv("DB_NAME", "BancoDeConhecimentoInteligentedb")
DB_HOST = os.getenv("DB_HOST", "postgres")
DB_PORT = os.getenv("DB_PORT", "5432")

DATABASE_URL = f"postgresql+psycopg2://{DB_USER}:{DB_PASSWORD}@{DB_HOST}:{DB_PORT}/{DB_NAME}"

engine = create_engine(DATABASE_URL, echo=True)

try:
    with engine.connect() as connection:
        result = connection.execute("SELECT 1;")
        print("✅ Conexão bem-sucedida com o banco:", result.scalar())
except Exception as e:
    print("❌ Erro ao conectar ao banco:", e)
