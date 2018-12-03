using System;
using System.Collections.Generic;
using System.Text;

using System.Linq.Expressions;

namespace MyCompiler
{
    class LambdaBuilder
    {

        public static Func<double, double> BuildFrom (string _formula)
        {

            Parcer p = new Parcer();
            Expression e = p.Parce(_formula).Execute();
            Console.WriteLine(e.ToString());
           
            var res = Expression.Lambda<Func<double, double>>(e).Compile();
            return res;
        }
    }
}
