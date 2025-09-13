using Facturacion.Data;
using Facturacion.Data.Utils;
using Facturacion.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//inyeccion de dependencias
builder.Services.AddScoped<IArticleRepository, ArticleRepository>(); //interfaz se corresponde con el repository
builder.Services.AddScoped<IArticleService, ArticleService>();
builder.Services.Configure<DbSettings>(builder.Configuration.GetSection("DbSettings")); //buscar la cadena de conexion a appsettings.json
builder.Services.AddScoped<IBudgetService, BudgetService>(); //cuando se necesite crear una interfaz se va a crear un repository
builder.Services.AddScoped<UnitOfWork>(provider =>
{
    var config = provider.GetRequiredService<IConfiguration>();
    var cnnString = config.GetSection("DbSettings").Get<DbSettings>().ConnectionString;
    return new UnitOfWork(cnnString);
});



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
