# Number Classification API

## Introduction
This API classifies numbers and provides interesting properties along with a fun fact.

## Technology Stack
- ASP.NET Core
- Entity Framework Core
- Swagger

## How to Run
1. Clone the repository.
2. Navigate to the project directory.
3. Run `dotnet run`.

## API Specification
- **Endpoint**: `GET /api/classify-number?number={number}`
- **Response (200 OK)**:
```json
{
    "number": 371,
    "is_prime": false,
    "is_perfect": false,
    "properties": ["armstrong", "odd"],
    "digit_sum": 11,
    "fun_fact": "371 is an Armstrong number because 3^3 + 7^3 + 1^3 = 371"
}
