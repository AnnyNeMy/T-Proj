using invest_api.Enum;
using invest_api.SQL;
using invest_api.Services.TInvest;
using invest_api.SQL;

namespace invest_api.Services
{
  internal static class DownloadDataService
  {
    public static void Refresh()
    {
      RefreshData();
      RefreshHistoryPrices();

      RefreshPortfolio();
      //RefreshDeals();
    }

    public static async Task<int> RefreshAsync()
    {
      await RefreshDataAsync();
      await RefreshHistoryPricesAsync();
      await RefreshPortfolioAsync();
      //RefreshDeals();
      return 1;
    }


    private static void RefreshHistoryPrices()
    {
      var bonds = BondsDBService.GetBonds();

      HistoryPriceDBService.Clean();

      foreach (var bond in bonds)
      {
        var prices = new TInvestService().GetHistory(bond.Figi, bond.Isin);

        foreach (var price in prices)
        {
          HistoryPriceDBService.Write(price);
        }

        Console.WriteLine($"{bond.Isin} {bond.Name} Write prices complete");

        Thread.Sleep(700);
      }
    }

    private static async Task<int> RefreshHistoryPricesAsync()
    {
      var bonds = await BondsDBService.GetBondsAsync();

      await HistoryPriceDBService.CleanAsync();

      foreach (var bond in bonds)
      {
        var prices = await new TInvestService().GetHistoryAsync(bond.Figi, bond.Isin);

        foreach (var price in prices)
        {
          HistoryPriceDBService.WriteAsync(price);
        }

        Console.WriteLine($"{bond.Isin} {bond.Name} Write prices complete");

        Thread.Sleep(700);
      }
      return 1;
    }


    private static void RefreshDeals()
    {
      DealsDBService.CleanDeals();
      var deals = new TInvestService().GetDeals();

      foreach (var deal in deals)
      {
        DealsDBService.Write(deal);
      }

      Console.WriteLine("RefreshDeals complete");
    }

    private static void RefreshPortfolio()
    {
      PortfolioDBService.CleanPortfolio();

      var bonds = new TInvestService().GetBondsData();
      var positions = new TInvestService().GetPortfolio();

      foreach (var position in positions)
      {
        foreach (var bond in bonds)
        {
          if (bond.Figi == position.Figi)
          {
            position.SellDate = bond.MaturityDate;
            position.Isin = bond.Isin;
            position.Name = bond.Name;
          }
        }

        PortfolioDBService.Write(position);
      }

      PortfolioDBService.UpdatePositionsOfferDate();

      Console.WriteLine("RefreshPortfolio complete");
    }

    private static async Task<int> RefreshPortfolioAsync()
    {
      await PortfolioDBService.CleanPortfolioAsync();

      var bonds = await new TInvestService().GetBondsDataAsync();
      var positions = await new TInvestService().GetPortfolioAsync();

      foreach (var position in positions)
      {
        foreach (var bond in bonds)
        {
          if (bond.Figi == position.Figi)
          {
            position.SellDate = bond.MaturityDate;
            position.Isin = bond.Isin;
            position.Name = bond.Name;
          }
        }
        await PortfolioDBService.WriteAsync(position);
      }

      await PortfolioDBService.UpdatePositionsOfferDateAsync();

      Console.WriteLine("RefreshPortfolio complete");
      return 1;
    }

    private static void RefreshData()
    {
      BondsDBService.CleanBond();
      BondsDBService.CleanBondEvents();

      var bonds = new TInvestService().GetBondsData();

      foreach (var bond in bonds)
      {
        BondsDBService.Write(bond);
      }

      foreach (var bond in bonds)
      {
        var events = new TInvestService().GetBondEvents(bond.Figi);

        foreach (var _event in events)
        {
          BondsDBService.Write(_event);
        }

        Thread.Sleep(700);

        Console.WriteLine($"{bond.Isin} {bond.Name} Fill Bond complete");
      }

      BondsDBService.UpdateBondCouponePay();
      BondsDBService.UpdateBondOfferDate();
    }

    private static async Task<bool> RefreshDataAsync()
    {
      var cleanBondRes = await BondsDBService.CleanBondAsync();
      var cleanBondEventsRes = await BondsDBService.CleanBondEventsAsync();

      if (cleanBondRes?.Status == ECommonStatus.Ok && cleanBondEventsRes?.Status == ECommonStatus.Ok)
      {

      }
      var bonds = await new TInvestService().GetBondsDataAsync();

      foreach (var bond in bonds)
      {
        await BondsDBService.WriteAsync(bond);
      }

      foreach (var bond in bonds)
      {
        var events = await new TInvestService().GetBondEventsAsync(bond.Figi);

        foreach (var _event in events)
        {
          await BondsDBService.WriteAsync(_event);
        }

        Thread.Sleep(700);

        Console.WriteLine($"{bond.Isin} {bond.Name} Fill Bond complete");
      }

      await BondsDBService.UpdateBondCouponePayAsync();
      await BondsDBService.UpdateBondOfferDateAsync();
      return true;
    }
  }
}
