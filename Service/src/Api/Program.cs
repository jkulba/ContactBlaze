var builder = WebApplication.CreateBuilder(args);

// Configure user secrets (automatically included in Development environment)
// User secrets are automatically loaded when running in Development environment
// You can also explicitly add them if needed:
// builder.Configuration.AddUserSecrets<Program>();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

// Example endpoint to demonstrate reading user secrets and configuration
app.MapGet("/config", (IConfiguration config) =>
{
    // Example: Reading secret values from user secrets
    var connectionString = config["ConnectionStrings:DefaultConnection"];
    var apiKey = config["ApiKeys:ExternalService"];
    var secretValue = config["MySecret"];

    return new
    {
        Message = "Configuration loaded successfully",
        HasConnectionString = !string.IsNullOrEmpty(connectionString),
        HasApiKey = !string.IsNullOrEmpty(apiKey),
        HasSecretValue = !string.IsNullOrEmpty(secretValue),
        Environment = app.Environment.EnvironmentName,
        UserSecretsId = config["UserSecretsId"] ?? "Not configured"
    };
})
.WithName("GetConfiguration");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
