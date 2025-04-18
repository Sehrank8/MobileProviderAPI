using System.ComponentModel.DataAnnotations;

namespace MobileProviderAPI.Model
{
    public class Bill
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string SubscriberNo { get; set; }

        [Range(1, 12)]
        public int Month { get; set; }

        [Required]
        public int Year { get; set; }

        public double TotalPaid { get; set; }

        public double RemainingPayment { get; set; }
    }
}
