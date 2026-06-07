namespace Nox.API.Models.Domain
{
    public class Transaction
    {
        public required string Id { get; set; }
        public required string CustomerId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public required string Description { get; set; }
        public required string SourceAccount { get; set; }
    }
}
