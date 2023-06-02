using FinancialChallenge.Service.CashFlowAPI.Controllers;
using FinancialChallenge.Service.CashFlowAPI.Models.Dto;
using FinancialChallenge.Service.CashFlowAPI.Repository;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace FinancialChallenge.Service.CashFlowAPI.Tests.Controllers
{
    public class CashFlowAPIControllerTest
    {
        private readonly CashFlowAPIController _controller;
        private readonly Mock<ITransactionRepository> _transactionRepository;

        public CashFlowAPIControllerTest()
        {
            _transactionRepository = new Mock<ITransactionRepository>();
            var _logger = new Mock<ILogger<CashFlowAPIController>>();
            _controller = new CashFlowAPIController(_transactionRepository.Object, _logger.Object);

        }

        //GetTransactions
        [Fact(DisplayName = "When calling the registered transactions of the day it should return all available transactions")]
        private async Task when_calling_registered_transactions_day_should_return_all_available_transactions()
        {
            IEnumerable<TransactionDto> transactionDtos = new List<TransactionDto>();
            _transactionRepository.Setup(x => x.GetTransactions()).ReturnsAsync(transactionDtos);

            var result = await _controller.Get();

            result.Should().BeOfType<ResponseDto>()
                .Which.Result.As<IEnumerable<TransactionDto>>().Should().BeEquivalentTo(transactionDtos);
        }


        //GetTransactionById(id)
        [Fact(DisplayName = "when passing the id of a transaction it should return the existing transaction")]
        private async Task when_passing_id_transaction_should_return_existing_transaction()
        {
            var id = Guid.NewGuid();
            TransactionDto transactionDto = new TransactionDto();

            _transactionRepository.Setup(x => x.GetTransactionById(id)).ReturnsAsync(transactionDto);

            var result = await _controller.Get(id);

            result.Should().BeOfType<ResponseDto>()
                .Which.Result.As<TransactionDto>().Should().BeEquivalentTo(transactionDto);
        }

        //GetTransactionsByDate(id)
        [Fact(DisplayName = "when passing a date it should return all existing transactions on that date")]
        private async Task when_passing_date_should_return_all_existing_transactions_date()
        {
            var date = DateTimeOffset.Now;
            IEnumerable<TransactionDto> transactionDtos = new List<TransactionDto>();

            _transactionRepository.Setup(x => x.GetTransactionsByDate(date)).ReturnsAsync(transactionDtos);

            var result = await _controller.GetTransactionsByDate(date);

            result.Should().BeOfType<ResponseDto>()
                .Which.Result.As<IEnumerable<TransactionDto>>().Should().BeEquivalentTo(transactionDtos);
        }

        //Delete(id)
        [Fact(DisplayName = "when passing the id of a transaction you must delete the existing transaction")]
        private async Task when_passing_id_transaction_must_delete_existing_transaction()
        {
            var id = Guid.NewGuid();
            TransactionDto transactionDto = new TransactionDto();

            _transactionRepository.Setup(x => x.DeleteTransaction(id)).ReturnsAsync(true);

            var result = await _controller.Delete(id);

            result.Should().BeOfType<ResponseDto>()
                .Which.Result.As<bool>().Should().BeTrue();
        }

        //CreateTransaction(transaction)
        [Fact(DisplayName = "When creating a transaction, it must return the created transaction")]
        private async Task When_creating_transaction_must_return_created_transaction()
        {
            var transactionDto = new TransactionDto();
            var transaction = new TransactionDto
            {
                TransactionId = Guid.NewGuid(),
                TransactionType = Models.Enums.TransactionType.Input,
                Description = "Test Description",
                TypePayment = Models.Enums.TypePayment.Credit,
                AmountParcels = 1,
                Total = 550,
                TransactionDate = DateTimeOffset.Now,
                Active = true,
                UserCreated = "ffm",
                CreatedDate = DateTimeOffset.Now
            };

            _transactionRepository.Setup(x => x.CreateTransaction(transaction)).ReturnsAsync(transactionDto);

            var result = await _controller.Post(transaction);

            result.Should().BeOfType<ResponseDto>()
                .Which.Result.As<TransactionDto>().Should().BeEquivalentTo(transactionDto);
        }

        //UpdateTransaction(transaction)
        [Fact(DisplayName = "When updating a transaction it should return the updated transaction")]
        private async Task when_updating_transaction_should_return_updated_transaction()
        {
            var transactionDto = new TransactionDto();
            var transaction = new TransactionDto
            {
                TransactionId = Guid.Parse("19aa3d92-816c-4190-b0e0-d420fea9fc2a"),
                TransactionType = Models.Enums.TransactionType.Output,
                Description = "Test Description Update",
                TypePayment = Models.Enums.TypePayment.Debit,
                AmountParcels = 1,
                Total = 750,
                TransactionDate = DateTimeOffset.Now,
                Active = true,
                UserUpdate = "ffm",
                UpdatedDate = DateTimeOffset.Now
            };

            _transactionRepository.Setup(x => x.UpdateTransaction(transaction)).ReturnsAsync(transactionDto);

            var result = await _controller.Put(transaction);

            result.Should().BeOfType<ResponseDto>()
                .Which.Result.As<TransactionDto>().Should().BeEquivalentTo(transactionDto);
        }
    }
}
