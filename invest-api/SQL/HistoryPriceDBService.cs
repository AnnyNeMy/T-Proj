using invest_api.Common;
using Npgsql;

namespace invest_api.SQL
{
  public static class HistoryPriceDBService
  {
    public static List<HistoryPrice> GetHistoryPricebyIsin(string isin)
    {
      var result = new List<HistoryPrice>();

      try
      {
        using var conn = new NpgsqlConnection(CommonVariables.ConnectionString);
        conn.Open();

        var cmdStr = "select figi, isin, date, price from public.historyprice where Isin = @isin;";

        using (var cmd = new NpgsqlCommand(cmdStr, conn))
        {
          cmd.Parameters.AddWithValue("isin", isin);

          var reader = cmd.ExecuteReader();

          while (reader.Read())
          {

            var figi = reader.GetString(0);
            var _isin = reader.GetString(1);
            var date = reader.GetDateTime(2);
            var price = reader.GetDouble(3);

            var hPrice = new HistoryPrice()
            {
              Figi = figi,
              Isin = _isin,
              Date = date,
              Price = price,
            };

            result.Add(hPrice);
          }
        }
        return result;
      }
      catch (Exception ex)
      {
        Console.WriteLine($"{nameof(GetHistoryPricebyIsin)} {ex.Message}");
        throw;
      }
    }


    public static async Task<List<HistoryPrice>> GetHistoryPricebyIsinAsync(string isin)
    {
      var result = new List<HistoryPrice>();

      try
      {
        using var conn = new NpgsqlConnection(CommonVariables.ConnectionString);
        await conn.OpenAsync();

        var cmdStr = "select figi, isin, date, price from public.historyprice where Isin = @isin;";



        await using var cmd = new NpgsqlCommand(cmdStr, conn);
        cmd.Parameters.AddWithValue("isin", isin);
        var reader = await cmd.ExecuteReaderAsync();
        while (reader.Read())
        {

          var figi = reader.GetString(0);
          var _isin = reader.GetString(1);
          var date = reader.GetDateTime(2);
          var price = reader.GetDouble(3);

          var hPrice = new HistoryPrice()
          {
            Figi = figi,
            Isin = _isin,
            Date = date,
            Price = price,
          };

          result.Add(hPrice);
        }

        return result;
      }
      catch (Exception ex)
      {
        Console.WriteLine($"{nameof(GetHistoryPricebyIsinAsync)} {ex.Message}");
        throw;
      }
    }

    public static void Write(HistoryPrice position)
    {
      try
      {
        using var conn = new NpgsqlConnection(CommonVariables.ConnectionString);
        conn.Open();

        var cmdStr = "INSERT INTO historyprice (figi, isin, date, price) " +
            "VALUES (@figi, @isin, @date, @price)";

        using (var cmd = new NpgsqlCommand(cmdStr, conn))
        {
          cmd.Parameters.AddWithValue("figi", position.Figi);
          cmd.Parameters.AddWithValue("isin", position.Isin);
          cmd.Parameters.AddWithValue("date", position.Date);
          cmd.Parameters.AddWithValue("price", position.Price);

          cmd.ExecuteNonQuery();
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }
    }

    public static async Task<int> WriteAsync(HistoryPrice position)
    {
      try
      {
        using var conn = new NpgsqlConnection(CommonVariables.ConnectionString);
        await conn.OpenAsync();

        var cmdStr = "INSERT INTO historyprice (figi, isin, date, price) " +
            "VALUES (@figi, @isin, @date, @price)";

        await using var cmd = new NpgsqlCommand(cmdStr, conn);
        cmd.Parameters.AddWithValue("figi", position.Figi);
        cmd.Parameters.AddWithValue("isin", position.Isin);
        cmd.Parameters.AddWithValue("date", position.Date);
        cmd.Parameters.AddWithValue("price", position.Price);

        int result = await cmd.ExecuteNonQueryAsync();
        return result;
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
        return -1;
      }
    }


    public static void Clean()
    {
      try
      {
        using var conn = new NpgsqlConnection(CommonVariables.ConnectionString);
        conn.Open();

        var cmdStr = "delete from historyprice";

        using (var cmd = new NpgsqlCommand(cmdStr, conn))
        {
          cmd.ExecuteNonQuery();
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }
    }


    public static async Task<int> CleanAsync()
    {
      try
      {
        using var conn = new NpgsqlConnection(CommonVariables.ConnectionString);
        await conn.OpenAsync();

        var cmdStr = "delete from historyprice";

        await using var cmd = new NpgsqlCommand(cmdStr, conn);
        var result = await cmd.ExecuteNonQueryAsync();
        return (int)result;
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
        return -1;
      }
    }
  }
}
