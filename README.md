# AnimeHub API

AnimeHub é uma **Web API** construída em **.NET 9**, seguindo o padrão **Clean Architecture**, que gerencia informações sobre animes.  
Ela expõe endpoints RESTful versionados, com suporte a containerização via Docker, logging com Serilog e testes de unidade com xUnit.

---

## Funcionalidades

- Obter todos os animes.
- Obter animes por ID, Diretor, Nome ou qualquer combinação destes.
- Cadastrar um novo anime.
- Alterar um anime existente.
- Excluir um anime.
- Paginação opcional nas consultas.
- Logs estruturados com Serilog.
- Documentação interativa via Swagger.

---

## Stack técnica

- **ASP.NET Core 9**
- **Entity Framework Core** com SQL Server
- **MediatR** (CQRS)
- **Serilog** para logs
- **Swagger/OpenAPI** para documentação
- **xUnit** para testes de unidade
- **Docker + Docker Compose**

---

## Arquitetura (Clean Architecture)
- AnimeHub/
  - src/
    - AnimeHub.Api/              -> Presentation (Controllers, DI, Versioning, Swagger)
    - AnimeHub.Application/      -> Use cases (MediatR, DTOs, Validators)
    - AnimeHub.Domain/           -> Entidades, ValueObjects, Exceptions de domínio
    - AnimeHub.Infrastructure/   -> EF Core, Repositórios, Migrations, Serilog config
    - AnimeHub.UnitTests/        -> xUnit para Handlers e Services
  - .editorconfig
  - .gitignore
  - README.md
  - docker-compose.yml

## Configuração via Docker

Construa e suba os containers:
  docker compose up --build

Acesse a API (Swagger UI): http://localhost:8080/swagger

## Endpoints principais (v1)
| Método | Rota                    | Descrição                                                    |
| ------ | ----------------------- | ------------------------------------------------------------ |
| GET    | `/api/v1/animes`        | Lista todos os animes quando não passado filtro. (paginação) |
| GET    | `/api/v1/animes/{id}`   | Obtém um anime por ID.                                       |
| GET    | `/api/v1/animes`        | Consulta por nome/diretor/ID combinados (suporta paginação). |
| POST   | `/api/v1/animes`        | Cadastra um novo anime.                                      |
| PUT    | `/api/v1/animes/{id}`   | Atualiza um anime existente.                                 |
| DELETE | `/api/v1/animes/{id}`   | Exclui um anime.                                             |
