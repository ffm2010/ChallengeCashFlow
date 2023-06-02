using System.ComponentModel;

namespace FinancialChallenge.Web.Models.Enums
{
    public enum TransactionType : byte
    {
        [Description("Entrada")]
        Input = 1,
        [Description("Saída")]
        Output
    }
}
