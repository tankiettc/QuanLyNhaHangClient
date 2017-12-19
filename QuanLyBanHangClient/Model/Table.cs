using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanHangAPI.model
{
    public class Table
    {
        public int TableId { get; set; }
        public Table()
        {

        }
        public Table(int tableId)
        {
            TableId = tableId;
        }

        public override string ToString()
        {
            return $"{nameof(TableId)}: {TableId}";
        }

        public static Table Create(int tableNumber)
        {
            return new Table(tableNumber);
        }
    }
}
