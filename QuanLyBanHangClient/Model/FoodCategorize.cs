using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanHangAPI.model.SQL
{
    public class FoodCategorize
    {
        public int FoodCategorizeId { get; set; }
        public String Name { get; set; }

        public FoodCategorize()
        {
        }

        public FoodCategorize(int foodCategorizeId)
        {
            this.FoodCategorizeId = foodCategorizeId;
        }
    }
}
