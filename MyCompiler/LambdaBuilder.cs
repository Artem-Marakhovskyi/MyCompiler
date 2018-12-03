using System;
using System.Collections.Generic;
using System.Text;

using System.Linq.Expressions;

namespace MyCompiler
{
    class LambdaBuilder
    {

        public static Func<double> BuildFrom (string _formula)
        {
            Parcer p = new Parcer();
            
            Expression e = p.Parce(_formula).Execute();

           

            //ParameterExpression param = Expression.Parameter(typeof(double));

            /// !!!!! 
            var res = Expression.Lambda<Func<double>>(e /*parameter */).Compile();
            return res;
        }
    }
}
