using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MatrixTools;

namespace MatrixTools.Demonstration
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Creating 3x3 Matrix.");

            Matrix demoMatrix = new Matrix(3, 3);
            demoMatrix[0, 0] = 1;
            demoMatrix[0, 1] = 2;
            demoMatrix[0, 2] = 3;
            demoMatrix[1, 0] = 4;
            demoMatrix[1, 1] = 5;
            demoMatrix[1, 2] = 6;
            demoMatrix[2, 0] = 7;
            demoMatrix[2, 1] = 8;
            demoMatrix[2, 2] = 9;

            Console.WriteLine("");
            Console.WriteLine("Original Matrix:");
            Console.WriteLine(demoMatrix.ToString("0.0000E+000"));

            Console.WriteLine("");
            Console.WriteLine("Determinant of original Matrix:");
            Console.WriteLine(Matrix.Determinant(demoMatrix));

            Console.WriteLine("");
            Console.WriteLine("LU decomposition of original matrix.");
            Tuple<int, Matrix, Vector> tuple = Matrix.LUDecomposition(demoMatrix);
            Matrix lu = tuple.Item2;
            Console.WriteLine(lu.ToString("0.0000E+000"));

            Console.ReadKey();
        }
    }
}
