using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using PaymentService.Application.Interfaces;
using PaymentService.Infrastructure.Clients;
using PaymentService.Infrastructure.Data; 

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PaymentDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IPaymentService, PaymentService.Infrastructure.Services.PaymentService>();

builder.Services.AddHttpClient<TicketServiceClient>(c =>
    c.BaseAddress = new Uri("https://localhost:5002"));
builder.Services.AddHttpClient<NotificationServiceClient>(c =>
    c.BaseAddress = new Uri("https://localhost:5003"));

// JWT Auth
var app = builder.Build();

// Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication(); // ⚠️ ОБЯЗАТЕЛЬНО
app.UseAuthorization();
app.MapControllers();
app.Run();
