using System;
using System.IO;

namespace QX_Frame.Helper_DG
{
    //author:qixiao
    //time:2017-1-6 17:11:40
    public abstract class IO_Helper_DG
    {
        public static bool CopyFile(string sourceFilePath,string newFilePath,bool allowCoverSameNameFiles=true)
        {
            File.Copy(sourceFilePath,newFilePath, allowCoverSameNameFiles);//允许覆盖同名文件
            return true;
        }
        //create Directory
        public static bool CreateDirectoryIfNotExist(string filePath)
        {
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            return true;
        }

        public static string DeskTopPath
        {
            get { return Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory); }
        }

        public static void OpenDirectory(string directoryPath)
        {
            System.Diagnostics.Process.Start("Explorer.exe", directoryPath);
        }
    }
}
