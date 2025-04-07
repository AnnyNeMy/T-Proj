namespace invest_api.Contracts
{
  /// <summary>
  /// Запрос на отчет облигаций
  /// </summary>
  public class BondsReportRequest
  {
    /// <summary>
    /// Валюта.
    /// </summary>
    public string Currency { get; set; }

    /// <summary>
    /// Дата погашения.
    /// </summary>
    public DateTime CloseDate { get; set; }

    /// <summary>
    /// Прибыль в год.
    /// </summary>
    public double Profit { get; set; }

    /// <summary>
    /// Цена дней назад.
    /// </summary>
    public double LagDay { get; set; }
  }
}
