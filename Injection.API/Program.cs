using Injection.Data.Persistence;
using Injection.Data.Repositories;
using Injection.Entities;
using Injection.Services;
using Injection.Services.Interface;
using Injection.Services.Mapper;

var builder = WebApplication.CreateBuilder(args);

//Services
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IPersonService, PersonService>();

//Repositories
builder.Services.AddScoped<IRepository<Product>, Repository<Product>>();
builder.Services.AddScoped<IRepository<Order>, Repository<Order>>();
builder.Services.AddScoped<IRepository<Person>, Repository<Person>>();

//DbContext
builder.Services.AddDbContext<IMainDbContext, MainDbContext>();

//AutoMapper
builder.Services.AddAutoMapper(typeof(ProductProfile));
builder.Services.AddAutoMapper(typeof(OrderProfile));
builder.Services.AddAutoMapper(typeof(PersonProfile));

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
