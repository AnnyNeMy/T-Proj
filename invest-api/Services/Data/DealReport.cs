namespace invest_api.Services.Data
{
  public class DealReport
  {
    public string Isin { get; set; }
    public string Name { get; set; }
    public DateTime BuyDate { get; set; }
    public DateTime? SellDate { get; set; }
    public int Count { get; set; }
    public bool IsClose { get; set; }
    public double BuyPrice { get; set; }
    public double? SellPrice { get; set; }
    public bool IsCoupone { get; set; }
    public double? SumChange
    {
      get
      {
        if (SellPrice == null)
        {
          return null;
        }

        return (SellPrice - BuyPrice) * Count;
      }
    }
    public double? PrcntChange
    {
      get
      {
        if (SellPrice == null)
        {
          return null;
        }

        return 100 * (SellPrice - BuyPrice) / BuyPrice;
      }
    }

    public double? PrcntPerDay
    {
      get
      {
        if (SellDate == null || SellPrice == null)
        {
          return null;
        }

        var diffdate = ((DateTime)SellDate - BuyDate).TotalDays;

        diffdate = diffdate > 0 ? diffdate : 1;

        return PrcntChange / diffdate;
      }
    }
  }
}
