namespace invest_api.Contracts
{

  /// <summary>
  /// Наблюдаемые цены.
  /// </summary>
  public class ObservedPricesReport
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
    /// Предыдщая цена
    /// </summary>
    public string LagPrice { get; set; }

    /// <summary>
    /// Текущая цена
    /// </summary>
    public string CurrentPrice { get; set; }

    /// <summary>
    /// Изменение цены в процентах
    /// </summary>
    public string ChangePrice { get; set; }
  }
}
