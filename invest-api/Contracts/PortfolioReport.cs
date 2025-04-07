namespace invest_api.Contracts
{
  /// <summary>
  /// Отчет о текущих позициях
  /// </summary>
  public class PortfolioReport
  {
    /// <summary>
    /// Порядковый номер.
    /// </summary>
    public string OrderId { get; set; }

    /// <summary>
    /// ISIN
    /// </summary>
    public string Isin { get; set; }

    /// <summary>
    /// Наименование
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Количество
    /// </summary>
    public string Count { get; set; }

    /// <summary>
    /// Валюта.
    /// </summary>
    public string Currency { get; set; }

    /// <summary>
    /// Текущая цена.
    /// </summary>
    public string Price { get; set; }
  }
}
