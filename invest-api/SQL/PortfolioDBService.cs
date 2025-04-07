using invest_api.Common;
using Npgsql;
using System.Collections.Generic;
using System.Diagnostics;

namespace invest_api.SQL
{
    public static class PortfolioDBService
    {
        public static List<Position> GetPositions()
        {
            try
            {
                var positions = new List<Position>();

                using var conn = new NpgsqlConnection(CommonVariables.ConnectionString);
                conn.Open();

                var cmdStr = "select figi, isin, name, selldate from public.positions";

                using (var cmd = new NpgsqlCommand(cmdStr, conn))
                {
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var figi = reader.GetString(0);
                        var isin = reader.GetString(1);
                        var name = reader.GetString(2);
                        var selldate = reader.GetDateTime(3);

                        var position = new Position()
                        {
                            Figi = figi,
                            Isin = isin,
                            Name = name,
                            SellDate = selldate
                        };

                        positions.Add(position);
                    }
                }

                return positions;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    public static async Task<List<Position>> GetPositionsAsync()
    {
      try
      {
        var positions = new List<Position>();

        using var conn = new NpgsqlConnection(CommonVariables.ConnectionString);
        await conn.OpenAsync();

        var cmdStr = "select figi, isin, name, selldate from public.positions";

        using (var cmd = new NpgsqlCommand(cmdStr, conn))
        {
          var reader = await cmd.ExecuteReaderAsync();

          while (await reader.ReadAsync())
          {
            var figi = reader.GetString(0);
            var isin = reader.GetString(1);
            var name = reader.GetString(2);
            var selldate = reader.GetDateTime(3);

            var position = new Position()
            {
              Figi = figi,
              Isin = isin,
              Name = name,
              SellDate = selldate
            };

            positions.Add(position);
          }
        }

        return positions;
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message);
      }
    }

    public static  List<BondEvent> GetBondevent()
        {
            try
            {
                var positions = new List<BondEvent>();

                using var conn = new NpgsqlConnection(CommonVariables.ConnectionString);
                conn.Open();

                var cmdStr = "select figi, eventdate, eventtype, execution, operationtype, note, payvalue  from public.bondevent";

                using (var cmd = new NpgsqlCommand(cmdStr, conn))
                {
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Console.WriteLine($"figi: {reader["figi"]}, isin: {reader["isin"]}, name: {reader["name"]}, " +
                                     $"dealdate: {reader["dealdate"]}, dealtype: {reader["dealtype"]}, sum: {reader["sum"]}, count: {reader["count"]}");
                        var figi = reader.GetString(0);
                        var eventdate = reader.GetDateTime(1);
                        var eventtype = reader.GetString(2);
                        var execution = reader.GetString(3);
                        var operationtype = reader.GetString(4);
                        var note = reader.GetString(5);
                        var payvalue = reader.GetDouble(6);

                        var position = new BondEvent()
                        {
                            Figi = figi,
                            EventDate = eventdate,
                            EventType = eventtype,
                            Execution = execution,
                            OperationType = operationtype,
                            Note = note,
                            PayValue = payvalue
                        };
                        positions.Add(position);
                    }
                }
                return positions;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    public static async Task<List<BondEvent>> GetBondeventAsync()
    {
      try
      {
        var positions = new List<BondEvent>();

        using var conn = new NpgsqlConnection(CommonVariables.ConnectionString);
        await conn.OpenAsync();

        var cmdStr = "select figi, eventdate, eventtype, execution, operationtype, note, payvalue  from public.bondevent";

        using (var cmd = new NpgsqlCommand(cmdStr, conn))
        {
          using var reader = await cmd.ExecuteReaderAsync();

          while (await reader.ReadAsync())
          {
            Console.WriteLine($"figi: {reader["figi"]}, isin: {reader["isin"]}, name: {reader["name"]}, " +
                         $"dealdate: {reader["dealdate"]}, dealtype: {reader["dealtype"]}, sum: {reader["sum"]}, count: {reader["count"]}");
            var figi = reader.GetString(0);
            var eventdate = reader.GetDateTime(1);
            var eventtype = reader.GetString(2);
            var execution = reader.GetString(3);
            var operationtype = reader.GetString(4);
            var note = reader.GetString(5);
            var payvalue = reader.GetDouble(6);

            var position = new BondEvent()
            {
              Figi = figi,
              EventDate = eventdate,
              EventType = eventtype,
              Execution = execution,
              OperationType = operationtype,
              Note = note,
              PayValue = payvalue
            };
            positions.Add(position);
          }
        }
        

        return positions;
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Ошибка: {ex.Message}");
        throw new Exception(ex.Message);
      }
    }


    public static void CleanPortfolio()
        {
            try
            {
                using var conn = new NpgsqlConnection(CommonVariables.ConnectionString);
                conn.Open();

                var cmdStr = "delete from positions";

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

    public static async Task<int> CleanPortfolioAsync()
    {
      try
      {
        using var conn = new NpgsqlConnection(CommonVariables.ConnectionString);
        await conn.OpenAsync();

        var cmdStr = "delete from positions";

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

    public static void UpdatePositionsOfferDate()
        {
            try
            {
                using var conn = new NpgsqlConnection(CommonVariables.ConnectionString);
                conn.Open();

                var cmdStr = "UPDATE public.positions SET SellDate = GetBondsOfferDate(figi) where GetBondsOfferDate(figi) is not null";

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

    public static async Task<int> UpdatePositionsOfferDateAsync()
    {
      try
      {
        using var conn = new NpgsqlConnection(CommonVariables.ConnectionString);
        await conn.OpenAsync();

        var cmdStr = "UPDATE public.positions SET SellDate = GetBondsOfferDate(figi) where GetBondsOfferDate(figi) is not null";

        await using var cmd = new NpgsqlCommand(cmdStr, conn);
        var res = await cmd.ExecuteNonQueryAsync();
        return (int)res;
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
        return -1;
      }
    }

    public static void Write(Position position)
        {
            try
            {
                using var conn = new NpgsqlConnection(CommonVariables.ConnectionString);
                conn.Open();

                var cmdStr = "INSERT INTO positions (figi, isin, name, selldate) " +
                    "VALUES (@figi, @isin, @name, @dt)";

                using (var cmd = new NpgsqlCommand(cmdStr, conn))
                {
                    cmd.Parameters.AddWithValue("figi", position.Figi);
                    cmd.Parameters.AddWithValue("isin", position.Isin);
                    cmd.Parameters.AddWithValue("name", position.Name);
                    cmd.Parameters.AddWithValue("dt", position.SellDate);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    public static async Task<int> WriteAsync(Position position)
    {
      try
      {
        using var conn = new NpgsqlConnection(CommonVariables.ConnectionString);
        await conn.OpenAsync();

        var cmdStr = "INSERT INTO positions (figi, isin, name, selldate) " +
            "VALUES (@figi, @isin, @name, @dt)";

        await using var cmd = new NpgsqlCommand(cmdStr, conn);
        
        cmd.Parameters.AddWithValue("figi", position.Figi);
        cmd.Parameters.AddWithValue("isin", position.Isin);
        cmd.Parameters.AddWithValue("name", position.Name);
        cmd.Parameters.AddWithValue("dt", position.SellDate);

        var result = await cmd.ExecuteNonQueryAsync();
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
