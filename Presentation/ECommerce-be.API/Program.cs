using ECommerce_be.Application;
using ECommerce_be.Application.Validators.Products;
using ECommerce_be.Infrastructure;
using ECommerce_be.Infrastructure.Filters;
using ECommerce_be.Persistence;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistenceServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();

//builder.Services.AddStorage<LocalStorage>();
builder.Services.AddStorage(ECommerce_be.Infrastructure.Enums.StorageType.Local);

builder.Services.AddCors(options => options.AddDefaultPolicy(
    policy => policy
    .WithOrigins("http://localhost:4200/", "https://localhost:4200/") // Cors'a takılmamak için hangi url'lerden istek atılabilr onlar belirtiliyor.
    .AllowAnyHeader()
    .AllowAnyMethod())
);

builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>())
    .AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>())
    .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true); // default validate kontrollerini devre dışı bırakır.

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers().AddNewtonsoftJson();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options =>
    options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
