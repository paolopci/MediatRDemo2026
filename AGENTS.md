# Repository Guidelines

## Struttura e architettura

`MediatoRSolution.slnx` comprende tre progetti .NET 10:
- `BlazorUI/`: applicazione Blazor con supporto Interactive Server; pagine in `Components/Pages/`, layout in `Components/Layout/`, asset e Bootstrap in `wwwroot/`.
- `DemoLibrary/`: libreria con MediatR 14.2.0. Contiene `Models/`, `DataAccess/`, `Queries/` e `Handles/` (nome effettivo della cartella degli handler). `Commands/` è predisposta nel progetto.
- `DemoApi/`: Web API con controller, MediatR 14.2.0 e `Microsoft.AspNetCore.OpenApi` 10.0.11. Espone `GET /WeatherForecast`; non ha ancora un riferimento a `DemoLibrary` né una registrazione MediatR nella DI.

L'accesso dati è in memoria: `IDemoDataAccess` è registrato come singleton in `BlazorUI/Program.cs`. MediatR registra gli handler dall'assembly di `DemoDataAccess`. `Home.razor` invia `GetPersonListQuery` tramite `IMediator`; `GetPersonListHandler` legge le persone da `IDemoDataAccess`. Mantenere logica applicativa nella libreria e presentazione nei componenti Razor. I dati sono condivisi nell'istanza Blazor e si perdono al riavvio; non sono presenti database o migrazioni.

## Build e sviluppo locale

Eseguire dalla radice con SDK .NET 10 disponibile:

```powershell
dotnet restore MediatoRSolution.slnx
dotnet build MediatoRSolution.slnx --no-restore
dotnet run --project BlazorUI --no-restore --launch-profile https
dotnet watch --project BlazorUI --launch-profile https
dotnet run --project DemoApi --no-restore --launch-profile https
```

I comandi consentono ripristino, compilazione, avvio e ricaricamento durante lo sviluppo; scegliere il comando di avvio del progetto interessato. Il profilo HTTPS di Blazor espone `https://localhost:7095` e `http://localhost:5242`; quello di DemoApi espone `https://localhost:7259` e `http://localhost:5044`. Per eseguire entrambi, usare terminali separati. DemoApi non apre automaticamente il browser: aprire `/WeatherForecast` oppure, in Development, `/openapi/v1.json`. Non è configurata una UI Swagger. Richiedere autorizzazione prima di installare SDK, strumenti o dipendenze mancanti.

## Stile e convenzioni

Usare quattro spazi per l'indentazione C# e mantenere lo stile del file modificato. Usare PascalCase per tipi, metodi e proprietà, camelCase per parametri e variabili, prefisso `I` per interfacce. Seguire nomi quali `GetPersonListQuery` e `GetPersonListHandler`. Nullable e implicit usings sono abilitati: gestire esplicitamente valori null e inizializzazione. Non risultano formatter, linter o `.editorconfig` dedicati.

## Verifiche e test

Non sono presenti progetti di test né soglie di copertura. Per nuovi test, preferire xUnit e nomi `Metodo_Scenario_RisultatoAtteso`, previa autorizzazione alle dipendenze. Dopo averli aggiunti, eseguire `dotnet test MediatoRSolution.slnx`. Per modifiche funzionali, compilare e verificare le pagine o gli endpoint coinvolti: la Home deve visualizzare le persone e `/WeatherForecast` deve restituire una risposta JSON. Controllare DI, async/await e nullability: `Home.razor` dichiara `people` senza inizializzazione e lo enumera nel rendering. Una build riuscita non dimostra il funzionamento a runtime. Dichiarare verifiche omesse e motivi.

## Commit e pull request

La cronologia usa messaggi descrittivi senza una convenzione formalizzata. Usare messaggi brevi e orientati all'azione. Nelle PR indicare scopo, file coinvolti, verifiche eseguite, eventuali issue e screenshot per modifiche visive.

## Regole operative e configurazione

Preservare le modifiche preesistenti. Chiedere permesso prima di eliminazioni, installazioni o attività fuori progetto. Per modifiche significative preparare un piano e attenderne l'approvazione. Non inserire segreti in `appsettings*.json`; spiegare l'impatto prima di modificare configurazioni sensibili.
