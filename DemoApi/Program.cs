using DemoLibrary.DataAccess;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi(options =>
{
    options.AddSchemaTransformer((schema, context, cancellationToken) =>
    {
        // Descrive i parametri int come numeri interi per Swagger UI.
        if (context.JsonTypeInfo.Type == typeof(int) &&
            context.ParameterDescription is not null)
        {
            schema.Type = JsonSchemaType.Integer;
            schema.Pattern = null;
        }

        return Task.CompletedTask;
    });
});




builder.Services.AddSingleton<IDemoDataAccess, DemoDataAccess>();
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssemblies(typeof(DemoDataAccess).Assembly));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    // Mostra Swagger UI usando il documento OpenAPI generato da ASP.NET Core.
    app.UseSwaggerUI(options =>
        options.SwaggerEndpoint("../openapi/v1.json", "DemoApi v1"));
}



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
