# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Structure

This is a .NET 9.0 solution containing two projects:
- **TDDCourse**: Test project using NUnit, FluentAssertions, and coverlet for test coverage
- **WebApplication1**: ASP.NET Core MVC web application

## Build and Test Commands

```bash
# Build the solution
dotnet build

# Run all tests
dotnet test

# Run tests with coverage
dotnet test --collect:"XPlat Code Coverage"

# Run specific test project
dotnet test TDDCourse/TDDCourse.csproj

# Build and run the web application
dotnet run --project WebApplication1/WebApplication1.csproj
```

## Architecture Overview

### Budget System Design

The Budget functionality is implemented with the following architecture:

#### Core Components:
- **Budget Class**: Contains `string YearMonth` (format: 'YYYYMM', e.g., '202507') and `int Amount` properties
- **IBudgetRepo Interface**: Repository pattern with `GetAll()` method returning `List<Budget>`
- **BudgetServices Class**: Service layer located in `WebApplication1/Services/BudgetServices.cs`

#### Budget Query Feature:
- **Method**: Budget query functionality in BudgetServices class
- **Parameters**: Start date and end date for querying budget data
- **Return Type**: `decimal` 
- **Data Access**: Uses IBudgetRepo.GetAll() to retrieve budget records
- **Logic**: Filters budgets by date range and calculates total amount

#### Example Budget Data:
```csharp
// Budget example
YearMonth = "202507"  // July 2025
Amount = 310
```

## Testing Framework

- Uses NUnit 4.2.2 for unit testing
- FluentAssertions 6.12.0 for readable assertions
- Test files located in TDDCourse project
- Follow TDD practices when implementing new features