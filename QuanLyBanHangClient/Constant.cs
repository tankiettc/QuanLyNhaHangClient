using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

    }
    public class ComboData {
        public int Id { get; set; }
        public string Value { get; set; }
    }
}
