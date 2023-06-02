using FinancialChallenge.Web.Models;

namespace FinancialChallenge.Web.Services.IServices
{
    public interface ITransactionService : IBaseService
    {
        Task<T> GetAllTransactionsAsync<T>(string token);
        Task<T> GetTransactionByIdAsync<T>(Guid id, string token);
        Task<T> CreateTransactionAsync<T>(TransactionDto transactionDto, string token);
        Task<T> UpdateTransactionAsync<T>(TransactionDto transactionDto, string token);
        Task<T> DeleteTransactionAsync<T>(Guid id, string token);
        Task<T> GetAllTransactionsByDateAsync<T>(DateTimeOffset date, string token);
    }
}
