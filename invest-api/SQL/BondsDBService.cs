using invest_api.Common;
using invest_api.Contracts;
using Npgsql;
using NpgsqlTypes;
using System.Data;

namespace invest_api.SQL
{
    public static class BondsDBService
    {

    public static Bond GetBondByIsin(string isin)
    {
      try
      {
        var bond = new Bond();

        using var conn = new NpgsqlConnection(CommonVariables.ConnectionString);
        conn.Open();

        var cmdStr = "select Figi, Isin, Name, Currency, NominalPrice, LastPrice, MaturityDate, CouponeCount, Coupone, IsQual from public.bond where Isin = @isin;";

        using (var cmd = new NpgsqlCommand(cmdStr, conn))
        {
          cmd.Parameters.AddWithValue("isin", isin);

          var reader = cmd.ExecuteReader();

          int count = 0;

          while (reader.Read())
          {
            if (count > 0)
            {
              throw new Exception($"{nameof(GetBondByIsin)} bonds count > 0");
            }

            count++;

            var figi = reader.GetString(0);
            var _isin = reader.GetString(1);
            var name = reader.GetString(2);
            var currency = reader.GetString(3);
            var nominalPrice = reader.GetDouble(4);
            var lastPrice = reader.GetDouble(5);
            var maturityDate = reader.GetDateTime(6);
            var couponeCount = reader.GetInt32(7);

            double? coupone = !reader.IsDBNull(8) ? reader.GetFieldValue<double>(8) : new double?();

            var isQual = reader.GetInt32(9);

            bond = new Bond()
            {
              Figi = figi,
              Isin = isin,
              Name = name,
              Currency = currency,
              NominalPrice = nominalPrice,
              LastPrice = lastPrice,
              MaturityDate = maturityDate,
              CouponeCount = couponeCount,
              Coupone = coupone ?? 0,
              IsQual = isQual
            };
          }
        }

        return bond;
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message);
      }
    }

    public static async Task<Bond> GetBondByIsinAsync(string isin)
    {
      try
      {
        var bond = new Bond();

        using var conn = new NpgsqlConnection(CommonVariables.ConnectionString);
        await conn.OpenAsync();

        var cmdStr = "select Figi, Isin, Name, Currency, NominalPrice, LastPrice, MaturityDate, CouponeCount, Coupone, IsQual from public.bond where Isin = @isin;";

        using (var cmd = new NpgsqlCommand(cmdStr, conn))
        {
          cmd.Parameters.AddWithValue("isin", isin);

          var reader = await cmd.ExecuteReaderAsync();

          int count = 0;

          while (await reader.ReadAsync())
          {
            if (count > 0)
            {
              throw new Exception($"{nameof(GetBondByIsin)} bonds count > 0");
            }

            count++;

            var figi = reader.GetString(0);
            var _isin = reader.GetString(1);
            var name = reader.GetString(2);
            var currency = reader.GetString(3);
            var nominalPrice = reader.GetDouble(4);
            var lastPrice = reader.GetDouble(5);
            var maturityDate = reader.GetDateTime(6);
            var couponeCount = reader.GetInt32(7);

            double? coupone = !reader.IsDBNull(8) ? reader.GetFieldValue<double>(8) : new double?();

            var isQual = reader.GetInt32(9);

            bond = new Bond()
            {
              Figi = figi,
              Isin = isin,
              Name = name,
              Currency = currency,
              NominalPrice = nominalPrice,
              LastPrice = lastPrice,
              MaturityDate = maturityDate,
              CouponeCount = couponeCount,
              Coupone = coupone ?? 0,
              IsQual = isQual
            };
          }
        }

        return bond;
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message);
      }
    }


    public static List<BondsReport> GetBondReport(BondsReportRequest request)
        {
          if (request == null)
          {
            throw new ArgumentNullException(nameof(request));
          }

          try
            {
                var result = new List<BondsReport>();

                using var conn = new NpgsqlConnection(CommonVariables.ConnectionString);
                conn.Open();

                var cmdStr = "select * from AnaliticBond(:currency, :date, :profit, :lagday);";

                using (var cmd = new NpgsqlCommand(cmdStr, conn))
                {
                    cmd.Parameters.Add(new NpgsqlParameter("currency", NpgsqlDbType.Varchar));
                    cmd.Parameters.Add(new NpgsqlParameter("date", NpgsqlDbType.Date));
                    cmd.Parameters.Add(new NpgsqlParameter("profit", NpgsqlDbType.Real));
                    cmd.Parameters.Add(new NpgsqlParameter("lagday", NpgsqlDbType.Integer));

                    cmd.Parameters[0].Value = request.Currency;
                    cmd.Parameters[1].Value = request.CloseDate;
                    cmd.Parameters[2].Value = request.Profit;
                    cmd.Parameters[3].Value = request.LagDay;

                    var dtResults = new DataTable();
                    var da = new NpgsqlDataAdapter(cmd);
                    da.Fill(dtResults);

                    foreach (DataRow row in dtResults.Rows)
                    {
                        var report = new BondsReport()
                        {
                            Isin = row[0]?.ToString(),
                            Name = row[1]?.ToString(),
                            Currency = row[2]?.ToString(),
                            CloseDate = row[3]?.ToString(),
                            LastPrice = row[4]?.ToString(),
                            Profit = row[5]?.ToString(),
                            Volatily = row[6]?.ToString(),
                            Change = row[7]?.ToString(),
                        };

                        result.Add(report);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    public static async Task<List<BondsReport>> GetBondReportAsync(BondsReportRequest request)
    {
      if (request == null)
      {
        throw new ArgumentNullException(nameof(request));
      }

      try
      {
        var result = new List<BondsReport>();

        using var conn = new NpgsqlConnection(CommonVariables.ConnectionString);
        await conn.OpenAsync();

        var cmdStr = "select * from AnaliticBond(:currency, :date, :profit, :lagday);";

        using (var cmd = new NpgsqlCommand(cmdStr, conn))
        {
          cmd.Parameters.Add(new NpgsqlParameter("currency", NpgsqlDbType.Varchar));
          cmd.Parameters.Add(new NpgsqlParameter("date", NpgsqlDbType.Date));
          cmd.Parameters.Add(new NpgsqlParameter("profit", NpgsqlDbType.Real));
          cmd.Parameters.Add(new NpgsqlParameter("lagday", NpgsqlDbType.Integer));

          cmd.Parameters[0].Value = request.Currency.ToLower();
          cmd.Parameters[1].Value = request.CloseDate;
          cmd.Parameters[2].Value = request.Profit;
          cmd.Parameters[3].Value = request.LagDay;

          var dtResults = new DataTable();
          var da = new NpgsqlDataAdapter(cmd);
          da.Fill(dtResults);

          foreach (DataRow row in dtResults.Rows)
          {
            var report = new BondsReport()
            {
              Isin = row[0]?.ToString(),
              Name = row[1]?.ToString(),
              Currency = row[2]?.ToString(),
              CloseDate = row[3]?.ToString(),
              LastPrice = row[4]?.ToString(),
              Profit = row[5]?.ToString(),
              Volatily = row[6]?.ToString(),
              Change = row[7]?.ToString(),
            };

            result.Add(report);
          }
        }

        return result;
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message);
      }
    }


    public static List<Bond> GetBonds()
        {
            try
            {
                var bonds = new List<Bond>();

                using var conn = new NpgsqlConnection(CommonVariables.ConnectionString);
                conn.Open();

                var cmdStr = "select figi, isin, name, currency, nominalprice, lastprice, maturitydate, couponecount, coupone, isqual from public.bond";

                using (var cmd = new NpgsqlCommand(cmdStr, conn))
                {
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var figi = reader.GetString(0);
                        var isin = reader.GetString(1);
                        var name = reader.GetString(2);
                        var currency = reader.GetString(3);
                        var nominalPrice = reader.GetDouble(4);
                        var lastPrice = reader.GetDouble(5);
                        var maturityDate = reader.GetDateTime(6);
                        var couponeCount = reader.GetInt32(7);

                        //var couponeStr = reader.GetString(8);
                        //double coupone = 0; 

                        //if (!String.IsNullOrEmpty(couponeStr))
                        //{
                        //    coupone = Double.Parse(couponeStr);
                        //}

                        //var coupone = reader.GetDouble(8);

                        double? coupone = !reader.IsDBNull(8) ? reader.GetFieldValue<double>(8) : new double?();

                        var isQual = reader.GetInt32(9);

                        var bond = new Bond()
                        {
                            Figi = figi,
                            Isin = isin,
                            Name = name,
                            Currency = currency,
                            NominalPrice = nominalPrice,
                            LastPrice = lastPrice,
                            MaturityDate = maturityDate,
                            CouponeCount = couponeCount,
                            Coupone = coupone ?? 0,
                            IsQual = isQual
                        };

                        bonds.Add(bond);
                    }
                }

                return bonds;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    public static async Task<List<Bond>> GetBondsAsync()
    {
      try
      {
        var bonds = new List<Bond>();

        using var conn = new NpgsqlConnection(CommonVariables.ConnectionString);
        await conn.OpenAsync();

        var cmdStr = "select figi, isin, name, currency, nominalprice, lastprice, maturitydate, couponecount, coupone, isqual from public.bond";

        using (var cmd = new NpgsqlCommand(cmdStr, conn))
        {
          var reader = await cmd.ExecuteReaderAsync();

          while (await reader.ReadAsync())
          {
            var figi = reader.GetString(0);
            var isin = reader.GetString(1);
            var name = reader.GetString(2);
            var currency = reader.GetString(3);
            var nominalPrice = reader.GetDouble(4);
            var lastPrice = reader.GetDouble(5);
            var maturityDate = reader.GetDateTime(6);
            var couponeCount = reader.GetInt32(7);

            //var couponeStr = reader.GetString(8);
            //double coupone = 0; 

            //if (!String.IsNullOrEmpty(couponeStr))
            //{
            //    coupone = Double.Parse(couponeStr);
            //}

            //var coupone = reader.GetDouble(8);

            double? coupone = !reader.IsDBNull(8) ? reader.GetFieldValue<double>(8) : new double?();

            var isQual = reader.GetInt32(9);

            var bond = new Bond()
            {
              Figi = figi,
              Isin = isin,
              Name = name,
              Currency = currency,
              NominalPrice = nominalPrice,
              LastPrice = lastPrice,
              MaturityDate = maturityDate,
              CouponeCount = couponeCount,
              Coupone = coupone ?? 0,
              IsQual = isQual
            };

            bonds.Add(bond);
          }
        }

        return bonds;
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message);
      }
    }


    public static void UpdateBondCouponePay()
        {
            try
            {
                using var conn = new NpgsqlConnection(CommonVariables.ConnectionString);
                conn.Open();

                var cmdStr = "UPDATE public.bond SET coupone = GetBondsCouponePay(figi) ";

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

    public static async Task<int> UpdateBondCouponePayAsync()  
    {
      try
      {
        using var conn = new NpgsqlConnection(CommonVariables.ConnectionString);
        await conn.OpenAsync();

        var cmdStr = "UPDATE public.bond SET coupone = GetBondsCouponePay(figi) ";

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

    public static void UpdateBondOfferDate()
        {
            try
            {
                using var conn = new NpgsqlConnection(CommonVariables.ConnectionString);
                conn.Open();

                var cmdStr = "UPDATE public.bond SET MaturityDate = GetBondsOfferDate(figi) where GetBondsOfferDate(figi) is not null";

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

    public static async Task<int> UpdateBondOfferDateAsync()
    {
      try
      {
        using var conn = new NpgsqlConnection(CommonVariables.ConnectionString);
        await conn.OpenAsync();

        var cmdStr = "UPDATE public.bond SET MaturityDate = GetBondsOfferDate(figi) where GetBondsOfferDate(figi) is not null";

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

    public static void CleanBond()
        {
            try
            {
                using var conn = new NpgsqlConnection(CommonVariables.ConnectionString);
                conn.Open();

                var cmdStr = "delete from bond";

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

    public static async Task<CommonResponse> CleanBondAsync()
    {
      try
      {
        using var conn = new NpgsqlConnection(CommonVariables.ConnectionString);
        await conn.OpenAsync();

        var cmdStr = "delete from bond";

        await using var cmd = new NpgsqlCommand(cmdStr, conn);
        
        var res = await cmd.ExecuteNonQueryAsync();

        var rsp = res != null && res != 0 && res != -1 ? new CommonResponse(Enum.ECommonStatus.Ok) : new CommonResponse(Enum.ECommonStatus.Error, res.ToString());
        return rsp;

      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
        return new CommonResponse(Enum.ECommonStatus.Error, ex.Message);
      }
    }

    public static void CleanBondEvents()
        {
            try
            {
                using var conn = new NpgsqlConnection(CommonVariables.ConnectionString);
                conn.Open();

                var cmdStr = "delete from bondevent";

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

    public static async Task<CommonResponse> CleanBondEventsAsync()
    {
      try
      {
        using var conn = new NpgsqlConnection(CommonVariables.ConnectionString);
        await conn.OpenAsync();

        var cmdStr = "DELETE FROM bondevent";
        await using var cmd = new NpgsqlCommand(cmdStr, conn);
        int result = await cmd.ExecuteNonQueryAsync();
        var rsp = result != null && result != 0 && result != -1 ? new CommonResponse(Enum.ECommonStatus.Ok) : new CommonResponse(Enum.ECommonStatus.Error, result.ToString());
        return rsp;
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
        return new CommonResponse(Enum.ECommonStatus.Error, ex.Message);
      }
    }

    public static void Write(Bond bond)
        {
            try
            {
                using var conn = new NpgsqlConnection(CommonVariables.ConnectionString);
                conn.Open();

                var cmdStr = "INSERT INTO bond (figi, isin, name, currency, nominalprice, lastprice, maturitydate, couponecount, coupone, isqual) " +
                    "VALUES (@figi, @isin, @name, @currency, @nominalprice, @lastprice, @dt, @couponecount, @coupone, @isqual)";

                using (var cmd = new NpgsqlCommand(cmdStr, conn))
                {
                    cmd.Parameters.AddWithValue("figi", bond.Figi);
                    cmd.Parameters.AddWithValue("isin", bond.Isin);
                    cmd.Parameters.AddWithValue("name", bond.Name);
                    cmd.Parameters.AddWithValue("currency", bond.Currency);
                    cmd.Parameters.AddWithValue("nominalprice", bond.NominalPrice);
                    cmd.Parameters.AddWithValue("lastprice", bond.LastPrice);
                    cmd.Parameters.AddWithValue("dt", bond.MaturityDate);
                    cmd.Parameters.AddWithValue("couponecount", bond.CouponeCount);
                    cmd.Parameters.AddWithValue("coupone", bond.Coupone);
                    cmd.Parameters.AddWithValue("isqual", bond.IsQual);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void Write(BondEvent bondEvent)
        {
            try
            {
                using var conn = new NpgsqlConnection(CommonVariables.ConnectionString);
                conn.Open();

                var cmdtxt = "INSERT INTO bondevent (figi, eventDate, eventType, execution, operationType, note, payvalue) VALUES (@figi, @eventDate, @eventType, @execution, @operationType, @note, @payvalue)";

                using (var cmd = new NpgsqlCommand(cmdtxt, conn))
                {
                    cmd.Parameters.AddWithValue("figi", bondEvent.Figi);
                    cmd.Parameters.AddWithValue("eventDate", bondEvent.EventDate);
                    cmd.Parameters.AddWithValue("eventType", bondEvent.EventType);
                    cmd.Parameters.AddWithValue("execution", bondEvent.Execution);
                    cmd.Parameters.AddWithValue("operationType", bondEvent.OperationType);
                    cmd.Parameters.AddWithValue("note", bondEvent.Note);

                    cmd.Parameters.AddWithValue("payvalue", bondEvent.PayValue??0);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{bondEvent.Figi} {bondEvent.EventType} {ex.Message}");
            }
        }

    public static async Task<int> WriteAsync(Bond bond)
    {
      try
      {
        using var conn = new NpgsqlConnection(CommonVariables.ConnectionString);
        await conn.OpenAsync();

        var cmdStr = "INSERT INTO bond (figi, isin, name, currency, nominalprice, lastprice, maturitydate, couponecount, coupone, isqual) " +
            "VALUES (@figi, @isin, @name, @currency, @nominalprice, @lastprice, @dt, @couponecount, @coupone, @isqual)";
        await using var cmd = new NpgsqlCommand(cmdStr, conn);
        cmd.Parameters.AddWithValue("figi", bond.Figi);
        cmd.Parameters.AddWithValue("isin", bond.Isin);
        cmd.Parameters.AddWithValue("name", bond.Name);
        cmd.Parameters.AddWithValue("currency", bond.Currency);
        cmd.Parameters.AddWithValue("nominalprice", bond.NominalPrice);
        cmd.Parameters.AddWithValue("lastprice", bond.LastPrice);
        cmd.Parameters.AddWithValue("dt", bond.MaturityDate);
        cmd.Parameters.AddWithValue("couponecount", bond.CouponeCount);
        cmd.Parameters.AddWithValue("coupone", bond.Coupone);
        cmd.Parameters.AddWithValue("isqual", bond.IsQual);

        var result = await cmd.ExecuteNonQueryAsync();
        return result;
     
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
        return -1;
      }
    }

    public static async Task<int> WriteAsync(BondEvent bondEvent)
    {
      try
      {
        using var conn = new NpgsqlConnection(CommonVariables.ConnectionString);
        await conn.OpenAsync();

        var cmdtxt = "INSERT INTO bondevent (figi, eventDate, eventType, execution, operationType, note, payvalue) VALUES (@figi, @eventDate, @eventType, @execution, @operationType, @note, @payvalue)";

        await using var cmd = new NpgsqlCommand(cmdtxt, conn);
        cmd.Parameters.AddWithValue("figi", bondEvent.Figi);
        cmd.Parameters.AddWithValue("eventDate", bondEvent.EventDate);
        cmd.Parameters.AddWithValue("eventType", bondEvent.EventType);
        cmd.Parameters.AddWithValue("execution", bondEvent.Execution);
        cmd.Parameters.AddWithValue("operationType", bondEvent.OperationType);
        cmd.Parameters.AddWithValue("note", bondEvent.Note);

        cmd.Parameters.AddWithValue("payvalue", bondEvent.PayValue ?? 0);
        var result = await cmd.ExecuteNonQueryAsync();
        return (int)result; 
       
      }
      catch (Exception ex)
      {
        Console.WriteLine($"{bondEvent.Figi} {bondEvent.EventType} {ex.Message}");
        return -1;
      }
    }
  }
}
