using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp_FlowchartToCode_DG.CodeCreate
{
    class QX_FrameToRESTWebApiController
    {
        public static string CreateCode(Dictionary<string, dynamic> CreateCodeDic)
        {
            string usings = CreateCodeDic["Using"];                                             //Using
            string[] usingsArray = usings.Split(';');
            string NameSpace = CreateCodeDic["NameSpace"];                                      //NameSpace
            string NameSpaceCommonPlus = CreateCodeDic["NameSpaceCommonPlus"];                  //NameSpaceCommonPlus
            string TableName = CreateCodeDic["TableName"];                                      //TableName
            string ClassName = CreateCodeDic["Class"];                                          //ClassName
            string ClassNamePlus = CreateCodeDic["ClassNamePlus"];                              //ClassName Plus
            string ClassNameExtends = CreateCodeDic["ClassExtends"];                             //ClassExtends
            if (!string.IsNullOrEmpty(ClassNameExtends))
            {
                ClassNameExtends = ":" + ClassNameExtends;
            }
            string ClassNameAndExtends = ClassName + ClassNamePlus + ClassNameExtends;          //Class whole name
            List<string> FeildName = CreateCodeDic["FeildName"];                                //表字段名称
            List<string> FeildType = CreateCodeDic["FeildType"];                                //表字段类型
            List<string> FeildLength = CreateCodeDic["FeildLength"];                            //表字段长度
            List<string> FeildIsNullable = CreateCodeDic["FeildIsNullable"];                    //表字段可空
            List<string> FeildDescription = CreateCodeDic["FeildDescription"];                  //表字段说明
            List<string> FeildIsPK = CreateCodeDic["FeildIsPK"];                                //表字段是否主键
            List<string> FeildIsIdentity = CreateCodeDic["FeildIsIdentity"];                    //表字段是否自增


            StringBuilder str = new StringBuilder();

            //foreach (var item in usingsArray)
            //{
            //    if (usingsArray.Last().Equals(item))
            //    {
            //        break;
            //    }
            //    str.Append($"{item};\r\n");
            //}

            #region 版权信息
            //版权信息
            str.Append(Info.CopyRight);
            str.Append("\r\n");
            #endregion

            //添加using
            str.Append($"{usings}\r\n");
            str.Append("\r\n");//引用结束换行

            //添加命名空间
            str.Append($"namespace QX_Frame.WebApi.Controllers\r\n");
            str.Append("{" + "\r\n");

            //添加实体类
            str.Append("\t" + "/// <summary>" + "\r\n");
            str.Append("\t" + $"///class {ClassName}{ClassNamePlus}\r\n");
            str.Append("\t" + "/// </summary>" + "\r\n");
            str.Append("\t" + $"public class {ClassNameAndExtends}\r\n");
            str.Append("\t" + "{" + "\r\n");

            //添加 Get Method
            str.Append("\t\t" + $"// GET: api/{ClassName}" + "\r\n");
            str.Append("\t\t" + "public IHttpActionResult Get(int pageIndex,int pageSize,bool isDesc)\r\n");
            str.Append("\t\t" + "{" + "\r\n");
            str.Append("\t\t\t" + "throw new Exception_DG_Internationalization(9999);" + "\r\n");
            str.Append("\t\t" + "}" + "\r\n");
            str.Append("\r\n");

            //添加 Get Method
            str.Append("\t\t" + $"// GET: api/{ClassName}/id" + "\r\n");
            str.Append("\t\t" + "public IHttpActionResult Get(string id)\r\n");
            str.Append("\t\t" + "{" + "\r\n");
            str.Append("\t\t\t" + "throw new Exception_DG_Internationalization(9999);" + "\r\n");
            str.Append("\t\t" + "}" + "\r\n");
            str.Append("\r\n");

            //添加 Post Method
            str.Append("\t\t" + $"// POST: api/{ClassName}" + "\r\n");
            str.Append("\t\t" + "public IHttpActionResult Post([FromBody]dynamic query)\r\n");
            str.Append("\t\t" + "{" + "\r\n");
            str.Append("\t\t\t" + "throw new Exception_DG_Internationalization(9999);" + "\r\n");
            str.Append("\t\t" + "}" + "\r\n");
            str.Append("\r\n");

            //添加 Put Method
            str.Append("\t\t" + $"// PUT: api/{ClassName}" + "\r\n");
            str.Append("\t\t" + "public IHttpActionResult Put([FromBody]dynamic query)\r\n");
            str.Append("\t\t" + "{" + "\r\n");
            str.Append("\t\t\t" + "throw new Exception_DG_Internationalization(9999);" + "\r\n");
            str.Append("\t\t" + "}" + "\r\n");
            str.Append("\r\n");

            //添加 Delete Method
            str.Append("\t\t" + $"// DELETE: api/{ClassName}" + "\r\n");
            str.Append("\t\t" + "public IHttpActionResult Delete([FromBody]dynamic query)\r\n");
            str.Append("\t\t" + "{" + "\r\n");
            str.Append("\t\t\t" + "throw new Exception_DG_Internationalization(9999);" + "\r\n");
            str.Append("\t\t" + "}" + "\r\n");


            str.Append("\t" + "}" + "\r\n");//public class }
            str.Append("}" + "\r\n");//namespace class }

            return str.ToString();
        }
    }
}
