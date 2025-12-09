# Wiki do Frontend - bcti-app

## Visão Geral
O frontend `bcti-app` é uma aplicação web desenvolvida com Next.js, que fornece a interface do usuário para o projeto Banco de Dados Inteligente. Ele está localizado no diretório `frontend/bcti-app/`.

## Estrutura do Projeto
Abaixo está a estrutura principal do diretório `bcti-app`:

```
bcti-app/
├── components.json       # Configuração de componentes
├── eslint.config.mjs     # Configuração do ESLint
├── next-env.d.ts         # Tipos do Next.js
├── next.config.ts        # Configuração do Next.js
├── package.json          # Dependências do projeto
├── postcss.config.mjs    # Configuração do PostCSS
├── tsconfig.json         # Configuração do TypeScript
├── public/               # Arquivos públicos (imagens, etc.)
├── src/                  # Código-fonte principal
```

### Diretório `src`
O diretório `src` contém o código principal da aplicação, incluindo páginas, componentes e estilos.

## Dependências
As dependências do projeto estão listadas no arquivo `package.json`. Certifique-se de instalar essas dependências antes de executar a aplicação. Para instalar, use o comando:

```bash
npm install
```

## Scripts Disponíveis
Os scripts disponíveis no `package.json` incluem:

- **Iniciar o servidor de desenvolvimento**:

  ```bash
  npm run dev
  ```

- **Construir a aplicação para produção**:

  ```bash
  npm run build
  ```

- **Iniciar o servidor de produção**:

  ```bash
  npm start
  ```

- **Executar o ESLint**:

  ```bash
  npm run lint
  ```

## Configuração do Ambiente
Certifique-se de configurar as variáveis de ambiente necessárias no arquivo `.env.local` antes de executar a aplicação. Consulte a documentação do Next.js para mais detalhes sobre variáveis de ambiente.

## Contribuindo
Para contribuir com o desenvolvimento do frontend, siga estas etapas:

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
