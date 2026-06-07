using Microsoft.AspNetCore.Mvc;
using Nox.API.Application.DTOs;
using Nox.API.Application.Interfaces;
using Nox.API.Models.Domain;

namespace Nox.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionAggregateService _transactionAggregateService;

        public TransactionsController(ITransactionAggregateService transactionAggregateService)
        {
            _transactionAggregateService = transactionAggregateService;
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> Get(string customerId)
        {
            var transactions = await _transactionAggregateService.GetAggregatedTransactionsAsync(customerId);

            var response = new AggregtedTransactionResponse
            {
                CustomerId = customerId,
                TotalTransactions = transactions.Count,
                TotalAmount = transactions.Sum(t => t.Transaction.Amount),
                Transactions = transactions
            };

            return Ok(response);
        }
    }
}
