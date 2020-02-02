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
        #region Static members definition
        public enum MERGE_TYPE { Column, Row };
        #endregion

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
        public static explicit operator Vector(Matrix m1)
        {
            Vector returnVector = null;

            if (m1.Width == 1)
            {
                // Column Vector
                returnVector = new Vector(m1.Height, Vector.TYPES.ColumnVector);

                for (int i = 0; i < m1.Height; i++)
                {
                    returnVector[i] = m1[i, 0];
                }
            }
            else if (m1.Height == 1)
            {
                // Row vector
                returnVector = new Vector(m1.Width, Vector.TYPES.RowVector);

                for (int i = 0; i < m1.Width; i++)
                {
                    returnVector[i] = m1[0, i];
                }
            }

            return returnVector;
        }
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
            if ((rows != m2.Rows) || (columns != m2.Columns))
                { throw new ArgumentException("The matrices to add must have the same dimensions."); }

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
            if ((rows != m2.Rows) || (columns != m2.Columns))
                { throw new ArgumentException("The matrices to add must have the same dimensions."); }

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

            if (columns != v1.Size)
                { throw new ArgumentException("The height of the vector must match the width of the Matrix."); }

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

        public static Matrix operator *(Matrix m1, Matrix m2)
        {
            if (m1.Columns != m2.Rows)
                { throw new ArgumentException("The width of the first matrix must match the height of the second matrix."); }

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

        //public static bool operator ==(Matrix m1, Matrix m2)
        //{
        //    if ((object)m1 == null || (object)m2 == null)
        //    {
        //        return false;
        //    }

        //    if (m1.Rows != m2.Rows || m1.Columns != m2.Columns)
        //        { return false; }

        //    for (int i = 0; i < m1.Rows; i++)
        //    {
        //        for (int j = 0; j < m1.Columns; j++)
        //        {
        //            if (m1[i, j] != m2[i, j])
        //                { return false; }
        //        }
        //    }

        //    return true;
        //}

        //public static bool operator !=(Matrix m1, Matrix m2)
        //{
        //    return !(m1 == m2);
        //}

        public static Matrix operator ==(Matrix m1, Matrix m2)
        {
            if ((object)m1 == null)
                { throw new ArgumentNullException("The given matrices cannot be null.", nameof(m1)); }
            else if ((object)m2 == null)
                { throw new ArgumentNullException("The given matrices cannot be null.", nameof(m2)); }

            if (m1.Rows != m2.Rows)
                { throw new ArgumentException("The row count of the given matrices must be equal."); }
            else if (m1.Columns != m2.Columns)
                { throw new ArgumentException("The column count of the given matrices must be equal."); }

            Matrix returnMatrix = new Matrix(m1.Rows, m1.Columns);
            for (int cRow = 0; cRow < m1.Rows; cRow++)
            {
                for (int cCol = 0; cCol < m1.Columns; cCol++)
                {
                    returnMatrix[cRow, cCol] = ((m1[cRow, cCol] == m2[cRow, cCol]) == true ? 1.0 : 0.0);
                }
            }

            return returnMatrix;
        }

        public static Matrix operator !=(Matrix m1, Matrix m2)
        {
            Matrix equalities = (m1 == m2);
            for (int cRow = 0; cRow < equalities.Rows; cRow++)
            {
                for (int cCol = 0; cCol < equalities.Columns; cCol++)
                {
                    equalities[cRow, cCol] = (equalities[cRow, cCol] == 1.0) ? 0.0 : 1.0;
                }
            }

            return equalities;
        }
        #endregion
        #endregion

        // TODO: Add null checks to all these methods.
        #region Methods definition
        public static Matrix Merge(Matrix m1, Matrix m2, MERGE_TYPE type)
        {
            if ((object)m1 == null)
                { throw new ArgumentNullException("The given matrices cannot be null.", nameof(m1)); }
            else if ((object)m2 == null)
                { throw new ArgumentNullException("The given matrices cannot be null.", nameof(m2)); }

            Matrix returnMatrix = null;
            if (type == MERGE_TYPE.Column)
            {
                if (m1.Rows != m2.Rows)
                    { throw new ArgumentException("The row count of both matrices must be equal to perform a column merge."); }
                
                // Column merge (m2 is appended to the right side of m1)
                returnMatrix = new Matrix(m1.Rows, (m1.Columns + m2.Columns));

                for (int cRow = 0; cRow < returnMatrix.Rows; cRow++)
                {
                    for (int cCol = 0; cCol < m1.Columns; cCol++)
                        { returnMatrix[cRow, cCol] = m1[cRow, cCol]; }

                    for (int cCol = 0; cCol < m2.Columns; cCol++)
                        { returnMatrix[cRow, (cCol + m1.Columns - 1)] = m2[cRow, cCol]; }
                }
            }
            else if (type == MERGE_TYPE.Row)
            {
                if (m1.Columns != m2.Columns)
                    { throw new ArgumentException("The column count of both matrices must be equal to perform a row merge."); }
                
                // Row merge (m2 is appended to the bottom of m1)
                returnMatrix = new Matrix((m1.Rows + m2.Rows), m1.Columns);

                for (int cCol = 0; cCol < returnMatrix.Columns; cCol++)
                {
                    for (int cRow = 0; cRow < m1.Rows; cRow++)
                        { returnMatrix[cRow, cCol] = m1[cRow, cCol]; }

                    for (int cRow = 0; cRow < m2.Rows; cRow++)
                        { returnMatrix[(cRow + m1.Rows - 1), cCol] = m2[cRow, cCol]; }
                }
            }

            return returnMatrix;
        }
        
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
            if (this.Rows != 1 || this.Columns != 1)
                { throw new InvalidOperationException("You cannot convert a Matrix to a scalar when the Matrix contains more than one value."); }

            return this.InnerMatrix[0, 0];
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
            if ((object)m1 == null)
                { throw new ArgumentNullException("The given Matrix cannot be null.", nameof(m1)); }

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
                { returnVector[i] = this.InnerMatrix[row, i]; }

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
                { returnVector[i] = this.InnerMatrix[i, column]; }

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
            if ((object)m1 == null)
                { throw new ArgumentNullException("The given matrix cannot be null.", nameof(m1)); }

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
            if ((object)m1 == null)
                { throw new ArgumentNullException("The given Matrix cannot be null.", nameof(m1)); }

            if (!m1.IsSquare)
                { throw new ArgumentException("The given matrix must be square to put it to a power.", nameof(m1)); }

            // TODO: This affects the data in the given matrix; it does not make a clone of it.
            for (int i = 0; i < scalar; i++)
                { m1 *= m1; }

            return m1;
        }

        /// <summary>
        /// Element-wise power. Returns the Matrix resulting from raising each element of the given Matrix to the power of the given scalar.
        /// </summary>
        /// <param name="m1">Matrix to calculate the element-wise power of.</param>
        /// <param name="scalar">The number to raise each Matrix element to.</param>
        /// <returns>Matrix containing the result of the element-wise power calculation.</returns>
        public static Matrix EMPower(Matrix m1, double scalar)
        {
            if ((object)m1 == null)
                { throw new ArgumentNullException("The given Matrix cannot be null.", nameof(m1)); }

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
            if (size <= 0)
                { throw new ArgumentException("The Matrix size must be greater than 0.", nameof(size)); }

            Matrix returnMatrix = Matrix.Zeros(size);
            for (int i = 0; i < size; i++)
            {
                returnMatrix[i, i] = 1;
            }

            return returnMatrix;
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
                { throw new ArgumentException("The Matrix size must be greater than 0.", nameof(size)); }

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
            if (size <= 0)
                { throw new ArgumentException("The Matrix size must be greater than 0.", nameof(size)); }
            
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
            if ((object)m1 == null)
                { throw new ArgumentNullException("The given Matrix cannot be null.", nameof(m1)); }

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

        /// <summary>
        /// If possible, swaps the row or column at the given index of this Matrix with the given Vector.
        /// </summary>
        /// <param name="v1">Vector to swap the row/column of this Matrix with.</param>
        /// <param name="index">Index of the row/column to swap.</param>
        /// <exception cref="ArgumentException">
        /// Thrown if the applicable dimension of the given Matrix does not match the size of the given Vector,
        /// if the type of the Vector is invalid,
        /// or if the given index is less than 0.
        /// </exception> 
        public void Swap(Vector v1, int index)
        {
            Matrix.Swap(this, v1, index);
        }

        /// <summary>
        /// If possible, swaps the row or column at the given index of the given Matrix with the given Vector.
        /// </summary>
        /// <param name="m1">Matrix to swap a row/column of.</param>
        /// <param name="v1">Vector to swap the row/column of the given Matrix with.</param>
        /// <param name="index">Index of the row/column to swap.</param>
        /// <exception cref="ArgumentException">
        /// Thrown if the applicable dimension of the given Matrix does not match the size of the given Vector,
        /// if the type of the Vector is invalid,
        /// or if the given index is less than 0.
        /// </exception> 
        public static void Swap(Matrix m1, Vector v1, int index)
        {
            if ((object)m1 == null)
                { throw new ArgumentNullException("The given Matrix cannot be null.", nameof(m1)); }
            if ((object)v1 == null)
                { throw new ArgumentNullException("The given Vector cannot be null.", nameof(v1)); }

            if (index < 0)
                { throw new ArgumentException("The given index must be greater than or equal to 0.", nameof(index)); }

            if (v1.Type == Vector.TYPES.ColumnVector)
            {
                int rows = m1.Rows;
                if (rows != v1.Size)
                    { throw new ArgumentException("The size of the given Vector must match the applicable dimension of the given Matrix."); }

                for (int i = 0; i < rows; i++)
                    { m1[i, index] = v1[i]; }
            } 
            else if (v1.Type == Vector.TYPES.RowVector)
            {
                int columns = m1.Columns;
                if (columns != v1.Size)
                    { throw new ArgumentException("The size of the given Vector must match the applicable dimension of the given Matrix."); }

                for (int i = 0; i < columns; i++)
                    { m1[index, i] = v1[i]; }
            }
            else
            {
                throw new ArgumentException("Vector type is invalid.", nameof(v1));
            }
        }

        /// <summary>
        /// If possible, swaps the row o column at index1 with the row or column at index2 within this Matrix.
        /// </summary>
        /// <param name="index1">Integer index of the first row/column to swap.</param>
        /// <param name="index2">Integer index of the first row/column to swap.</param>
        /// <param name="isRow">Boolean of whether this is swapping a row (true) or a column (false).</param>
        /// <exception cref="ArgumentException">Thrown if either of the given indices are less than 0, or larger than the applicable dimension of this Matrix.</exception> 
        public void Swap(int index1, int index2, bool isRow)
        {
            Matrix.Swap(this, index1, index2, isRow);
        }

        /// <summary>
        /// If possible, swaps the row or column at index1 with the row or column at index2 within the given Matrix.
        /// </summary>
        /// <param name="m1">Matrix to swap the rows or columns of.</param>
        /// <param name="index1">Integer index of the first row/column to swap.</param>
        /// <param name="index2">Integer index of the second row/column to swap.</param>
        /// <param name="isRow">Boolean of whether this is swapping a row (true) or a column (false).</param>
        /// <exception cref="ArgumentException">Thrown if either of the given indices are less than 0, or larger than the applicable dimension of the given Matrix.</exception> 
        public static void Swap(Matrix m1, int index1, int index2, bool isRow)
        {
            if (index1 < 0)
                { throw new ArgumentException("The given indices must be greater than or equal to 0.", nameof(index1)); }
            else if (index2 < 0)
                { throw new ArgumentException("The given indices must be greater than or equal to 0.", nameof(index2)); }
            else if ((object)m1 == null)
                { throw new ArgumentNullException("The given Matrix cannot be null.", nameof(m1)); }
            
            if (isRow)
            {
                // Swapping rows
                int columns = m1.Columns;
                if (index1 >= columns)
                    { throw new ArgumentException("Both indices must be within the width of the given Matrix if doing a row swap.", nameof(index1)); }
                else if (index2 >= columns)
                    { throw new ArgumentException("Both indices must be within the height of the given Matrix if doing a row swap.", nameof(index2)); }
                
                Vector r1 = m1.GetRow(index1);
                Vector r2 = m1.GetRow(index2);
                for (int i = 0; i < columns; i++)
                {
                    m1[index1, i] = r2[i];
                    m1[index2, i] = r1[i];
                } 
            }
            else
            {
                // Swapping columns
                int rows = m1.Rows;
                if (index1 >= rows)
                    { throw new ArgumentException("Both indices must be within the height of the given Matrix if doing a column swap.", nameof(index1)); }
                else if (index2 >= rows)
                    { throw new ArgumentException("Both indices must be within the height of the given Matrix if doing a column swap.", nameof(index2)); }
                
                Vector c1 = m1.GetColumn(index1);
                Vector c2 = m1.GetColumn(index2);
                for (int i = 0; i < rows; i++)
                {
                    m1[i, index1] = c2[i];
                    m1[i, index2] = c1[i];
                }
            }
        }

        /// <summary>
        /// Creates a copy of the given Matrix and returns it.
        /// </summary>
        /// <param name="m1">Matrix to copy.</param>
        /// <returns>A copy of the given Matrix.</returns>
        public static Matrix From(Matrix m1)
        {
            if ((object)m1 == null)
                { throw new ArgumentNullException("The given Matrix cannot be null.", nameof(m1)); }

            int rows = m1.Rows;
            int columns = m1.Columns;
            Matrix returnMatrix = new Matrix(rows, columns);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    returnMatrix[i, j] = m1[i, j];
                }
            }

            return returnMatrix;
        }

        /// <summary>
        /// If possible, calculates the determinant of the given square Matrix.
        /// </summary>
        /// <param name="m1">Matrix to calculate the determinant of.</param>
        /// <exception cref="ArgumentException">Thrown if the given Matrix is not square.</exception>
        /// <exception cref="ArgumentNullException">Thrown if the given Matrix is null.</exception> 
        /// <returns>The determinant of the given matrix.</returns>
        public static double Determinant(Matrix m1)
        {
            if ((object)m1 == null)
                { throw new ArgumentNullException("The given Matrix cannot be null.", nameof(m1)); }
            if (!m1.IsSquare)
                { throw new ArgumentException("The given Matrix must be square to calculate the determinant.", nameof(m1)); }

            Tuple<int, Matrix, Vector> decompResult = Matrix.LUDecomposition(m1);
            double det = decompResult.Item1;
            for (int i = 0;  i < decompResult.Item2.Rows; i++)
                { det *= decompResult.Item2[i, i]; }

            return det;
        }

        /// <summary>
        /// If possible, calculates the determinant of the given square Matrix with the given det value.
        /// </summary>
        /// <param name="det">The initial toggle value. Equivalent to Item1 from the LUDecomposition return Tuple.</param>
        /// <param name="m1">The Matrix to calculate the determinant of.</param>
        /// <returns></returns>
        public static double Determinant(double det, Matrix m1)
        {
            if ((object)m1 == null)
                { throw new ArgumentNullException("The given Matrix cannot be null.", nameof(m1)); }
            if (!m1.IsSquare)
                { throw new ArgumentException("The given Matrix must be square to calculate the determinant.", nameof(m1)); }
            if (det != 1 && det != -1)
                { throw new ArgumentException("The given det value must be either positive or negative 1.", nameof(det)); }

            for (int i = 0; i < m1.Rows; i++)
                { det *= m1[i, i]; }

            return det;
        }

        /// <summary>
        /// Calculates the LU decomposition of the given Matrix using Crout's algorithm.
        /// Credit to Dr. James McCaffrey of Microsoft Research for the initial implementation of this algorithm,
        /// which was adjusted to fit this library's use case.
        /// The original implementation is available at <see cref="https://msdn.microsoft.com/en-us/magazine/mt736457.aspx?f=255&MSPPError=-2147217396">MSDN</see>.
        /// </summary>
        /// <param name="m1">Matrix to calculate the LU decomposition of.</param>
        /// <exception cref="ArgumentException">Thrown if the given Matrix is not square.</exception> 
        /// <returns>
        /// Tuple with Item1 an int for the permutation toggle,
        /// Item2 a Matrix that is the combined L and U matrices resulting from the calculation,
        /// and Item3 a Vector that is the permutations for each row of the given Matrix.
        /// </returns>
        public static Tuple<int, Matrix, Vector> LUDecomposition(Matrix m1)
        {
            if ((object)m1 == null)
                { throw new ArgumentException("The given Matrix must be square to take the LU decomposition.", nameof(m1)); }

            if (!m1.IsSquare)
                { throw new ArgumentNullException("The given Matrix cannot be null.", nameof(m1)); }

            int toggle = +1;
            int size = m1.Rows;
            Matrix lum = Matrix.From(m1);

            Vector perm = new Vector(size, Vector.TYPES.RowVector);
            for (int i = 0; i < size; i++)
                { perm[i] = i; }

            for (int j = 0; j < size - 1; j++)
            {
                double max = Math.Abs(lum[j, j]);
                int pivot = j;

                for (int i = j + 1; i < size; i++)
                {
                    double xij = Math.Abs(lum[i, j]);
                    if (xij > max)
                    {
                        max = xij;
                        pivot = i;
                    }
                }

                if (pivot != j)
                {
                    // Swap rows j and pivot in Matrix `lum` and elements j and pivot in Vector `perm`.
                    lum.Swap(pivot, j, isRow: true);
                    perm.Swap(pivot, j);

                    toggle = -toggle;
                }

                double xjj = lum[j, j];
                if (xjj != 0.0)
                {
                    for (int i = j + 1; i < size; i++)
                    {
                        double xij = lum[i, j] / xjj;
                        lum[i, j] = xij;
                        for (int k = j + 1; k < size; k++)
                            { lum[i, k] -= xij * lum[j, k]; }
                    }
                }
            }

            return new Tuple<int, Matrix, Vector>(toggle, lum, perm);
        }

        public static Matrix Inverse(Matrix m1)
        {
            if ((object)m1 != null)
                { throw new ArgumentNullException("The given Matrix cannot be null.", nameof(m1)); }

            Tuple<int, Matrix, Vector> decompResult = Matrix.LUDecomposition(m1);
            double det = Matrix.Determinant(decompResult.Item1, decompResult.Item2);

            if (det == 0)
                { return null; }
            
            throw new NotImplementedException("I haven't done this yet.");
        }

        /// <summary>
        /// Converts this Matrix to a string with each element being formatted using Double.Format using the given format string.
        /// If no format string is given, the default formatting is used.
        /// </summary>
        /// <param name="format">Format string to use for each element.</param>
        /// <returns>The string representation of this Matrix.</returns>
        public string ToString(string format = "")
        {
            StringBuilder outputBuilder = new StringBuilder();
            int rows = this.Rows;
            int columns = this.Columns;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    double num = this.InnerMatrix[i, j];
                    if (num < 0)
                        { outputBuilder.Length--; }
                    outputBuilder.Append(num.ToString(format));
                    if (j < columns - 1)
                        { outputBuilder.Append("  "); }
                }

                if (i < rows)
                    { outputBuilder.Append(Environment.NewLine); }
            }

            return outputBuilder.ToString();
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
