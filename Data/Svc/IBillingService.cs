using MobileProviderAPI.Model.Dto;

namespace MobileProviderAPI.Data.Svc
{
    public interface IBillingService
    {
        string AddUsage(UsageDto dto);
        string CalculateBill(string subscriberNo, int month, int year);
        BillResultDetailedDto QueryBillDetailed(string subscriberNo, int month, int year, int page, int pageSize);
        BillResultDto QueryBill(string subscriberNo, int month, int year);
        string PayBill(string subscriberNo, int month, int year);
    }
}