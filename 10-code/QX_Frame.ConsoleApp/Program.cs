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
using QX_Frame.Helper_DG.Service;

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

                TB_PeopleQueryObject peopleQueryObject = new TB_PeopleQueryObject();

                //peopleQueryObject.Name = "1";
                //peopleQueryObject.Age = 9;


                List<TB_People> peopleList = channel.QueryAll(peopleQueryObject).Cast<List<TB_People>>();

                foreach (var item in peopleList)
                {
                    Console.WriteLine($"{item.Uid} , {item.Name} , {item.Age} , {item.ClassId}");
                }
            }

                //    //TB_People people = channel.QuerySingle(new TB_PeopleQueryObject { QueryCondition = t => t.Name.Equals("lilongji9") }).Cast<TB_People>();

                //    //Console.WriteLine(people.Name);

                //    //people.Name = "jonnyR";

                //    //if (channel.Update(people))
                //    //{
                //    //    Console.WriteLine("success!");
                //    //}

                //    // Console.WriteLine(Internationalization.GetString("MSG_1001"));

                //}

                #endregion



            Console.WriteLine("any key to exit ...");
            Console.ReadKey();
        }
    }
}
