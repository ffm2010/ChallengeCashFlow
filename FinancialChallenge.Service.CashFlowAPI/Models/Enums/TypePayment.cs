using System.ComponentModel;

namespace FinancialChallenge.Service.CashFlowAPI.Models.Enums
{
    public enum TypePayment : byte
    {
        [Description("Débito")]
        Debit = 1,
        [Description("Crédito")]
        Credit
    }
}
