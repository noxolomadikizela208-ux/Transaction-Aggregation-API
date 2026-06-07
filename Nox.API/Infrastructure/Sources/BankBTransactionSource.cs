using Nox.API.Application.Interfaces;
using Nox.API.Models.Domain;

namespace Nox.API.Infrastructure.Sources
{
    public class BankBTransactionSource : ITransactionSource
    {
        public Task<List<Transaction>> GetTransactionsAsync(string customerId)
        {
            var transactions = new List<Transaction>
            {
                new()
                {
                    Id = "B001",
                    CustomerId = customerId,
                    Amount = -180m,
                    Description = "Grocery Store",
                    Date = DateTime.UtcNow.AddDays(-2),
                    SourceAccount = "BankB"
                },
                new()
                {
                    Id = "B002",
                    CustomerId = customerId,
                    Amount = -50m,
                    Description = "Uber",
                    Date = DateTime.UtcNow.AddDays(-1),
                    SourceAccount = "BankB"
                }
            };

            return Task.FromResult(transactions);
        }
    }
}
