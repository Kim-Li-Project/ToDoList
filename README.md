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

The backend uses a layered architecture inspired by Microsoft’s Clean Architecture guidance, separating Domain,
Application, Infrastructure, and API layers.
https://learn.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures

## Package

- SwaggerUI 10.2.3
- Moq 4.20.72

#Test #Service Tests dotnet test tests/TodoList.Application.Tests/TodoList.Application.Tests.csproj #Api Tests dotnet
test tests/TodoList.Api.Tests/TodoList.Api.Tests.csproj

## Frontend

1. Angular 22.1.7
2. Node.js 22.22.3
3. npm 12.0.2

## Frontend design

In the frontend design, we will use the Angular Component、Router、Signals、Reactive Forms、HttpClient and Vitest to build
the application.

## Running the Application

The frontend and backend run as separate processes during development.

### 1. Start the Backend
run the following command to start the backend under root directory
```bash
dotnet restore backend/TodoList.slnx
dotnet run --project backend/src/TodoList.Api/TodoList.Api.csproj
```
xUnit tests can be run with the following command:
```bash
dotnet test backend/TodoList.slnx
```

### 2. Start the frontend
run the following command to start the frontend
```bash
cd frontend
npm install
npm start or npm run build
```

test can be run with the following command:
```bash
npm test
```

API
http://localhost:5167
Swagger UI:
http://localhost:5167/swagger
Frontend:
http://localhost:4200