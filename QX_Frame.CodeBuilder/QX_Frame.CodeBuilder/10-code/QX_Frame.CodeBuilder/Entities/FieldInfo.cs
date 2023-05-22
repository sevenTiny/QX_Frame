/*********************************************************
 * CopyRight: QIXIAO CODE BUILDER. 
 * Version:5.0.0
 * Author:qixiao(柒小)
 * Create:2017-10-01 17:29:15
 * Update:2017-10-01 17:29:15
 * E-mail: dong@qixiao.me | wd8622088@foxmail.com 
 * GitHub: https://github.com/dong666 
 * Personal web site: http://qixiao.me 
 * Technical WebSit: http://www.cnblogs.com/qixiaoyizhan/ 
 * Description:
 * Thx , Best Regards ~
 *********************************************************/

namespace CSharp_FlowchartToCode_DG.Entities
{
    public class FieldInfo
    {
        public string Field { get; set; }
        public string FieldType { get; set; }
        public int Length { get; set; }
        public int Nullable { get; set; }
        public string Description { get; set; }
        public int IsPK { get; set; }
        public int IsIdentity { get; set; }
    }
}
