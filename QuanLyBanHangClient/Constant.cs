using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;

namespace QuanLyBanHangClient
{
    class Constant
    {
        public const string MS_ERROR_NETWORK = "Có lỗi xảy ra trong quá trình kết nối, xin kiểm tra lại đường truyền của bạn";
        public const string MS_ERROR_SOMETHING = "Có lỗi xảy ra trong quá trình thực hiện, xin kiểm tra lại thông tin của bạn";
        public const string MS_CHECK_INFO_AGAIN = "Kiểm tra lại và điền đầy đủ thông tin";
        public const int ID_CREATE_NEW = -1;
    }
    class UtilFuction {
        public static double GetTime(DateTime dateTime) {
            return dateTime.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
        }
        public static string formatMoney(decimal money) {
            return string.Format("{0:#,0.0}", money);
        }
        public static void WriteToBinaryFile<T>(string filePath, T objectToWrite, bool append = false) {
            using (Stream stream = File.Open(filePath, append ? FileMode.Append : FileMode.Create)) {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                binaryFormatter.Serialize(stream, objectToWrite);
            }
        }
        public static T ReadFromBinaryFile<T>(string filePath) {
            using (Stream stream = File.Open(filePath, FileMode.Open)) {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                return (T)binaryFormatter.Deserialize(stream);
            }
        }
        public static void WriteToXmlFile<T>(string filePath, T objectToWrite, bool append = false) where T : new() {
            TextWriter writer = null;
            try {
                var serializer = new XmlSerializer(typeof(T));
                writer = new StreamWriter(filePath, append);
                serializer.Serialize(writer, objectToWrite);
            } finally {
                if (writer != null)
                    writer.Close();
            }
        }
        public static T ReadFromXmlFile<T>(string filePath) where T : new() {
            TextReader reader = null;
            try {
                var serializer = new XmlSerializer(typeof(T));
                reader = new StreamReader(filePath);
                return (T)serializer.Deserialize(reader);
            } finally {
                if (reader != null)
                    reader.Close();
            }
        }
        public static byte[] ImageToBinary(Image image) {
            using (MemoryStream ms = new MemoryStream()) {
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.GetBuffer();
            }
        }
        public static Image ByteToImage(byte[] imageAsByte) {
            return (Bitmap)((new ImageConverter()).ConvertFrom(imageAsByte));
        }
        public static byte[] ResizeImage(byte[] imageAsByte, int width, int height) {
            Image image = ByteToImage(imageAsByte);
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage)) {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes()) {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return ImageToBinary(destImage);
        }
        public static BitmapSource imageToBitmapSource(Image image) {
            var bitmap = new Bitmap(image);
            IntPtr bmpPt = bitmap.GetHbitmap();
            BitmapSource bitmapSource =
             System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                   bmpPt,
                   IntPtr.Zero,
                   Int32Rect.Empty,
                   BitmapSizeOptions.FromEmptyOptions());

            //freeze bitmapSource and clear memory to avoid memory leaks
            bitmapSource.Freeze();
            //DeleteObject(bmpPt);

            return bitmapSource;
        }
        public static string Utf8ToUtf16(string utf8String) {
            // Get UTF-8 bytes by reading each byte with ANSI encoding
            byte[] utf8Bytes = Encoding.Default.GetBytes(utf8String);

            // Convert UTF-8 bytes to UTF-16 bytes
            byte[] utf16Bytes = Encoding.Convert(Encoding.UTF8, Encoding.Unicode, utf8Bytes);

            // Return UTF-16 bytes as UTF-16 string
            return Encoding.Unicode.GetString(utf16Bytes);
        }
        public static string DecodeFromUtf8(string utf8String) {
            // copy the string as UTF-8 bytes.
            byte[] utf8Bytes = new byte[utf8String.Length];
            for (int i = 0; i < utf8String.Length; ++i) {
                //Debug.Assert( 0 <= utf8String[i] && utf8String[i] <= 255, "the char must be in byte's range");
                utf8Bytes[i] = (byte)utf8String[i];
            }

            return Encoding.UTF8.GetString(utf8Bytes, 0, utf8Bytes.Length);
        }
        public static string getSpacesFromQuantityChar(int totalChar, string currentStr) {
            if(totalChar <= currentStr.Length) {
                return "";
            }
            string rs = "";
            for(int i = currentStr.Length; i < totalChar; i++) {
                rs += " ";
            }
            return rs;
        }
    }
    public class ComboData {
        public int Id { get; set; }
        public string Value { get; set; }
    }
}
