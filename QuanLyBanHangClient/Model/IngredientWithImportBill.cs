using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanHangAPI.model.SQL
{
    public class IngredientWithImportBill
    {

        public int IngredientWithImportBillId { get; set; }
        public int ImportBillId { get; set; }
        public virtual ImportBill ImportBill { get; set; }
        public int IngredientId { get; set; }
        public virtual Ingredient Ingredient { get; set; }
        public float Quantities { get; set; }
        public decimal SinglePricePerUnit { get; set; }
        public decimal TotalPrice { get; set; }
        public IngredientWithImportBill()
        {

        }

        public override string ToString()
        {
            return $"{nameof(IngredientWithImportBillId)}: {IngredientWithImportBillId}, {nameof(ImportBillId)}: {ImportBillId}, {nameof(ImportBill)}: {ImportBill}, {nameof(IngredientId)}: {IngredientId}, {nameof(Ingredient)}: {Ingredient}, {nameof(Quantities)}: {Quantities}, {nameof(SinglePricePerUnit)}: {SinglePricePerUnit}, {nameof(TotalPrice)}: {TotalPrice}";
        }
    }
}
