using FinancialChallenge.Web.Models;
using FinancialChallenge.Web.Services.IServices;

namespace FinancialChallenge.Web.Services
{
    public class TransactionService : BaseService, ITransactionService
    {
        private readonly IHttpClientFactory _clientFactory;

        public TransactionService(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<T> CreateTransactionAsync<T>(TransactionDto transactionDto, string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = transactionDto,
                Url = SD.CashFlowAPIBase + "/api/cashflow",
                AccessToken = token
            });
        }

        public async Task<T> DeleteTransactionAsync<T>(Guid id, string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.CashFlowAPIBase + "/api/cashflow/" + id,
                AccessToken = token
            });
        }

        public async Task<T> GetAllTransactionsAsync<T>(string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.CashFlowAPIBase + "/api/cashflow",
                AccessToken = token
            });
        }

        public async Task<T> GetTransactionByIdAsync<T>(Guid id, string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.CashFlowAPIBase + "/api/cashflow/" + id,
                AccessToken = token
            });
        }

        public async Task<T> UpdateTransactionAsync<T>(TransactionDto transactionDto, string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = transactionDto,
                Url = SD.CashFlowAPIBase + "/api/cashflow",
                AccessToken = token
            });
        }

        public async Task<T> GetAllTransactionsByDateAsync<T>(DateTimeOffset date, string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.CashFlowAPIBase + "/api/cashflow/dashboard/" + date.ToString("yyyy-MM-dd"),
                AccessToken = token
            });
        }
    }
}
