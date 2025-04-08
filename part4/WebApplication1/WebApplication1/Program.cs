using Repository.Entities;
using Service.Services;
using Service;
using Repository.Interface;
using Repository.Repositories;
using Microsoft.EntityFrameworkCore;
using DataContext;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])) 
        };
    });
builder.Services.AddAuthorization();

builder.Services.AddDbContext<StoreDataContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IContext>(provider => provider.GetService<StoreDataContext>());


builder.Services.AddControllers();
builder.Services.AddScoped<IRepository<Supplier>, SupplierRepository>();
builder.Services.AddScoped<IRepository<Product>, ProductRepository>();
builder.Services.AddScoped<IRepository<SupplierProducts>, SupplierProductRepository>();
builder.Services.AddScoped<IOrderRepository<Order>, OrderRepository>();
builder.Services.AddScoped<IRepository<OrderDetails>, OrderDetailsRepository>();

builder.Services.AddScoped<IService<Supplier>, SupplierService>();
builder.Services.AddScoped<IService<Product>, ProductService>();
builder.Services.AddScoped<IService<SupplierProducts>, SupplierProductService>();
builder.Services.AddScoped<IOrderService<Order>, OrderService>();
builder.Services.AddScoped<IService<OrderDetails>, OrderDetailsService>();

builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost",
        builder => builder
            .WithOrigins("http://localhost:4200") // כתובת ה-Origin של ה-frontend שלך
            .AllowAnyMethod()
            .AllowAnyHeader());
});

var app = builder.Build();

//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowLocalhost");
app.UseHttpsRedirection();
app.UseAuthentication(); 
app.UseAuthorization();

app.MapControllers();

app.Run();
