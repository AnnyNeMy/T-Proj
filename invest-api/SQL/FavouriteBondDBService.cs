using invest_api.Common;
using invest_api.Enum;
using invest_api.Services;
using invest_api.Services.TInvest;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace invest_api.SQL
{
  public static class FavouriteBondDBService
  {
    public static async Task<List<FavouriteBond>> GetFavouriteBondsAsync()
    {
      try
      {
        var favouriteBonds = new List<FavouriteBond>();

        using var conn = new NpgsqlConnection(CommonVariables.ConnectionString);
        await conn.OpenAsync();

        var cmdStr = "select figi, isin, name from public.favouritebond";

        using (var cmd = new NpgsqlCommand(cmdStr, conn))
        {
          var reader = await cmd.ExecuteReaderAsync();

          while (await reader.ReadAsync())
          {
            var figi = reader.GetString(0);
            var isin = reader.GetString(1);
            var name = reader.GetString(2);

            var fbond = new FavouriteBond()
            {
              Figi = figi,
              Isin = isin,
              Name = name,
            };

            favouriteBonds.Add(fbond);
          }
        }

        return favouriteBonds;
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message);
      }
    }

    public static async Task<int> CleanFavouriteBondAsync()
    {
      try
      {
        using var conn = new NpgsqlConnection(CommonVariables.ConnectionString);
        await conn.OpenAsync();

        var cmdStr = "delete from favouritebond";

        await using var cmd = new NpgsqlCommand(cmdStr, conn);
        var res = await cmd.ExecuteNonQueryAsync();
        return res;

      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
        return -1;
      }
    }

    public static async Task<int> CleanFavouriteBondByIsinAsync(string isin)
    {
      try
      {
        using var conn = new NpgsqlConnection(CommonVariables.ConnectionString);
        await conn.OpenAsync();

        var cmdStr = "DELETE FROM public.favouritebond WHERE isin = @isin";

        await using var cmd = new NpgsqlCommand(cmdStr, conn);
        cmd.Parameters.AddWithValue("@isin", isin);

        var res = await cmd.ExecuteNonQueryAsync();
        if (res == 0)
        {
          Console.WriteLine("CleanFavouriteBondByIsinAsync - Операция не затронула ни одной строки в БД.");
        //  throw new Exception("CleanFavouriteBondByIsinAsync - Операция не затронула ни одной строки в БД.");
        }

        return res;

      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
        return -1;
      }
    }

    public static async Task<CommonResponse> WriteFavouriteBondAsync(string isin)
    {
      try
      {
        var fBond =  new FavouriteBond();

        using var conn = new NpgsqlConnection(CommonVariables.ConnectionString);
        await conn.OpenAsync();

        var cmdStrFind = "SELECT COUNT(*) FROM public.favouritebond WHERE isin = @isin";
        await using var cmdFind = new NpgsqlCommand(cmdStrFind, conn);
        cmdFind.Parameters.AddWithValue("@isin", isin);
     
        var res = await cmdFind.ExecuteScalarAsync() ?? 0;
        int count = Convert.ToInt32(res);

        if ((int)count > 0)
        {
          return new CommonResponse(ECommonStatus.Error, $"Ошибка добавления {isin}, так как он уже добавлен в таблицу favorite");
        }

        var bond = await new TInvestPortfolioService().GetFavouriteBondAsync(isin);
        if (bond != null)
        {
          fBond.Figi = bond.Figi;
          fBond.Isin = bond.Isin;
          fBond.Name = bond.Name;

          var cmdStr = "INSERT INTO favouritebond (figi, isin, name)" +
          "VALUES (@figi, @isin, @name)";
          await using var cmd = new NpgsqlCommand(cmdStr, conn);
          cmd.Parameters.AddWithValue("figi", fBond.Figi);
          cmd.Parameters.AddWithValue("isin", fBond.Isin);
          cmd.Parameters.AddWithValue("name", fBond.Name);

          var result = await cmd.ExecuteNonQueryAsync();

          return (int)Convert.ToInt32(result) > 0 ? new CommonResponse(ECommonStatus.Ok, $" Isin {isin} успешно добавлен в таблицу favorite") : new CommonResponse(ECommonStatus.Error, $"Ошибка добавления {isin}");
        }
        return new CommonResponse(ECommonStatus.Error, $"Ошибка добавления {isin}, так как он уже добавлен в таблицу favorite"); 
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
        return new CommonResponse(ECommonStatus.Error, $"Ошибка добавления {isin}, Exeption: {ex.Message}");
      }
    }

  }
}
