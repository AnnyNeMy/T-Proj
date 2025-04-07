namespace invest_api.Contracts
{
  /// <summary>
  /// Выплаты по облигациям (купон или погашение)
  /// </summary>
  public class CouponeReport
  {
    /// <summary>
    /// Порядковый номер.
    /// </summary>
    public string OrderId { get; set; }

    /// <summary>
    /// Isin
    /// </summary>
    public string Isin { get; set; }

    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Дата выплаты.
    /// </summary>
    public string PayDate { get; set; }

    /// <summary>
    /// Сумма.
    /// </summary>
    public string Sum { get; set; }
  }
}
