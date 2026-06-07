using Nox.API.Application.Interfaces;
using Nox.API.Models.Domain;

namespace Nox.API.Application.Services
{
    public class TransactionAggregateService : ITransactionAggregateService
    {
        private readonly IEnumerable<ITransactionSource> _transactionSources;
        private readonly TransactionCategorizationService _categorizer;

        public TransactionAggregateService(
            IEnumerable<ITransactionSource> transactionSources,
            TransactionCategorizationService categorizer)
        {
            _transactionSources = transactionSources;
            _categorizer = categorizer;
        }

        public async Task<List<CategorizedTransaction>> GetAggregatedTransactionsAsync(string customerId)
        {
            var categorizedTransactions = new List<CategorizedTransaction>();

            foreach (var source in _transactionSources)
            {
                var transactions = await source.GetTransactionsAsync(customerId);

                foreach (var transaction in transactions)
                {
                    var category = _categorizer.Categorize(transaction);

                    categorizedTransactions.Add(new CategorizedTransaction
                    {
                        Transaction = transaction,
                        Category = category
                    });
                }
            }

            return categorizedTransactions;
        }
    }
}