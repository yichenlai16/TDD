using WebApplication1.Repo;
using WebApplication1.Models;

namespace WebApplication1.Services;

public class BudgetServices(IBudgetRepo budgetRepo)
{
    public decimal Query(DateTime startDate, DateTime endDate)
    {
        var budgets = CreateBudgetLookup(budgetRepo.GetAll());
        var monthlyCalculations = new Dictionary<string, decimal>();
        decimal totalAmount = 0;

        for (var date = startDate; date <= endDate; date = date.AddDays(1))
        {
            totalAmount += GetDailyBudgetForDate(date, budgets, monthlyCalculations);
        }

        return totalAmount;
    }

    private decimal GetDailyBudgetForDate(DateTime date, Dictionary<string, Budget> budgets, Dictionary<string, decimal> cache)
    {
        var yearMonth = date.ToString("yyyyMM");
        
        if (cache.TryGetValue(yearMonth, out var dailyAmount))
            return dailyAmount;
            
        if (budgets.TryGetValue(yearMonth, out var budget))
        {
            var daysInMonth = DateTime.DaysInMonth(date.Year, date.Month);
            dailyAmount = (decimal)budget.Amount / daysInMonth;
            cache[yearMonth] = dailyAmount;
            return dailyAmount;
        }
        
        return 0;
    }

    private Dictionary<string, Budget> CreateBudgetLookup(List<Budget> budgets) =>
        budgets.ToDictionary(b => b.YearMonth, b => b);
}