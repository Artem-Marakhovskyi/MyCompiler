using System;
using System.Collections.Generic;
using System.Text;

using System.Linq.Expressions;

namespace MyCompiler
{
    class LambdaBuilder
    {

        public static Func<double> BuildFrom(string _formula)
        {
            Parcer p = new Parcer();
            Expression e = p.Parce(_formula).Execute();


            var res = Expression.Lambda<Func<double>>(e).Compile();
            return res;
        }
    }
}
