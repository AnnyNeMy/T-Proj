namespace invest_api.Contracts
{
  /// <summary>
  /// Отчет о движении денежных средств.
  /// </summary>
  public class MoneyReport
  {
    /// <summary>
    /// Дата.
    /// </summary>
    public string Date { get; set; }

    /// <summary>
    /// Действие.
    /// </summary>
    public string Action { get; set; }

    /// <summary>
    /// Сумма зачисления.
    /// </summary>
    public string Sum { get; set; }

    public override string ToString()
    {
      return $"{Sum}";
      //return $"{Date}\t{Action}\t{Sum}";
    }
  }
}
