# Bookstore API

Uma API desenvolvida em ASP .NET Core WebAPI para gerenciar operações essenciais no sistema de uma loja de livros. Feita exclusivamente para estudar sobre C#, WebAPI, Entity Framework e SQL Server.

## Recursos

- Gerenciamento de livros e gêneros.
- Pesquisa avançada de livros e informações detalhadas.

## Recursos futuros

- Autenticação e autorização para acesso aos recursos.
- Gerenciamento de autores.
- Integração com sistemas de pagamento.
- Rastreamento de transações e histórico de compras.

## Pré-Requisitos

Para começar a usar a Bookstore API, você precisará ter:

- .NET SDK instalado (versão 7.0 ou superior).
- SQL Server.

## Primeiros passos

1. Crie um arquivo chamado `.env` na raiz do projeto com base no [.env.example](.env.example)
2. Configure a string de conexão do SQL Server
3. Execute o comando `dotnet ef database update`
4. Rodar o projeto `dotnet run seed` (não é obrigatório usar `seed` ele serve para colocar alguns dados no banco)
5. Para ver os endpoints você pode, por exemplo, entrar em `localhost:5161/swagger`
