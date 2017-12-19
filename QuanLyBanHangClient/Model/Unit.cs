using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanHangAPI.model
{
    public class Unit
    {
        public int UnitId { get; set; }
        public String Name { get; set; }
        public Unit()
        {

        }

        public Unit(int unitId)
        {
            UnitId = unitId;
        }

        public override string ToString()
        {
            return $"{nameof(UnitId)}: {UnitId}, {nameof(Name)}: {Name}";
        }
    }
}
