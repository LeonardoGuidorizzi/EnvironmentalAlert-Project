# 🌍 Projeto - EnvironmentalAlert

## 🧩 Descrição

O **EnvironmentalAlert** é uma API desenvolvida em **C# .NET** com foco em temas **ESG (Environmental, Social and Governance)**.  
O objetivo do projeto é monitorar o consumo de energia de dispositivos e gerar **alertas de consumo** quando limites pré-estabelecidos forem ultrapassados.  
A aplicação foi projetada com arquitetura modular, containerizada com **Docker**, e implantada na nuvem com **Azure App Service**, utilizando **CI/CD via GitHub Actions**.

---

## 🚀 Como executar localmente com Docker

Siga os passos abaixo para rodar a aplicação em um ambiente local usando Docker.

### Pré-requisitos

- [Docker](https://www.docker.com/) instalado
- [.NET SDK 8.0+](https://dotnet.microsoft.com/download)
- Terminal com acesso a linha de comando

### Passos para execução

```bash
# 1. Clone o repositório
git clone https://github.com/LeonardoGuidorizzi/EnvironmentalAlert-Project.git

# 2. Acesse a pasta do projeto
cd EnvironmentalAlert-Project

# 3. Crie a imagem Docker
docker build -t environmentalalert-api:latest .

# 4. Execute o container mapeando a porta 8080
docker run -d -p 8080:8080 environmentalalert-api:latest
```

A aplicação ficará disponível em:  
👉 [http://localhost:8080/swagger/index.html](http://localhost:8080/swagger/index.html)

---

## 🔄 Pipeline CI/CD

O pipeline foi implementado com **GitHub Actions** e **Azure Web App** para automação de build, teste e deploy.

### 🧰 Ferramentas utilizadas

- **GitHub Actions** → Automatiza o pipeline de integração e entrega contínua.  
- **Azure Web App** → Hospeda a aplicação em ambiente de produção.  
- **Docker Hub / Azure Container Registry (ACR)** → Armazena a imagem container da aplicação.  

### ⚙️ Etapas do pipeline

1. **Checkout**: Baixa o código-fonte do repositório.
2. **Build da imagem Docker**: Cria a imagem a partir do Dockerfile.
3. **Testes**: Executa testes unitários (quando configurados).
4. **Push da imagem**: Envia a imagem para o Azure Container Registry.
5. **Deploy no Azure App Service**: Publica a imagem no ambiente de staging e produção.

### 📜 Lógica do pipeline

O workflow é disparado automaticamente nos eventos:
- `push` na branch `main`
- `pull_request` para `main`

Exemplo simplificado de trecho do pipeline:

```yaml
- name: Login no Azure
  uses: azure/login@v2
  with:
    auth-type: 'service-principal'
    client-id: ${{ secrets.AZURE_CLIENT_ID }}
    tenant-id: ${{ secrets.AZURE_TENANT_ID }}
    client-secret: ${{ secrets.AZURE_CLIENT_SECRET }}

- name: Build da imagem Docker
  run: docker build -t environmentalalert-api:latest .

- name: Deploy no Azure Web App
  uses: azure/webapps-deploy@v3
  with:
    app-name: 'enviromentalalert-fiap'
    images: 'environmentalalert-api:latest'
```

---

## 🐳 Containerização

### 🧱 Arquitetura

A aplicação segue uma arquitetura **multi-camada** com:
- **Models** → Representação das entidades (Device, ConsumptionAlert, DeviceConsumption)
- **Controllers** → Endpoints RESTful
- **Services e Repositories** → Lógica de negócio e persistência
- **Swagger** → Documentação interativa da API

### 📦 Dockerfile

```dockerfile
# Etapa 1 - Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o /app

# Etapa 2 - Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .
EXPOSE 8080
ENTRYPOINT ["dotnet", "EnvironmentalAlert.dll"]
```

---

## 🧠 Endpoints Principais

A API segue o padrão RESTful com CRUD completo para as seguintes entidades:

### 🔔 ConsumptionAlert
- `GET /api/ConsumptionAlert`
- `POST /api/ConsumptionAlert`
- `GET /api/ConsumptionAlert/{id}`
- `PUT /api/ConsumptionAlert/{id}`
- `DELETE /api/ConsumptionAlert/{id}`

### ⚙️ Device
- `GET /api/Device`
- `POST /api/Device`
- `GET /api/Device/{id}`
- `PUT /api/Device/{id}`
- `DELETE /api/Device/{id}`

### ⚡ DeviceConsumption
- `GET /api/DeviceConsumption`
- `POST /api/DeviceConsumption`
- `GET /api/DeviceConsumption/{id}`
- `PUT /api/DeviceConsumption/{id}`
- `DELETE /api/DeviceConsumption/{id}`

---

## 🖼️ Prints do funcionamento

### 🔹 Swagger - Rotas disponíveis
![Swagger](./docs/swagger-routes.png)

### 🔹 Azure App Service - Deploy concluído
![Deploy Azure](./docs/azure-deploy.png)

### 🔹 GitHub Actions - Pipeline CI/CD
![Pipeline](./docs/github-actions.png)

---

## 🧩 Tecnologias utilizadas

| Categoria | Tecnologia |
|------------|-------------|
| **Linguagem** | C# (.NET 8) |
| **Framework Web** | ASP.NET Core |
| **Banco de Dados** | SQL Server |
| **ORM** | Entity Framework Core |
| **CI/CD** | GitHub Actions |
| **Cloud** | Azure App Service |
| **Containerização** | Docker |
| **Documentação** | Swagger UI |

---

## ⚠️ Desafios encontrados e soluções

| Desafio | Solução |
|----------|----------|
| Erros de autenticação no Azure Service Principal | Ajuste no `azure/login@v2` com parâmetros corretos (`auth-type`, `client-id`, `tenant-id`, `client-secret`) |
| Falhas no deploy por imagem inexistente | Verificação do build e push no ACR antes do deploy |
| Conflito de portas no Docker local | Definição explícita da porta `8080:8080` no comando `docker run` |
| Integração com Swagger | Adição do pacote `Swashbuckle.AspNetCore` e configuração no `Program.cs` |


