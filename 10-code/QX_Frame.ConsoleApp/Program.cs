using Autofac;
using QX_Frame.App.Base;
using QX_Frame.Data.Contract;
using QX_Frame.Data.Entities;
using QX_Frame.Data.QueryObject;
using QX_Frame.Data.Service;
using QX_Frame.Helper_DG;
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
using QX_Frame.App.Base.Options;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Data.Entity;

namespace QX_Frame.ConsoleApp
{
    class Program : AppBase
    {
        static void Main(string[] args)
        {
            #region Wcf Test
            new Config.ClassRegisters();   //register classes
            new Config.ConfigBootStrap();

            using (var fact = Wcf<UserAccountService>())
            {
                var channel = fact.CreateChannel();

                // List<tb_UserAccount> userAccountList = channel.QueryAll(new tb_UserAccountQueryObject()).Cast<List<tb_UserAccount>>();
                //List<tb_UserAccount> userAccountList = channel.QuerySql(
                //    new tb_UserAccountQueryObject
                //    {
                //        SqlStatementTextOrSpName = "select * from tb_UserAccount",
                //        SqlExecuteType = ExecuteType.Execute_List_T
                //    }).Cast<List<tb_UserAccount>>();

                //foreach (var item in userAccountList)
                //{
                //    Console.WriteLine(item.loginId);
                //}


                Console.WriteLine(Internationalization.GetString("MSG_1001"));

            }

            #endregion


            Console.WriteLine("any key to exit ...");
            Console.ReadKey();
        }

    }
}
