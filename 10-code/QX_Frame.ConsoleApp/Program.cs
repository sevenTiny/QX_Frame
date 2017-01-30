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

namespace QX_Frame.ConsoleApp
{
    class Program : AppBase
    {

        static void Main(string[] args)
        {
            DateTime timesStart = DateTime.Now;

            UserAccountQueryObject query = new UserAccountQueryObject();

            AppBase.Register(c => new UserAccountService());

            using (var fact = Wcf<UserAccountService>())
            {
                var channel = fact.CreateChannel();
                List<tb_userAccount> list = channel.QueryAll(query).Cast<List<tb_userAccount>>();
                foreach (var item in list)
                {
                    Console.WriteLine(item.loginId);
                }
            }

            DateTime timeEnd = DateTime.Now;
            TimeSpan span = timeEnd.Subtract(timesStart);
            Console.WriteLine($"time total use {span.TotalMilliseconds}");

            string a = "123";
            int aa = a.ToInt32();

            Console.WriteLine("any key to exit ...");
            Console.ReadKey();
        }
        
    }
}
