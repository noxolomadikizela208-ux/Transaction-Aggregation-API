# Nox API

This is a .NET 8 API I built to pull together transactions from multiple bank sources, categorize them, and hand back a clean summary for a given customer. Nothing fancy — just a straightforward aggregation service.

## What it does

The idea is simple: a customer might have accounts at different banks. Instead of hitting each bank separately, you call one endpoint and get everything back in one go — with each transaction tagged by category (salary, groceries, transport, etc.).

Right now Bank A and Bank B are stubbed out with hardcoded data, but the structure is set up so plugging in real sources later should be painless.

## Project structure

```
Nox.API/
├── Controllers/        — just the one endpoint for now
├── Application/
│   ├── Interfaces/     — ITransactionSource, ITransactionAggregateService
│   ├── Services/       — aggregation + categorization logic
│   └── DTOs/           — response shapes
├── Infrastructure/
│   └── Sources/        — BankA and BankB data sources
└── Models/
    └── Domain/         — Transaction, CategorizedTransaction, TransactionCategory
```

I kept it layered so the business logic doesn't care where the data comes from. Each bank source implements `ITransactionSource` and gets picked up automatically by the aggregation service.

## The endpoint

```
GET /api/transactions/{customerId}
```

Pass in a customer ID and you'll get back their transactions from all sources, plus totals.

Example:
```
GET /api/transactions/C001
```

```json
{
  "customerId": "C001",
  "totalTransactions": 2,
  "totalAmount": 19820.00,
  "transactions": [
    {
      "transaction": {
        "id": "A003",
        "customerId": "C001",
        "amount": 20000.00,
        "date": "2026-06-04T...",
        "description": "salary",
        "sourceAccount": "BankA"
      },
      "category": "Income"
    },
    {
      "transaction": {
        "id": "B001",
        "customerId": "C001",
        "amount": -180.00,
        "date": "2026-06-05T...",
        "description": "Grocery Store",
        "sourceAccount": "BankB"
      },
      "category": "Food"
    }
  ]
}
```

## How categorization works

It's keyword-based for now — nothing ML, just string matching:

| Category    | How it's detected                              |
|-------------|------------------------------------------------|
| `Income`    | Amount is positive                             |
| `Food`      | Description has "grocery" or "supermarket"     |
| `Transport` | Description has "uber" or "lyft"               |
| `Other`     | Anything that doesn't match the above          |

Easy to extend if you need more categories.

## Running it

You'll need .NET 8 SDK installed. Then:

```bash
cd Nox.API
dotnet run
```

Hit `https://localhost:{port}/swagger` to poke around with the Swagger UI.

## Running with Docker

If you have NuGet access inside Docker:

```bash
docker build -t nox-api -f Nox.API/Dockerfile Nox.API/
docker run -d -p 8080:80 --name nox-api nox-api
```

If you're behind a corporate proxy (NuGet won't resolve inside the container), publish first then build:

```bash
dotnet publish Nox.API/Nox.API.csproj -c Release -o Nox.API/docker-out
docker build -t nox-api -f Nox.API/Dockerfile.runtime Nox.API/
docker run -d -p 8080:80 --name nox-api nox-api
```

App runs on `http://localhost:8080`.

```bash
docker logs nox-api     # check logs
docker stop nox-api     # stop it
docker rm nox-api       # clean up
```

## Adding a new bank source

1. Add a class to `Infrastructure/Sources/` that implements `ITransactionSource`
2. Register it in `Program.cs`:
```csharp
builder.Services.AddScoped<ITransactionSource, BankCTransactionSource>();
```

That's it — the aggregation service picks it up automatically.

## Status

Still a work in progress. The bank sources are stubbed with dummy data and there's no real database yet (EF Core with SQL Server is wired in but not really used). Real integrations and proper persistence are next on the list.
