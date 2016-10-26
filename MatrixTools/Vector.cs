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
        public Vector(int size, TYPES type)
        {
            this._type = type;
            this._innerVector = new double[size];
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
                    {
                        returnMatrix[i, 0] = v1[i];
                    }
                    break;
                case TYPES.RowVector:
                    returnMatrix = new Matrix(1, v1.Size);
                    for (int i = 0; i < v1.Size; i++)
                    {
                        returnMatrix[0, i] = v1[i];
                    }
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
            {
                returnVector[i] = v1[i] + scalar;
            }

            return returnVector;
        }

        public static Vector operator +(Vector v1, Vector v2)
        {
            int size = v1.Size;
            TYPES type = v1.Type;

            if (type == v2.Type && size == v2.Size)
            {
                Vector returnVector = new Vector(size, type);
                for (int i = 0; i < size; i++)
                {
                    returnVector[i] = v1[i] + v2[i];
                }

                return returnVector;
            }

            throw new ArgumentException("The vectors to add must have the same dimensions.");
        }

        public static Vector operator -(Vector v1, double scalar)
        {
            int size = v1.Size;
            TYPES type = v1.Type;

            Vector returnVector = new Vector(size, type);
            for (int i = 0; i < size; i++)
            {
                returnVector[i] = v1[i] - scalar;
            }

            return returnVector;
        }

        public static Vector operator -(Vector v1, Vector v2)
        {
            int size = v1.Size;
            TYPES type = v1.Type;

            if (type == v2.Type && size == v2.Size)
            {
                Vector returnVector = new Vector(size, type);
                for (int i = 0; i < size; i++)
                {
                    returnVector[i] = v1[i] - v2[i];
                }

                return returnVector;
            }

            throw new ArgumentException("The vectors to add must have the same dimensions.");
        }

        public static Vector operator *(Vector v1, double scalar)
        {
            int size = v1.Size;
            TYPES type = v1.Type;

            Vector returnVector = new Vector(size, type);
            for (int i = 0; i < size; i++)
            {
                returnVector[i] = v1[i] * scalar;
            }

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
            {
                returnVector[i] = v1[i] / scalar;
            }

            return returnVector;
        }

        public static bool operator ==(Vector v1, Vector v2)
        {
            int size = v1.Size;
            TYPES type = v1.Type;

            if (type == v2.Type && size == v2.Size)
            {
                for (int i = 0; i < size; i++)
                {
                    if (v1[i] != v2[i])
                        { return false; }
                }

                return true;
            }

            return false;
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
            if (this.Size == 1)
            {
                return this.InnerVector[0];
            }

            throw new InvalidOperationException("You cannot convert a Vector to a scalar when the Vector contains more than one value.");
        }

        public double Sum()
        {
            return Vector.Sum(this);
        }

        public static double Sum(Vector v1)
        {
            double sum = 0;
            
            for (int i = 0; i < v1.Size; i++)
            {
                sum += v1[i];
            }

            return sum;
        }

        public double DotProduct(Vector v1)
        {
            return Vector.DotProduct(this, v1);
        }

        public static Vector EMPower(Vector v1, double scalar)
        {
            int size = v1.Size;
            TYPES type = v1.Type;

            Vector returnVector = new Vector(size, type);
            for (int i = 0; i < size; i++)
            {
                returnVector[i] = Math.Pow(v1[i], scalar);
            }

            return returnVector;
        }

        public static double DotProduct(Vector v1, Vector v2)
        {
            int size = v1.Size;
            if (size == v2.Size)
            {
                double product = 0;
                for (int i = 0; i < size; i++)
                {
                    product += v1[i] + v2[i];
                }

                return product;
            }

            throw new ArgumentException("The two Vectors must be the same size.");
        }

        public static Vector CrossProduct(Vector v1, Vector v2)
        {
            throw new NotImplementedException("Cross product has not been implemented yet.");
        }

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
            {
                transposed[i] = v1[i];
            }

            return transposed;
        }

        public Vector Transpose()
        {
            return Vector.Transpose(this);
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
