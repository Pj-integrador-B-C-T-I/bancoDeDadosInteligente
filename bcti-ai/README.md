# Wiki do Backend - bcti-ai

## Visão Geral
O backend `bcti-ai` é uma aplicação Python que fornece funcionalidades de inteligência artificial para o projeto Banco de Dados Inteligente. Ele está localizado no diretório `bcti-ai/ai_api` e utiliza bibliotecas específicas para IA e manipulação de dados.

## Estrutura do Projeto
Abaixo está a estrutura principal do diretório `ai_api`:

```
ai_api/
├── Dockerfile          # Configuração para container Docker
├── requirements.txt    # Dependências do projeto
├── app/                # Código-fonte principal da aplicação
```

### Diretório `app`
O diretório `app` contém o código principal da aplicação, incluindo os módulos e scripts necessários para o funcionamento do backend.

## Dependências
As dependências do projeto estão listadas no arquivo `requirements.txt`. Certifique-se de instalar essas dependências antes de executar a aplicação. Para instalar, use o comando:

```bash
pip install -r requirements.txt
```

## Uso do Docker
O projeto inclui um `Dockerfile` para facilitar a criação de um container Docker. Para construir e executar o container, siga os passos abaixo:

1. Construa a imagem Docker:

   ```bash
   docker build -t bcti-ai-backend .
   ```

2. Execute o container:

   ```bash
   docker run -p 8000:8000 bcti-ai-backend
   ```

## Contribuindo
Para contribuir com o desenvolvimento do backend, siga estas etapas:

1. Faça um fork do repositório.
2. Crie uma nova branch para suas alterações:

   ```bash
   git checkout -b minha-feature
   ```

3. Faça commit das suas alterações:

   ```bash
   git commit -m "Descrição das alterações"
   ```

4. Envie suas alterações para o repositório remoto:

   ```bash
   git push origin minha-feature
   ```

5. Abra um Pull Request para revisão.

## Contato
Para dúvidas ou suporte, entre em contato com a equipe de desenvolvimento.

---

Este documento será atualizado conforme o projeto evolui.
