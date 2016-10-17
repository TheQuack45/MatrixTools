using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixTools
{
    public class Matrix : IEnumerable<double>
    {
        #region Members definition
        private double[,] _innerMatrix;
        protected double[,] InnerMatrix
        {
            get { return _innerMatrix; }
        }

        [System.Runtime.CompilerServices.IndexerName("Indexer")]
        public double this[int row, int column]
        {
            get { return this._innerMatrix[row, column]; }
            set { this._innerMatrix[row, column] = value; }
        }

        public int Rows
        {
            get { return this.InnerMatrix.GetLength(0); }
        }

        public int Height
        {
            get { return this.Rows; }
        }

        public int Columns
        {
            get { return this.InnerMatrix.GetLength(1); }
        }

        public int Width
        {
            get { return this.Columns; }
        }
        #endregion

        #region Constructors definition
        public Matrix(int rows, int columns)
        {
            this._innerMatrix = new double[rows, columns];
        }
        #endregion

        #region Operator overloading
        public static Matrix operator +(Matrix m1, double scalar)
        {
            int rows = m1.Rows;
            int columns = m1.Columns;

            Matrix returnMatrix = new Matrix(rows, columns);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    returnMatrix[i, j] = m1[i, j] + scalar;
                }
            }

            return returnMatrix;
        }

        public static Matrix operator +(Matrix m1, Matrix m2)
        {
            int rows = m1.Rows;
            int columns = m1.Columns;
            if ((rows == m2.Rows) && (columns == m2.Columns))
            {
                Matrix returnMatrix = new Matrix(rows, m1.Columns);
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        returnMatrix[i, j] = m1[i, j] + m2[i, j];
                    }
                }

                return returnMatrix;
            }

            throw new ArgumentException("The matrices to add must have the same dimensions.");
        }

        public static Matrix operator -(Matrix m1, double scalar)
        {
            int rows = m1.Rows;
            int columns = m1.Columns;

            Matrix returnMatrix = new Matrix(rows, columns);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    returnMatrix[i, j] = m1[i, j] - scalar;
                }
            }

            return returnMatrix;
        }

        public static Matrix operator -(Matrix m1, Matrix m2)
        {
            int rows = m1.Rows;
            int columns = m1.Columns;
            if ((rows == m2.Rows) && (columns == m2.Columns))
            {
                Matrix returnMatrix = new Matrix(rows, m1.Columns);
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        returnMatrix[i, j] = m1[i, j] - m2[i, j];
                    }
                }

                return returnMatrix;
            }

            throw new ArgumentException("The matrices to add must have the same dimensions.");
        }

        public static Matrix operator *(Matrix m1, double scalar)
        {
            int rows = m1.Rows;
            int columns = m1.Columns;

            Matrix returnMatrix = new Matrix(rows, columns);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    returnMatrix[i, j] = m1[i, j] * scalar;
                }
            }

            return returnMatrix;
        }

        public static Matrix operator *(Matrix m1, Matrix m2)
        {
            if (m1.Columns == m2.Rows)
            {
                Matrix returnMatrix = new Matrix(m1.Rows, m2.Columns);

                for (int i = 0; i < m1.Rows; i++)
                {
                    for (int j = 0; j < m2.Columns; j++)
                    {
                        for (int k = 0; k < m2.Rows; k++)
                        {
                            returnMatrix[i, j] += m1[i, k] * m2[k, j];
                        }
                    }
                }

                return returnMatrix;
            }

            throw new ArgumentException("The width of the first matrix must match the height of the second matrix.");
        }

        public static Matrix operator /(Matrix m1, double scalar)
        {
            int rows = m1.Rows;
            int columns = m1.Columns;

            Matrix returnMatrix = new Matrix(rows, columns);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    returnMatrix[i, j] = m1[i, j] / scalar;
                }
            }

            return returnMatrix;
        }
        #endregion

        #region Methods definition
        public Matrix Get(int row, int column)
        {
            Matrix returnMatrix = new Matrix(1, 1);
            returnMatrix[0, 0] = this.InnerMatrix[row, column];
            return returnMatrix;
        }

        public double GetAsScalar(int row, int column)
        {
            return this.InnerMatrix[row, column];
        }

        public double ToScalar()
        {
            if (this.Rows == 1 && this.Columns == 1)
            {
                return this.InnerMatrix[0, 0];
            }

            throw new InvalidOperationException("You cannot convert a matrix to a scalar when the matrix contains more than one value.");
        }

        public Matrix GetRow(int row)
        {
            int columns = this.Columns;
            Matrix returnMatrix = new Matrix(1, columns);
            
            for (int i = 0; i < columns; i++)
            {
                returnMatrix[0, i] = this.InnerMatrix[0, i];
            }

            return returnMatrix;
        }

        public Matrix GetColumn(int column)
        {
            int rows = this.Rows;
            Matrix returnMatrix = new Matrix(rows, 1);

            for (int i = 0; i < rows; i++)
            {
                returnMatrix[i, 0] = this.InnerMatrix[i, 0];
            }

            return returnMatrix;
        }

        IEnumerator<double> IEnumerable<double>.GetEnumerator()
        {
            IEnumerator enumerator = this._innerMatrix.GetEnumerator();
            
            while (enumerator.MoveNext())
            {
                yield return (double)enumerator.Current;
            }
        }

        public IEnumerator GetEnumerator()
        {
            return this._innerMatrix.GetEnumerator();
        }
        #endregion
    }
}
