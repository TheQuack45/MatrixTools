using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixTools.Tests
{
    [TestFixture]
    public static class CostFunctionTest
    {
        [Test]
        public static void TestCostFunction()
        {
            Matrix X = new Matrix(3, 2);
            X[0, 0] = 1;
            X[0, 1] = 1;
            X[1, 0] = 1;
            X[1, 1] = 2;
            X[2, 0] = 1;
            X[2, 1] = 3;

            Vector y = new Vector(3, Vector.TYPES.ColumnVector);
            y[0] = 1;
            y[1] = 2;
            y[2] = 3;

            Vector theta = new Vector(2, Vector.TYPES.ColumnVector);
            theta[0] = 0;
            theta[1] = 0;

            int m = y.Size;

            Vector predictions = (X * theta).ToVector();
            Vector sqrErrors = Matrix.Power(predictions - y, 2).ToVector();
            double sum = Convert.ToInt32(Vector.Sum(sqrErrors));

            double J = (1 / (2 * (double)m)) * sum;

            Console.WriteLine("J: {0}", J);
        }
    }
}
