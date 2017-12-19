using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanHangAPI.model
{
    public class PrepareFoodArchive
    {
        public String Id { get; set; }
        public int FoodId { get; set; }
        public String FoodName { get; set; }
        public int OrderId { get; set; }
        public int TableId { get; set; }
        public long CreatedTime { get; set; }
        public String Note { get; set; }

        public PrepareFoodArchive()
        {
            
        }
    }
}
