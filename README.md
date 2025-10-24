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
