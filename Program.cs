using CurrancyQuery_API.Interfaz;
using CurrancyQuery_API.Services;
using CurrencyQuery_API.Interfaz;
using CurrencyQuery_API.Services;

var builder = WebApplication.CreateBuilder(args);

var filePath = builder.Configuration["FilePathDataCurrency"];

// Add services to the container.

builder.Services.AddControllers();

// Configuración de servicios
builder.Services.AddScoped<IExchangeRate, ExchangeRateService>();
builder.Services.AddScoped<IPostBin, PostBinService>();
builder.Services.AddScoped<IConvertCurrencies, ConvertCurrenciesService>();
builder.Services.AddScoped<ICurrencyFileReader>( _ => new CurrencyFileReader(filePath));


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
