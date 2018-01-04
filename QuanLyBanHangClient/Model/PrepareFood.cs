using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanHangAPI.model
{
    public class PrepareFood {
        public int PrepareFoodId { get; set; }
        public int OrderId { get; set; }
        public int TableId { get; set; }
        public int FoodId { get; set; }
        public Food Food { get; set; }
        public int PrepareStateId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public String Note { get; set; }

        public PrepareFood()
        {
            
        }
    }
}
