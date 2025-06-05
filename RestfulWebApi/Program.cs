using DataBaseConnectionSharedLibrary;
using Microsoft.Data.SqlClient;
using RestfulWebApiTest.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//SqlConnectionStringBuilder _connection = new SqlConnectionStringBuilder()
//{
//    DataSource = "DELL",
//    InitialCatalog = "DotNetTraining",
//    UserID = "SA",
//    Password = "root",
//    TrustServerCertificate = true
//};
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddScoped<DapperServices>(n => new DapperServices(_connection));
builder.Services.AddScoped<IDapperServices, DapperServices>();
builder.Services.AddScoped<IPersonServices, PersonServices>();
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
