using IOTBack.Configuracao;
using IOTBack.Infraestrutura;
using IOTBack.Model.Consumo;
using IOTBack.Model.Empregado;
using IOTBack.Model.Utilizador;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers();
 
//O que adicionei #############################
builder.Services.AddAutoMapper(typeof(DomainToDTOMapping));
builder.Services.AddTransient<IEmpregado, REmpregado>();
builder.Services.AddTransient<IUtilizador, RUtilizador>();
builder.Services.AddTransient<IConsumo, RConsumo>();


// No método ConfigureServices
builder.Services.AddSingleton<SmsService>();
 
//Para ter acesso as variáveis do json
IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

 
//Token
var key = Encoding.ASCII.GetBytes(Key.Secret);
builder.Services.AddAuthentication(x => {
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
 
x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidIssuer = configuration["jwt:issuer"],  //Quem valida
        ValidAudience = configuration["jwt:audiente"],  // Que acede
        ClockSkew = TimeSpan.Zero
    };

});

//--Fim o que adicionei. ####################################

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
            Name ="Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
    });
  

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                },
                Scheme="oauth2",
                Name="Bearer",
                In=ParameterLocation.Header
            },
            new List<string>()
        }
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error-development"); //Adicionei só essa linha
    app.UseSwagger();
    app.UseSwaggerUI();
}
//Else completo Adicionei
else
{
    app.UseExceptionHandler("/error");
}

//app.UseHttpsRedirection();

//app.UseStaticFiles();
app.UseRouting();

app.UseCors(builder =>
{
    builder.AllowAnyOrigin().SetIsOriginAllowed(origin => true)
                   .AllowAnyMethod()
                   .AllowAnyHeader();
                   
});

app.UseAuthorization();

app.MapControllers();

app.Run();
