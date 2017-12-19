using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanHangAPI.model.SQL
{
    public class Image
    {
        private byte[] imageBytes;

        public Image(byte[] imageBytes)
        {
            this.imageBytes = imageBytes;
        }

        public int ImageId { get; set; }
        public byte[] Binary { get; set; }

        public static Image Create(byte[] randomImageByte)
        {
            return new Image(randomImageByte);
        }
    }
}
