using System;
using System.IO;

namespace QX_Frame.Helper_DG
{
    //author:qixiao
    //time:2017-1-6 16:59:05
    public abstract class Txt_Helper_DG
    {
        public static Boolean Write(string filePath, string text, bool isAppend)
        {
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            using (StreamWriter log = new StreamWriter(filePath, isAppend))
            {
                log.Write(text);
             }
            return true;
        }
        public static Boolean WriteLine(string filePath, string text, bool isAppend)
        {
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            using (StreamWriter log = new StreamWriter(filePath, isAppend))
            {
                log.WriteLine(text);
            }
            return true;
        }
    }
}
