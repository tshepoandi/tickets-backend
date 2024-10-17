using System.Collections.Immutable;
using tickets.Data;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using System;
using Microsoft.AspNetCore.Mvc;
using tickets.Entities;



var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddDbContext<TicketsDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});


var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapControllers();


app.Run();