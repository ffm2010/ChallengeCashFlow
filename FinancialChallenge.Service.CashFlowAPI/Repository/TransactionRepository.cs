using AutoMapper;
using FinancialChallenge.Service.CashFlowAPI.DbContexts;
using FinancialChallenge.Service.CashFlowAPI.Models;
using FinancialChallenge.Service.CashFlowAPI.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace FinancialChallenge.Service.CashFlowAPI.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly ILogger<TransactionRepository> _logger;

        public TransactionRepository(ApplicationDbContext db, IMapper mapper, ILogger<TransactionRepository> logger)
        {
            _db = db;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<TransactionDto> CreateTransaction(TransactionDto transactionDto)
        {
            _logger.LogInformation($"Validando o mapeio das informações para o entidade");
            var transaction = _mapper.Map<TransactionDto, Transaction>(transactionDto);

            transaction.CreatedDate = DateTimeOffset.Now;

            if (transaction.TransactionType == Models.Enums.TransactionType.Output)
                transaction.Total *= -1;

            _logger.LogInformation($"Criando a transação");
            _db.Transctions.Add(transaction);

            _logger.LogInformation($"salvando a transação");
            await _db.SaveChangesAsync();

            _logger.LogInformation($"Retornando a transação criada");
            return _mapper.Map<Transaction, TransactionDto>(transaction);
        }
        public async Task<TransactionDto> UpdateTransaction(TransactionDto transactionDto)
        {
            var transaction = _mapper.Map<TransactionDto, Transaction>(transactionDto);
            var transactionOld = await _db.Transctions.AsNoTracking().FirstOrDefaultAsync(u => u.TransactionId == transactionDto.TransactionId && u.Active);

            if (transaction.TransactionType == Models.Enums.TransactionType.Output)
                transaction.Total *= -1;

            transaction.UserCreated = transactionOld.UserCreated;
            transaction.CreatedDate = transactionOld.CreatedDate;
            transaction.UpdatedDate = DateTimeOffset.Now;

            _db.Transctions.Update(transaction);

            await _db.SaveChangesAsync();

            return _mapper.Map<Transaction, TransactionDto>(transaction);
        }
        public async Task<bool> DeleteTransaction(Guid transactionId)
        {
            try
            {
                var transaction = await _db.Transctions.FirstOrDefaultAsync(u => u.TransactionId == transactionId && u.Active);
                if (transaction == null)
                {
                    return false;
                }

                transaction.Active = false;
                transaction.UpdatedDate = DateTimeOffset.Now;

                _db.Transctions.Update(transaction);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<TransactionDto> GetTransactionById(Guid transactionId)
        {
            var transaction = await _db.Transctions.Where(x => x.TransactionId == transactionId && x.Active).FirstOrDefaultAsync();
            return _mapper.Map<TransactionDto>(transaction);
        }
        public async Task<IEnumerable<TransactionDto>> GetTransactions()
        {
            List<Transaction> transactionList = await _db.Transctions.Where(x => x.TransactionDate.Date == DateTimeOffset.Now.Date && x.Active).ToListAsync();

            return _mapper.Map<List<TransactionDto>>(transactionList);

        }

        public async Task<IEnumerable<TransactionDto>> GetTransactionsByDate(DateTimeOffset date)
        {
            List<Transaction> transactionList = await _db.Transctions.Where(x => x.TransactionDate.Date == date.Date && x.Active).ToListAsync();

            return _mapper.Map<List<TransactionDto>>(transactionList);

        }
    }
}
