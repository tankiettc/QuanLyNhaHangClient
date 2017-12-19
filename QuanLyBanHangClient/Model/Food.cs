using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyBanHangAPI.model.SQL;

namespace QuanLyBanHangAPI.model
{
    public class Food
    {
        public int FoodId { get; set; }
        public Food()
        {

        }
        public String Name { get; set; }
        public decimal Price { get; set; }
        public int? ImageId { get; set; }
        public int FoodCategorizeId { get; set; }
        public FoodCategorize FoodCategorize { get; set; }

        public Food(int foodId)
        {
            FoodId = foodId;
        }
        public virtual ICollection<IngredientWithFood> IngredientWithFoods { get; set; }

        public override string ToString()
        {
            return $"{nameof(FoodId)}: {FoodId}, {nameof(Name)}: {Name}, {nameof(Price)}: {Price}, {nameof(IngredientWithFoods)}: {IngredientWithFoods}";
        }

        
    }
}
