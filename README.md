# Feature Flag System (.NET Core)

## 📌 Overview


This project implements a **production-style Feature Flag (Feature Toggle) system** using **.NET Core**,


###SOLID Principles Applied
Principle	How
SRP -	Engine, services, repositories separated
OCP -	New override levels don’t break logic
LSP -	Interfaces used consistently
ISP -	Small, focused interfaces
DIP -	Services depend on abstractions

## 🚀 Running the Project

### Prerequisites
- .NET 9 SDK
- SQLite (via EF Core)

### Restore & Build
```bash
dotnet restore
dotnet build

### Run Tests
```bash
dotnet test

### Run Api
```bash
dotnet run --project src/FeatureFlags.Api


