# APBD API Template

Template ASP.NET Core Web API dla egzaminu APBD. Generuje projekt z kontrolerami, prostym CQRS, EF Core, SQL Server, generycznym repozytorium, Unit of Work oraz walidacją requestów przed wykonaniem handlera.

## Wymagania

- .NET SDK 10
- Docker, jeśli chcesz uruchamiać lokalny SQL Server z `docker-compose.yml`

## Instalacja template'u

Z katalogu tego repozytorium uruchom:

```bash
dotnet new install .
```

Po instalacji template powinien być widoczny jako:

```bash
dotnet new list apbd-api
```

## Tworzenie nowego projektu

```bash
dotnet new apbd-api -n MyExamApi
cd MyExamApi
```

Nazwa `MyExamApi` zostanie podstawiona w nazwie projektu, namespace'ach i plikach generowanych z `Apbd`.

## Baza danych

Template używa SQL Server i connection stringa `DefaultConnection` w `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=ApbdDb;User Id=sa;Password=YourStrongPassword123!;TrustServerCertificate=True"
  }
}
```

Możesz uruchomić lokalną bazę z Docker Compose:

```bash
docker compose up -d
```

Domyślne dane są zgodne z `docker-compose.yml`:

- server: `localhost,1433`
- user: `sa`
- password: `YourStrongPassword123!`
- database: `ApbdDb`

## Migracje EF Core

Po wygenerowaniu projektu i uruchomieniu bazy:

```bash
dotnet restore
dotnet ef database update
```

Jeśli `dotnet ef` nie jest zainstalowane:

```bash
dotnet tool install --global dotnet-ef
```

Nowa migracja:

```bash
dotnet ef migrations add MigrationName
dotnet ef database update
```

## Uruchomienie API

```bash
dotnet run
```

Domyślne adresy z `Properties/launchSettings.json`:

- HTTP: `http://localhost:5000`
- HTTPS: `https://localhost:5001`

Przykładowe requesty są w pliku `Apbd.http`.

## CQRS i walidacja requestów

Request implementuje `IRequest<TResponse>`, handler implementuje `IRequestHandler<TRequest, TResponse>`.

Walidator requestu implementuje:

```csharp
public class CreateWeatherRequestValidator : IRequestValidator<CreateWeatherRequest>
{
    public Task<IReadOnlyCollection<ValidationError>> ValidateAsync(CreateWeatherRequest request)
    {
        var errors = new List<ValidationError>();

        if (string.IsNullOrWhiteSpace(request.City))
        {
            errors.Add(new ValidationError(nameof(request.City), "City is required."));
        }

        return Task.FromResult<IReadOnlyCollection<ValidationError>>(errors);
    }
}
```

Wszystkie `IRequestValidator<TRequest>` są rejestrowane automatycznie w `Program.cs`. `Dispatcher` uruchamia walidatory przed handlerem. Jeśli walidacja zwróci błędy, rzucany jest `RequestValidationException`, a `ExceptionHandlingMiddleware` zwraca HTTP 400 z `application/problem+json`.

## Dodawanie nowego endpointu

1. Dodaj request w katalogu `Features/<Feature>/<Action>`.
2. Dodaj handler i zarejestruj go w `RegisterRequestHandlers` w `Program.cs`.
3. Opcjonalnie dodaj validator `IRequestValidator<TRequest>`.
4. Dodaj akcję w kontrolerze i wywołaj `Dispatcher.Dispatch(request)`.

Validatorów nie trzeba rejestrować ręcznie.
