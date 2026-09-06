using Microsoft.EntityFrameworkCore;
using CommissionManagement.Models;
using CommissionManagement.Services.QaSettingSer;
using CommissionManagement.Services.QaQuestionSer;
using CommissionManagement.Services.CommissionOrderSer;
using CommissionManagement.Services.SocialPlatformSer;
using CommissionManagement.Services.CommissionTypeSer;
using CommissionManagement.Services.CommissionPeriodSer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddDbContext<CommissionContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CommissionContext")));
builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IQaSettingService, QaSettingService>();
builder.Services.AddScoped<IQaQuestionService, QaQuestionService>();
builder.Services.AddScoped<ICommissionOrderService, CommissionOrderService>();
builder.Services.AddScoped<ISocialPlatformService, SocialPlatformService>();
builder.Services.AddScoped<ICommissionTypeService, CommissionTypeService>();
builder.Services.AddScoped<ICommissionPeriodService, CommissionPeriodService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
