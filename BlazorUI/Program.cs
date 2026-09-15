using BlazorUI.Components;
using DemoLibrary.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Qui Singleton ha senso perché DemoDataAccess contiene direttamente i dati in memoria,
// simulando un piccolo database condiviso.
// Singleton: Una sola istanza per tutta l’applicazione: tutti gli utenti accedono alla stessa lista.
builder.Services.AddSingleton<IDemoDataAccess, DemoDataAccess>();
builder.Services.AddMediatR(cfg=>
    cfg.RegisterServicesFromAssemblies(typeof(DemoDataAccess).Assembly));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
