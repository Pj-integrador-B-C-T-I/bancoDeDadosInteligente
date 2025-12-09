# **Documento de Visão – Banco de Conhecimento Técnico Inteligente (BCTI)**

## Introdução

### **Objetivo do Documento**
Este documento define os requisitos, escopo e planejamento para o desenvolvimento do **Banco de Conhecimento Técnico Inteligente (BCTI)**, uma solução destinada à CTI Comunicação de Dados e Tecnologia LTDA, como parte dos **Projetos Integradores do SENAI**. O objetivo principal é centralizar o conhecimento técnico da empresa, oferecendo busca inteligente, categorização automática e colaboração entre equipes.

### **Escopo do Produto**
O BCTI será um sistema web com funcionalidades avançadas de IA para melhorar a busca e organização do conhecimento técnico da CTI. Ele será desenvolvido por 5 alunos do SENAI-SP e atenderá técnicos, analistas e gestores da organização.

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
=======
### **Visão Geral**
Este documento apresenta o posicionamento, stakeholders, arquitetura, requisitos, riscos, cronograma, user stories e protótipo textual, conforme critérios do Projeto Integrador.

---

#  **DESENVOLVIMENTO**

## **Problemática**
Atualmente, a CTI não possui um sistema centralizado de conhecimento técnico, causando:

- Perda de tempo na busca por soluções
- Duplicação de esforços
- Baixa colaboração entre equipes
- Inconsistências em processos internos

O BCTI resolve esses problemas centralizando informações e aplicando IA para busca e categorização.

---

## **Descrição do Produto**
O **Banco de Conhecimento Técnico Inteligente** é um sistema web com:

- Cadastro de artigos técnicos
- Busca inteligente por linguagem natural
- Categorização automática
- Chat inteligente
- Relatórios de uso

---

## **Declaração de Posição do Produto**
Para profissionais de TI da CTI, o BCTI é um sistema de gerenciamento de conhecimento que aumenta a produtividade, reduz o tempo de busca e incentiva a colaboração.

---

#  **Usuários e Stakeholders**

## **Stakeholders**
- **Patrocinador:** Diretoria da CTI  
- **Desenvolvedores:** 5 alunos do SENAI Sorocaba  
- **Usuários finais:** Técnicos, analistas e gerentes de TI  
- **Suporte:** TI interno da CTI  
- **Avaliadores:** Instrutores SENAI e representantes do Saga  

---

## **Perfis**
- **Técnico de Suporte:** Busca soluções rápidas  
- **Analista de Sistemas:** Documenta e integra sistemas  
- **Gerente de Projetos:** Analisa relatórios e acessos  

---

## **Ambiente Operacional**
- Navegadores: Chrome, Firefox, Edge  
- Hospedagem: Servidores internos ou nuvem  

---

## **Principais Funcionalidades**
- Cadastro e edição de artigos técnicos  
- Sistema de busca inteligente  
- Categorização automática por IA  
- Chat inteligente  
- Relatórios e métricas  

---

## **Limitações**
- Não substitui treinamentos  
- Requer internet  
- Depende da qualidade dos modelos de IA  

---

#  **Requisitos**

## **Requisitos Funcionais**
- **RF01**: Cadastro e edição de artigos  
- **RF02**: Busca por palavra-chave  
- **RF03**: Categorização automática  
- **RF04**: Chat inteligente  

## **Requisitos Não Funcionais**
- **RNF01**: Resposta da busca < 10s  
- **RNF02**: Suporte a 30 usuários simultâneos  
- **RNF03**: Compatível com Chrome, Firefox, Edge, mobile  
- **RNF04**: Disponibilidade comercial  

---

#  **Características de Qualidade**
- **Usabilidade:** Interface intuitiva  
- **Confiabilidade:** Disponibilidade comercial  
- **Desempenho:** Resposta de até 5s  
- **Segurança:** Login e senha  
- **Portabilidade:** Navegadores modernos  

---

#  **Riscos**
- Atraso nas APIs cognitivas  
- Falhas na infraestrutura  
- Baixa qualidade dos dados  
- Resistência dos usuários  

---

#  **Cronograma**
- **11/08/2025** – Documento de Visão  
- **22/08/2025** – Protótipo + Sprints  
- **03/10/2025** – Aplicação Parcial  
- **Outubro/Novembro 2025** – Artigo Científico  
- **12/12/2025** – Aplicação Final  

---

#  **User Stories**
- **US01:** Buscar soluções rapidamente  
- **US02:** Cadastrar artigos técnicos  
- **US03:** Visualizar relatórios de acesso  

---

#  **Protótipo (Descrição para Figma)**

### **Tela de Login**
- Email, senha, botão entrar

### **Tela de Busca**
- Campo de busca com linguagem natural  
- Lista de resultados categorizados  

### **Tela de Cadastro**
- Formulário + anexos + sugestão automática de categorias  

### **Tela de Relatórios**
- Gráficos e estatísticas  

### **Tela de Chat Inteligente**
- Perguntas e respostas automáticas  

---

# **Arquitetura Geral do Sistema**

### **Frontend**
- Next.js + React + Typescript

### **Backend**
- ASP.NET Core (APIs REST)

### **Banco de Dados**
- PostgreSQL

### **Versionamento**
- GitHub com GitFlow

---

#  **Apêndices**

### **Glossário**
- **BCTI:** Banco de Conhecimento Técnico Inteligente

### **Informações Complementares**
- **Local:** SENAI Sorocaba  
- **Período:** Dez/2024 a Dez/2025  
- **Equipe:** 5 alunos  

