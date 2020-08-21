# Lviv Regional Puppet Theater

## Backend technology stack:
- .NET Core 2.2
- ASP.NET Core (with SignalR Core)
- ORM: Entity Framework Core
- DB: SQLite
- Unit tests: xUnit
- GitLab CI

## How to setup:  
```
dotnet restore
dotnet ef migrations add InitialCreate
dotnet ef database update
dotnet run
```
## API documentation:
http://localhost:5000/swagger

heeey guuys
