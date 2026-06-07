using Nox.API.Models.Domain;

namespace Nox.API.Application.DTOs
{
    public class AggregtedTransactionResponse
    {
        public required string CustomerId { get; set; }
        public int TotalTransactions { get; set; }
        public decimal TotalAmount { get; set; }
        public required List<CategorizedTransaction> Transactions { get; set; }
    }
}
