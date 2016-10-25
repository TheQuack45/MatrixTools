using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using MatrixTools;

namespace MatrixTools.Tests
{
    [TestFixture]
    public static class DeclarationTest
    {
        [Test]
        public static void TestDeclaration()
        {
            Matrix testMatrix = new Matrix(2, 3);
            Assert.AreEqual(2, testMatrix.Rows);
            Assert.AreEqual(2, testMatrix.Height);
            Assert.AreEqual(3, testMatrix.Columns);
            Assert.AreEqual(3, testMatrix.Width);
            Console.WriteLine("Dimensions are valid.");

            testMatrix[0, 0] = 1;
            testMatrix[0, 1] = 2;
            testMatrix[0, 2] = 3;
            testMatrix[1, 0] = 4;
            testMatrix[1, 1] = 5;
            testMatrix[1, 2] = 6;
            Assert.AreEqual(1, testMatrix[0, 0]);
            Assert.AreEqual(2, testMatrix[0, 1]);
            Assert.AreEqual(3, testMatrix[0, 2]);
            Assert.AreEqual(4, testMatrix[1, 0]);
            Assert.AreEqual(5, testMatrix[1, 1]);
            Assert.AreEqual(6, testMatrix[1, 2]);
            Console.WriteLine("Values are valid.");

            Matrix firstRow = new Matrix(1, 3);
            Assert.AreEqual(firstRow.Rows, testMatrix.GetRow(0).Rows);
            Assert.AreEqual(firstRow.Columns, testMatrix.GetRow(0).Columns);
            Console.WriteLine("GetRow is valid.");

            Matrix firstColumn = new Matrix(2, 1);
            Assert.AreEqual(firstColumn.Rows, testMatrix.GetColumn(0).Rows);
            Assert.AreEqual(firstColumn.Columns, testMatrix.GetColumn(0).Columns);
            Console.WriteLine("GetColumn is valid.");

            Matrix eye2 = Matrix.IdentityMatrix(2);
            Assert.AreEqual(2, eye2.Rows);
            Assert.AreEqual(2, eye2.Columns);
            Assert.AreEqual(1, eye2[0, 0]);
            Assert.AreEqual(0, eye2[0, 1]);
            Assert.AreEqual(0, eye2[1, 0]);
            Assert.AreEqual(1, eye2[1, 1]);
            Console.WriteLine("Matrix.IdentityMatrix is valid.");
            Assert.AreEqual(eye2, Matrix.eye(2));
            Console.WriteLine("Matrix.eye is valid.");
        }
    }
}
