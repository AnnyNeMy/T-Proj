namespace invest_api.Contracts
{
  /// <summary>
  /// Отчет по позициям.
  /// </summary>
  public class PositionReport
  {
    /// <summary>
    /// ISIN
    /// </summary>
    public string Isin { get; set; }

    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Изменение цены
    /// </summary>
    public string ChangeCost { get; set; }
  }
}
