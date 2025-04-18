namespace MobileProviderAPI.Model.Dto
{
    public class BillResultDetailedDto
    {
        public string SubscriberNo { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public double Total { get; set; }
        public double Remaining { get; set; }
        public bool IsPaid { get; set; }
        public double PhoneAmount { get; set; }
        public double InternetAmount { get; set; }
        public List<UsageTypeDto> Details { get; set; }
    }
}