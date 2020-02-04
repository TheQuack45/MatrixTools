using NUnit.Framework;
using System;

namespace MatrixTools.Tests
{
    [TestFixture]
    public class OperationsTest
    {
        [Test]
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

            Vector testVector = new Vector(3, Vector.TYPES.ColumnVector);
            testVector[0] = 1;
            testVector[1] = 2;
            testVector[2] = 3;

            Vector transposedVector = Vector.Transpose(testVector);
            Assert.AreEqual(Vector.TYPES.RowVector, transposedVector.Type);
            Assert.AreEqual(1, transposedVector[0]);
            Assert.AreEqual(2, transposedVector[1]);
            Assert.AreEqual(3, transposedVector[2]);
        }

        [Test]
        public static void TestSwap()
        {
            Matrix testMatrix = new Matrix(2, 3);
            testMatrix[0, 0] = 1;
            testMatrix[0, 1] = 3;
            testMatrix[0, 2] = 5;
            testMatrix[1, 0] = 2;
            testMatrix[1, 1] = 4;
            testMatrix[1, 2] = 6;

            Vector testVector = new Vector(3, Vector.TYPES.RowVector);
            testVector[0] = 2;
            testVector[1] = 4;
            testVector[2] = 6;

            Assert.AreEqual(1, testMatrix[0, 0]);
            Assert.AreEqual(3, testMatrix[0, 1]);
            Assert.AreEqual(5, testMatrix[0, 2]);

            testMatrix.Swap(testVector, 0);

            Assert.AreEqual(2, testMatrix[0, 0]);
            Assert.AreEqual(4, testMatrix[0, 1]);
            Assert.AreEqual(6, testMatrix[0, 2]);

            testMatrix = new Matrix(2, 3);
            testMatrix[0, 0] = 1;
            testMatrix[0, 1] = 3;
            testMatrix[0, 2] = 5;
            testMatrix[1, 0] = 2;
            testMatrix[1, 1] = 4;
            testMatrix[1, 2] = 6;

            testVector = new Vector(2, Vector.TYPES.ColumnVector);
            testVector[0] = 2;
            testVector[1] = 4;

            Assert.AreEqual(1, testMatrix[0, 0]);
            Assert.AreEqual(2, testMatrix[1, 0]);

            testMatrix.Swap(testVector, 0);

            Assert.AreEqual(2, testMatrix[0, 0]);
            Assert.AreEqual(4, testMatrix[1, 0]);

            Matrix tm2 = new Matrix(2, 3);
            tm2[0, 0] = 1;
            tm2[0, 1] = 2;
            tm2[0, 2] = 3;
            tm2[1, 0] = 4;
            tm2[1, 1] = 5;
            tm2[1, 2] = 6;

            tm2.Swap(0, 1, true);
            Assert.AreEqual(4, tm2[0, 0]);
            Assert.AreEqual(5, tm2[0, 1]);
            Assert.AreEqual(6, tm2[0, 2]);
            Assert.AreEqual(1, tm2[1, 0]);
            Assert.AreEqual(2, tm2[1, 1]);
            Assert.AreEqual(3, tm2[1, 2]);

            tm2[0, 0] = 1;
            tm2[0, 1] = 2;
            tm2[0, 2] = 3;
            tm2[1, 0] = 4;
            tm2[1, 1] = 5;
            tm2[1, 2] = 6;

            tm2.Swap(0, 1, false);
            Assert.AreEqual(2, tm2[0, 0]);
            Assert.AreEqual(5, tm2[1, 0]);
            Assert.AreEqual(1, tm2[0, 1]);
            Assert.AreEqual(4, tm2[1, 1]);
            Assert.AreEqual(3, tm2[0, 2]);
            Assert.AreEqual(6, tm2[1, 2]);
        }

        [Test]
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

            Tuple<int, Matrix, Vector> LUTuple = Matrix.LUDecomposition(testMatrix);
            Matrix LU = LUTuple.Item2;

            Console.WriteLine("L: ");
            Console.WriteLine("{0} {1} {2}", LU[0, 0], LU[0, 1], LU[0, 2]);
            Console.WriteLine("{0} {1} {2}", LU[1, 0], LU[1, 1], LU[1, 2]);
            Console.WriteLine("{0} {1} {2}", LU[2, 0], LU[2, 1], LU[2, 2]);
        }

        [Test]
        public static void TestDeterminant()
        {
            Matrix testMatrix = new Matrix(2, 2);
            testMatrix[0, 0] = 0.232580;
            testMatrix[0, 1] = 0.980771;
            testMatrix[1, 0] = 0.030324;
            testMatrix[1, 1] = 0.045210;

            double result = Matrix.Determinant(testMatrix);
            // Assertion fails if you don't round, but I don't have more digits from Octave because I'm incompetent.
            Assert.AreEqual(-0.019226, Math.Round(result, 6));
        }

        [Test]
        public static void TestMatrixCopy()
        {
            Matrix testMatrix = new Matrix(2, 2);
            testMatrix[0, 0] = 0.232580;
            testMatrix[0, 1] = 0.980771;
            testMatrix[1, 0] = 0.030324;
            testMatrix[1, 1] = 0.045210;

            Matrix testMatrixCopy = testMatrix.Copy();

            Assert.False(object.ReferenceEquals(testMatrix, testMatrixCopy));
            Assert.AreEqual(testMatrix.Rows, testMatrixCopy.Rows);
            Assert.AreEqual(testMatrix.Columns, testMatrixCopy.Columns);
            Assert.AreEqual(testMatrix[0, 0], testMatrixCopy[0, 0]);
            Assert.AreEqual(testMatrix[0, 1], testMatrixCopy[0, 1]);
            Assert.AreEqual(testMatrix[1, 0], testMatrixCopy[1, 0]);
            Assert.AreEqual(testMatrix[1, 1], testMatrixCopy[1, 1]);
        }

        [Test]
        public static void TestStaticMatrixCopy()
        {
            Matrix testMatrix = new Matrix(2, 2);
            testMatrix[0, 0] = 0.232580;
            testMatrix[0, 1] = 0.980771;
            testMatrix[1, 0] = 0.030324;
            testMatrix[1, 1] = 0.045210;

            Matrix testMatrixCopy = Matrix.Copy(testMatrix);

            Assert.False(object.ReferenceEquals(testMatrix, testMatrixCopy));
            Assert.AreEqual(testMatrix.Rows, testMatrixCopy.Rows);
            Assert.AreEqual(testMatrix.Columns, testMatrixCopy.Columns);
            Assert.AreEqual(testMatrix[0, 0], testMatrixCopy[0, 0]);
            Assert.AreEqual(testMatrix[0, 1], testMatrixCopy[0, 1]);
            Assert.AreEqual(testMatrix[1, 0], testMatrixCopy[1, 0]);
            Assert.AreEqual(testMatrix[1, 1], testMatrixCopy[1, 1]);
        }

        [Test]
        public static void TestVectorCopy()
        {
            Vector testVector = new Vector(3, Vector.TYPES.RowVector);
            testVector[0] = 3;
            testVector[1] = 16;
            testVector[2] = -7;

            Vector testVectorCopy = testVector.Copy();

            Assert.False(object.ReferenceEquals(testVector, testVectorCopy));
            Assert.AreEqual(testVector.Size, testVectorCopy.Size);
            Assert.AreEqual(testVector.Type, testVectorCopy.Type);
            Assert.AreEqual(testVector[0], testVectorCopy[0]);
            Assert.AreEqual(testVector[1], testVectorCopy[1]);
            Assert.AreEqual(testVector[2], testVectorCopy[2]);
        }

        [Test]
        public static void TestStaticVectorCopy()
        {
            Vector testVector = new Vector(3, Vector.TYPES.RowVector);
            testVector[0] = 3;
            testVector[1] = 16;
            testVector[2] = -7;

            Vector testVectorCopy = Vector.Copy(testVector);

            Assert.False(object.ReferenceEquals(testVector, testVectorCopy));
            Assert.AreEqual(testVector.Size, testVectorCopy.Size);
            Assert.AreEqual(testVector.Type, testVectorCopy.Type);
            Assert.AreEqual(testVector[0], testVectorCopy[0]);
            Assert.AreEqual(testVector[1], testVectorCopy[1]);
            Assert.AreEqual(testVector[2], testVectorCopy[2]);
        }
    }
}
