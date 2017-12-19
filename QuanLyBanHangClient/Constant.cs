using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanHangClient
{
    class Constant
    {
        public const string MS_ERROR_NETWORK = "Có lỗi xảy ra trong quá trình kết nối, xin kiểm tra lại đường truyền của bạn";
        public const string MS_ERROR_SOMETHING = "Có lỗi xảy ra trong quá trình thực hiện, xin kiểm tra lại thông tin của bạn";
        public const string MS_CHECK_INFO_AGAIN = "Kiểm tra lại và điền đầy đủ thông tin";
        public const int ID_CREATE_NEW = -1;
    }
    public class ComboData {
        public int Id { get; set; }
        public string Value { get; set; }
    }
}
