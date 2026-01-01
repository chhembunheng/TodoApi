# TodoApi - Professional ASP.NET Core REST API

A professional, production-ready Todo API built with ASP.NET Core 9.0, Entity Framework Core, and Microsoft SQL Server. Features clean architecture, repository pattern, service layer, DTOs, comprehensive error handling, and Swagger documentation.

## ✨ Features

- **Clean Architecture** - Proper separation of concerns with layers
- **Repository Pattern** - Data access abstraction
- **Service Layer** - Business logic separation
- **DTOs** - Data Transfer Objects for API contracts
- **Error Handling** - Comprehensive exception handling and logging
- **Swagger UI** - Interactive API documentation
- **CORS** - Cross-Origin Resource Sharing enabled
- **Timestamps** - Automatic CreatedAt and UpdatedAt tracking
- **Validation** - Model validation with data annotations
- **Async/Await** - Full asynchronous operations

## Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/get-started) (for SQL Server)
- [dotnet-ef CLI tools](https://docs.microsoft.com/en-us/ef/core/cli/dotnet)

## Getting Started

### 1. Install SQL Server (using Docker)

Run SQL Server 2022 in a Docker container:

```bash
sudo docker run -e "ACCEPT_EULA=Y" \
  -e "MSSQL_SA_PASSWORD=YourStrong@Passw0rd" \
  -p 1433:1433 \
  --name sqlserver2022 \
  --hostname sqlserver2022 \
  -d mcr.microsoft.com/mssql/server:2022-latest
```

Verify SQL Server is running:

```bash
sudo docker ps
```

### 2. Configure Connection String

The connection string is in `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=TodoDb;User Id=sa;Password=YourStrong@Passw0rd;TrustServerCertificate=True;"
}
```

**Note:** Update the password if you used a different one.

### 3. Install EF Core Tools

```bash
dotnet tool install --global dotnet-ef --version 9.0.0
```

### 4. Apply Database Migrations

```bash
cd TodoApi
dotnet ef database update
```

### 5. Run the Application

```bash
dotnet run
```

The API will be available at: `http://localhost:5165`

**Swagger UI** will be accessible at: `http://localhost:5165` (root URL in development)

## API Endpoints

### Get All Todos
```
GET /api/todo
```

Example:
```bash
curl http://localhost:5165/api/todo
```

Response:
```json
[
  {
    "id": 1,
    "title": "Buy groceries",
    "isCompleted": false,
    "createdAt": "2026-01-02T10:30:00Z",
    "updatedAt": null
  }
]
```

### Get Todo by ID
```
GET /api/todo/{id}
```

Example:
```bash
curl http://localhost:5165/api/todo/1
```

### Create a New Todo
```
POST /api/todo
Content-Type: application/json

{
  "title": "Your todo title",
  "isCompleted": false
}
```

Example:
```bash
curl -X POST http://localhost:5165/api/todo \
  -H "Content-Type: application/json" \
  -d '{"title": "Buy groceries", "isCompleted": false}'
```

### Update a Todo
```
PUT /api/todo/{id}
Content-Type: application/json

{
  "title": "Updated title",
  "isCompleted": true
}
```

Example:
```bash
curl -X PUT http://localhost:5165/api/todo/1 \
  -H "Content-Type: application/json" \
  -d '{"title": "Buy groceries - Done", "isCompleted": true}'
```

### Toggle Todo Completion
```
PATCH /api/todo/{id}/toggle
```

Example:
```bash
curl -X PATCH http://localhost:5165/api/todo/1/toggle
```

### Delete a Todo
```
DELETE /api/todo/{id}
```

Example:
```bash
curl -X DELETE http://localhost:5165/api/todo/1
```

## Project Structure

```
TodoApi/
├── Controllers/
│   └── TodoController.cs          # API endpoints with error handling
├── Data/
│   └── TodoContext.cs              # EF Core DbContext
├── DTOs/
│   ├── CreateTodoDto.cs            # DTO for creating todos
│   ├── UpdateTodoDto.cs            # DTO for updating todos
│   └── TodoDto.cs                  # DTO for todo responses
├── Interface/
│   ├── ITodoRepository.cs          # Repository interface
│   └── ITodoService.cs             # Service interface
├── Migrations/                     # EF Core migrations
│   └── [timestamp]_InitialCreate.cs
├── Model/
│   └── Todo.cs                     # Todo entity model
├── Properties/
│   └── launchSettings.json         # Launch configuration
├── Repository/
│   └── TodoRepository.cs           # Repository implementation
├── Service/
│   └── TodoService.cs              # Business logic service
├── appsettings.json                # Configuration
├── Program.cs                      # Application entry point
└── TodoApi.csproj                  # Project file
```

## Architecture Layers

### 1. **Controllers Layer**
- Handles HTTP requests/responses
- Input validation
- Error handling and logging
- Returns proper HTTP status codes

### 2. **Service Layer**
- Business logic
- DTO mapping
- Orchestrates repository calls

### 3. **Repository Layer**
- Data access abstraction
- EF Core operations
- Database queries

### 4. **Data Layer**
- DbContext configuration
- Entity configurations

### 5. **DTOs (Data Transfer Objects)**
- API contracts
- Input validation
- Decouples entities from API responses

## Technologies Used

- **ASP.NET Core 9.0** - Web framework
- **Entity Framework Core 9.0** - ORM
- **SQL Server 2022** - Database
- **Microsoft.Data.SqlClient** - SQL Server provider

## Database Schema

### Todos Table

| Column       | Type          | Constraints           |
|-------------|---------------|-----------------------|
| Id          | int           | Primary Key, Identity |
| Title       | nvarchar(200) | Required, MaxLength   |
| IsCompleted | bit           | Required              |
| CreatedAt   | datetime2     | Required              |
| UpdatedAt   | datetime2     | Nullable              |


### Logging
- Console logging enabled
- Debug logging enabled
- Error logging with context

### CORS
CORS is enabled for all origins in development. Configure appropriately for production.

## Troubleshooting

### SQL Server Connection Issues

1. Check Docker container is running:
   ```bash
   sudo docker ps
   ```

2. Start the container if stopped:
   ```bash
   sudo docker start sqlserver2022
   ```

3. Check SQL Server logs:
   ```bash
   sudo docker logs sqlserver2022
   ```

### Port Already in Use

If port 5165 is already in use, modify `Properties/launchSettings.json`:

```json
"applicationUrl": "http://localhost:YOUR_PORT"
```

### Migration Issues

Reset migrations:

```bash
# Remove all migrations
rm -rf Migrations

# Create new migration
dotnet ef migrations add InitialCreate

# Apply migration
dotnet ef database update
```

### Build Errors

Clean and rebuild:

```bash
dotnet clean
dotnet build
```

## API Response Codes

| Code | Description |
|------|-------------|
| 200  | OK - Request succeeded |
| 201  | Created - Resource created successfully |
| 204  | No Content - Resource deleted successfully |
| 400  | Bad Request - Invalid input |
| 404  | Not Found - Resource not found |
| 500  | Internal Server Error - Server error |

## Best Practices Implemented

✅ Repository Pattern for data access  
✅ Service Layer for business logic  
✅ DTOs for API contracts  
✅ Async/Await throughout  
✅ Dependency Injection  
✅ Error handling and logging  
✅ Model validation  
✅ Clean architecture  
✅ SOLID principles  

## Stop SQL Server

To stop the SQL Server container:

```bash
sudo docker stop sqlserver2022
```

To remove the container:

```bash
sudo docker rm sqlserver2022
```

## License

This project is for educational purposes.
