using Nox.API.Models.Domain;

namespace Nox.API.Application.Interfaces
{
    public interface ITransactionSource
    {
        Task<List<Transaction>> GetTransactionsAsync(string customerId);
    }
}
