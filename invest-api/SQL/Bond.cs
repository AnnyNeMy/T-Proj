namespace invest_api.SQL
{
    public class Bond
    {
        public string Figi { get; set; }

        public string Isin { get; set; }

        public string Name { get; set; }

        public string Currency { get; set; }

        public double NominalPrice { get; set; }

        public double LastPrice { get; set; }

        public DateTime MaturityDate { get; set; }

        public int CouponeCount { get; set; }

        public double Coupone { get; set; }

        public int IsQual { get; set; }
    }
}
