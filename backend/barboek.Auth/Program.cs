using barboek.Auth.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<DataStore>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "frontend", builder =>
    {
        builder.WithOrigins("https://localhost")
        .AllowAnyHeader();
    });
});

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("frontend");


app.UseAuthorization();

app.MapControllers();

app.Run();
