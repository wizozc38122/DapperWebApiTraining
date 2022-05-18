using DapperWebApiTraining.Repository.Interfaces;
using DapperWebApiTraining.Repository.Implements;
using DapperWebApiTraining.Repository.UnitOfWork.Interfaces;
using DapperWebApiTraining.Repository.UnitOfWork.Implements;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Repository
builder.Services.AddTransient<IBlogRepository, BlogRepository>();
builder.Services.AddTransient<IPostRepository, PostRepository>();

// Unit Of Work
builder.Services.AddTransient<IUnitOfWork, UnitOfWork > (s => new UnitOfWork(builder.Configuration.GetConnectionString("SqlServer") ));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
