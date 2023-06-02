using FinancialChallenge.Service.CashFlowAPI.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace FinancialChallenge.Service.CashFlowAPI.Models
{
    public class Transaction
    {
        [Key]
        public Guid TransactionId { get; set; }
        [Required]
        public TransactionType TransactionType { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public TypePayment TypePayment { get; set; }
        [Required]
        public int AmountParcels { get; set; }
        [Required]
        public decimal Total { get; set; }
        [Required]
        public DateTimeOffset TransactionDate { get; set; }
        [Required]
        public bool Active { get; set; }
        public string UserCreated { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public string UserUpdate { get; set; }
        public DateTimeOffset? UpdatedDate { get; set; }
    }
}
