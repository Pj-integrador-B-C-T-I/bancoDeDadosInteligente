from fastapi import Request, HTTPException
import jwt

def get_user_id_from_jwt(request: Request) -> int:
    """
    Extrai o userId do JWT enviado no header Authorization.
    Não valida a assinatura do token, apenas decodifica para pegar o claim.
    """
    auth_header = request.headers.get("Authorization")
    if not auth_header or not auth_header.startswith("Bearer "):
        raise HTTPException(status_code=401, detail="Token JWT não fornecido")
    
    token = auth_header.split(" ")[1]
    
    try:
        payload = jwt.decode(token, options={"verify_signature": False})
        user_id = int(payload.get("nameid"))  # claim usado no backend .NET
        return user_id
    except Exception as e:
        raise HTTPException(status_code=401, detail=f"Token inválido: {e}")
