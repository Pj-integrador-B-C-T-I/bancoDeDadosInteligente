# 🧾 Relatório Final – Desenvolvimento do Sistema de Banco de Conhecimentos Técnicos Inteligente (BCTI)

## 🎯 Objetivo do Projeto
O objetivo deste projeto foi desenvolver um sistema web inteligente capaz de centralizar conteúdos técnicos, facilitar o acesso a informações e permitir consultas por meio de um chat com inteligência artificial, além de oferecer recursos colaborativos entre os usuários.

---

## 🗓️ Semana 1 – Planejamento e Estrutura Inicial
Na primeira semana, definimos qual seria o projeto e seus objetivos principais. Fizemos reuniões para entender o problema que queríamos resolver e levantamos os requisitos necessários para o sistema.  
Decidimos utilizar **.NET** para o backend, **Next.js** para o frontend e **PostgreSQL** como banco de dados.  
Também configuramos o repositório no GitHub, criamos o ambiente de desenvolvimento e começamos a montar a base do projeto.  
Além disso, foram elaborados os primeiros protótipos no **Figma**, com as telas de login, dashboard e chat.

---

## 🗓️ Semana 2 – Autenticação e Base de Conhecimento
Na segunda semana, implementamos o sistema de autenticação, com cadastro, login e logout de usuários, garantindo segurança com o uso de **tokens (JWT)**.  
Começamos o módulo da **Base de Conhecimento**, permitindo cadastrar e editar artigos técnicos.  
Também configuramos **categorias e tags** para organizar os conteúdos e incluímos o **upload de arquivos**, possibilitando anexar documentos como PDF e DOCX.  
Ao final da semana, a base do sistema já estava funcional e conectada ao banco de dados.

---

## 🗓️ Semana 3 – Chat Inteligente com IA
Na terceira semana, desenvolvemos a interface do **Chat Inteligente** e conectamos ao backend.  
Implementamos o histórico de conversas para cada usuário e realizamos a integração com um **modelo de IA**, que passou a responder perguntas com base nos artigos da Base de Conhecimento.  
Foram feitos testes com dados reais para validar o funcionamento da IA e aprimorar o retorno das respostas, exibindo também as fontes consultadas.  
Ao final dessa etapa, o chat estava totalmente funcional como **MVP**.
