namespace FastPropertyGettersWithExpressions
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;

    public class Program
    {
        public static void Main()
        {
            // Agenda
            // return RedirectToAction("Index", new { id = 5, query = "Text" });
            // ASP.NET needs to get from this new { id = 5, query = "Text" } Annonymous Type a Dictionary,
            // which contains the names and values of both of the properties
            // Dictionary { ["id"] = 5, ["query"] = "Text" }
            // It is easy to do it with Reflection, but Reflection is not very fast
            // And this code has to be executed on every request.
            // Lets see how we can do the same with faster with Expression trees

            //var obj = new { id = 5, query = "Text" };

            //var dict = new Dictionary<string, object>();

            //var stopwatch = Stopwatch.StartNew();

            //for (int i = 0; i < 1000000; i++)
            //{
            //    obj
            //        .GetType()
            //        .GetProperties()
            //        .Select(pr => new
            //        {
            //            pr.Name,
            //            Value = pr.GetValue(obj)
            //        })
            //        .ToList()
            //        .ForEach(pr => dict[pr.Name] = pr.Value);
            //}

            //Console.WriteLine($"{stopwatch.Elapsed} - Reflection Property Getters");
            //Console.WriteLine(dict.Count);


            //dict = new Dictionary<string, object>();

            //stopwatch = Stopwatch.StartNew();

            //for (int i = 0; i < 1000000; i++)
            //{
            //    PropertyHelper
            //        .GetProperties(obj.GetType())
            //        .Select(pr => new
            //        {
            //            pr.Name,
            //            Value = pr.Getter(obj)
            //        })
            //        .ToList()
            //        .ForEach(pr => dict[pr.Name] = pr.Value);
            //}

            //Console.WriteLine($"{stopwatch.Elapsed} - Expression Tree Getters");
            //Console.WriteLine(dict.Count);


            string a = "x";
            string b = "x";

            Console.WriteLine(GetAddress(a));
            Console.WriteLine(GetAddress(b));
        }

        public static unsafe IntPtr GetAddress(object o)
        {
            TypedReference tr = __makeref(o);
            IntPtr ptr = **(IntPtr**)(&tr);
            return ptr;
        }
    }
}
