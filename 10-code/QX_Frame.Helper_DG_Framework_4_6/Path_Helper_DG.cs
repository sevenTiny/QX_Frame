using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QX_Frame.Helper_DG_Framework
{
    //auth:qixiao
    //time:2017-1-6 17:14:26
    public abstract class Path_Helper_DG
    {
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
        public static void  OpenDirectory(string directoryPath)
        {
            System.Diagnostics.Process.Start("Explorer.exe", directoryPath);
        }
    }
}
