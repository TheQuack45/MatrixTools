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
        protected double[,] _innerMatrix;
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

        public int Size
        {
            get { return this.Rows * this.Columns; }
        }

        public bool IsSquare
        {
            get { return (this.Rows == this.Columns); }
        }
        #endregion

        #region Constructors definition
        /// <summary>
        /// Represents a Matrix containing n*m values of type double.
        /// </summary>
        /// <param name="rows">Amount of rows in the Matrix.</param>
        /// <param name="columns">Amount of columns in the Matrix.</param>
        public Matrix(int rows, int columns)
        {
            this._innerMatrix = new double[rows, columns];
        }
        #endregion

        #region Operator overloads
        #region Cast operators
        
        #endregion

        #region Standard operators
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

        public static Vector operator *(Matrix m1, Vector v1)
        {
            int rows = m1.Rows;
            int columns = m1.Columns;

            if (columns == v1.Size)
            {
                Vector returnVector = new Vector(rows, Vector.TYPES.ColumnVector);
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        returnVector[i] += m1[i, j] * v1[j];
                    }
                }

                return returnVector;
            }

            throw new ArgumentException("The height of the vector must match the width of the Matrix.");
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

        public static bool operator ==(Matrix m1, Matrix m2)
        {
            if (m1.Rows != m2.Rows || m1.Columns != m2.Columns)
                { return false; }

            for (int i = 0; i < m1.Rows; i++)
            {
                for (int j = 0; j < m1.Columns; j++)
                {
                    if (m1[i, j] != m2[i, j])
                        { return false; }
                }
            }

            return true;
        }

        public static bool operator !=(Matrix m1, Matrix m2)
        {
            return !(m1 == m2);
        }
        #endregion
        #endregion

        #region Methods definition
        /// <summary>
        /// Gets a 1*1 Matrix containing the element at the specified location.
        /// </summary>
        /// <param name="row">Row index to access.</param>
        /// <param name="column">Column index to access.</param>
        /// <returns>1*1 Matrix containing the retrieved element.</returns>
        public Matrix Get(int row, int column)
        {
            Matrix returnMatrix = new Matrix(1, 1);
            returnMatrix[0, 0] = this.InnerMatrix[row, column];
            return returnMatrix;
        }

        /// <summary>
        /// Gets the element at the specified location as a double.
        /// </summary>
        /// <param name="row">Row index to access.</param>
        /// <param name="column">Column index to access.</param>
        /// <returns>The retrieved element as a double.</returns>
        public double GetAsScalar(int row, int column)
        {
            return this.InnerMatrix[row, column];
        }

        /// <summary>
        /// Converts this Matrix to a scalar if it is a 1x1 matrix.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if this Matrix is not a 1x1 Matrix.</exception> 
        /// <returns>The single value from this Matrix as a double.</returns>
        public double ToScalar()
        {
            if (this.Rows == 1 && this.Columns == 1)
            {
                return this.InnerMatrix[0, 0];
            }

            throw new InvalidOperationException("You cannot convert a Matrix to a scalar when the Matrix contains more than one value.");
        }

        /// <summary>
        /// Converts this Matrix to a Vector.
        /// </summary>
        /// <returns>Vector that is equivalent to this Matrix.</returns>
        public Vector ToVector()
        {
            return Matrix.ToVector(this);
        }

        /// <summary>
        /// Converts the given Matrix to a Vector.
        /// </summary>
        /// <param name="m1">Matrix to convert.</param>
        /// <returns>Vector that is equivalent to the given Matrix.</returns>
        public static Vector ToVector(Matrix m1)
        {
            if (m1.Rows == 1)
            {
                Vector returnVector = new Vector(m1.Columns, Vector.TYPES.RowVector);
                for (int i = 0; i < returnVector.Size; i++)
                {
                    returnVector[i] = m1[0, i];
                }
                return returnVector;
            }
            else if (m1.Columns == 1)
            {
                Vector returnVector = new Vector(m1.Rows, Vector.TYPES.ColumnVector);
                for (int i = 0; i < returnVector.Size; i++)
                {
                    returnVector[i] = m1[i, 0];
                }
                return returnVector;
            }
            else
            {
                throw new ArgumentException("The width and/or height of the given Matrix must be 1 to be converted to a Vector.", nameof(m1));
            }
        }

        /// <summary>
        /// Gets the full row at the specified index as a 1*m Vector.
        /// </summary>
        /// <param name="row">Row index to get.</param>
        /// <returns>1*m Vector of the retrieved row.</returns>
        public Vector GetRow(int row)
        {
            int columns = this.Columns;
            Vector returnVector = new Vector(columns, Vector.TYPES.RowVector);

            for (int i = 0; i < columns; i++)
            {
                returnVector[i] = this.InnerMatrix[row, i];
            }

            return returnVector;
        }

        /// <summary>
        /// Gets the full column at the specified index as an m*1 Vector.
        /// </summary>
        /// <param name="column">Column index to get.</param>
        /// <returns>m*1 Vector of the retrieved column.</returns>
        public Vector GetColumn(int column)
        {
            int rows = this.Rows;
            Vector returnVector = new Vector(rows, Vector.TYPES.ColumnVector);

            for (int i = 0; i < rows; i++)
            {
                returnVector[i] = this.InnerMatrix[i, column];
            }

            return returnVector;
        }

        /// <summary>
        /// Returns a row-Vector containing the sum of each column of this Matrix.
        /// </summary>
        /// <returns>Row-Vector with each element containing the sum of this Matrix' corresponding column.</returns>
        public Vector Sum()
        {
            return Matrix.Sum(this);
        }

        /// <summary>
        /// Returns a row-Vector containing the sum of each column of the given Matrix.
        /// </summary>
        /// <param name="m1">Matrix to get the sum of.</param>
        /// <returns>Row-Vector with each element containing the sum of the given Matrix' corresponding column.</returns>
        public static Vector Sum(Matrix m1)
        {
            int rows = m1.Rows;
            int columns = m1.Columns;
            Vector returnVector = new Vector(columns, Vector.TYPES.RowVector);

            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    returnVector[i] += m1[j, i];
                }
            }

            return returnVector;
        }

        /// <summary>
        /// Puts the given square Matrix to the power of the given scalar.
        /// </summary>
        /// <param name="m1">Matrix to get the exponentiation of.</param>
        /// <param name="scalar">Power to put the Matrix to.</param>
        /// <exception cref="ArgumentException">Thrown if the given Matrix is not a square Matrix.</exception> 
        /// <returns>The result Matrix of the exponentiation.</returns>
        public static Matrix Power(Matrix m1, double scalar)
        {
            if (m1.IsSquare)
            {
                for (int i = 0; i < scalar; i++)
                {
                    m1 *= m1;
                }

                return m1;
            }

            throw new ArgumentException("The given matrix must be square to put it to a power.", nameof(m1));
        }

        /// <summary>
        /// Element-wise power. Returns the Matrix resulting from raising each element of the given Matrix to the power of the given scalar.
        /// </summary>
        /// <param name="m1">Matrix to calculate the element-wise power of.</param>
        /// <param name="scalar">The number to raise each Matrix element to.</param>
        /// <returns>Matrix containing the result of the element-wise power calculation.</returns>
        public static Matrix EMPower(Matrix m1, double scalar)
        {
            int rows = m1.Rows;
            int columns = m1.Columns;

            Matrix returnMatrix = new Matrix(rows, columns);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    returnMatrix[i, j] = Math.Pow(m1[i, j], scalar);
                }
            }

            return returnMatrix;
        }

        /// <summary>
        /// Returns the identity matrix of the given Matrix size. Equivalent to Matrix.eye.
        /// </summary>
        /// <param name="size">The size of the identity matrix to calculate.</param>
        /// <returns>The identity matrix for the given size.</returns>
        public static Matrix IdentityMatrix(int size)
        {
            if (size > 0)
            {
                Matrix returnMatrix = new Matrix(size, size);
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        if (i == j)
                        {
                            returnMatrix[i, j] = 1;
                        }
                        else
                        {
                            returnMatrix[i, j] = 0;
                        }
                    }
                }

                return returnMatrix;
            }

            throw new ArgumentException("The Matrix size must be greater than 0.", nameof(size));
        }

        /// <summary>
        /// Returns the identity matrix of the given Matrix size. Identical to Matrix.IdentityMatrix. This alternate method name is added due to its use in MATLAB.
        /// </summary>
        /// <param name="size">The size of the identity matrix to calculate.</param>
        /// <returns>The identity matrix for the given size.</returns>
        public static Matrix eye(int size)
        {
            return Matrix.IdentityMatrix(size);
        }

        /// <summary>
        /// Returns a square Matrix of the given size with each element as a zero.
        /// </summary>
        /// <param name="size">Size of the Matrix to produce.</param>
        /// <returns>Square Matrix of the given size filled with zeroes.</returns>
        public static Matrix Zeros(int size)
        {
            if (size > 0)
            {
                Matrix returnMatrix = new Matrix(size, size);
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        returnMatrix[i, j] = 0;
                    }
                }

                return returnMatrix;
            }

            throw new ArgumentException("The Matrix size must be greater than 0.", nameof(size));
        }

        /// <summary>
        /// Returns a Matrix with the given rows and the given columns with each element as a zero.
        /// </summary>
        /// <param name="rows">The amount of rows in the produced Matrix.</param>
        /// <param name="columns">The amount of columns in the produced Matrix.</param>
        /// <returns>Matrix of the given size filled with zeroes.</returns>
        public static Matrix Zeros(int rows, int columns)
        {
            if (rows <= 0)
                { throw new ArgumentException("The Matrix height must be greater than 0.", nameof(rows)); }
            else if (columns <= 0)
                { throw new ArgumentException("The Matrix width must be greater than 0.", nameof(columns)); }

            Matrix returnMatrix = new Matrix(rows, columns);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    returnMatrix[i, j] = 0;
                }
            }

            return returnMatrix;
        }

        /// <summary>
        /// Returns a square Matrix of the given size with each element as a one.
        /// </summary>
        /// <param name="size">Size of the Matrix to produce.</param>
        /// <returns>Square Matrix of the given size filled with ones.</returns>
        public static Matrix Ones(int size)
        {
            if (size > 0)
            {
                Matrix returnMatrix = new Matrix(size, size);
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        returnMatrix[i, j] = 1;
                    }
                }

                return returnMatrix;
            }

            throw new ArgumentException("The Matrix size must be greater than 0.", nameof(size));
        }

        /// <summary>
        /// Returns a Matrix with the given rows and the given columns with each element as a one.
        /// </summary>
        /// <param name="rows">The amount of rows in the produced Matrix.</param>
        /// <param name="columns">The amount of columns in the produced Matrix.</param>
        /// <returns>Matrix of the given size filled with ones.</returns>
        public static Matrix Ones(int rows, int columns)
        {
            if (rows <= 0)
                { throw new ArgumentException("The Matrix height must be greater than 0.", nameof(rows)); }
            else if (columns <= 0)
                { throw new ArgumentException("The Matrix width must be greater than 0.", nameof(columns)); }

            Matrix returnMatrix = new Matrix(rows, columns);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    returnMatrix[i, j] = 1;
                }
            }

            return returnMatrix;
        }

        /// <summary>
        /// Returns the transposed version of the given Matrix.
        /// </summary>
        /// <param name="m1">The Matrix to transpose.</param>
        /// <returns>Matrix that is the transposed version of the given Matrix.</returns>
        public static Matrix Transpose(Matrix m1)
        {
            int rows = m1.Rows;
            int columns = m1.Columns;

            Matrix transposed = new Matrix(columns, rows);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    transposed[j, i] = m1[i, j];
                }
            }

            return transposed;
        }

        /// <summary>
        /// Returns the transposed version of this Matrix.
        /// </summary>
        /// <returns>Matrix that is the transposed version of this Matrix.</returns>
        public Matrix Transpose()
        {
            return Matrix.Transpose(this);
        }

        public static double Determinant(Matrix m1)
        {
            throw new NotImplementedException();
        }

        public static Tuple<Matrix, Matrix> LUDecomposition(Matrix m1)
        {
            // TODO: This probably doesn't work.
            if (m1.IsSquare)
            {
                int rows = m1.Rows;
                Matrix L = Matrix.Zeros(rows);
                Matrix U = Matrix.IdentityMatrix(rows);

                for (int i = 0; i < rows; i++)
                {
                    L[i, 0] = m1[i, 0];
                    U[i, i] = 1;
                }

                for (int j = 1; j < rows; j++)
                {
                    U[0, j] = m1[0, j] / L[0, 0];
                }

                for (int i = 1; i < rows; i++)
                {
                    for (int j = 1; j < i; j++)
                    {
                        L[i, j] = m1[i, j] - (L.GetRow(i) * U.GetColumn(j));
                    }

                    for (int j = i - 1; j < rows; j++)
                    {
                        U[i, j] = (m1[i, j] - (L.GetRow(i) * U.GetColumn(j))) / L[i, i];
                    }
                }

                return new Tuple<Matrix, Matrix>(L, U);
            }

            throw new ArgumentException("The given Matrix must be square to take the LU decomposition.", nameof(m1));
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
