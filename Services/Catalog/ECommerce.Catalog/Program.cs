using ECommerce.Catalog.Services.CategoryServices;
using ECommerce.Catalog.Services.FeatureSliderServices;
using ECommerce.Catalog.Services.ProductDetailServices;
using ECommerce.Catalog.Services.ProductImageServices;
using ECommerce.Catalog.Services.ProductServices;
using ECommerce.Catalog.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    opt.Authority = builder.Configuration["IdentityServerUrl"]; //appsettings.json'a ekledim.
    opt.Audience = "ResourceCatalog"; //catalog mikroservisi kullanabilmesi icin IdentityServer config eslesmesi
    opt.RequireHttpsMetadata = false; //https'i zorunluluktan cikardik
});

//DI konfigürasyonlari
builder.Services.AddScoped<ICategoryService,CategoryService>();    
builder.Services.AddScoped<IProductService,ProductService>();    
builder.Services.AddScoped<IProductDetailService,ProductDetailService>();    
builder.Services.AddScoped<IProductImageService,ProductImageService>();
builder.Services.AddScoped<IFeatureSliderService,FeatureSliderService>();

//Automappper konfigürasyonu
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

//MongoDB database konfigürasyonlarý
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));

//burada DatabaseSetting Sinifi içerisindeki proplarin value larini almak için kullanilir
builder.Services.AddScoped<IDatabaseSettings>(sp =>
{
    return sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
});


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
