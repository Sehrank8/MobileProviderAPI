using MobileProviderAPI.Data.Db;
using MobileProviderAPI.Model.Dto;
using MobileProviderAPI.Model;
using System.Collections.Generic;

namespace MobileProviderAPI.Data.Svc
{
    public class BillingService : IBillingService
    {
        private readonly BillingAccess _access;

        public BillingService(BillingAccess access)
        {
            _access = access;
        }

        public string AddUsage(UsageDto dto)
        {
            var usage = new BillUsage
            {
                SubscriberNo = dto.SubscriberNo,
                Month = dto.Month,
                Year = dto.Year,
                UsageType = dto.UsageType.ToUpper(),
                Amount = dto.Amount,
                IsPaid = false
            };

            _access.AddUsage(usage);
            return "Usage Added.";
        }

        public string CalculateBill(string subscriberNo, int month, int year)
        {
            var usages = _access.GetUsages(subscriberNo, month, year).Where(u => !u.IsPaid);
            if (usages == null)
                return null;

            var total = CalculateTotal(usages);

            var existingBill = _access.GetBill(subscriberNo, month, year);
            // if no bill exists creates a new one
            if (existingBill == null)
            {
                var newBill = new Bill
                {
                    SubscriberNo = subscriberNo,
                    Month = month,
                    Year = year,
                    TotalPaid = 0,
                    RemainingPayment = total
                };
                _access.AddOrUpdateBill(newBill);
            }
            else
            {
                //                  Usage	Total Paid  Remaining
                //First Calculation   0MB   $50   $0    $50
                //Payment Made		        $50   $50   $0
                //Add 10GB more   10240MB   $60   $50   $10
                //Issue encountered
                existingBill.TotalPaid = CalculateTotal(_access.GetUsages(subscriberNo, month, year).Where(u => u.IsPaid));
                existingBill.RemainingPayment = total;
                _access.AddOrUpdateBill(existingBill);
            }

            return "Bill Calculated";
        }


        public BillResultDetailedDto QueryBillDetailed(string subscriberNo, int month, int year, int page, int pageSize)
        {
            var bill = _access.GetBill(subscriberNo, month, year);
            if (bill == null)
                return null;
            var all = _access.GetUsages(subscriberNo, month, year).ToList();
            var pageItems = all.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return new BillResultDetailedDto
            {
                SubscriberNo = subscriberNo,
                Month = month,
                Year = year,
                PhoneAmount = CalculatePhone(pageItems),
                InternetAmount = CalculateInternet(pageItems),
                Total = CalculateTotal(all),
                Remaining = bill?.RemainingPayment ?? 0,
                IsPaid = bill?.RemainingPayment == 0,
                Details = pageItems.Select(u => new UsageTypeDto
                {
                    UsageType = u.UsageType,
                    Amount = u.Amount,
                    Description = $"Used {u.Amount} {(u.UsageType == "PHONE" ? "minutes" : "MB")}"
                }).ToList()
            };
        }

        public BillResultDto QueryBill(string subscriberNo, int month, int year)
        {
            var bill = _access.GetBill(subscriberNo, month, year);
            if (bill == null)
                return null;
            return new BillResultDto
            {
                TotalRemaining = bill?.RemainingPayment ?? 0,
                IsPaid = bill?.RemainingPayment == 0,
            };
        }

        public string PayBill(string subscriberNo, int month, int year)
        {
            _access.MarkAsPaid(subscriberNo, month, year);

            var bill = _access.GetBill(subscriberNo, month, year);

            if (bill == null)
            {
                return "Bill not found.";
            }

            bill.TotalPaid = bill.RemainingPayment;
            bill.RemainingPayment = 0;

            _access.AddOrUpdateBill(bill);

            return "Payment completed.";
        }


        private double CalculateTotal(IEnumerable<BillUsage> usages)
        {
            return (CalculateInternet(usages) + CalculatePhone(usages));
        }
        private double CalculateInternet(IEnumerable<BillUsage> usages)
        {
            int total = usages.Where(u => u.UsageType == "INTERNET").Sum(u => u.Amount);

            if (total > 20480)
                return Math.Round(50 + ((total - 20480) / 10240.0) * 10,2); // up to 20 gb free then 10 dollars every 10 gbs
            return 50; // 50 dollars even if usage is 0, mobile providers are evil indeed
        
        }
        private double CalculatePhone(IEnumerable<BillUsage> usages)
        {
            int total = usages.Where(u => u.UsageType == "PHONE").Sum(u => u.Amount);
            if (total > 1000)
                return Math.Round(((total - 1000) / 1000.0) * 10,2); // first 1000 mins is free then 10 dollars per 1000 mins
            return 0; // if mins < 1000 free
        }
    }
}
