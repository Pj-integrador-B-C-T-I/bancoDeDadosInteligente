# Banco de Conhecimentos Técnicos Inteligente (BCTI)

## 📌 Links Importantes
- **Documentação de Visão (Notion):** [Notion-BCTI](https://www.notion.so/Documento-de-Vis-o-Banco-de-Conhecimento-T-cnico-Inteligente-9fa5c04504a9460f826e28496e93fe9d?source=copy_link)  
- **Protótipo no Figma:** [Figma-BCTI](https://www.figma.com/design/YyMuWpLQ9j3jOzT3qSID3F/PJ_integrador?node-id=0-1&t=i0llnCYYjtMmTRwU-1)  

---

## 🚀 Planejamento de Sprints

### 🟢 Sprint 1 – Planejamento e Estrutura Inicial
📅 Duração: ~1 mês  
🎯 Objetivo: Definir requisitos, preparar ambiente e criar base do sistema.  
**Entregas:**
- Configuração do repositório GitHub (monorepo ou separados).  
- Setup do backend (API inicial com .NET).  
- Setup do frontend (Desenvolvimento do Figma e estruturação do projeto em Next.js).  
- Criação do banco de dados inicial (PostgreSQL + tabelas principais).  
- Prototipação das telas principais no Figma (login, dashboard, chat).  

---

### 🟡 Sprint 2 – Autenticação e Base de Conhecimento
📅 Duração: ~1 mês  
🎯 Objetivo: Criar fluxo de usuários e primeiro CRUD de conhecimento.  
**Entregas:**
- Implementar autenticação (JWT ou Identity).  
- Cadastro/Login/Logout de usuários.  
- Página de **Base de Conhecimento** com CRUD de artigos.  
- Suporte a **categorias e tags**.  
- Estrutura para upload de documentos (PDF, DOCX, etc).
  
---

### 🟠 Sprint 3 – Chat Inteligente com IA (MVP)  
📅 Duração: ~1 mês  
🎯 Objetivo: Integrar o chat com IA e busca semântica básica.  
**Entregas:**
- Página de **Chat Inteligente** no frontend.  
- Histórico de conversas por usuário.  
- Integração com modelo de IA (Ollama, Gemini ou OpenAI somente para teste das funcionalidades do frontend).  
- Retorno da IA com citações das fontes.  
- Testes iniciais com dados reais da empresa.  

---

### 🔵 Sprint 4 – Administração e Colaboração
📅 Duração: ~1 mês  
🎯 Objetivo: Criar área administrativa e recursos colaborativos.  
**Entregas:**
- Página de **gestão de usuários** (roles: user, moderador, admin).  
- Página de **configurações do sistema** (categorias, tags, integrações).  
- Workflow de **aprovação de artigos** (opcional).  
- Comentários e avaliações em artigos.  
- Estatísticas básicas (artigos mais acessados, buscas mais feitas).  

---

### 🔴 Sprint 5 – Refinamento e Entrega Final
📅 Duração: ~1 mês  
🎯 Objetivo: Ajustes, testes finais e preparação para entrega.  
**Entregas:**
- Melhorias de UX/UI no frontend (animações, responsividade).  
- Documentação completa no README + Notion.  
- Deploy do backend (Docker + servidor).  
- Deploy do frontend (Vercel ou similar).  
- Deploy do banco (Postgres + pgvector).  
- Testes de usabilidade e ajustes finais.  
- Apresentação final do projeto.  

---

## ✅ Resultado esperado
Ao final das 5 sprints teremos um **Sistema de Banco de Conhecimentos Técnicos Inteligente (BCTI)** que permitirá:
- Centralizar e gerenciar conteúdos técnicos.  
- Oferecer um **chat inteligente** que consulta dados internos.  
- Suporte a **colaboração** entre usuários.  
- **Interface moderna e responsiva**, acessível por web e mobile.  
