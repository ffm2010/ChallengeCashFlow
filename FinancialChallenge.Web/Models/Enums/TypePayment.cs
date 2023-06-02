using System.ComponentModel;

namespace FinancialChallenge.Web.Models.Enums
{
    public enum TypePayment : byte
    {
        [Description("Débito")]
        Debit = 1,
        [Description("Crédito")]
        Credit
    }
}
