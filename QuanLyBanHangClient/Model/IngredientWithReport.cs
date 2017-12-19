using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanHangAPI.model.SQL
{
    public class IngredientWithReport 
    {
        public int IngredientWithReportId { get; set; }

        public string DailyReportId { get; set; }
        public virtual DailyReport DailyReport { get; set; }
        public int IngredientId { get; set; }
        public virtual Ingredient Ingredient { get; set; }
        public float StorageQuantities { get; set; }
        public float EstimateQuantitiesUse { get; set; }
        public float EstimateQuantitiesLeft { get; set; }
        public float RealQuantitiesLeft { get; set; }

        public IngredientWithReport()
        {
            StorageQuantities = 0;
            EstimateQuantitiesUse = 0;
            EstimateQuantitiesLeft = 0;
            RealQuantitiesLeft = 0;
        }

        public override string ToString()
        {
            return $"{nameof(IngredientWithReportId)}: {IngredientWithReportId}, {nameof(DailyReportId)}: {DailyReportId}, {nameof(DailyReport)}: {DailyReport}, {nameof(IngredientId)}: {IngredientId}, {nameof(Ingredient)}: {Ingredient}, {nameof(StorageQuantities)}: {StorageQuantities}, {nameof(EstimateQuantitiesUse)}: {EstimateQuantitiesUse}, {nameof(EstimateQuantitiesLeft)}: {EstimateQuantitiesLeft}, {nameof(RealQuantitiesLeft)}: {RealQuantitiesLeft}";
        }
    }
}
