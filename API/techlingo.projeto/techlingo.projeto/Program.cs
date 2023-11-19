using Microsoft.EntityFrameworkCore;
using techlingo.projeto.Repository.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DatabaseConnection");
var connectionString2 = builder.Configuration.GetConnectionString("DatabaseConnectionMysql");

builder.Services.AddDbContext<DataBaseContext>(options =>
    options.UseOracle(connectionString).EnableSensitiveDataLogging(true).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
);

//builder.Services.AddDbContext<DataBaseContext>(options => options.UseMySql(connectionString2, ServerVersion.AutoDetect(connectionString2)));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Configurar as políticas de CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Verifica se o banco de dados existe, se não existir, cria o banco de dados
//await using (var scope = app.Services.CreateAsyncScope())
//{
//    var services = scope.ServiceProvider;
//    var context = services.GetRequiredService<DataBaseContext>();
//    await context.Database.MigrateAsync();
//}

// Habilitar o uso das políticas de CORS
app.UseCors();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
