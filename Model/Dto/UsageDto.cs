namespace MobileProviderAPI.Model.Dto
{
    public class UsageDto
    {
        public string SubscriberNo { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public string UsageType { get; set; }
        public int Amount { get; set; }
    }
}