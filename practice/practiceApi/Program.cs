//using practiceApi.Logging;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using practiceApi;
using practiceApi.Data;
using practiceApi.Models;
using practiceApi.Repository;
using practiceApi.Repository.IRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(option =>
{
    //option.ReturnHttpNotAcceptable = true;
}).AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddSingleton<ILogging, Logging>();
builder.Services.AddDbContext<ApplicationDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(typeof(MappingConfig));

builder.Services.AddScoped<IVillaRepository, VillaRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("VillaCors", p =>
    {
        p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
}); // for Vue js

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("VillaCors"); // for Vue js

app.UseStaticFiles(); // for File upload

app.MapControllers();

app.Run();
