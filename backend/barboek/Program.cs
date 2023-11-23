using barboek.Data;
using barboek.Interface;
using barboek.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;


string MYSECRET = "DitIsEenSecretEnHiermeeZalJeHetMoetenDoen";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataStore>();
builder.Services.AddScoped<AccountService>();
builder.Services.AddScoped<ItemService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<FinanceService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "frontend", builder =>
    {
        builder.WithOrigins("https://localhost")
        .AllowAnyHeader();
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
