# Wiki do Backend

## Introdução
O backend deste projeto foi desenvolvido em .NET e serve como a API principal para o sistema de Banco de Conhecimento Inteligente. Ele gerencia a lógica de negócios, persistência de dados e fornece endpoints para o frontend e outros serviços consumirem.

## Estrutura do Projeto
Abaixo está uma visão geral da estrutura do backend:

- **Controllers/**: Contém os controladores da API, responsáveis por lidar com as requisições HTTP.
- **Data/**: Contém o contexto do banco de dados (`AppDbContext`).
- **Dtos/**: Contém os objetos de transferência de dados (Data Transfer Objects).
- **Migrations/**: Contém as migrações do Entity Framework para gerenciar o banco de dados.
- **Models/**: Contém os modelos de dados usados pelo Entity Framework.
- **Services/**: Contém os serviços que encapsulam a lógica de negócios.
- **Program.cs**: Ponto de entrada da aplicação.
- **appsettings.json**: Arquivo de configuração da aplicação.

## Configuração do Ambiente

1. Certifique-se de ter o .NET SDK instalado. Você pode verificar com o comando:
   ```bash
   dotnet --version
   ```

2. Restaure as dependências do projeto:
   ```bash
   dotnet restore
   ```

3. Configure o banco de dados no arquivo `appsettings.Development.json`.

4. Aplique as migrações para criar o banco de dados:
   ```bash
   dotnet ef database update
   ```

## Endpoints da API
Os principais endpoints estão organizados nos seguintes controladores:

- **AnswerController**: Gerencia respostas.
- **ArticleController**: Gerencia artigos.
- **AuthController**: Gerencia autenticação e autorização.
- **CategoryController**: Gerencia categorias.
- **ChatController**: Gerencia chats.
- **DocumentController**: Gerencia documentos.

Exemplo de endpoint:
```http
GET /api/answers
```
Retorna todas as respostas.

## Banco de Dados
O projeto utiliza o Entity Framework Core para gerenciar o banco de dados. As migrações estão localizadas na pasta `Migrations/`.

Para criar uma nova migração:
```bash
dotnet ef migrations add NomeDaMigracao
```

## Execução e Testes

Para executar o projeto em modo de desenvolvimento:
```bash
dotnet run
```

Para rodar os testes (caso existam):
```bash
dotnet test
```

## Docker
O backend pode ser executado em um contêiner Docker. Certifique-se de ter o Docker instalado e utilize o arquivo `Dockerfile` fornecido.

1. Construa a imagem Docker:
   ```bash
   docker build -t backend-image .
   ```

2. Execute o contêiner:
   ```bash
   docker run -p 5000:5000 backend-image
   ```

Agora o backend estará disponível em `http://localhost:5184`.

---

Essa wiki fornece uma visão geral do backend e deve ser expandida conforme necessário.
