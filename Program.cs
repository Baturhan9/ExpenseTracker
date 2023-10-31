using ExpenseTracker.Interfaces;
using ExpenseTracker.Interfaces.ExpenseGroupInterface;
using ExpenseTracker.Interfaces.SpendingInterface;
using ExpenseTracker.Middleware;
using ExpenseTracker.Models;
using ExpenseTracker.Repositorys;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Razor;
using System.Globalization;
using Microsoft.AspNetCore.Localization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix);

builder.Services.AddLocalization(option =>
{
    option.ResourcesPath = "Resources";
});

builder.Services.Configure<RequestLocalizationOptions>(option =>
{
    var supportedCultures = new[] {"en","ru"};
    option.SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);
});

builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IRepository<Client>, ClientRepository>();
builder.Services.AddScoped<IExpenseGroupRepository, ExpenseRepository>();
builder.Services.AddScoped<ISpendingRepository, SpendingRepository>();

builder.Services.AddDbContext<AppExpenseTrackerContext>(option =>
    option.UseNpgsql("Host=localhost;Port=5432;Database=app_expense_tracker;username=postgres;password=1"));

var app = builder.Build();
app.UseRequestLocalization();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession();

app.UseRouting();

app.UseAuthorization();

app.UseMiddleware<CookiesMiddleware>();
app.UseMiddleware<LanguageMiddleware>();
 

app.MapControllerRoute(
    name: "language",
    pattern: "{controller=Language}/{action=Change}/{lang?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Client}/{action=Index}/{id?}");
app.Run();
