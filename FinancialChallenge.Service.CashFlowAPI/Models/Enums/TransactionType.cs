using System.ComponentModel;

namespace FinancialChallenge.Service.CashFlowAPI.Models.Enums
{
    public enum TransactionType : byte
    {
        [Description("Entrada")]
        Input = 1,
        [Description("Saída")]
        Output
    }
}
