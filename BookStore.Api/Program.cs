using System.Text.Json.Serialization;
using BookStore.Application.Interfaces;
using BookStore.Application.Services;
using BookStore.Application.ValidationsAndAttributes;
using BookStore.Infrastructure.Persistance;
using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<BookStoreDbContext>((sp, options) =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<IBookStoreDbContext, BookStoreDbContext>();
// builder.Services.AddSingleton<IBookService, InMemoryBookService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<BookRequestValidator>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(o => o.DisplayRequestDuration());
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();