using WebApplication1.Repo;
using WebApplication1.Models;

namespace WebApplication1.Services;

public class BudgetServices(IBudgetRepo budgetRepo)
{
    public decimal Query(DateTime startDate, DateTime endDate)
    {
        var budgets = budgetRepo.GetAll();
        decimal totalAmount = 0;

        for (var date = startDate; date <= endDate; date = date.AddDays(1))
        {
            var yearMonth = date.ToString("yyyyMM");
            var budget = budgets.FirstOrDefault(b => b.YearMonth == yearMonth);

            if (budget != null)
            {
                var daysInMonth = DateTime.DaysInMonth(date.Year, date.Month);
                var dailyAmount = (decimal)budget.Amount / daysInMonth;
                totalAmount += dailyAmount;
            }
        }

        return totalAmount;
    }
}