using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyBanHangAPI.model.SQL;

namespace QuanLyBanHangAPI.model
{
    public class DailyReport
    {
        public string DailyReportId { get; set; }
        public decimal MoneySpentForImportIngredient { get; set; }
        public decimal TotalMoneySpent => MoneySpentForImportIngredient ;
        public decimal TotalBillMoney { get; set; }
        public decimal TotalMoneyReceive => TotalBillMoney;
        public decimal Profit => TotalMoneyReceive - TotalMoneySpent;
        public string Note;
        public virtual ICollection<IngredientWithReport> IngredientWithReports { get; set; }

        public void SetDailyReportId(DateTime reportDate)
        {
            DailyReportId = reportDate.ToShortDateString();
        }
        protected DailyReport()
        {
           
        }
        public override string ToString()
        {
            return $"{nameof(Note)}: {Note}, {nameof(DailyReportId)}: {DailyReportId}, {nameof(MoneySpentForImportIngredient)}: {MoneySpentForImportIngredient}, {nameof(TotalMoneySpent)}: {TotalMoneySpent}, {nameof(TotalBillMoney)}: {TotalBillMoney}, {nameof(TotalMoneyReceive)}: {TotalMoneyReceive}, {nameof(Profit)}: {Profit}, {nameof(IngredientWithReports)}: {IngredientWithReports}";
        }
    }
}
