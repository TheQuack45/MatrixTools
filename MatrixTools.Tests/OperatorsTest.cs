using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MatrixTools;
using NUnit.Framework;

namespace MatrixTools.Tests
{
    [TestFixture]
    public static class OperatorsTest
    {
        [Test]
        public static void TestMatrixScalarAddition()
        {
            Matrix testMatrix = new Matrix(2, 2);
            testMatrix[0, 0] = 1;
            testMatrix[0, 1] = 2;
            testMatrix[1, 0] = 3;
            testMatrix[1, 1] = 4;

            Matrix addedMatrix = testMatrix + 1;
            Assert.AreEqual(addedMatrix[0, 0], 2);
            Assert.AreEqual(addedMatrix[0, 1], 3);
            Assert.AreEqual(addedMatrix[1, 0], 4);
            Assert.AreEqual(addedMatrix[1, 1], 5);

            testMatrix += 1;
            Assert.AreEqual(testMatrix[0, 0], 2);
            Assert.AreEqual(testMatrix[0, 1], 3);
            Assert.AreEqual(testMatrix[1, 0], 4);
            Assert.AreEqual(testMatrix[1, 1], 5);
        }

        [Test]
        public static void TestMatrixMatrixAddition()
        {
            Matrix testMatrix = new Matrix(2, 2);
            testMatrix[0, 0] = 1;
            testMatrix[0, 1] = 2;
            testMatrix[1, 0] = 3;
            testMatrix[1, 1] = 4;
            Matrix testMatrixTwo = new Matrix(2, 2);
            testMatrixTwo[0, 0] = 1;
            testMatrixTwo[0, 1] = 2;
            testMatrixTwo[1, 0] = 3;
            testMatrixTwo[1, 1] = 4;

            Matrix addedMatrix = testMatrix + testMatrixTwo;
            Assert.AreEqual(addedMatrix[0, 0], 2);
            Assert.AreEqual(addedMatrix[0, 1], 4);
            Assert.AreEqual(addedMatrix[1, 0], 6);
            Assert.AreEqual(addedMatrix[1, 1], 8);

            testMatrix += testMatrixTwo;
            Assert.AreEqual(testMatrix[0, 0], 2);
            Assert.AreEqual(testMatrix[0, 1], 4);
            Assert.AreEqual(testMatrix[1, 0], 6);
            Assert.AreEqual(testMatrix[1, 1], 8);

            Matrix invalidMatrix = new Matrix(2, 1);
            invalidMatrix[0, 0] = 1;
            invalidMatrix[1, 0] = 2;
        }

        [Test]
        public static void TestMatrixScalarSubtraction()
        {
            Matrix testMatrix = new Matrix(2, 2);
            testMatrix[0, 0] = 1;
            testMatrix[0, 1] = 2;
            testMatrix[1, 0] = 3;
            testMatrix[1, 1] = 4;

            Matrix addedMatrix = testMatrix - 1;
            Assert.AreEqual(addedMatrix[0, 0], 0);
            Assert.AreEqual(addedMatrix[0, 1], 1);
            Assert.AreEqual(addedMatrix[1, 0], 2);
            Assert.AreEqual(addedMatrix[1, 1], 3);

            testMatrix -= 1;
            Assert.AreEqual(testMatrix[0, 0], 0);
            Assert.AreEqual(testMatrix[0, 1], 1);
            Assert.AreEqual(testMatrix[1, 0], 2);
            Assert.AreEqual(testMatrix[1, 1], 3);
        }

        [Test]
        public static void TestMatrixMatrixSubtraction()
        {
            Matrix testMatrix = new Matrix(2, 2);
            testMatrix[0, 0] = 1;
            testMatrix[0, 1] = 2;
            testMatrix[1, 0] = 3;
            testMatrix[1, 1] = 4;
            Matrix testMatrixTwo = new Matrix(2, 2);
            testMatrixTwo[0, 0] = 1;
            testMatrixTwo[0, 1] = 2;
            testMatrixTwo[1, 0] = 3;
            testMatrixTwo[1, 1] = 4;

            Matrix resultMatrix = testMatrix - testMatrixTwo;
            Assert.AreEqual(resultMatrix[0, 0], 0);
            Assert.AreEqual(resultMatrix[0, 1], 0);
            Assert.AreEqual(resultMatrix[1, 0], 0);
            Assert.AreEqual(resultMatrix[1, 1], 0);

            testMatrix -= testMatrixTwo;
            Assert.AreEqual(testMatrix[0, 0], 0);
            Assert.AreEqual(testMatrix[0, 1], 0);
            Assert.AreEqual(testMatrix[1, 0], 0);
            Assert.AreEqual(testMatrix[1, 1], 0);

            Matrix invalidMatrix = new Matrix(2, 1);
            invalidMatrix[0, 0] = 1;
            invalidMatrix[1, 0] = 2;
        }

        [Test]
        public static void TestMatrixScalarMultiplication()
        {
            Matrix testMatrix = new Matrix(2, 2);
            testMatrix[0, 0] = 1;
            testMatrix[0, 1] = 2;
            testMatrix[1, 0] = 3;
            testMatrix[1, 1] = 4;

            Matrix product = testMatrix * 2;
            Assert.AreEqual(product.Rows, 2);
            Assert.AreEqual(product.Columns, 2);
            Assert.AreEqual(product[0, 0], 2);
            Assert.AreEqual(product[0, 1], 4);
            Assert.AreEqual(product[1, 0], 6);
            Assert.AreEqual(product[1, 1], 8);

            testMatrix *= 2;
            Assert.AreEqual(testMatrix.Rows, 2);
            Assert.AreEqual(testMatrix.Columns, 2);
            Assert.AreEqual(testMatrix[0, 0], 2);
            Assert.AreEqual(testMatrix[0, 1], 4);
            Assert.AreEqual(testMatrix[1, 0], 6);
            Assert.AreEqual(testMatrix[1, 1], 8);
        }

        [Test]
        public static void TestMatrixMatrixMultiplication()
        {
            Matrix testMatrix = new Matrix(2, 2);
            testMatrix[0, 0] = 1;
            testMatrix[0, 1] = 2;
            testMatrix[1, 0] = 3;
            testMatrix[1, 1] = 4;
            Matrix testMatrixTwo = new Matrix(2, 2);
            testMatrixTwo[0, 0] = 1;
            testMatrixTwo[0, 1] = 2;
            testMatrixTwo[1, 0] = 3;
            testMatrixTwo[1, 1] = 4;

            Matrix product = testMatrix * testMatrixTwo;
            Assert.AreEqual(product.Rows, 2);
            Assert.AreEqual(product.Columns, 2);
            Assert.AreEqual(product[0, 0], 7);
            Assert.AreEqual(product[0, 1], 10);
            Assert.AreEqual(product[1, 0], 15);

            Matrix invalidMatrix = new Matrix(1, 2);
            invalidMatrix[0, 0] = 1;
            invalidMatrix[0, 1] = 2;
        }

        [Test]
        public static void TestMatrixVectorMultiplication()
        {
            Matrix testMatrix = new Matrix(2, 2);
            testMatrix[0, 0] = 1;
            testMatrix[0, 1] = 2;
            testMatrix[1, 0] = 3;
            testMatrix[1, 1] = 4;
            Vector testVector = new Vector(2, Vector.TYPES.ColumnVector);
            testVector[0] = 2;
            testVector[1] = 3;

            Vector product = testMatrix * testVector;
            Assert.AreEqual(product[0], 8);
            Assert.AreEqual(product[1], 18);
        }

        [Test]
        public static void TestMatrixScalarDivision()
        {
            Matrix testMatrix = new Matrix(2, 2);
            testMatrix[0, 0] = 2;
            testMatrix[0, 1] = 4;
            testMatrix[1, 0] = 6;
            testMatrix[1, 1] = 8;

            Matrix product = testMatrix / 2;
            Assert.AreEqual(product.Rows, 2);
            Assert.AreEqual(product.Columns, 2);
            Assert.AreEqual(product[0, 0], 1);
            Assert.AreEqual(product[0, 1], 2);
            Assert.AreEqual(product[1, 0], 3);
            Assert.AreEqual(product[1, 1], 4);

            testMatrix /= 2;
            Assert.AreEqual(testMatrix.Rows, 2);
            Assert.AreEqual(testMatrix.Columns, 2);
            Assert.AreEqual(testMatrix[0, 0], 1);
            Assert.AreEqual(testMatrix[0, 1], 2);
            Assert.AreEqual(testMatrix[1, 0], 3);
            Assert.AreEqual(testMatrix[1, 1], 4);
        }
    }
}
