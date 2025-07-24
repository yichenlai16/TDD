using WebApplication1.Models;

namespace WebApplication1.Repo;

public interface IBudgetRepo
{
    List<Budget> GetAll();
}