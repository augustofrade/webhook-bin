using BlazorBlueprint.Components;
using FluentValidation;
using Scalar.AspNetCore;
using WebhookBin.App.Public.Endpoints;
using WebhookBin.App.Shared.Bins.Notifications;
using WebhookBin.App.Shared.Commands;
using WebhookBin.App.Shared.Queries;
using WebhookBin.App.UI;
using WebhookBin.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services.AddAndConfigureDbContext(builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.RegisterRepositories();

builder.Services.AddScoped<BinRequestNotifier>();
builder.Services.AddCommandHandlers();
builder.Services.AddQueryHandlers();

builder.Services.AddPublicApiEndpoints();
builder.Services.AddOpenApi();

builder.Services.AddBlazorBlueprintComponents();

builder.Services.AddSignalR();

var app = builder.Build();

app.MapPublicApiEndpoints();
app.MapBinsHub();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
