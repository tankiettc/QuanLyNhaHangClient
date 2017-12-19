using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanHangAPI.model.SQL
{
    public class ImportBill
    {
        public int ImportBillId { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual ICollection<IngredientWithImportBill> IngredientWithImportBills { get; set; }

        public decimal BillMoney { get; set; }
        public String Note { get; set; }

        public ImportBill()
        {
        }

        public ImportBill(int importBillId)
        {
            ImportBillId = importBillId;
        }

        public override string ToString()
        {
            return $"{nameof(ImportBillId)}: {ImportBillId}, {nameof(CreatedDate)}: {CreatedDate}, {nameof(IngredientWithImportBills)}: {IngredientWithImportBills}, {nameof(BillMoney)}: {BillMoney}, {nameof(Note)}: {Note}";
        }
    }
}
