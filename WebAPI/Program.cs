using DataAccess.ClassRepository;
using Microsoft.EntityFrameworkCore;
using Model;
using Service.ClassServicePackage;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<MyDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Database")));

// Đăng ký các lớp Service/Repository
builder.Services.AddScoped<ClassService>();
builder.Services.AddScoped<ClassRepository>();


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

app.UseAuthorization();

app.MapControllers();

app.Run();
