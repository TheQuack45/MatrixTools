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

            Console.WriteLine("");

            //GradientDescentTest.TestGradientDescent();

            Console.WriteLine("");

            NeuralNetworkTest.TestNeuralNetworks();

            Console.WriteLine("");

            Console.WriteLine("Tests successful.");
            Console.ReadKey();
        }
    }
}
