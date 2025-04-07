namespace invest_api.Contracts
{
  /// <summary>
  /// Отчет о сделках
  /// </summary>
  public class PositionDealsReport
  {
    /// <summary>
    /// ISIN
    /// </summary>
    public string Isin { get; set; }

    /// <summary>
    /// Наименование
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Дата покупки
    /// </summary>
    public string BuyDate { get; set; }

    /// <summary>
    /// Дата продажи
    /// </summary>
    public string SellDate { get; set; }

    /// <summary>
    /// Количество
    /// </summary>
    public string Count { get; set; }

    /// <summary>
    /// Позиция закрыта
    /// </summary>
    public string IsClose { get; set; }

    /// <summary>
    /// Цена покупки.
    /// </summary>
    public string BuyPrice { get; set; }

    /// <summary>
    /// Цена продажи
    /// </summary>
    public string SellPrice { get; set; }

    /// <summary>
    /// Изменение абсолбтное
    /// </summary>
    public string SumChange { get; set; }

    /// <summary>
    /// Изменение в процентах
    /// </summary>
    public string PrcntChange { get; set; }

    /// <summary>
    /// Изменение в процентах дневное
    /// </summary>
    public string PrcntPerDay { get; set; }

    /// <summary>
    /// Изменение в процентах годовое
    /// </summary>
    public string PrcntPerYear { get; set; }
  }
}
