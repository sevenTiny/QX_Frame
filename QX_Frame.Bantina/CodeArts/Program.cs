using CodeArts.Arrays;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeArts
{
    class Program
    {
        static void Main(string[] args)
        {
            IList<Student> list = new List<Student>
            {
                new Student{Id=1,Name="11",Age=1},
                new Student{Id=2,Name="22",Age=2},
                new Student{Id=3,Name="33",Age=3}
            };

            Student[] studentArray = list.ToArray_DG();

            foreach (var item in studentArray)
            {
                Console.WriteLine(item.Id);
            }

            Console.ReadKey();
        }
    }
}
