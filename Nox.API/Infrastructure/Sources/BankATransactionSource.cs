using Nox.API.Application.Interfaces;
using Nox.API.Models.Domain;

namespace Nox.API.Infrastructure.Sources
{
    public class BankATransactionSource : ITransactionSource
    {
        public Task<List<Transaction>> GetTransactionsAsync(string customerId)
        {
            var transactions = new List<Transaction>
            {
                new()
                {
                    Id = "A003",
                    CustomerId = customerId,
                    Amount = 20000m,
                    Description = "Salary",
                    Date = DateTime.UtcNow.AddDays(-3),
                    SourceAccount = "BankA"
                }
            };

            return Task.FromResult(transactions);
        }
    }
}
