
using invest_api;
using invest_api.SQL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Net.WebSockets;
using System.Text;

//Process.RefreshPortfolio();

//Process.RefreshDeals();

//var serv = PortfolioDBService.GetBondevent();
//var t = PortfolioDBService.GetPositions();


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers().AddJsonOptions(options =>
{
  options.JsonSerializerOptions.PropertyNamingPolicy = null; // Отключает изменение регистра
});

builder.WebHost.ConfigureKestrel(options =>
{
  // использована настройка из appsettings.json
  options.ConfigureEndpointDefaults(endpointOptions =>
  {
    endpointOptions.UseHttps();
  });
});

var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
      options.RequireHttpsMetadata = false;
      options.SaveToken = true;
      options.TokenValidationParameters = new TokenValidationParameters
      {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(secretKey),
        ValidateIssuer = false,
        ValidateAudience = false,
      //  ClockSkew = TimeSpan.Zero
      };
    });

builder.Services.AddAuthorization();
// Добавляем сервисы в DI контейнер
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Добавляем поддержку CORS 
builder.Services.AddCors(options =>
{
  options.AddPolicy("AllowAll", policy =>
  {
    policy.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader()
            .AllowCredentials();
    policy.WithOrigins("http://localhost:4200");
  });
});

// Создаем приложение
var app = builder.Build();
app.MapGet("/", () => "API is running!");
// Используем маршрутизацию и CORS
app.UseCors("AllowAll");

// Включаем Swagger для API 
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

// Используем редирект с HTTP на HTTPS
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
// Включаем маршруты контроллеров
app.MapControllers();

// Запуск приложения
app.Run();
