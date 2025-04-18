namespace MobileProviderAPI.Model.Dto
{
    public class UsageTypeDto
    {
        public string UsageType { get; set; } // "Phone" or "Internet"
        public int Amount { get; set; } // minutes or MB
        public string Description { get; set; }
    }
}
