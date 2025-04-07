export class BondsReportRequest {

    public Currency: string = "";
    public CloseDate:  Date | null = null;
    public Profit: number = 0;
    public LagDay: number = 0;

    constructor(currency: string, closeDate: Date, profit: number, lagDay: number ) {
        this.Currency = currency;
        this.CloseDate = closeDate;
        this.Profit = profit;
        this.LagDay = lagDay;
    }
}