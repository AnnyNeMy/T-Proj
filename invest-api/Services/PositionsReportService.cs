using invest_api.Common;
using invest_api.Contracts;
using invest_api.Services.Data;
using invest_api.Services.Mappers;
using invest_api.Services.TInvest;
using invest_api.SQL;
using System.Globalization;

namespace invest_api.Services
{
  public class PositionsReportService
  {
    private readonly List<FavouriteBond> TempRecords = new List<FavouriteBond>()
    {
      new FavouriteBond(){Figi = "BBG00DQFVRK3",Isin = "RU000A0JWST1",Name = "ГТЛК БО-08",},
      new FavouriteBond(){Figi = "BBG00FRWYRH1",Isin = "RU000A0JX5W4",Name = "О'КЕЙ выпуск 2",},
      new FavouriteBond(){Figi = "BBG00YJJPT66",Isin = "RU000A102H91",Name = "Автодор БО-3 выпуск 1",},
      new FavouriteBond(){Figi = "TCS00A102K96",Isin = "RU000A102K96",Name = "ВЭБ.РФ ПБО-001Р-23В",},
    };

    public List<PositionReport> GetPositionsReport()
    {
      var result = new List<PositionReport>();
      foreach (var item in TempRecords)
      {
        result.Add(GetPositionReportByIsin(item.Isin));
      }

      return result;
    }

    public async Task<List<PositionReport>> GetPositionsReportAsync()
    {
      var result = new List<PositionReport>();
      foreach (var item in TempRecords)
      {
        var isin = await GetPositionReportByIsinAsync(item.Isin);
        result.Add(isin);
      }

      return result;
    }

    public async Task<List<PositionReport>> GetFaviritePositionsReportAsync()
    {
      var result = new List<PositionReport>();
      var fbounds = await FavouriteBondDBService.GetFavouriteBondsAsync();
      foreach (var item in fbounds)
      {
        var isin = await GetPositionReportByIsinAsync(item.Isin);
        result.Add(isin);
      }

      return result;
    }

    public PositionReport GetPositionReportByIsin(string isin)
    {
      var result = GetDealReportsByIsin(isin);

      var report = new PositionReport()
      {
        Isin = isin,
      };

      if (!result.Any())
      {
        return report;
      }

      report.Name = result.First().Name;

      var coupone = result.Where(x => x.IsCoupone)?.Sum(x => x.SellPrice);
      ;
      if (result.Any(x => x.IsClose))
      {
        var sum = result.Sum(x => x.SumChange) + coupone;
        report.ChangeCost = sum?.ToString(CommonVariables.MoneyFormat);
      }

      return report;
    }


    public async Task<PositionReport> GetPositionReportByIsinAsync(string isin)
    {
      var result = await GetDealReportsByIsinAsync(isin);

      var report = new PositionReport()
      {
        Isin = isin,
      };

      if (!result.Any())
      {
        return report;
      }

      report.Name = result.First().Name;

      var coupone = result.Where(x => x.IsCoupone)?.Sum(x => x.SellPrice);
      ;
      if (result.Any(x => x.IsClose))
      {
        var sum = result.Sum(x => x.SumChange) + coupone;
        report.ChangeCost = sum?.ToString(CommonVariables.MoneyFormat);
      }

      return report;
    }


    public List<PositionDealsReport> GetPositionDealsReportByIsin(string isin)
    {
      var records = GetDealReportsByIsin(isin);

      return records.Select(x => Mapper.Map(x)).ToList();
    }

    public async Task<List<PositionDealsReport>> GetPositionDealsReportByIsinAsync(string isin)
    {
      var records =  await GetDealReportsByIsinAsync(isin);

      return records.Select(x => Mapper.Map(x)).ToList();
    }

    public List<DealReport> GetDealReportsByIsin(string isin)
    {
      var deals = new TInvestServiceDeals().ReportDeals(isin);

      var dealReports = Mapper.Map(deals);

      var results = Group(dealReports);

      results = new TInvestPricesService().FillCurrentPrice(results);

      var coupones = new TInvestPortfolioService().GetCouponePay().Where(x => x.Isin == isin);

      if (coupones?.Count() > 0)
      {
        foreach (var coupone in coupones)
        {
          var deal = new DealReport()
          {
            Isin = coupone.Isin,
            Name = coupone.Name,
            SellPrice = double.Parse(coupone.Sum),
            IsCoupone = true,
          };

          results.Add(deal);
        }
      }

      return results;
    }

    public async Task <List<DealReport>> GetDealReportsByIsinAsync(string isin)
    {
      var deals = await new TInvestServiceDeals().ReportDealsAsync(isin);

      var dealReports = Mapper.Map(deals);

      var results = Group(dealReports);

      results = await new TInvestPricesService().FillCurrentPriceAsync(results);

      var coupones = (await new TInvestPortfolioService().GetCouponePayAsync()).Where(x => x.Isin == isin);

      if (coupones?.Count() > 0)
      {
        foreach (var coupone in coupones)
        {
          var deal = new DealReport()
          {
            Isin = coupone.Isin,
            Name = coupone.Name,
            SellPrice = double.Parse(coupone.Sum, CultureInfo.InvariantCulture),
            IsCoupone = true,
          };

          results.Add(deal);
        }
      }

      return results;
    }

    private static List<DealReport> Group(List<DealReport> deals)
    {
      var _deals = from deal in deals
                   group deal by (
                       deal.Isin,
                       deal.Name,
                       deal.BuyDate,
                       deal.BuyPrice,
                       deal.SellDate,
                       deal.SellPrice,
                       deal.IsClose
                       ) into g
                   select new DealReport
                   {
                     Isin = g.Key.Isin,
                     Name = g.Key.Name,
                     BuyDate = g.Key.BuyDate,
                     BuyPrice = g.Key.BuyPrice,
                     SellDate = g.Key.SellDate,
                     SellPrice = g.Key.SellPrice,
                     IsClose = g.Key.IsClose,
                     Count = g.Count()
                   };

      return _deals.ToList();
    }
  }
}
