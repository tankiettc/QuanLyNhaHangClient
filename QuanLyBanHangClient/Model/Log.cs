using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanHangClient.Model {
    public class Log {
        public int LogId { get; set; }
        public String Message { get; set; }
        public String RequestUsername { get; set; }
        public int LogTypeId { get; set; }
        public String JsonObject { get; set; }
        public DateTime CreatedDateTime { get; set; }

        public Log() {

        }
    }
}
