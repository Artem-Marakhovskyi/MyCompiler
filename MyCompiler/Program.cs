using System;
using System.Text.RegularExpressions;

namespace MyCompiler
{
    class Program
    {
        static void Main(string[] args)
        {   
            string formula = "23.34 + 34.4 * 5 ^ 2 + p";
            Console.WriteLine(formula);

            var function = LambdaBuilder.BuildFrom(formula);

            Console.WriteLine($"lambda result {function(3.7)}");
            Console.ReadLine();
        }
    }
}
