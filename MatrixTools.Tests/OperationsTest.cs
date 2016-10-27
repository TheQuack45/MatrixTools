using NUnit.Framework;
using System;

namespace MatrixTools.Tests
{
    [TestFixture]
    public class OperationsTest
    {
        public static void TestOperations()
        {
            TestTranspose();
            // TODO: TestLUDecomp();
        }

        public static void TestTranspose()
        {
            Matrix testMatrix = new Matrix(2, 3);
            testMatrix[0, 0] = 1;
            testMatrix[0, 1] = 3;
            testMatrix[0, 2] = 5;
            testMatrix[1, 0] = 2;
            testMatrix[1, 1] = 4;
            testMatrix[1, 2] = 6;

            Matrix transposedMatrix = Matrix.Transpose(testMatrix);
            Assert.AreEqual(1, transposedMatrix[0, 0]);
            Assert.AreEqual(2, transposedMatrix[0, 1]);
            Assert.AreEqual(3, transposedMatrix[1, 0]);
            Assert.AreEqual(4, transposedMatrix[1, 1]);
            Assert.AreEqual(5, transposedMatrix[2, 0]);
            Assert.AreEqual(6, transposedMatrix[2, 1]);
            Console.WriteLine("Matrix transpose is valid.");

            Vector testVector = new Vector(3, Vector.TYPES.ColumnVector);
            testVector[0] = 1;
            testVector[1] = 2;
            testVector[2] = 3;

            Vector transposedVector = Vector.Transpose(testVector);
            Assert.AreEqual(Vector.TYPES.RowVector, transposedVector.Type);
            Assert.AreEqual(1, transposedVector[0]);
            Assert.AreEqual(2, transposedVector[1]);
            Assert.AreEqual(3, transposedVector[2]);
            Console.WriteLine("Vector transpose is valid.");
        }

        public static void TestLUDecomp()
        {
            Matrix testMatrix = new Matrix(3, 3);
            testMatrix[0, 0] = 9;
            testMatrix[0, 1] = 3;
            testMatrix[0, 2] = 4;
            testMatrix[1, 0] = 4;
            testMatrix[1, 1] = 3;
            testMatrix[1, 2] = 4;
            testMatrix[2, 0] = 1;
            testMatrix[2, 1] = 1;
            testMatrix[2, 2] = 1;

            Tuple<Matrix, Matrix> LU = Matrix.LUDecomposition(testMatrix);
            Matrix L = LU.Item1;
            Matrix U = LU.Item2;

            Console.WriteLine("L: ");
            Console.WriteLine("{0} {1} {2}", L[0, 0], L[0, 1], L[0, 2]);
            Console.WriteLine("{0} {1} {2}", L[1, 0], L[1, 1], L[1, 2]);
            Console.WriteLine("{0} {1} {2}", L[2, 0], L[2, 1], L[2, 2]);

            Console.WriteLine("");

            Console.WriteLine("U: ");
            Console.WriteLine("{0} {1} {2}", U[0, 0], U[0, 1], U[0, 2]);
            Console.WriteLine("{0} {1} {2}", U[1, 0], U[1, 1], U[1, 2]);
            Console.WriteLine("{0} {1} {2}", U[2, 0], U[2, 1], U[2, 2]);
        }
    }
}
