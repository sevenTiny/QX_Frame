using System.IO;

namespace QX_Frame.Helper_DG_Framework
{
    //author:qixiao
    //time:2017-1-6 17:11:40
    public abstract class IO_Helper_DG
    {
        public static bool Copy(string sourceFilePath,string newFilePath,bool allowCoverSameNameFiles=true)
        {
            File.Copy(sourceFilePath,newFilePath, allowCoverSameNameFiles);//允许覆盖同名文件
            return true;
        }
    }
}
