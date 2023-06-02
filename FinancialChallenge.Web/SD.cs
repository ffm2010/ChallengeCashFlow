namespace FinancialChallenge.Web
{
    public static class SD
    {
        public static string CashFlowAPIBase { get; set; }
        public enum ApiType
        {
            GET,
            POST,
            PUT,
            DELETE
        }
    }
}
