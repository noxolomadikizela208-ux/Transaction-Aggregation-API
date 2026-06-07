using Nox.API.Models.Domain;

namespace Nox.API.Application.Interfaces
{
    public interface ITransactionAggregateService
    {
        Task<List<CategorizedTransaction>> GetAggregatedTransactionsAsync(string customerId);
    }
}
