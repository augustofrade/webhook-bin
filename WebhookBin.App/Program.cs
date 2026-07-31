using BlazorBlueprint.Components;
using FluentValidation;
using Scalar.AspNetCore;
using WebhookBin.App.Endpoints;
using WebhookBin.App.UI;
using WebhookBin.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services.AddAndConfigureDbContext(builder.Configuration.GetConnectionString("DefaultConnection"));

builder.Services.AddPublicApiEndpoints();
builder.Services.AddOpenApi();

builder.Services.AddBlazorBlueprintComponents();

var app = builder.Build();

app.MapPublicApiEndpoints();

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
