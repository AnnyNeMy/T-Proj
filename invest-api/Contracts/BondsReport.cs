namespace invest_api.Contracts
{
  /// <summary>
  /// Отчет облигаций.
  /// </summary>
  public class BondsReport
  {
    /// <summary>
    /// Isin
    /// </summary>
    public string Isin { get; set; }

    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Валюта.
    /// </summary>
    public string Currency { get; set; }

    /// <summary>
    /// Купленный.
    /// </summary>
    public bool IsBuyed { get; set; }

    /// <summary>
    /// Дата погашения.
    /// </summary>
    public string CloseDate { get; set; }

    /// <summary>
    /// Текущая цена.
    /// </summary>
    public string LastPrice { get; set; }

    /// <summary>
    /// Прибыль в год.
    /// </summary>
    public string Profit { get; set; }

    /// <summary>
    /// Волатильность.
    /// </summary>
    public string Volatily { get; set; }

    /// <summary>
    /// Изменение за период
    /// </summary>
    public string Change { get; set; }
  }
}
