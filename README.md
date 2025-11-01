# MinimalApiDemo

Minimal ASP.NET Core 8 API demonstrating:
- Minimal APIs
- Clean Architecture (Domain / Application / Infrastructure / Api)
- EF Core + Migrations
- Repository pattern (async)
- Swagger, Logging, Global Exception Middleware

## How to run locally
1. Ensure .NET 8 SDK installed
2. Update connection string in `Api/appsettings.json`
3. Run migrations:
   ```bash
   cd src/Api
   dotnet ef database update --project ../Infrastructure --startup-project .
