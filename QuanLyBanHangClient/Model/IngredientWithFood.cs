using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanHangAPI.model
{
    public class IngredientWithFood
    {

        public int IngredientWithFoodId { get; set; }
        public int FoodId { get; set; }
//        public Food Food { get; set; }
        public int IngredientId { get; set; }
//        public Ingredient Ingredient { get; set; }
        public float Quantities { get; set; }
        public IngredientWithFood()
        {

        }

        public IngredientWithFood(int ingredientWithFoodId)
        {
            IngredientWithFoodId = ingredientWithFoodId;
        }

        public override string ToString()
        {
            return $"{nameof(IngredientWithFoodId)}: {IngredientWithFoodId}, {nameof(FoodId)}: {FoodId}, {nameof(IngredientId)}: {IngredientId}, {nameof(Quantities)}: {Quantities}";
        }
    }
}
