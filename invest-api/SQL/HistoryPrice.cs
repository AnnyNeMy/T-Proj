namespace invest_api.SQL
{
    public class HistoryPrice
    {
        public string Figi { get; set; }
        public string Isin { get; set; }
        public DateTime Date { get; set; }
        public double Price { get; set; }
    }
}
