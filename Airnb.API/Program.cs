using Airnb.Application.DependencyInjection;
using Airnb.Infrastructure.DependencyInjection;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi(options =>
{
    options.OpenApiVersion = Microsoft.OpenApi.OpenApiSpecVersion.OpenApi3_0;
    options.AddDocumentTransformer((document, context, cancellationToken) => //Et document transformer er bare en funktion, der får lov at ændre OpenAPI-dokumentet,
                                                                             //inden det bliver sendt ud. Her bruges den kun til at sætte Info,
                                                                             //men den kan også bruges til fx at tilføje login-krav til dokumentationen senere.
    {
        document.Info = new()
        {
            Title = "Airnb API",
            Version = "v1",
            Description = "API til håndtering af bookinger, boliger og gæster."
        };
        return Task.CompletedTask;
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
