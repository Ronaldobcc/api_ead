# Plataforma EAD Funcidance

Plataforma de ensino a distância (EAD) desenvolvida para a academia Funcidance, oferecendo aulas em vídeo, sistema de cadastro de alunos e autenticação por e-mail. Aplicação full-stack com backend em ASP.NET Core e frontend integrado.

## Tecnologias utilizadas

- .NET 8.0 (ASP.NET Core Web API)
- Entity Framework Core (SQL Server e MySQL)
- Autenticação via e-mail
- Razor Pages para views
- Newtonsoft.Json para manipulação JSON
- Banco de dados relacional (SQL Server/MySQL)

## Funcionalidades principais

- Cadastro e gerenciamento de alunos
- Login e autenticação segura por e-mail
- Área de aulas em vídeo com suporte a vídeos online
- Página de administração para gerenciamento de conteúdo
- Integração com banco de dados usando Entity Framework Core
- Plataforma responsiva para acesso via web

## Como rodar o projeto

1. Clone o repositório
2. Configure a string de conexão no `appsettings.json`
3. Execute as migrations do Entity Framework para criar o banco:

dotnet ef database update

4. Rode o projeto:

dotnet run

## Contribuição

Contribuições são bem-vindas! Abra issues ou envie pull requests para melhorias.

## Contato

Desenvolvido por Ronaldo.  
Email: ronaldofotoevento@gmail.com  
LinkedIn: https://www.linkedin.com/in/ronaldo-batista-84b876241/

---

