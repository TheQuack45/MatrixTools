using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixTools
{
    public class Vector : IEnumerable<double>
    {
        #region Static members definition
        public enum TYPES { RowVector, ColumnVector };
        #endregion

        #region Members definition
        protected double[] _innerVector;
        protected double[] InnerVector
        {
            get { return _innerVector; }
        }

        public double this[int index]
        {
            get { return this._innerVector[index]; }
            set { this._innerVector[index] = value; }
        }

        public int Size
        {
            get { return this.InnerVector.Length; }
        }

        public int Rows
        {
            get
            {
                int returnValue = 0;
                if (this.Type == TYPES.ColumnVector)
                {
                    returnValue = this.Size;
                }
                else if (this.Type == TYPES.RowVector)
                {
                    returnValue = 1;
                }
                return returnValue;
            }
        }

        public int Columns
        {
            get
            {
                int returnValue = 0;
                if (this.Type == TYPES.ColumnVector)
                {
                    returnValue = 1;
                }
                else if (this.Type == TYPES.RowVector)
                {
                    returnValue = this.Size;
                }
                return returnValue;
            }
        }

        protected TYPES _type;
        public TYPES Type
        {
            get { return _type; }
        }
        #endregion

        #region Constructors definition
        /// <summary>
        /// Represents a vector containing m values of type double.
        /// </summary>
        /// <param name="size">Length of the Vector.</param>
        /// <param name="type">Whether the Vector is wide (row vector) or tall (column vector).</param>
        public Vector(int size, TYPES type)
        {
            this._type = type;
            this._innerVector = new double[size];
        }

        public Vector()
        {
        }
        #endregion

        #region Operator overloads
        #region Casts definitions
        public static explicit operator Matrix(Vector v1)
        {
            Matrix returnMatrix = null;
            switch (v1.Type)
            {
                case TYPES.ColumnVector:
                    returnMatrix = new Matrix(v1.Size, 1);
                    for (int i = 0; i < v1.Size; i++)
                        { returnMatrix[i, 0] = v1[i]; }
                    break;
                case TYPES.RowVector:
                    returnMatrix = new Matrix(1, v1.Size);
                    for (int i = 0; i < v1.Size; i++)
                        { returnMatrix[0, i] = v1[i]; }
                    break;
            }
            return returnMatrix;
        }
        #endregion

        #region Standard operators
        public static Vector operator +(Vector v1, double scalar)
        {
            int size = v1.Size;
            TYPES type = v1.Type;

            Vector returnVector = new Vector(size, type);
            for (int i = 0; i < size; i++)
                { returnVector[i] = v1[i] + scalar; }

            return returnVector;
        }

        public static Vector operator +(Vector v1, Vector v2)
        {
            int size = v1.Size;
            TYPES type = v1.Type;

            if (type != v2.Type || size != v2.Size)
                { throw new ArgumentException("The vectors to add must have the same dimensions."); }

            Vector returnVector = new Vector(size, type);
            for (int i = 0; i < size; i++)
                { returnVector[i] = v1[i] + v2[i]; }

            return returnVector;
        }

        public static Vector operator -(Vector v1, double scalar)
        {
            int size = v1.Size;
            TYPES type = v1.Type;

            Vector returnVector = new Vector(size, type);
            for (int i = 0; i < size; i++)
                { returnVector[i] = v1[i] - scalar; }

            return returnVector;
        }

        public static Vector operator -(Vector v1, Vector v2)
        {
            int size = v1.Size;
            TYPES type = v1.Type;

            if (type != v2.Type || size != v2.Size)
                { throw new ArgumentException("The vectors to add must have the same dimensions."); }

            Vector returnVector = new Vector(size, type);
            for (int i = 0; i < size; i++)
            {
                returnVector[i] = v1[i] - v2[i];
            }

            return returnVector;
        }

        public static Vector operator *(Vector v1, double scalar)
        {
            int size = v1.Size;
            TYPES type = v1.Type;

            Vector returnVector = new Vector(size, type);
            for (int i = 0; i < size; i++)
                { returnVector[i] = v1[i] * scalar; }

            return returnVector;
        }

        public static double operator *(Vector v1, Vector v2)
        {
            return Vector.DotProduct(v1, v2);
        }

        public static Vector operator /(Vector v1, double scalar)
        {
            int size = v1.Size;
            TYPES type = v1.Type;

            Vector returnVector = new Vector(size, type);
            for (int i = 0; i < size; i++)
                { returnVector[i] = v1[i] / scalar; }

            return returnVector;
        }

        public static bool operator ==(Vector v1, Vector v2)
        {
            int size = v1.Size;
            TYPES type = v1.Type;

            if ((object)v1 == null || (object)v2 == null)
                { return false; }

            if (type != v2.Type || size != v2.Size)
                { return false; }

            for (int i = 0; i < size; i++)
            {
                if (v1[i] != v2[i])
                    { return false; }
            }

            return true;
        }

        public static bool operator !=(Vector v1, Vector v2)
        {
            return !(v1 == v2);
        }
        #endregion
        #endregion

        #region Methods definition
        /// <summary>
        /// Converts this Vector to a scalar if it is a Vector of length 1.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if this Vector is not of length 1.</exception> 
        /// <returns>The single value form this Vector as a double.</returns>
        public double ToScalar()
        {
            if (this.Size != 1)
                { throw new InvalidOperationException("You cannot convert a Vector to a scalar when the Vector contains more than one value."); }

            return this.InnerVector[0];
        }

        /// <summary>
        /// Sum every single element in this Vector and return the summed value.
        /// </summary>
        /// <returns>Double of the summed values from this Vector.</returns>
        public double Sum()
        {
            return Vector.Sum(this);
        }

        /// <summary>
        /// Sum every single element in the given Vector and return the summed value.
        /// </summary>
        /// <param name="v1">Vector to sum the values of.</param>
        /// <returns>Double of the summed values from the given Vector.</returns>
        public static double Sum(Vector v1)
        {
            double sum = 0;

            for (int i = 0; i < v1.Size; i++)
                { sum += v1[i]; }

            return sum;
        }

        /// <summary>
        /// Take the dot-product of this Vector and the given Vector and return it.
        /// </summary>
        /// <param name="v1">Vector to get the dot product with this instance Vector.</param>
        /// <returns>Double of the dot-product of this Vector and the given Vector.</returns>
        public double DotProduct(Vector v1)
        {
            return Vector.DotProduct(this, v1);
        }

        /// <summary>
        /// Take the dot-product of the two given Vectors and return it.
        /// </summary>
        /// <param name="v1">First Vector of the dot-product equation.</param>
        /// <param name="v2">Second Vector of the dot-product equation.</param>
        /// <returns>Double of the dot-product of the two given Vectors.</returns>
        public static double DotProduct(Vector v1, Vector v2)
        {
            int size = v1.Size;
            if (size != v2.Size)
                { throw new ArgumentException("The two Vectors must be the same size."); }

            double product = 0;
            for (int i = 0; i < size; i++)
                { product += v1[i] + v2[i]; }

            return product;
        }

        /// <summary>
        /// Take the element-wise power of this Vector and the given scalar. (Put each element in this Vector to the power of the given scalar)
        /// </summary>
        /// <param name="scalar">Power to put each element of this Vector to.</param>
        /// <returns>Vector containing the result of the element-wise exponentiation.</returns>
        public Vector EMPower(double scalar)
        {
            return Vector.EMPower(this, scalar);
        }

        /// <summary>
        /// Take the element-wise power of the given Vector and the given scalar. (Put each element in the given Vector to the power of the given scalar)
        /// </summary>
        /// <param name="v1">Vector to operate on.</param>
        /// <param name="scalar">Power to put each element of the given Vector to.</param>
        /// <returns>Vector containing the result of the element-wise exponentiation.</returns>
        public static Vector EMPower(Vector v1, double scalar)
        {
            int size = v1.Size;
            TYPES type = v1.Type;

            Vector returnVector = new Vector(size, type);
            for (int i = 0; i < size; i++)
                { returnVector[i] = Math.Pow(v1[i], scalar); }

            return returnVector;
        }

        public static Vector CrossProduct(Vector v1, Vector v2)
        {
            // TODO: this
            throw new NotImplementedException("Cross product has not been implemented yet.");
        }

        /// <summary>
        /// Returns the transposed version of the given Vector.
        /// </summary>
        /// <param name="v1">The Vector to transpose.</param>
        /// <returns>Vector that is the transposed version of the given Vector.</returns>
        public static Vector Transpose(Vector v1)
        {
            Vector transposed = null;
            switch (v1.Type)
            {
                case TYPES.ColumnVector:
                    transposed = new Vector(v1.Size, TYPES.RowVector);
                    break;
                case TYPES.RowVector:
                    transposed = new Vector(v1.Size, TYPES.ColumnVector);
                    break;
            }

            for (int i = 0; i < v1.Size; i++)
                { transposed[i] = v1[i]; }

            return transposed;
        }

        /// <summary>
        /// Returns the transposed version of this Vector.
        /// </summary>
        /// <returns>Vector that is the transposed version of this Vector.</returns>
        public Vector Transpose()
        {
            return Vector.Transpose(this);
        }

        /// <summary>
        /// If possible, swaps the value at index1 with the value at index2 within this Vector.
        /// </summary>
        /// <param name="index1">Integer index of the first element to swap.</param>
        /// <param name="index2">Integer index of the first element to swap.</param>
        /// <exception cref="ArgumentException">Thrown if either of the given indices are less than 0, or larger than the size of this Vector.</exception> 
        public void Swap(int index1, int index2)
        {
            Vector.Swap(this, index1, index2);
        }

        /// <summary>
        /// If possible, swaps the value at index1 with the value at index2 within the given Vector.
        /// </summary>
        /// <param name="v1">Vector to swap elements of.</param>
        /// <param name="index1">Integer index of the first element to swap.</param>
        /// <param name="index2">Integer index of the second element to swap.</param>
        /// <exception cref="ArgumentException">Thrown if either of the given indices are less than 0, or larger than the size of the given Vector.</exception> 
        public static void Swap(Vector v1, int index1, int index2)
        {
            if (index1 < 0)
                { throw new ArgumentException("The given indices must be greater than or equal to 0.", nameof(index1)); }
            else if (index2 < 0)
                { throw new ArgumentException("The given indices must be greater than or equal to 0.", nameof(index2)); }
            else if (index1 >= v1.Size)
                { throw new ArgumentException("Both indices must be within the size of the given Vector.", nameof(index1)); }
            else if (index2 >= v1.Size)
                { throw new ArgumentException("Both indices must be within the size of the given Vector.", nameof(index2)); }

            double tmp = v1[index1];
            v1[index1] = v1[index2];
            v1[index2] = tmp;
        }

        public string ToString(string format = "")
        {
            // TODO: Docs and unit test for this
            StringBuilder outputBuilder = new StringBuilder();
            int size = this.Size;
            TYPES type = this.Type;

            for (int i = 0; i < size; i++)
            {
                double num = this.InnerVector[i];
                if (num < 0 && i != 0)
                    { outputBuilder.Length--; }
                outputBuilder.Append(num.ToString(format));

                if (i < size - 1)
                {
                    if (type == TYPES.RowVector)
                        { outputBuilder.Append("  "); }
                    else if (type == TYPES.ColumnVector)
                        { outputBuilder.Append(Environment.NewLine); }
                }
            }

            return outputBuilder.ToString();
        }

        IEnumerator<double> IEnumerable<double>.GetEnumerator()
        {
            IEnumerator enumerator = this._innerVector.GetEnumerator();

            while (enumerator.MoveNext())
            {
                yield return (double)enumerator.Current;
            }
        }

        public IEnumerator GetEnumerator()
        {
            return this._innerVector.GetEnumerator();
        }
        #endregion
    }
}
