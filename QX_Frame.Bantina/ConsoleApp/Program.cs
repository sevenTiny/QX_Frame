using QX_Frame.Bantina;
using QX_Frame.Bantina.Bankinate;
using QX_Frame.Bantina.BankinateAuto;
using QX_Frame.Bantina.Extends;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Test.ConsoleApp1.NETFramework461.Config;
using QX_Frame.Bantina.Validation;
using System.Data.Common;
using System.Data.SqlClient;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            new ConfigBootStrap();

            //Dictionary<string, object> dataDic = new Dictionary<string, object>();
            //dataDic.Add("classid", "1");

            //using (var db = new DB_QX_Frame_Test())
            //{
            //    //if (db.Insert("TB_People", dataDic))
            //    //{
            //    //    Console.WriteLine(db.Message);
            //    //}
            //    //else
            //    //{
            //    //    Console.WriteLine(db.Message);
            //    //}
            //    List<TB_People> peoples = db.QueryEntities<TB_People>(t=>t.Name.Contains("123"));
            //    Console.WriteLine(peoples.Count);
            //}
            //List<TB_People> peopleList = Db_Helper_DG.ExecuteList<TB_People>("select * from TB_People where ClassId=@ClassId", System.Data.CommandType.Text, new Dictionary<string, object> { {"ClassId", 1} });
            //foreach (var item in peopleList)
            //{
            //    Console.WriteLine(item.Name);
            //}

            Console.WriteLine("any key to exit ...");
            Console.ReadKey();
        }
    }
    public class DB_QX_Frame_Test : Bankinate
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
        public int ClassId { get; set; }
        [ForeignTable(ForeignKeyFieldName = "ClassId")]
        public TB_ClassName TB_ClassName { get; set; }
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

    public static class PredicateExtensions
    {
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return Compose(first, second, Expression.AndAlso);
        }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return Compose(first, second, Expression.OrElse);
        }

        private static Expression<T> Compose<T>(this Expression<T> first, Expression<T> second, Func<Expression, Expression, Expression> merge)
        {
            // build parameter map (from parameters of second to parameters of first)
            var map = first.Parameters.Select((f, i) => new { f, s = second.Parameters[i] }).ToDictionary(p => p.s, p => p.f);

            // replace parameters in the second lambda expression with parameters from the first
            var secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);

            // apply composition of lambda expression bodies to parameters from the first expression 
            return Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);
        }

        partial class ParameterRebinder : ExpressionVisitor
        {
            private readonly Dictionary<ParameterExpression, ParameterExpression> map;

            public ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
            {
                this.map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
            }

            public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression exp)
            {
                return new ParameterRebinder(map).Visit(exp);
            }

            protected override Expression VisitParameter(ParameterExpression p)
            {
                ParameterExpression replacement;
                if (map.TryGetValue(p, out replacement))
                {
                    p = replacement;
                }
                return base.VisitParameter(p);
            }
        }
    }
}
