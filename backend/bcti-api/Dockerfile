# ===============================
# Etapa 1 - Build
# ===============================
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copia o arquivo de projeto primeiro (melhor cache)
COPY BancoDeConhecimentoInteligente.csproj ./

# Restaura dependências
RUN dotnet restore BancoDeConhecimentoInteligente.csproj

# Copia o restante do código
COPY . ./

# Publica a aplicação (otimizado para produção)
RUN dotnet publish BancoDeConhecimentoInteligente.csproj -c Release -o /app/out /p:UseAppHost=false


# ===============================
# Etapa 2 - Runtime
# ===============================
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

# Copia os arquivos publicados
COPY --from=build /app/out .

# Define variáveis de ambiente
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://+:8080
ENV DOTNET_RUNNING_IN_CONTAINER=true
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

# Expõe a porta usada pelo Render
EXPOSE 8080

# Ponto de entrada
ENTRYPOINT ["dotnet", "BancoDeConhecimentoInteligente.dll"]
