using FluentAssertions;
using NSubstitute;
using WebApplication1.Models;
using WebApplication1.Repo;
using WebApplication1.Services;

namespace TDDCourse;

public class BudgetServiceTests
{
    private BudgetServices _budgetService;
    private IBudgetRepo _mockRepo;

    [SetUp]
    public void Setup()
    {
        _mockRepo = Substitute.For<IBudgetRepo>();
        _budgetService = new BudgetServices(_mockRepo);
    }

    [Test]
    public void Query_ShouldReturn20_WhenQueryingJuly2025Range()
    {
        // Arrange
        _mockRepo.GetAll().Returns(new List<Budget>
        {
            new Budget { YearMonth = "202507", Amount = 310 }
        });

        var startDate = new DateTime(2025, 7, 1);
        var endDate = new DateTime(2025, 7, 2);

        // Act
        var result = _budgetService.Query(startDate, endDate);

        // Assert
        result.Should().Be(20m);
    }
}