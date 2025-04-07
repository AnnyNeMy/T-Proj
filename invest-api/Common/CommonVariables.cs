namespace invest_api.Common
{
    public static class CommonVariables
    {
      public const string ConnectionString = "Host=localhost;Username=postgres;Password= ;Database=Bonds";
      public const string Token = "";
      public const string BondName = "bond";


      public const string FigiUsdRub = "USD000UTSTOM";
      public const string FigiEurRub = "EUR000UTSTOM";

      public const string USD = "usd";
      public const string EUR = "eur";
      public const string RUB = "rub";

      public const string MoneyFormat = "F3";

      public const string DateFormat = "dd-MM-yyyy";
      public static readonly DateTime StartDate = new DateTime(2025, 1, 1, 10, 5, 1, 1, DateTimeKind.Utc);
  }
}
