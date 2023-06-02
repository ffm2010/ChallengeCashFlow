using FinancialChallenge.Web.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace FinancialChallenge.Web.Models
{
    public class TransactionDto
    {
        public Guid TransactionId { get; set; }
        public TransactionType TransactionType { get; set; }
        public string Description { get; set; }
        public TypePayment TypePayment { get; set; }
        public int AmountParcels { get; set; } = 1;
        public decimal Total { get; set; } = 0;
        public DateTimeOffset TransactionDate { get; set; }
        public bool Active { get; set; } = true;
        public string UserCreated { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public string UserUpdate { get; set; }
        public DateTimeOffset? UpdatedDate { get; set; }
    }
}
