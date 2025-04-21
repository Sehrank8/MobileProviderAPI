using System;
using System.Collections.Generic;
using MobileProviderAPI.Context;
using MobileProviderAPI.Model;


namespace MobileProviderAPI.Data.Db
{
    public class BillingAccess
    {
        private readonly BillingContext _context;

        public BillingAccess(BillingContext context)
        {
            _context = context;
        }

        public void AddUsage(BillUsage usage)
        {
            _context.Usages.Add(usage);
            _context.SaveChanges();
        }

        public IEnumerable<BillUsage> GetUsages(string subscriberNo, int month, int year)
        {
            return _context.Usages
                .Where(u => u.SubscriberNo == subscriberNo && u.Month == month && u.Year == year);
        }

        public void MarkAsPaid(string subscriberNo, int month, int year)
        {
            var items = GetUsages(subscriberNo, month, year);
            foreach (var item in items)
            {
                item.IsPaid = true;
            }

            _context.SaveChanges();
        }
        public Bill GetBill(string subscriberNo, int month, int year)
        {
            return _context.Bills.FirstOrDefault(b => b.SubscriberNo == subscriberNo && b.Month == month && b.Year == year);
        }

        public void AddOrUpdateBill(Bill bill)
        {
            var existing = GetBill(bill.SubscriberNo, bill.Month, bill.Year);
            if (existing != null)
            {
                existing.TotalPaid = bill.TotalPaid;
                existing.RemainingPayment = bill.RemainingPayment;
            }
            else
            {
                _context.Bills.Add(bill);
            }
            _context.SaveChanges();
        }
    }
}
