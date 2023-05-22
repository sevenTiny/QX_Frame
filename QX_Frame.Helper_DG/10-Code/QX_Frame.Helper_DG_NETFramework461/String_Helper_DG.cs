using System;
using System.Text;

/**
 * author:qixiao
 * create:2016-11-2 15:13:28
 * update:2017-5-15 15:05:34
 * */
namespace QX_Frame.Helper_DG
{
    public abstract class String_Helper_DG
    {
        /// <summary>
        /// get Random String
        /// </summary>
        /// <param name="length">string length</param>
        /// <returns></returns>
        public static string GetRandomString(int length)
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
        /// get String By Stream
        /// </summary>
        /// <param name="inputStream">inputStream</param>
        /// <param name="encoding">encoding="UTF-8"</param>
        /// <returns></returns>
        public static string GetStringByStream(System.IO.Stream inputStream, string encoding = "UTF-8")
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
        /// Get String By Stream
        /// </summary>
        /// <param name="inputStream">inputStream</param>
        /// <param name="count">out count has read</param>
        /// <param name="encoding">encoding="UTF-8"</param>
        /// <returns></returns>
        public static string GetStringByStream(System.IO.Stream inputStream, out int count, string encoding = "UTF-8")
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
