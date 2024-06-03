using Microsoft.EntityFrameworkCore;
using BookService.Models;
using BookService.Endpoints;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration
                        .GetConnectionString("DefaultConnection");

builder.Services.AddSqlServer<BookServiceContext>(connString);

var app = builder.Build();

app.MapBooksEndpoints();
app.MapAuthorsEndpoints();

app.Run();