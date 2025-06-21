using Xunit;
using Moq;
using PaymentService.Application.DTOs;
using PaymentService.Domain.Models;
using PaymentService.Infrastructure.Data;
using PaymentService.Infrastructure.Services;
using PaymentService.Infrastructure.Clients;
using Microsoft.EntityFrameworkCore;

using System.Threading.Tasks;

public class PaymentServiceTests
{
    private PaymentDbContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<PaymentDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new PaymentDbContext(options);
    }

    private PaymentService.Infrastructure.Services.PaymentService GetService(PaymentDbContext db)
    {
        var ticketClient = new Mock<TicketServiceClient>(MockBehavior.Strict, new HttpClient());
        var notificationClient = new Mock<NotificationServiceClient>(MockBehavior.Strict, new HttpClient());

        // Setup fake methods only for successful case
        ticketClient.Setup(x => x.UpdateTicketStatus(It.IsAny<int>(), "Paid"))
                    .Returns(Task.CompletedTask);

        notificationClient.Setup(x => x.SendPaymentSuccess(It.IsAny<int>(), It.IsAny<int>()))
                          .Returns(Task.CompletedTask);

        return new PaymentService.Infrastructure.Services.PaymentService(db, ticketClient.Object, notificationClient.Object);
    }

    [Fact]
    public async Task ProcessPaymentAsync_ShouldCreateSuccessfulPayment()
    {
        // Arrange
        var db = GetDbContext();
        var service = GetService(db);
        var request = new CreatePaymentRequest { TicketId = 1, Amount = 100, Method = "TestCard" };

        // Act
        var result = await service.ProcessPaymentAsync(request, userId: 42);

        // Assert
        Assert.Equal("Success", result.Status);
        Assert.Equal(1, result.TicketId);
        Assert.Equal(42, result.UserId);
    }

    [Fact]
    public async Task ProcessPaymentAsync_ShouldCreateFailedPayment()
    {
        // Arrange
        var db = GetDbContext();
        var service = GetService(db);
        var request = new CreatePaymentRequest { TicketId = 2, Amount = 50, Method = "FakeCard" };

        // Act
        var result = await service.ProcessPaymentAsync(request, userId: 33);

        // Assert
        Assert.Equal("Failed", result.Status);
    }

    [Fact]
    public async Task GetPaymentByIdAsync_ShouldReturnCorrectPayment()
    {
        var db = GetDbContext();
        var service = GetService(db);

        var payment = new Payment { PaymentId = 1, TicketId = 3, UserId = 5, Amount = 99, Method = "TestCard", Status = "Success", Timestamp = DateTime.UtcNow };
        db.Payments.Add(payment);
        await db.SaveChangesAsync();

        var result = await service.GetPaymentByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal(3, result!.TicketId);
    }

    [Fact]
    public async Task GetPaymentsByUserAsync_ShouldReturnAllForUser()
    {
        var db = GetDbContext();
        var service = GetService(db);

        db.Payments.AddRange(
            new Payment { PaymentId = 1, TicketId = 1, UserId = 10, Amount = 20, Method = "TestCard", Status = "Success", Timestamp = DateTime.UtcNow },
            new Payment { PaymentId = 2, TicketId = 2, UserId = 10, Amount = 30, Method = "TestCard", Status = "Success", Timestamp = DateTime.UtcNow },
            new Payment { PaymentId = 3, TicketId = 3, UserId = 11, Amount = 40, Method = "TestCard", Status = "Success", Timestamp = DateTime.UtcNow }
        );
        await db.SaveChangesAsync();

        var result = await service.GetPaymentsByUserAsync(10);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task DeletePaymentAsync_ShouldDeleteCorrectly()
    {
        var db = GetDbContext();
        var service = GetService(db);

        db.Payments.Add(new Payment { PaymentId = 1, TicketId = 1, UserId = 99, Amount = 55, Method = "TestCard", Status = "Success", Timestamp = DateTime.UtcNow });
        await db.SaveChangesAsync();

        var deleted = await service.DeletePaymentAsync(1);
        Assert.True(deleted);
        Assert.Empty(db.Payments);
    }
}
