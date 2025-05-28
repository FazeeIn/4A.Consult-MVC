using System.Net;
using Project.Core.Validators;
using Project.Database.Abstractions;
using Project.Database.Repositories;
using Project.Services.Abstractions;
using Project.Services.Services;

ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IBookRepository, BookRepository>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var connectionString = configuration.GetConnectionString("DefaultConnection");
    
    if (connectionString != null)
    {
        return new BookRepository(connectionString);
    }

    throw new Exception();
});

builder.Services.AddScoped<BookValidator>();

builder.Services.AddControllersWithViews();

var app = builder.Build();
app.UseRouting();

app.UseStaticFiles();

app.MapControllers();

app.Run();