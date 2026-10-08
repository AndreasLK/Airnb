using Airnb.API.ExceptionHandling;
using Airnb.Application.DependencyInjection;
using Airnb.Infrastructure.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddProblemDetails();                              
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();  //det er vigtigt den kommer før AddControllers, ellers vil den ikke fange exceptions fra controllerne.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi(options =>
{
    options.OpenApiVersion = Microsoft.OpenApi.OpenApiSpecVersion.OpenApi3_0;
    options.AddDocumentTransformer((document, context, cancellationToken) => //Et document transformer er bare en funktion, der får lov at ændre OpenAPI-dokumentet,
                                                                             //inden det bliver sendt ud. Her bruges den kun til at sætte Info,
                                                                             //men den kan også bruges til fx at tilføje login-krav til dokumentationen senere.
    {
        document.Info = new()          //her sætter vi info om API'et, som vil blive vist i Swagger UI
        {
            Title = "Airnb API",
            Version = "v1",            //her sætter vi versionen af API'et, som vil blive vist i Swagger UI
            Description = "API til håndtering af bookinger, boliger og gæster."
        };
        return Task.CompletedTask;
    });
});
builder.Services.AddCors(options =>             //her sætter vi CORS policy, så vores Blazor client kan tilgå API'et uden problemer.                               
{
    options.AddPolicy("BlazorClient", policy =>
        policy.WithOrigins("https://localhost:7065")
              .AllowAnyHeader()
              .AllowAnyMethod());
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)      //her sætter vi op, hvordan vi vil validere JWT tokens, som kommer med i requesten. Vi bruger JwtBearer, som er standarden for JWT tokens.
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],    

            ValidateAudience = true,
            ValidAudience = builder.Configuration["Jwt:Audience"],

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),

            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();
app.UseExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "v1"); //her sætter vi endpointet for Swagger UI, som vil blive brugt til at hente OpenAPI-dokumentet. Det skal matche det, vi satte i AddOpenApi.
    });
}

app.UseHttpsRedirection();

app.UseCors("BlazorClient"); //rækkefølgen gør noget i forhold til UseCors skal komme, før requesten når frem til controllerne.

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
