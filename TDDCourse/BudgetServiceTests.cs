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

    [Test]
    public void Query_ShouldReturn10_WhenQueryingSameDayInJuly2025()
    {
        // Arrange
        _mockRepo.GetAll().Returns(new List<Budget>
        {
            new Budget { YearMonth = "202507", Amount = 310 }
        });

        var startDate = new DateTime(2025, 7, 1);
        var endDate = new DateTime(2025, 7, 1);

        // Act
        var result = _budgetService.Query(startDate, endDate);

        // Assert
        result.Should().Be(10m);
    }

    [Test]
    public void Query_ShouldReturn310_WhenQueryingFullJuly2025()
    {
        // Arrange
        _mockRepo.GetAll().Returns(new List<Budget>
        {
            new Budget { YearMonth = "202507", Amount = 310 }
        });

        var startDate = new DateTime(2025, 7, 1);
        var endDate = new DateTime(2025, 7, 31);

        // Act
        var result = _budgetService.Query(startDate, endDate);

        // Assert
        result.Should().Be(310m);
    }

    [Test]
    public void Query_ShouldReturn410_WhenQueryingJuly1ToAugust1()
    {
        // Arrange
        _mockRepo.GetAll().Returns(new List<Budget>
        {
            new Budget { YearMonth = "202507", Amount = 310 },
            new Budget { YearMonth = "202508", Amount = 3100 }
        });

        var startDate = new DateTime(2025, 7, 1);
        var endDate = new DateTime(2025, 8, 1);

        // Act
        var result = _budgetService.Query(startDate, endDate);

        // Assert
        result.Should().Be(410m);
    }

    [Test]
    public void Query_ShouldReturn3310_WhenQueryingJulyToSeptemberWithMissingAugustData()
    {
        // Arrange
        _mockRepo.GetAll().Returns(new List<Budget>
        {
            new Budget { YearMonth = "202507", Amount = 310 },
            new Budget { YearMonth = "202509", Amount = 3000 }
        });

        var startDate = new DateTime(2025, 7, 1);
        var endDate = new DateTime(2025, 9, 30);

        // Act
        var result = _budgetService.Query(startDate, endDate);

        // Assert
        result.Should().Be(3310m);
    }

    [Test]
    public void Query_ShouldReturn200_WhenQueryingLeapYearFebruary()
    {
        // Arrange
        _mockRepo.GetAll().Returns(new List<Budget>
        {
            new Budget { YearMonth = "202402", Amount = 2900 }
        });

        var startDate = new DateTime(2024, 2, 1);
        var endDate = new DateTime(2024, 2, 2);

        // Act
        var result = _budgetService.Query(startDate, endDate);

        // Assert
        result.Should().Be(200m);
    }
}