using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanHangAPI.model
{
    public class User
    {
        public String Username { get; set; }
        public String password { get; set; }
        public int RoleOrder { get; set; }
        public int RoleFood { get; set; }
        public int RoleTable { get; set; }
        public int RoleIngredient { get; set; }
        public int RolePrepareFood { get; set; }
        public User()
        {
            RoleOrder = 0;
            RoleFood = 0;
            RoleTable = 0;
            RoleIngredient = 0;
            RolePrepareFood = 0;
        }
        public override string ToString()
        {
            return $"{nameof(Username)}: {Username}, {nameof(password)}: {password}, {nameof(RoleOrder)}: {RoleOrder}, {nameof(RoleFood)}: {RoleFood}, {nameof(RoleTable)}: {RoleTable}, {nameof(RoleIngredient)}: {RoleIngredient}, {nameof(RolePrepareFood)}: {RolePrepareFood}";
        }
    }
}
