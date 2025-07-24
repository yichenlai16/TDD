using WebApplication1.Repo;

namespace WebApplication1.Services;

public class BudgetServices(IBudgetRepo mockRepo)
{
    public decimal Query(DateTime startDate, DateTime endDate)
    {
        throw new NotImplementedException();
    }
}