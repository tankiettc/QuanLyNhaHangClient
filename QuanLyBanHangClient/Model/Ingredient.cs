using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanHangAPI.model
{
    public class Ingredient
    {
        public int IngredientId { get; set; }
        public String Name { get; set; }
        public int UnitId { get; set; }
        public virtual Unit Unit { get; set; }
        public int? ImageId { get; set; }
        public Ingredient()
        {

        }

        public Ingredient(int ingredientId)
        {
            IngredientId = ingredientId;
        }

        public override string ToString()
        {
            return $"{nameof(IngredientId)}: {IngredientId}, {nameof(Name)}: {Name}, {nameof(UnitId)}: {UnitId}, {nameof(Unit)}: {Unit}";
        }

    }
}
