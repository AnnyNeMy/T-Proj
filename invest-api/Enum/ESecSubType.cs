namespace invest_api.Enum
{
    public enum ESecSubType
    {
        None = 0,

        /// <summary>
        /// Облигации с ипотечным покрытием
        /// </summary>
        MortgageBacked = 1,

        /// <summary>
        /// Структурные облигации
        /// </summary>
        Structured = 2,

        /// <summary>
        /// Субординированные облигации
        /// </summary>
        Subordinated = 3,
    }
}
