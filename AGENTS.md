# Repository Guidelines

## Struttura e architettura

`MediatoRSolution.slnx` comprende tre progetti .NET 10:
- `BlazorUI/`: applicazione Blazor con supporto Interactive Server; pagine in `Components/Pages/`, layout in `Components/Layout/`, asset e Bootstrap in `wwwroot/`.
- `DemoLibrary/`: libreria con MediatR 14.2.0. Contiene `Models/`, `DataAccess/`, `Queries/`, `Commands/` e `Handles/` (nome effettivo della cartella degli handler). Le richieste sono `GetPersonListQuery`, `GetPersonByIdQuery` e `InsertPersonCommand`; quest'ultimo è dichiarato nel file `Commands/InsertPersonCommandClass.cs`.
- `DemoApi/`: Web API con riferimento a `DemoLibrary`, MediatR 14.2.0, `Microsoft.AspNetCore.OpenApi` 10.0.11 e `Swashbuckle.AspNetCore.SwaggerUI` 10.2.3. Espone `GET /WeatherForecast`, `GET /api/Person`, `GET /api/Person/{id}` e `POST /api/Person`. Il POST riceve un `PersonModel`, inoltra nome e cognome tramite `InsertPersonCommand` e restituisce la persona con ID assegnato dall'accesso dati.

L'accesso dati è in memoria: entrambi i `Program.cs` registrano `IDemoDataAccess` come singleton e gli handler MediatR dall'assembly di `DemoDataAccess`. API e Blazor hanno istanze dati separate; la Home usa direttamente MediatR, senza chiamare l'API. `GetPersonListHandler` e `InsertPersonHandler` accedono a `IDemoDataAccess`; `GetPersonByIdHandler` invia a sua volta `GetPersonListQuery` e filtra il risultato. Mantenere logica applicativa nella libreria e presentazione nei componenti Razor. I dati sono condivisi solo all'interno della rispettiva applicazione e si perdono al riavvio; non sono presenti database o migrazioni. La lista mutabile e l'assegnazione ID con `Max + 1` non proteggono da inserimenti concorrenti.

## Build e sviluppo locale

Eseguire dalla radice con SDK .NET 10 disponibile:

```powershell
dotnet restore MediatoRSolution.slnx
dotnet build MediatoRSolution.slnx --no-restore
dotnet run --project BlazorUI --no-restore --launch-profile https
dotnet watch --project BlazorUI --launch-profile https
dotnet run --project DemoApi --no-restore --launch-profile https
```

I comandi consentono ripristino, compilazione, avvio e ricaricamento durante lo sviluppo; scegliere il comando di avvio del progetto interessato. Il profilo HTTPS di Blazor espone `https://localhost:7095` e `http://localhost:5242`; quello di DemoApi espone `https://localhost:7259` e `http://localhost:5044`. Per eseguire entrambi, usare terminali separati. I profili di DemoApi configurano `launchBrowser: true` e `launchUrl: swagger`; se il comando o l'IDE non apre il browser, aprire `https://localhost:7259/swagger`. Swagger UI e `/openapi/v1.json` sono disponibili solo in Development. Il documento è generato da `AddOpenApi`; un trasformatore descrive i parametri `int` come interi per Swagger UI. Richiedere autorizzazione prima di installare SDK, strumenti o dipendenze mancanti.

## Stile e convenzioni

Usare quattro spazi per l'indentazione C# e mantenere lo stile del file modificato. Usare PascalCase per tipi, metodi e proprietà, camelCase per parametri e variabili, prefisso `I` per interfacce. Seguire nomi quali `GetPersonListQuery` e `GetPersonListHandler`. Nullable e implicit usings sono abilitati: gestire esplicitamente valori null e inizializzazione. Non risultano formatter, linter o `.editorconfig` dedicati.

## Verifiche e test

Non sono presenti progetti di test né soglie di copertura. Per nuovi test, preferire xUnit e nomi `Metodo_Scenario_RisultatoAtteso`, previa autorizzazione alle dipendenze. Dopo averli aggiunti, eseguire `dotnet test MediatoRSolution.slnx`. Per modifiche funzionali, compilare e verificare le pagine o gli endpoint coinvolti:

- Home: visualizzazione delle persone; API: elenco, ricerca per ID e inserimento con corpo JSON `{"firstName":"Mario","lastName":"Rossi"}`, seguito dalla lettura dell'ID restituito.
- Swagger: caricamento della UI e del documento OpenAPI in Development; `/WeatherForecast`: risposta JSON.
- DI, async/await e nullability: `Home.razor` enumera `people` senza inizializzazione preventiva; `PersonModel` contiene stringhe non inizializzate; la ricerca per ID può restituire `null` con contratto non nullable e non gestisce esplicitamente un `404`. Verificare ID inesistenti e input non validi senza presumere un comportamento già implementato.

Una build riuscita non dimostra il funzionamento a runtime. Dichiarare verifiche omesse e motivi. Per modifiche esclusivamente documentali, verificare coerenza con i sorgenti e `git diff --check`; build e avvio non sono necessari.

## Commit e pull request

La cronologia usa messaggi descrittivi senza una convenzione formalizzata. Usare messaggi brevi e orientati all'azione. Nelle PR indicare scopo, file coinvolti, verifiche eseguite, eventuali issue e screenshot per modifiche visive.

## Regole operative e configurazione

Preservare le modifiche preesistenti. Chiedere permesso prima di eliminazioni, installazioni o attività fuori progetto. Per modifiche significative preparare un piano e attenderne l'approvazione. Non inserire segreti in `appsettings*.json`; spiegare l'impatto prima di modificare configurazioni sensibili.
