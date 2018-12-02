using System;
using System.Text.RegularExpressions;

namespace MyCompiler
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            string formula = "23.34 + 34.4 * 5 ^ 2";
            Console.WriteLine(formula);

            var function = LambdaBuilder.BuildFrom(formula);

            Console.WriteLine($"lambda result {function()}");
            Console.ReadLine();
        }
    }
}
