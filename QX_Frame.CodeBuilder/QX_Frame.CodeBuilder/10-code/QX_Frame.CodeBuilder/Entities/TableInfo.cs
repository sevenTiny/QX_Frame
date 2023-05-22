/*********************************************************
 * CopyRight: QIXIAO CODE BUILDER. 
 * Version:5.0.0
 * Author:qixiao(柒小)
 * Create:2017-10-01 17:03:26
 * Update:2017-10-01 17:03:26
 * E-mail: dong@qixiao.me | wd8622088@foxmail.com 
 * GitHub: https://github.com/dong666 
 * Personal web site: http://qixiao.me 
 * Technical WebSit: http://www.cnblogs.com/qixiaoyizhan/ 
 * Description:
 * Thx , Best Regards ~
 *********************************************************/

using System.Collections.Generic;
using System.Data;

namespace CSharp_FlowchartToCode_DG.Entities
{
    public class TableInfo
    {
        public string TableName { get; set; }
        public List<FieldInfo> FieldInfos { get; set; }
        public DataTable FieldInfosTable { get; set; }
    }
}
