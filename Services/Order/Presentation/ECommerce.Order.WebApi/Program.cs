using ECommerce.Order.Application.Features.CQRS.Handlers.AddressHandlers;
using ECommerce.Order.Application.Features.CQRS.Handlers.OrderDetailHandlers;
using ECommerce.Order.Application.Interfaces;
using ECommerce.Order.Application.Services;
using ECommerce.Order.Persistence.Context;
using ECommerce.Order.Persistence.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    opt.Authority = builder.Configuration["IdentityServerUrl"]; //appsettings.json'a ekledim.
    opt.Audience = "ResourceOrder"; //discount mikroservisi kullanabilmesi icin IdentityServer config eslesmesi
    opt.RequireHttpsMetadata = false; //https'i zorunluluktan cikardik
});


// Add services to the container.

#region

//Context sinifi konfigürasyonu
builder.Services.AddDbContext<OrderContext>();

//Address handler lerin konfigürasyonu
builder.Services.AddScoped<CreateAddressCommandHandler>();
builder.Services.AddScoped<GetAddressByIdQueryHandler>();
builder.Services.AddScoped<GetAddressQueryHandler>();
builder.Services.AddScoped<RemoveAddressCommandHandler>();
builder.Services.AddScoped<UpdateAddressCommandHandler>();

//OrderDetail handler lerin konfigürasyonu
builder.Services.AddScoped<CreateOrderDetailCommandHandler>();
builder.Services.AddScoped<GetOrderDetailByIdQueryHandler>();
builder.Services.AddScoped<GetOrderDetailQueryHandler>();
builder.Services.AddScoped<RemoveOrderDetailCommandHandler>();
builder.Services.AddScoped<UpdateOrderDetailCommandHandler>();

//Repository'lerin konfigürasyonu
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

//Ordering için MediatR konfigürasyonu
builder.Services.AddApplicationService(builder.Configuration);

#endregion

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
