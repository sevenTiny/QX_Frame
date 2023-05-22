/*********************************************************
 * CopyRight: QIXIAO CODE BUILDER. 
 * Version:5.0.0
 * Author:qixiao(柒小)
 * Create:2017-10-01 17:14:36
 * Update:2017-10-01 17:14:36
 * E-mail: dong@qixiao.me | wd8622088@foxmail.com 
 * GitHub: https://github.com/dong666 
 * Personal web site: http://qixiao.me 
 * Technical WebSit: http://www.cnblogs.com/qixiaoyizhan/ 
 * Description:
 * Thx , Best Regards ~
 *********************************************************/
using System.Collections.Generic;

namespace CSharp_FlowchartToCode_DG.Entities
{
    public class DataBaseInfo
    {
        public string DataBaseName { get; set; }
        public List<TableInfo> Tables { get; set; }
    }
}
