using Nox.API.Models.Domain;

namespace Nox.API.Application.Services
{
    public class TransactionCategorizationService
    {
        public TransactionCategory Categorize(Transaction transaction)
        {   
            var description = transaction.Description.ToLower();

            if (description.Contains("grocery") || description.Contains("supermarket"))
            
                return TransactionCategory.Food;
            
            if (description.Contains("uber") || description.Contains("lyft"))
            
                return TransactionCategory.Transport;
            

            if(transaction.Amount > 0)
            
                return TransactionCategory.Income;
            

            return TransactionCategory.Other;

        }
    }
}
