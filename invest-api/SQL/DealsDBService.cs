using invest_api.Common;
using invest_api.Enum;
using Npgsql;

namespace invest_api.SQL
{
    public static class DealsDBService
    {
        ///  const string CommonVariables.ConnectionString = "Host=localhost;Username=postgres;Password=160808;Database=Trading";

        public static List<Deal> GetDeals()
        {
            try
            {
                var deals = new List<Deal>();

                using var conn = new NpgsqlConnection(CommonVariables.ConnectionString);
                conn.Open();

                var cmdStr = "select figi, isin, name, dealdate, dealtype, sum, count from public.deal";

                using (var cmd = new NpgsqlCommand(cmdStr, conn))
                {
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var figi = reader.GetString(0);
                        var isin = reader.GetString(1);
                        var name = reader.GetString(2);
                        var dealdate = reader.GetDateTime(3);
                        var dealtype = reader.GetInt32(4);
                        var sum = reader.GetInt32(5);
                        var count = reader.GetInt32(6);

                        var deal = new Deal()
                        {
                            Figi = figi,
                            Isin = isin,
                            Name = name,
                            DealDate = dealdate,
                            DealType = (EDealType)dealtype,
                            Sum = sum,
                            Count = count,
                        };

                        deals.Add(deal);
                    }
                }

                return deals;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    public static async Task<List<Deal>> GetDealsAsync()
    {
      try
      {
        var deals = new List<Deal>();

        using var conn = new NpgsqlConnection(CommonVariables.ConnectionString);
        await conn.OpenAsync();

        var cmdStr = "select figi, isin, name, dealdate, dealtype, sum, count from public.deal";

        using (var cmd = new NpgsqlCommand(cmdStr, conn))
        {
          var reader = await cmd.ExecuteReaderAsync();

          while (await reader.ReadAsync())
          {
            var figi = reader.GetString(0);
            var isin = reader.GetString(1);
            var name = reader.GetString(2);
            var dealdate = reader.GetDateTime(3);
            var dealtype = reader.GetInt32(4);
            var sum = reader.GetInt32(5);
            var count = reader.GetInt32(6);

            var deal = new Deal()
            {
              Figi = figi,
              Isin = isin,
              Name = name,
              DealDate = dealdate,
              DealType = (EDealType)dealtype,
              Sum = sum,
              Count = count,
            };

            deals.Add(deal);
          }
        }

        return deals;
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message);
      }
    }


    public static void Write(Deal deal)
        {
            try
            {
                using var conn = new NpgsqlConnection(CommonVariables.ConnectionString);
                conn.Open();

                var cmdStr = "INSERT INTO deal (figi, isin, name, dealDate, dealType, sum, count) " +
                    "VALUES (@figi, @isin, @name, @dealDate, @dealType, @sum, @count)";

                using (var cmd = new NpgsqlCommand(cmdStr, conn))
                {
                    cmd.Parameters.AddWithValue("figi", deal.Figi);
                    cmd.Parameters.AddWithValue("isin", deal.Isin);
                    cmd.Parameters.AddWithValue("name", deal.Name);
                    cmd.Parameters.AddWithValue("dealDate", deal.DealDate);
                    cmd.Parameters.AddWithValue("dealType", (int)deal.DealType);
                    cmd.Parameters.AddWithValue("sum", deal.Sum);
                    cmd.Parameters.AddWithValue("count", deal.Count);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    public static async Task<int> WriteAsync(Deal deal)
    {
      try
      {
        using var conn = new NpgsqlConnection(CommonVariables.ConnectionString);
        await conn.OpenAsync();

        var cmdStr = "INSERT INTO deal (figi, isin, name, dealDate, dealType, sum, count) " +
            "VALUES (@figi, @isin, @name, @dealDate, @dealType, @sum, @count)";

          await using var cmd = new NpgsqlCommand(cmdStr, conn);
          cmd.Parameters.AddWithValue("figi", deal.Figi);
          cmd.Parameters.AddWithValue("isin", deal.Isin);
          cmd.Parameters.AddWithValue("name", deal.Name);
          cmd.Parameters.AddWithValue("dealDate", deal.DealDate);
          cmd.Parameters.AddWithValue("dealType", (int)deal.DealType);
          cmd.Parameters.AddWithValue("sum", deal.Sum);
          cmd.Parameters.AddWithValue("count", deal.Count);
          int result = await cmd.ExecuteNonQueryAsync();
        return result;
      } 
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
        return -1;
      }
    }


    public static void CleanDeals()
        {
            try
            {
                using var conn = new NpgsqlConnection(CommonVariables.ConnectionString);
                conn.Open();

                var cmdStr = "delete from deal";

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

    public static async Task<int> CleanDealsAsync()
    {
      try
      {
        using var conn = new NpgsqlConnection(CommonVariables.ConnectionString);
        await conn.OpenAsync();

        var cmdStr = "delete from deal";

        await using var cmd = new NpgsqlCommand(cmdStr, conn);
        int result = await cmd.ExecuteNonQueryAsync();
        return result;
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
        return -1;
      }
    }

  }
}
