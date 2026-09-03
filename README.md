## Todo List Application

## Build with ASP.NET web API and Angular

1. The user must be able to see their TODO list, add items to it and delete items from it
2. Manage the data on the backend in memory

## Functional Requirements
1. View Todo List
2. Add new Todo record
3. Delete Todo record

## Technology

## Backend
1. .NET 10.0.400
2. Swagger UI
3. xUnit

## Backend design
The backend uses a layered architecture inspired by Microsoft’s Clean Architecture guidance, 
separating Domain, Application, Infrastructure, and API layers.
https://learn.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures

## Package
- SwaggerUI 10.2.3
- Moq 4.20.72

#Test
#Service Tests
dotnet test tests/TodoList.Application.Tests/TodoList.Application.Tests.csproj
#Api Tests
dotnet test tests/TodoList.Api.Tests/TodoList.Api.Tests.csproj


## Frontend
1. Angular



## Running the Application

The frontend and backend run as separate processes during development.

### 1. Start the Backend

```bash
dotnet restore backend/TodoList.slnx
dotnet run --project backend/src/TodoList.Api/TodoList.Api.csproj
```
#API 
http://localhost:5167
Swagger UI:
http://localhost:5167/swagger