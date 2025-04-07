using invest_api.Enum;

namespace invest_api.SQL
{
    public class Deal
    {
        public string Figi { get; set; }
        public string Isin { get; set; }
        public string Name { get; set; }
        public DateTime DealDate { get; set; }
        public EDealType DealType { get; set; }
        public double Sum { get; set; }
        public int Count { get; set; }
    }
}
