namespace invest_api.Enum
{
    public enum ETypeName
    {
        None = 0,
        /// <summary>
        /// Биржевые облигации
        /// </summary>
        Exchange = 1,
        /// <summary>
        /// Еврооблигации МинФина
        /// </summary>
        MinFinEurobonds = 2,
        /// <summary>
        /// Корпоративные еврооблигации
        /// </summary>
        CorporateEurobonds = 3,
        /// <summary>
        /// Корпоративные облигации
        /// </summary>
        CorporateBonds = 4,
        /// <summary>
        /// Муниципальные облигации
        /// </summary>
        MunicipalBonds = 5,
        /// <summary>
        /// Облигации государств
        /// </summary>
        GovernmentBonds = 6,
        /// <summary>
        /// ОФЗ
        /// </summary>
        OFZ = 7,
        /// <summary>
        /// Региональные облигации
        /// </summary>
        RegionalBonds = 8,
    }
}
