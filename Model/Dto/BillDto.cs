namespace MobileProviderAPI.Model.Dto
{
    public class BillDto
    {
        public string SubscriberNo { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public double TotalPaid { get; set; }
        public double RemainingPayment { get; set; }
    }
}
