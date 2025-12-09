# Wiki Geral do Projeto - Banco de Dados Inteligente

## Visão Geral
O projeto **Banco de Dados Inteligente** é uma solução integrada que combina backend, frontend e inteligência artificial para gerenciar e processar informações de forma eficiente. Ele é dividido em três principais componentes:

1. **Backend**: Localizado em `backend/`, implementado em C#.
2. **Frontend**: Localizado em `frontend/bcti-app/`, implementado com Next.js.
3. **Inteligência Artificial**: Localizado em `bcti-ai/ai_api/`, implementado em Python.

## Estrutura do Repositório
Abaixo está a estrutura principal do repositório:

```
/
├── backend/                # Backend do projeto
├── frontend/               # Frontend do projeto
├── bcti-ai/                # Backend de IA
├── docker-compose.yml      # Configuração para orquestração com Docker
```

### Backend
- **Localização**: `backend/`
- **Descrição**: Implementado em C#, fornece a API principal para o sistema.
- **Documentação**: Consulte o arquivo [wiki-backend.md](backend/wiki-backend.md) para mais detalhes.

### Frontend
- **Localização**: `frontend/bcti-app/`
- **Descrição**: Implementado com Next.js, fornece a interface do usuário.
- **Documentação**: Consulte o arquivo [wiki-frontend.md](frontend/wiki-frontend.md) para mais detalhes.

### Inteligência Artificial
- **Localização**: `bcti-ai/ai_api/`
- **Descrição**: Implementado em Python, fornece funcionalidades de IA.
- **Documentação**: Consulte o arquivo [wiki-backend.md](bcti-ai/wiki-backend.md) para mais detalhes.

## Configuração do Ambiente
### Requisitos
- Docker e Docker Compose instalados.
- Node.js e npm para o frontend.
- Python 3.8+ para o backend de IA.

### Passos para Configuração
1. Clone o repositório:

   ```bash
   git clone https://github.com/Pj-integrador-B-C-T-I/bancoDeDadosInteligente.git
   ```

2. Navegue até o diretório do projeto:

   ```bash
   cd bancoDeDadosInteligente
   ```

3. Configure e inicie os serviços com Docker Compose:

   ```bash
   docker-compose up
   ```

## Contribuindo
Para contribuir com o projeto, siga estas etapas:

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
