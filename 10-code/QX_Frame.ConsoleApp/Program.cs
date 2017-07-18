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

            using (var fact = Wcf<PeopleService>())
            {
                var channel = fact.CreateChannel();

                List<V_People> poepleList = channel.QueryAll(new V_PeopleQueryObject()).Cast<List<V_People>>();

                foreach (var item in poepleList)
                {
                    Console.WriteLine($"{item.Uid} , {item.Name} , {item.Age} , {item.ClassName}");
                }

                


               // Console.WriteLine(Internationalization.GetString("MSG_1001"));

            }

            #endregion


            Console.WriteLine("any key to exit ...");
            Console.ReadKey();
        }

    }
}
