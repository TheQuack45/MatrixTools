using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixTools.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            DeclarationTest.TestDeclaration();

            Console.WriteLine("");

            OperatorsTest.TestOperators();

            Console.WriteLine("");

            OperationsTest.TestOperations();

            GradientDescentTest.TestGradientDescent();

            Console.WriteLine("");

            Console.WriteLine("Tests successful.");
            Console.ReadKey();
        }
    }
}
