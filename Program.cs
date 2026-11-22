using EventDaysMvc.Data;
using EventDaysMvc.Infrastructure;
using EventDaysMvc.Repositories;
using EventDaysMvc.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION")
    ?? "Host=localhost;Port=5432;Database=appdb;Username=postgres;Password=postgres";

builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

// MVC регистрирует и API-контроллеры, и контроллеры с Razor-представлениями.
builder.Services.AddControllersWithViews();
builder.Services.AddProblemDetails();
builder.Services.AddSingleton<DayCalculator>();
builder.Services.AddSingleton<IProductRepository, InMemoryProductRepository>();
builder.Services.AddSingleton<IStudentRepository, InMemoryStudentRepository>();
builder.Services.AddLabRequestTimeouts();

var app = builder.Build();
app.UseExceptionHandler();
app.UseStaticFiles();
app.UseRouting();
app.UseRequestTimeouts();
app.MapGet("/items", async (AppDbContext db, CancellationToken cancellationToken) =>
    await db.Items.AsNoTracking()
        .OrderBy(item => item.Id)
        .ToListAsync(cancellationToken));

app.MapControllers();
app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
app.Run();
