using Autofac;
using QX_Frame.App.Base;
using QX_Frame.Data.Contract;
using QX_Frame.Data.Entities;
using QX_Frame.Data.QueryObject;
using QX_Frame.Data.Service;
using QX_Frame.Helper_DG_Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QX_Frame.App.Base.AOP;
using QX_Frame.Data.Service.QX_Frame;
using QX_Frame.Data.Entities.QX_Frame;
using System.Data;
using QX_Frame.App.Base.options;
using System.Data.SqlClient;

namespace QX_Frame.ConsoleApp
{
    class Program : AppBase
    {
        static void Main(string[] args)
        {
            new ClassRegisters();   //register classes


            using (var fact = Wcf<UserAccountService>())
            {
                var channel = fact.CreateChannel();

                int count = 0;
                //List<tb_userAccount> userAccountList = channel.QueryAll(new UserAccountQueryObject()).Cast<List<tb_userAccount>>(out count);

                //foreach (var item in userAccountList)
                //{
                //    Console.WriteLine(item.loginId);
                //}

                //for (int i = 0; i < 100; i++)
                //{
                //    tb_userAccount userAccount = tb_userAccount.Build();
                //    userAccount.uid = Guid.NewGuid();
                //    userAccount.loginId = "qixiao" + i.ToString("D3");
                //    userAccount.pwd = "qx" + i.ToString("D3");

                //    bool isSave = channel.Add(userAccount);
                //    Console.WriteLine($"-> {i} item saved ! the result = {isSave}");
                //}

                //List<tb_userAccount> userAccountList = channel.QuerySql(new UserAccountQueryObject() { SqlStatementTextOrSpName = "select * from tb_userAccount order by loginId", SqlExecuteType=ExecuteType.Execute_List_T}).Cast<List<tb_userAccount>>(out count);

                int result = channel.QuerySql(new UserAccountQueryObject() {
                    SqlStatementTextOrSpName = "insert into tb_userAccount values(@uid,@loginId,@pwd)",
                    SqlParameters = new SqlParameter[] {
                        new SqlParameter("@uid", Guid.NewGuid()),
                        new SqlParameter("@loginId","123123"),
                        new SqlParameter("@pwd","123456")
                    },
                    SqlExecuteType = ExecuteType.ExecuteNonQuery }).Cast<dynamic>();

                //foreach (var item in userAccountList)
                //{
                //    Console.WriteLine(item.loginId);
                //}
                Console.WriteLine(result);
                //Console.WriteLine("\ncount = " + count);
            }



            Console.WriteLine("any key to exit ...");
            Console.ReadKey();
        }
    }
}
