using ECommerce.Discount.Context;
using ECommerce.Discount.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    opt.Authority = builder.Configuration["IdentityServerUrl"]; //appsettings.json'a ekledim.
    opt.Audience = "ResourceDiscount"; //discount mikroservisi kullanabilmesi icin IdentityServer config eslesmesi
    opt.RequireHttpsMetadata = false; //https'i zorunluluktan cikardik
});


// Add services to the container.

//Dapper ve DiscountService konfigürasyonlari
builder.Services.AddTransient<DapperContext>();
builder.Services.AddTransient<IDiscountService,DiscountService>();



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
