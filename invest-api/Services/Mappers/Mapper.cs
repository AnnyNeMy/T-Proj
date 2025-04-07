using invest_api.Common;
using invest_api.Contracts;
using invest_api.Enum;
using invest_api.Services.Data;

namespace invest_api.Services.Mappers
{
  internal static class Mapper
  {
    public static PositionDealsReport Map(DealReport deal)
    {
      if (deal == null)
      {
        throw new ArgumentNullException(nameof(deal));
      }

      var mapReport = new PositionDealsReport
      {
        Isin = deal.Isin,
        Name = deal.Name,
        BuyDate = deal.BuyDate.ToString(CommonVariables.DateFormat),
        SellDate = deal.SellDate?.ToString(CommonVariables.DateFormat),
        Count = deal.Count.ToString(),
        IsClose = deal.IsClose ? "Закрыта" : "Открыта",
        BuyPrice = deal.BuyPrice.ToString(CommonVariables.MoneyFormat),
        SellPrice = deal.SellPrice?.ToString(CommonVariables.MoneyFormat),
        SumChange = deal.SumChange?.ToString(CommonVariables.MoneyFormat),
        PrcntChange = deal.PrcntChange?.ToString(CommonVariables.MoneyFormat),
        PrcntPerDay = deal.PrcntPerDay?.ToString(CommonVariables.MoneyFormat),
        PrcntPerYear = "0",
      };

      return mapReport;
    }
    /// <summary>
    /// Маппим Deal в DealReport
    /// </summary>
    public static List<DealReport> Map(List<Deal> deals)
    {
      var dealsBuy = deals.Where(x => x.DealType == EDealType.Buy).OrderBy(x => x.DealDate);
      var dealsSell = deals.Where(x => x.DealType == EDealType.Sell);

      var reportDeals = new List<DealReport>();

      foreach (var dealBuy in dealsBuy)
      {
        var dealReport = new DealReport()
        {
          Isin = dealBuy.Isin,
          Name = dealBuy.Name,
          BuyDate = dealBuy.DealDate,
          BuyPrice = Math.Abs(dealBuy.Sum),
          Count = 1,
        };

        var sellDeal = dealsSell.Where(x => !x.IsUse && x.Isin == dealBuy.Isin).OrderBy(x => x.DealDate).FirstOrDefault();

        if (sellDeal != null)
        {
          dealReport.SellDate = sellDeal.DealDate;
          dealReport.SellPrice = sellDeal.Sum;
          dealReport.IsClose = true;

          sellDeal.IsUse = true;
        }

        reportDeals.Add(dealReport);
      }

      return reportDeals;
    }
  }
}
