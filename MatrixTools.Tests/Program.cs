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
            CostFunctionTest.TestCostFunction();

            Console.WriteLine("");

            DeclarationTest.TestDeclaration();

            Console.WriteLine("");

            OperatorsTest.TestOperators();

            Console.WriteLine("");

            OperationsTest.TestOperations();

            Console.WriteLine("Tests successful.");
            Console.ReadKey();
        }
    }
}
