using ProjectManagement.API.GraphQL;
using ProjectManagement.Core;
using ProjectManagement.Infra;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureServices();
builder.Services.ConfigureGraphQL();
builder.Services.ConfigureEF(builder.Configuration);

var app = builder.Build();
app.Services.MigrateDatabase();

app.MapGet("/", () => "Hello World!");
app.MapGraphQL();

app.Run();
