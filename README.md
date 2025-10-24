# VisionHive API - Checkpoint 5 (FIAP 2TDSPG)

API desenvolvida como evolução do projeto **VisionHive**, originalmente criado no **CP4** (Oracle Relacional), agora apliado com **MongoDB**, **HealthCheck** e **versionamento de API** via **Swagger**.

O projeto segue o padrão **Clean Architecture + DDD**, com camadas independentes e injeção de dependências configurada.

---

## Sumário
- [Arquitetura do Projeto](#arquitetura-do-projeto)
- [Objetivos do CP5](#objetivos-do-cp5)
- [Tecnologias Utilizadas](#tecnologias-utilizadas)
- [Execução do Projeto](#execução-do-projeto)
- [Estrutura de Pastas](#estrutura-de-pastas)
- [Health Checks](#health-checks)
- [Versionamento da API](#versionamento-da-api)
- [Rotas e Exemplos](#rotas-e-exemplos)
  - [Versão 1 (Oracle)](#versão-1-oracle)
  - [Versão 2 (MongoDB)](#versão-2-mongodb)
- [Autores](#autores)

---

## Arquitetura do Projeto

O projeto segue o padrão **Clean Architecture**, garantindo separação de responsabilidades e escalabilidade.

```bash
VisionHive.API/ → Camada de apresentação (Controllers, Swagger, Versionamento)
VisionHive.Application/ → Casos de uso, DTOs e validações
VisionHive.Domain/ → Entidades e regras de negócio
VisionHive.Infrastructure/→ Acesso a dados (Oracle + MongoDB), Contextos e Repositórios
```

---

## Objetivos do CP5
**Evoluir a aplicação do CP4**, adicionando:
- CRUD completo com **MongoDB**
- **Health Checks** para oracle e MongoDB
- **Versionamento via Swagger** (`v1` = Oracle, `v2` = Mongo)
- Aplicar **Clean Architecture e DDD**
- **Boas práticas REST** (HTTP verbs e status code)
- **Documentação completa no GitHub**

---

## Tecnologias Utilizadas

| Tecnologia | Função |
|-------------|--------|
| .NET 8.0 | Framework principal |
| Entity Framework Core 9 | ORM para Oracle (v1) |
| MongoDB.Driver 3.0 | Driver oficial para MongoDB (v2) |
| Asp.Versioning.Mvc | Versionamento de API |
| Swashbuckle.AspNetCore | Documentação Swagger |
| AspNetCore.HealthChecks.MongoDb / Oracle / Uris | Monitoramento de saúde da aplicação |
| FluentValidation | Validação de DTOs |
| Docker (opcional) | Containerização |

---

## Execução do Projeto

### Pré-requisitos
- .NET SDK 8.0 instalado
- MongoDB rodando localmente (ou remoto)
- Banco Oracle (apenas para v1)

### Executar a API

No diretório raiz do projeto:
```bash
dotnet restore
dotnet build
dotnet run --project VisionHive.API
```

Por padrão o servidor roda em:
`http://localhost:5259/swagger`

---

## Estrutura das Pastas

```bash
VisionHive/
 ├── VisionHive.API/
 │    ├── Controllers/
 │    │     ├── v1/
 │    │     └── v2/
 │    ├── Program.cs
 │    └── appsettings.json
 ├── VisionHive.Application/
 │    ├── DTO/
 │    ├── Validators/
 │    └── UseCases/
 ├── VisionHive.Domain/
 │    └── Entities/
 └── VisionHive.Infrastructure/
      ├── Contexts/
      ├── Repositories/
      └── Repositories/Mongo/
```

---

## Health Checks

A aplicação expõe o endpoint global `/health` com o status da aplicação e dos bancos:

### Exemplo de resposta:
```bash
{
  "status": "Healthy",
  "checks": [
    { "name": "self", "status": "Healthy" },
    { "name": "oracle", "status": "Healthy" },
    { "name": "mongodb", "status": "Healthy" }
  ]
}
```

---

## Versionamento da API

O projeto utiliza o pacote `Asp.Versioning.Mvc` para manter duas versões coexistentes:
| VERSÃO | DESCRIÇÃO | BANCO |
|--------|-----------|-------|
|   v1   | Versão relacional (mantida no CP4) | Oracle |
|   v2   | Versão não-relacional (nova no CP5) | MongoDB |

As duas versões aparecem no **Swagger** em abas separadas: 
`/swagger/index.html`

---

## Rotas e Exemplos

### Versão 1 (Oracle)

| MÉTODO | ROTA | DESCRIÇÃO |
|--------|------|-----------|
|   GET  | `api/v1/filiais` | Lista de filiais paginadas (Oracle) |
|   GET  | `api/v1/filiais/{id}` | Busca filial por ID |
|   POST  | `api/v1/filiais` | Cadastra nova filial |
|   PUT  | `api/v1/filiais/{id}` | Atualiza filial |
|   DELETE  | `api/v1/filiais/{id}` | Remove filial |

### Versão 2 (MongoDB)

