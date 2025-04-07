using invest_api.Contracts;
using invest_api.Services.TInvest;
using invest_api.SQL;

namespace invest_api.Services
{
    public static class ApiService
    {
      /// <summary>
      /// Движение средств
      /// </summary>
      public static async Task<List<MoneyReport>> MoneyReportAsync()
      {
        return await new TInvestPortfolioService().MoneyReportAsync();
      }

      /// <summary>
      /// Текщие позиции
      /// </summary>
      public static async Task<List<PortfolioReport>> PortfolioReportAsync()
      {
        return await new TInvestPortfolioService().PortfolioReportAsync();
      }

      /// <summary>
      /// Купоны
      /// </summary>
      public static async Task<List<CouponeReport>> CouponeReportAsync()
      {
        return await new TInvestPortfolioService().GetCouponePayAsync();
      }

      /// <summary>
      /// Сводка бондов.
      /// </summary>
      public static async Task<List<BondsReport>> GetBondAsync(BondsReportRequest request)
      {
        return await BondsDBService.GetBondReportAsync(request);
      }

      /// <summary>
      /// Сделки для позиции.
      /// </summary>
      public static async Task<List<PositionDealsReport>> PositionDealsReportAsync(string isin)
      {
        return await new PositionsReportService().GetPositionDealsReportByIsinAsync(isin);
      }

      /// <summary>
      /// Агрегированный отчет
      /// </summary>
      public static async Task<List<PositionReport>> PositionReportAsync()
      {
        return await new PositionsReportService().GetPositionsReportAsync();
      }

      [Obsolete]
      public static async Task<List<PositionReport>> FaviritePositionReportAsync()
      {
        return await new PositionsReportService().GetFaviritePositionsReportAsync();
      }

      /// <summary>
      /// Под наблюдением.
      /// </summary>
      public static async Task<List<ObservedPricesReport>> ObservedPricesAsync()
      {
        return await new TInvestPricesService().ObservedPricesAsync();
      }
  }
}
