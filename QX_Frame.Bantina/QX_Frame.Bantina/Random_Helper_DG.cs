/*********************************************************
 * CopyRight: QIXIAO CODE BUILDER. 
 * Version:4.2.0
 * Author:qixiao(柒小)
 * Create:2017-09-13 14:20:25
 * Update:2017-09-13 14:20:25
 * E-mail: dong@qixiao.me | wd8622088@foxmail.com 
 * GitHub: https://github.com/dong666 
 * Personal web site: http://qixiao.me 
 * Technical WebSit: http://www.cnblogs.com/qixiaoyizhan/ 
 * Description:
 * Thx , Best Regards ~
 *********************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QX_Frame.Bantina
{
    public abstract class Random_Helper_DG
    {
        private static readonly char[] NUMBER_SOURCE = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        private static readonly char[] CHARACTOR_SOURCE =
      {
        '0','1','2','3','4','5','6','7','8','9',
        'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',
        'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'
      };

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
        public static string GetRandomStringBy62Source(int length)
        {
            System.Text.StringBuilder newRandom = new System.Text.StringBuilder(62);
            Random rd = new Random((int)DateTime.Now.Ticks);
            for (int i = 0; i < length; i++)
            {
                newRandom.Append(CHARACTOR_SOURCE[rd.Next(62)]);
            }
            return newRandom.ToString();
        }

        public static string GetRandomNumber(int length)
        {
            System.Text.StringBuilder newRandom = new System.Text.StringBuilder(10);
            Random rd = new Random((int)DateTime.Now.Ticks);
            for (int i = 0; i < length; i++)
            {
                newRandom.Append(CHARACTOR_SOURCE[rd.Next(10)]);
            }
            return newRandom.ToString();
        }

    }
}
