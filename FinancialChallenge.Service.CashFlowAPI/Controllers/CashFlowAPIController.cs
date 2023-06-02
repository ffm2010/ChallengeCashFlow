using FinancialChallenge.Service.CashFlowAPI.Models.Dto;
using FinancialChallenge.Service.CashFlowAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinancialChallenge.Service.CashFlowAPI.Controllers
{
    [Route("api/cashflow")]
    public class CashFlowAPIController : Controller
    {
        protected ResponseDto _response;
        private ITransactionRepository _transactionRepository;
        private readonly ILogger<CashFlowAPIController> _logger;

        public CashFlowAPIController(ITransactionRepository transactionRepository, ILogger<CashFlowAPIController> logger)
        {
            _transactionRepository = transactionRepository;
            _logger = logger;
            this._response = new ResponseDto();
        }

        [HttpGet]
        [Authorize]
        public async Task<object> Get()
        {
            try
            {
                _logger.LogInformation($"Início da listagem das transações!");
                IEnumerable<TransactionDto> transactionDtos = await _transactionRepository.GetTransactions();
                _response.Result = transactionDtos;
                _logger.LogInformation($"Término da listagem das transações!");
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
                _logger.LogError($"Erro ao listar as transações - {ex}");
            }
            return _response;
        }

        [HttpGet]
        [Authorize]
        [Route("{id}")]
        public async Task<object> Get(Guid id)
        {
            try
            {
                _logger.LogInformation($"Início do retorno da transação com id = {id}");
                TransactionDto transactionDto = await _transactionRepository.GetTransactionById(id);
                _response.Result = transactionDto;
                _logger.LogInformation($"Término do retorno da transação com id = {id}");
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
                _logger.LogError($"Erro ao retornar a transação com id = {id}. Error: {ex}");
            }
            return _response;
        }

        [HttpPost]
        [Authorize]
        public async Task<object> Post([FromBody] TransactionDto transactionDto)
        {
            try
            {
                _logger.LogInformation($"Início do cadastro da transação");
                TransactionDto model = await _transactionRepository.CreateTransaction(transactionDto);
                _response.Result = model;
                _logger.LogInformation($"Término do cadastro da transação");

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
                _logger.LogError($"Erro ao cadastrar a transação {transactionDto}. Error: {ex}");
            }
            return _response;
        }


        [HttpPut]
        [Authorize]
        public async Task<object> Put([FromBody] TransactionDto transactionDto)
        {
            try
            {
                _logger.LogInformation($"Início da atualização da transação {transactionDto.TransactionId}");
                TransactionDto model = await _transactionRepository.UpdateTransaction(transactionDto);
                _response.Result = model;
                _logger.LogInformation($"Término da atualização da transação {transactionDto.TransactionId}");

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
                _logger.LogInformation($"Erro na atualização da transação {transactionDto.TransactionId}. Error: {ex}");
            }
            return _response;
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route("{id}")]
        public async Task<object> Delete(Guid id)
        {
            try
            {
                _logger.LogInformation($"Início da exclusão da transação {id}");
                bool isSuccess = await _transactionRepository.DeleteTransaction(id);
                _response.Result = isSuccess;
                _logger.LogInformation($"Término da exclusão da transação {id}");

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
                _logger.LogInformation($"Erro na exlusão da transação {id}. Error: {ex}");
            }
            return _response;
        }

        [HttpGet]
        [Authorize]
        [Route("dashboard/{date}")]
        public async Task<object> GetTransactionsByDate(DateTimeOffset date)
        {
            try
            {
                _logger.LogInformation($"Início do retorno das transações com a data = {date}");
                IEnumerable<TransactionDto> transactionDtos = await _transactionRepository.GetTransactionsByDate(date);
                _response.Result = transactionDtos;
                _logger.LogInformation($"Término do retorno das transações com a data = {date}");
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
                _logger.LogError($"Erro ao retornar as transações com a data = {date}. Error: {ex}");
            }
            return _response;
        }
    }
}
