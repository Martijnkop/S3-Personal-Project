using barboek.Data;
using barboek.Interface;
using barboek.Interface.IServices;
using barboek.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;


string MYSECRET = "DitIsEenSecretEnHiermeeZalJeHetMoetenDoen";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddRouting(options => { options.LowercaseUrls = true; });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataStore>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPriceService, PriceService>();
builder.Services.AddScoped<IPriceTypeService, PriceTypeService>();
builder.Services.AddScoped<ITaxTypeService, TaxTypeService>();

builder.Services.AddScoped<IItemCategoryService, ItemCategoryService>();

builder.Services.AddScoped<IDbItemCategoryService, ItemCategoryService>();
builder.Services.AddScoped<IDbItemService, ItemService>();
builder.Services.AddScoped<IDbTaxTypeService, TaxTypeService>();
builder.Services.AddScoped<IDbPriceTypeService, PriceTypeService>();
builder.Services.AddScoped<IDbPriceService, PriceService>();

builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddScoped<IItemService, ItemService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "frontend", builder =>
    {
        builder.WithOrigins("http://localhost:5173", "http://localhost:4000", "http://192.168.178.136:4000")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

var tokenValidationParameters = new TokenValidationParameters
{
    ValidateIssuer = true,
    ValidIssuer = "https://auth.barboek.nl/",
    ValidateAudience = true,
    ValidAudience = "https://barboek.nl/",
    ValidateIssuerSigningKey = true,
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(MYSECRET)),
    ValidateLifetime = true,
};

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.TokenValidationParameters = tokenValidationParameters;
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("frontend");

app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();

app.Run();
