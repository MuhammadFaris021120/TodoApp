using Microsoft.EntityFrameworkCore;
using TodoApp.Application.Services;
using TodoApp.Core.Entities;
using TodoApp.Core.Interfaces;
using TodoApp.Infrastructure.Data;
using TodoApp.Infrastructure.Repositories;

// Practice change for feature/test-branch


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("MyPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
Console.WriteLine("Conflict Test A");


// Add DbContext (SQLite)
builder.Services.AddDbContext<TodoDbContext>(options =>
    options.UseSqlite("Data Source=todos.db"));

// Register Repository
builder.Services.AddScoped<ITodoRepository, SQLiteTodoRepository>();
builder.Services.AddScoped<TodoService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors("MyPolicy");


app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/api/todos", async (TodoService service) =>
    await service.GetAllAsync());

app.MapGet("/api/todos/{id}", async (int id, TodoService service) =>
    await service.GetByIdAsync(id) is TodoItem todo ? Results.Ok(todo) : Results.NotFound());

app.MapPost("/api/todos", async (TodoItem item, TodoService service) =>
{
    try
    {
        var result = await service.AddAsync(item);
        return Results.Created($"/api/todos/{result.Id}", result);
    }
    catch (ArgumentException ex)
    {
        return Results.BadRequest(ex.Message);
    }
});

app.MapPut("/api/todos/{id}", async (int id, TodoItem item, TodoService service) =>
{
    item.Id = id;
    try
    {
        var result = await service.UpdateAsync(item);
        return result != null ? Results.Ok(result) : Results.NotFound();
    }
    catch (ArgumentException ex)
    {
        return Results.BadRequest(ex.Message);
    }
});
// Practice change for feature/test-branch

app.MapDelete("/api/todos/{id}", async (int id, TodoService service) =>
    await service.DeleteAsync(id) ? Results.Ok() : Results.NotFound());

app.Run();
