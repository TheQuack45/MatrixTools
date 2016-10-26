using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixTools.Tests
{
    [TestFixture]
    public class OperationsTest
    {
        public static void TestOperations()
        {
            TestTranspose();
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
    }
}
