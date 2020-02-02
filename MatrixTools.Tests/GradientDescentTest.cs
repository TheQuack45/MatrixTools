using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MatrixTools.Tests
{
    [TestFixture]
    public class GradientDescentTest
    {
        // TODO: This.
        //[Test]
        public static void TestGradientDescent()
        {
            Tuple<Matrix, Vector> loadedMatrices = LoadData();
            Matrix X = loadedMatrices.Item1;
            Vector y = loadedMatrices.Item2;
            Vector theta = Matrix.Zeros(2, 1).ToVector();
            double alpha = 0.01;

            for (int i = 0; i < 1500; i++)
            {
                Vector h = X * theta;
                Vector errors = h - y;
                Vector t = X.Transpose() * errors;
                Console.WriteLine(t.ToString());
                Vector change = ((X.Transpose() * errors) * alpha) * (1.0 / (double)X.Size);
                theta = theta - change;
            }

            Matrix predict1Matrix = new Matrix(1, 2);
            predict1Matrix[0, 0] = 1;
            predict1Matrix[0, 1] = 3.5;
            string test = ((predict1Matrix * theta).ToScalar() * 10000).ToString("0.000");
            Assert.AreEqual("4519.768", test);
            Matrix predict2Matrix = new Matrix(1, 2);
            predict2Matrix[0, 0] = 1;
            predict2Matrix[0, 1] = 7;
            Assert.AreEqual("45342.450", ((predict2Matrix * theta).ToScalar() * 10000).ToString("0.000"));
        }

        public static Tuple<Matrix, Vector> LoadData()
        {
            FileStream stream = new FileStream(Path.Combine(TestContext.CurrentContext.TestDirectory, "ex1data1.data"), FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(stream);
            List<string> xVals = new List<string>();
            List<string> yVals = new List<string>();
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] values = Regex.Split(line, ",");
                xVals.Add(values[0]);
                yVals.Add(values[1]);
            }

            Matrix X = Matrix.Ones(xVals.Count, 2);
            Vector y = new Vector(yVals.Count, Vector.TYPES.ColumnVector);

            for (int i = 0; i < xVals.Count; i++)
            {
                X[i, 1] = double.Parse(xVals[i]);
                y[i] = double.Parse(yVals[i]);
            }

            return new Tuple<Matrix, Vector>(X, y);
        }

        private static double ComputeCost(Matrix X, Vector y, Vector theta)
        {
            Vector predictions = X * theta;
            Vector sqrErrors = (predictions - y).EMPower(2);

            double J = (1.0 / (2.0 * (double)y.Size)) * sqrErrors.Sum();
            return J;
        }
    }
}
