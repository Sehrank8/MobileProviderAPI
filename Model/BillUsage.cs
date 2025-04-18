using System.ComponentModel.DataAnnotations;

namespace MobileProviderAPI.Model
{
    public class BillUsage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string SubscriberNo { get; set; }

        [Range(1, 12)]
        public int Month { get; set; }

        public int Year { get; set; }

        [Required]
        public string UsageType { get; set; } // "Phone" or "Internet"

        public int Amount { get; set; } // minutes or MB

        public bool IsPaid { get; set; }
    }
}