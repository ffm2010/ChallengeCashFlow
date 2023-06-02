using FinancialChallenge.Service.CashFlowAPI.Models.Enums;

namespace FinancialChallenge.Service.CashFlowAPI.Models.Dto
{
    public class TransactionDto
    {
        public Guid TransactionId { get; set; }
        public TransactionType TransactionType { get; set; }
        public string Description { get; set; }
        public TypePayment TypePayment { get; set; }
        public int AmountParcels { get; set; }
        public decimal Total { get; set; }
        public DateTimeOffset TransactionDate { get; set; }
        public bool Active { get; set; } = true;
        public string UserCreated { get; set; }
        public DateTimeOffset CreatedDate { get; set; } 
        public string UserUpdate { get; set; }
        public DateTimeOffset? UpdatedDate { get; set; }
    }
}
