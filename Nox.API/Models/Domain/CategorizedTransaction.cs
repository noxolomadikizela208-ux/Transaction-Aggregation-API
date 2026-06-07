namespace Nox.API.Models.Domain
{
    public class CategorizedTransaction
    {
        public required Transaction Transaction { get; set; }
        public TransactionCategory Category { get; set; }
    }
}
