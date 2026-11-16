# North - Gestor de Gastos Personales

Portfolio project - ASP.NET Core 9 + Angular 19 + PostgreSQL (Supabase)

## Demo
Coming soon (Fase 8)

## Arquitectura
Diagrama en Fase 8

## Como correr el proyecto

### Requisitos
- .NET 9 SDK
- Node.js 20+
- Cuenta en Supabase (https://supabase.com)

### Backend
  cd backend
  dotnet restore
  dotnet user-secrets set ConnectionString Host=...
  dotnet run --project src/North.Api

### Frontend
  cd frontend/north-app
  npm install
  ng serve

## Tests
  cd backend
  dotnet test
