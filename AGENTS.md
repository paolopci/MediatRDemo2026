# Repository Guidelines

## Struttura e architettura

`MediatoRSolution.slnx` comprende due progetti .NET 10:
- `BlazorUI/`: applicazione Blazor con supporto Interactive Server; pagine in `Components/Pages/`, layout in `Components/Layout/`, asset e Bootstrap in `wwwroot/`.
- `DemoLibrary/`: libreria con MediatR 14.2.0. Contiene `Models/`, `DataAccess/`, `Queries/` e `Handles/` (nome effettivo della cartella degli handler). `Commands/` è predisposta nel progetto.

L'accesso dati è in memoria: `IDemoDataAccess` è registrato come singleton in `BlazorUI/Program.cs`. Gli handler MediatR esistono, ma la relativa registrazione DI non è ancora presente. Mantenere logica applicativa nella libreria e presentazione nei componenti Razor.

## Build e sviluppo locale

Eseguire dalla radice con SDK .NET 10 disponibile:

```powershell
dotnet restore MediatoRSolution.slnx
dotnet build MediatoRSolution.slnx --no-restore
dotnet run --project BlazorUI --no-restore --launch-profile https
dotnet watch --project BlazorUI --launch-profile https
```

I comandi ripristinano le dipendenze, compilano, avviano l'applicazione e abilitano il ricaricamento durante lo sviluppo. Il profilo HTTPS espone `https://localhost:7095` e `http://localhost:5242`. Richiedere autorizzazione prima di installare SDK, strumenti o dipendenze mancanti.

## Stile e convenzioni

Usare quattro spazi per l'indentazione C# e mantenere lo stile del file modificato. Usare PascalCase per tipi, metodi e proprietà, camelCase per parametri e variabili, prefisso `I` per interfacce. Seguire nomi quali `GetPersonListQuery` e `GetPersonListHandler`. Nullable e implicit usings sono abilitati: gestire esplicitamente valori null e inizializzazione. Non risultano formatter, linter o `.editorconfig` dedicati.

## Verifiche e test

Non sono presenti progetti di test né soglie di copertura. Per nuovi test, preferire xUnit e nomi `Metodo_Scenario_RisultatoAtteso`, previa autorizzazione alle dipendenze. Dopo averli aggiunti, eseguire `dotnet test MediatoRSolution.slnx`. Per modifiche funzionali, compilare e verificare le pagine coinvolte. Attualmente `Home.razor` lancia `NotImplementedException`: una build riuscita non dimostra il funzionamento della pagina. Dichiarare verifiche omesse e motivi.

## Commit e pull request

La cronologia contiene un solo commit iniziale descrittivo; non emerge una convenzione formalizzata. Usare messaggi brevi e orientati all'azione. Nelle PR indicare scopo, file coinvolti, verifiche eseguite, eventuali issue e screenshot per modifiche visive.

## Regole operative e configurazione

Preservare le modifiche preesistenti. Chiedere permesso prima di eliminazioni, installazioni o attività fuori progetto. Per modifiche significative preparare un piano e attenderne l'approvazione. Non inserire segreti in `appsettings*.json`; spiegare l'impatto prima di modificare configurazioni sensibili.
