using Microsoft.EntityFrameworkCore;
using MyGymApp.Aplication;
using MyGymApp.Infrastructure;
using MyGymApp.Infrastructure.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<GeneralDbContext>(options => options.UseSqlServer(@"Data Source= ClientDirectory.db"));
builder.Services.AddInfrastructure();
builder.Services.AddApplication();

builder.Services.AddCors(options =>
{
    options.AddPolicy("MainPolicy",
        builder =>
        {
            builder
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
            //TODO: remove this line for production
            builder.SetIsOriginAllowed(x => true);
        });
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

app.UseCors("MainPolicy");

app.MapControllers();

app.Run();