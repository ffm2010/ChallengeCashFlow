using AutoMapper;
using FinancialChallenge.Service.CashFlowAPI.Models;
using FinancialChallenge.Service.CashFlowAPI.Models.Dto;

namespace FinancialChallenge.Service.CashFlowAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<TransactionDto, Transaction>();
                config.CreateMap<Transaction, TransactionDto>();
            });

            return mappingConfig;
        }
    }
}
