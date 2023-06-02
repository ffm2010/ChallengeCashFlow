using FinancialChallenge.Service.CashFlowAPI.Models.Dto;

namespace FinancialChallenge.Service.CashFlowAPI.Repository
{
    public interface ITransactionRepository
    {
        Task<TransactionDto> CreateTransaction(TransactionDto transactionDto);
        Task<TransactionDto> UpdateTransaction(TransactionDto transactionDto);
        Task<IEnumerable<TransactionDto>> GetTransactions();
        Task<TransactionDto> GetTransactionById(Guid transactionId);
        
        Task<bool> DeleteTransaction(Guid transactionId);
        Task<IEnumerable<TransactionDto>> GetTransactionsByDate(DateTimeOffset date);
    }
}
