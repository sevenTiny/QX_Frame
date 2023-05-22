using System;
using System.Text;

namespace QX_Frame.Helper_DG
{
    /*2016-11-2 15:13:28 author:qixiao*/
    public abstract class String_Helper_DG
    {
        /// <summary>
        /// get Random String
        /// </summary>
        /// <param name="length">string length</param>
        /// <returns></returns>
        public static string Get_RandomString(int length)
        {
            string str = string.Empty;
            long num2 = DateTime.Now.Ticks;
            Random random = new Random(((int)(((ulong)num2) & 0xffffffffL)) | ((int)(num2 >> 1)));
            for (int i = 0; i < length; i++)
            {
                char ch;
                int num = random.Next();
                if ((num % 2) == 0)
                {
                    ch = (char)(0x30 + ((ushort)(num % 10)));
                }
                else
                {
                    ch = (char)(0x41 + ((ushort)(num % 0x1a)));
                }
                str = str + ch.ToString();
            }
            return str;
        }

        /// <summary>
        /// get_String_By_Stream
        /// </summary>
        /// <param name="inputStream">inputStream</param>
        /// <param name="encoding">encoding="UTF-8"</param>
        /// <returns></returns>
        public static string get_String_By_Stream(System.IO.Stream inputStream, string encoding = "UTF-8")
        {
            int count = 0;
            int byteRead = 0;
            StringBuilder strBuilder = new StringBuilder();
            {
                byte[] buffer = new byte[1024];
                byteRead = inputStream.Read(buffer, count, buffer.Length);
                count += byteRead;
                strBuilder.Append(Encoding.GetEncoding(encoding).GetString(buffer));
            } while (byteRead > 0) ;
            return strBuilder.ToString();
        }

        /// <summary>
        /// get_String_By_Stream
        /// </summary>
        /// <param name="inputStream">inputStream</param>
        /// <param name="count">out count has read</param>
        /// <param name="encoding">encoding="UTF-8"</param>
        /// <returns></returns>
        public static string get_String_By_Stream(System.IO.Stream inputStream, out int count, string encoding = "UTF-8")
        {
            count = 0;
            int byteRead = 0;
            StringBuilder strBuilder = new StringBuilder();
            {
                byte[] buffer = new byte[1024];
                byteRead = inputStream.Read(buffer, count, buffer.Length);
                count += byteRead;
                strBuilder.Append(Encoding.GetEncoding(encoding).GetString(buffer));
            } while (byteRead > 0) ;
            return strBuilder.ToString();
        }
    }
}
