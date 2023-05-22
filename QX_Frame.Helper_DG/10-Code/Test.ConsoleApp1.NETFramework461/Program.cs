using QX_Frame.Helper_DG;
using QX_Frame.Helper_DG.Extends;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Test.ConsoleApp1.NETFramework461.Config;
using QX_Frame.Helper_DG.Service;
using QX_Frame.Helper_DG.Log;
using System.Threading;
using ServiceStack.Redis;
using System.Linq.Expressions;
using System.Reflection;
using QX_Frame.Helper_DG.Bantina;
using System.Data;

namespace Test.ConsoleApp1.NETFramework461
{
    class Program
    {
        static void Main(string[] args)
        {
            //new ConfigBootStrap();//BootStrap

            //---------

            //TB_People people = new TB_People ();
            //people.Uid = Guid.NewGuid();
            //people.Name = "555";
            //people.Age = 22;
            //people.ClassId = 3;

            using (DB_QX_Frame_Test test = new DB_QX_Frame_Test())
            {
                string sql = "select * from TB_People";
                List<TB_People> peopleList = test.ExecuteSqlToList<TB_People>(sql, null);
                foreach (var item in peopleList)
                {
                    Console.WriteLine($"uid = {item.Uid} , Name = {item.Name}");
                }
            }


            //string str1 = "csacsacascsa";
            //string str2 = "1233";

            //using (DB_QX_Frame_Test test = new DB_QX_Frame_Test())
            //{
            //    //List<TB_People> peopleList = test.QueryEntitiesPaging<TB_People, string>(1, 5, t => t.Name, t => t.Name.StartsWith("li"), out int count, true);
            //    List<TB_People> peopleList = test.QueryEntities<TB_People>();
            //    Console.WriteLine("Query DataCount - > " + peopleList.Count);
            //    foreach (var item in peopleList)
            //    {
            //        Console.WriteLine($"uid = {item.Uid} , Name = {item.Name} , ClassName = {item.TB_ClassName.ClassName}");
            //    }
            //}

            //using (DB_QX_Frame_Test test = new DB_QX_Frame_Test())
            //{
            //    List<TB_People> peopleList = test.QueryEntitiesPaging<TB_People, string>(1, 2, t => t.Name, t => t.Age == 3, out int count, true);
            //    foreach (var item in peopleList)
            //    {
            //        Console.WriteLine($"uid = {item.Uid} , Name = {item.Name} , ClassName = {item.TB_ClassName.ClassName}");
            //    }
            //}

            //#region QueryAll ExecuteTime

            //Console.WriteLine($"QueryList Spend Time - Bantina 1: " + DateTime_Helper_DG.CodeExecuteTimeCaculate(() =>
            // {
            //     using (DB_QX_Frame_Test test = new DB_QX_Frame_Test())
            //     {
            //         List<TB_People> peopleList = test.QueryEntities<TB_People>(t=>t.Name.Contains("2"));
            //         Console.WriteLine("Query DataCount - > " + peopleList.Count);
            //     }
            // }));

            //Console.WriteLine($"QueryList Spend Time - Bantina 2: " + DateTime_Helper_DG.CodeExecuteTimeCaculate(() =>
            //{
            //    using (DB_QX_Frame_Test test = new DB_QX_Frame_Test())
            //    {
            //        List<TB_People> peopleList = test.QueryEntities<TB_People>();
            //        Console.WriteLine("Query DataCount - > " + peopleList.Count);
            //    }
            //}));

            //Console.WriteLine($"QueryList Spend Time - Bantina 3: " + DateTime_Helper_DG.CodeExecuteTimeCaculate(() =>
            //{
            //    using (DB_QX_Frame_Test test = new DB_QX_Frame_Test())
            //    {
            //        List<TB_People> peopleList = test.QueryEntities<TB_People>();
            //        Console.WriteLine("Query DataCount - > " + peopleList.Count);
            //    }
            //}));

            //#endregion

            //#region QueryCondition ExecuteTime

            //Console.WriteLine($"QueryList Spend Time - Bantina 1: " + DateTime_Helper_DG.CodeExecuteTimeCaculate(() =>
            //{
            //    using (DB_QX_Frame_Test test = new DB_QX_Frame_Test())
            //    {
            //        List<TB_People> peopleList = test.QueryEntities<TB_People>(t=>t.Name.Contains("1"));
            //        Console.WriteLine("Query DataCount - > " + peopleList.Count);
            //    }
            //}));

            //Console.WriteLine($"QueryList Spend Time - Bantina 2: " + DateTime_Helper_DG.CodeExecuteTimeCaculate(() =>
            //{
            //    using (DB_QX_Frame_Test test = new DB_QX_Frame_Test())
            //    {
            //        List<TB_People> peopleList = test.QueryEntities<TB_People>(t => t.Name.Contains("2"));
            //        Console.WriteLine("Query DataCount - > " + peopleList.Count);
            //    }
            //}));

            //Console.WriteLine($"QueryList Spend Time - Bantina 3: " + DateTime_Helper_DG.CodeExecuteTimeCaculate(() =>
            //{
            //    using (DB_QX_Frame_Test test = new DB_QX_Frame_Test())
            //    {
            //        List<TB_People> peopleList = test.QueryEntities<TB_People>(t => t.Name.Contains("3"));
            //        Console.WriteLine("Query DataCount - > " + peopleList.Count);
            //    }
            //}));

            //#endregion









            // --------------
            Console.WriteLine("any key to exit ...");
            Console.ReadKey();
        }
    }


    public class DB_QX_Frame_Test : Bantina
    {
        public DB_QX_Frame_Test() : base("data source=.;initial catalog=DB_QX_Frame_Test;persist security info=True;user id=Sa;password=Sa123456;MultipleActiveResultSets=True;App=EntityFramework") { }
    }

    [Table(TableName = "TB_People")]
    public class TB_People
    {
        [Key]
        public Guid Uid { get; set; }
        [Column]
        public string Name { get; set; }
        [Column]
        public int Age { get; set; }
        [Column]
        [ForeignKey]
        public int ClassId { get; set; }
        //[ForeignTable]
        //public TB_ClassName TB_ClassName { get; set; }
    }

    [Table(TableName = "TB_ClassName")]
    public class TB_ClassName
    {
        // PK（identity）  
        [Key]
        public Int32 ClassId { get; set; }
        //
        [Column]
        public String ClassName { get; set; }
    }
}