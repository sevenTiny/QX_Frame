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
using System.Net.Mail;
using System.Data.Entity;

namespace QX_Frame.ConsoleApp
{
    class Program : AppBase
    {
        static void Main(string[] args)
        {
            #region Wcf Test
            new ClassRegisters();   //register classes

            //using (var fact = Wcf<UserAccountService>())
            //{
            //    var channel = fact.CreateChannel();

            //    tb_UserAccount userAccount = tb_UserAccount.Build();
            //    userAccount.uid = Guid.NewGuid();
            //    userAccount.loginId = "222";
            //    userAccount.pwd = "111";
            //    Console.WriteLine(channel.Add(userAccount));
            //}

            #endregion

            Console.WriteLine("any key to exit ...");
            Console.ReadKey();
        }

    }
}
