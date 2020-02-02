using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;
using NUnit.Framework;

namespace MatrixTools.Tests
{
    [TestFixture]
    public class NeuralNetworkTest
    {
        private const double EULER_NUMBER = 2.7182818284590452353602874;

        [Test]
        public static void TestNeuralNetworks()
        {
            Tuple<Matrix, Matrix, Matrix, Matrix> loadedMatrices = LoadData();
            Matrix theta1 = loadedMatrices.Item1;
            Matrix theta2 = loadedMatrices.Item2;
            Matrix X = loadedMatrices.Item3;
            Matrix y = loadedMatrices.Item4;

            Matrix a1 = Matrix.Merge(Matrix.Ones(X.Rows, 1), X, Matrix.MERGE_TYPE.Column);
            Matrix z2 = theta1 * a1.Transpose();
            Matrix a2 = Sigmoid(z2.Transpose());
            a2 = Matrix.Merge(Matrix.Ones(X.Rows, 1), a2, Matrix.MERGE_TYPE.Column);
            Matrix z3 = theta2 * a2.Transpose();
            Matrix a3 = Sigmoid(z3.Transpose());
            Matrix p = (Matrix)MaxRows(a3);

            Console.WriteLine("Neural network accuracy: {0}", Mean(p == y) * 100);
        }

        private static Vector MaxRows(Matrix m1)
        {
            Vector returnVector = new Vector(m1.Rows, Vector.TYPES.ColumnVector);

            for (int cRow = 0; cRow < m1.Rows; cRow++)
            {
                double rowMax = 0;
                double ind = 0;
                for (int cCol = 0; cCol < m1.Columns; cCol++)
                {
                    if (m1[cRow, cCol] > rowMax)
                    {
                        rowMax = m1[cRow, cCol];
                        ind = cCol;
                    }
                }

                returnVector[cRow] = ind;
            }

            return returnVector;
        }

        private static double Mean(Matrix m1)
        {
            double sum = 0;
            for (int cRow = 0; cRow < m1.Rows; cRow++)
            {
                for (int cCol = 0; cCol < m1.Columns; cCol++)
                {
                    sum += m1[cRow, cCol];
                }
            }

            return (sum / (double)(m1.Rows * m1.Columns));
        }

        private static Tuple<Matrix, Matrix, Matrix, Matrix> LoadData()
        {
            FileStream theta1Stream = new FileStream(Path.Combine(TestContext.CurrentContext.TestDirectory, "theta1.data"), FileMode.Open, FileAccess.Read);
            FileStream theta2Stream = new FileStream(Path.Combine(TestContext.CurrentContext.TestDirectory, "theta2.data"), FileMode.Open, FileAccess.Read);
            FileStream XStream = new FileStream(Path.Combine(TestContext.CurrentContext.TestDirectory, "X.data"), FileMode.Open, FileAccess.Read);
            FileStream yStream = new FileStream(Path.Combine(TestContext.CurrentContext.TestDirectory, "y.data"), FileMode.Open, FileAccess.Read);

            string theta1String = new StreamReader(theta1Stream).ReadToEnd();
            string theta2String = new StreamReader(theta2Stream).ReadToEnd();
            string XString = new StreamReader(XStream).ReadToEnd();
            string yString = new StreamReader(yStream).ReadToEnd();

            string[] theta1SplitArr = Regex.Split(theta1String, "\r\n");
            int theta1ColumnCount = Regex.Split(theta1SplitArr[0], " ").Length;
            Matrix theta1 = new Matrix(theta1SplitArr.Length, theta1ColumnCount);
            for (int cRow = 0; cRow < theta1SplitArr.Length; cRow++)
            {
                string[] nums = Regex.Split(theta1SplitArr[cRow], " ");
                
                for (int cCol = 0; cCol < nums.Length; cCol++)
                {
                    theta1[cRow, cCol] = double.Parse(nums[cCol]);
                }
            }

            string[] theta2SplitArr = Regex.Split(theta2String, "\r\n");
            int theta2ColumnCount = Regex.Split(theta2SplitArr[0], " ").Length;
            Matrix theta2 = new Matrix(theta2SplitArr.Length, theta2ColumnCount);
            for (int cRow = 0; cRow < theta2SplitArr.Length; cRow++)
            {
                string[] nums = Regex.Split(theta2SplitArr[cRow], " ");
                for (int cCol = 0; cCol < nums.Length; cCol++)
                {
                    theta2[cRow, cCol] = double.Parse(nums[cCol]);
                }
            }

            string[] XSplitArr = Regex.Split(XString, "\r\n");
            int XColumnCount = Regex.Split(XSplitArr[0], " ").Length;
            Matrix X = new Matrix(XSplitArr.Length, XColumnCount);
            for (int cRow = 0; cRow < XSplitArr.Length; cRow++)
            {
                string[] nums = Regex.Split(XSplitArr[cRow], " ");
                for (int cCol = 0; cCol < nums.Length; cCol++)
                {
                    X[cRow, cCol] = double.Parse(nums[cCol]);
                }
            }

            string[] ySplitArr = Regex.Split(yString, "\r\n");
            int yColumnCount = Regex.Split(ySplitArr[0], " ").Length;
            Matrix y = new Matrix(ySplitArr.Length, yColumnCount);
            for (int cRow = 0; cRow < ySplitArr.Length; cRow++)
            {
                string[] nums = Regex.Split(ySplitArr[cRow], " ");
                for (int cCol = 0; cCol < nums.Length; cCol++)
                {
                    y[cRow, cCol] = double.Parse(nums[cCol]);
                }
            }

            return new Tuple<Matrix, Matrix, Matrix, Matrix>(theta1, theta2, X, y);
        }

        private static Vector Log(Vector v1)
        {
            int vectorSize = v1.Size;
            Vector returnVector = new Vector(vectorSize, v1.Type);

            for (int i = 0; i < vectorSize; i++)
            {
                returnVector[i] = Math.Log(v1[i]);
            }

            return returnVector;
        }

        private static Vector Hypothesis(Vector theta, Vector X)
        {
            return Sigmoid((Vector)((Matrix)theta.Transpose() * (Matrix)X.Transpose()));
        }

        private static Matrix Sigmoid(Matrix m1)
        {
            int rows = m1.Rows;
            int cols = m1.Columns;
            Matrix returnMatrix = new Matrix(rows, cols);

            for (int cRow = 0; cRow < rows; cRow++)
            {
                for (int cCol = 0; cCol < cols; cCol++)
                {
                    returnMatrix[cRow, cCol] = Sigmoid(m1[cRow, cCol]);
                }
            }

            return returnMatrix;
        }

        private static Vector Sigmoid(Vector v1)
        {
            int vectorSize = v1.Size;
            Vector returnVector = new Vector(vectorSize, v1.Type);

            for (int i = 0; i < vectorSize; i++)
            {
                returnVector[i] = Sigmoid(v1[i]);
            }

            return returnVector;
        }

        private static double Sigmoid(double z)
        {
            return (1.0 / (1.0 + Math.Pow(EULER_NUMBER, -z)));
        }
    }
}
