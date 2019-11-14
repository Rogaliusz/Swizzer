Swizzer - very simple chat application created with good programming practices in .net core 3.0.

__Solution includes:__
* Web api:
  - .net core 3.0, SignalR, AutoMapper, Entity Framework (PostgreSQL), Autofac.
  - CQRS, DTOs, Adapters, IoC, Singleton.
* Frontend:
  - wpf, prism, unity, fluent validator.
  - MVVM, CQRS, Facade, IoC, Singleton.
* Common:
  - .net standard 2.0
  - providers, dtos
  
 ![Alt text](/docs/packages.png?raw=true "Optional Title")

__Migration instruction__

https://docs.microsoft.com/pl-pl/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli

```
dotnet ef migrations add ${name} -s ${project_path} -o Sql/Migrations   

dotnet ef database update -s ${project_path} 
```
