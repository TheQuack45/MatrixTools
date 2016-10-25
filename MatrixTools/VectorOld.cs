using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixTools
{
    public class Vector : Matrix
    {
        #region Static members definition
        public enum TYPES { RowVector, ColumnVector };
        #endregion
        
        #region Members definition
        public double this[int index]
        {
            get
            {
                double returnDouble = 0;
                if (this.Type == TYPES.ColumnVector)
                    { returnDouble = this.InnerMatrix[index, 0]; }
                else if (this.Type == TYPES.RowVector)
                    { returnDouble = this.InnerMatrix[0, index]; }
                return returnDouble;
            }
            set
            {
                if (this.Type == TYPES.ColumnVector)
                    { this.InnerMatrix[index, 0] = value; }
                else if (this.Type == TYPES.RowVector)
                    { this.InnerMatrix[0, index] = value; }
                else
                {
                    // This shouldn't ever happen.
                    this.InnerMatrix[index, index] = value;
                }
            }
        }

        private TYPES _type;
        public TYPES Type
        {
            get { return _type; }
        }

        public new int Size
        {
            get
            {
                int size = 0;
                if (this.Type == TYPES.ColumnVector)
                    { size = this.Rows; }
                else if (this.Type == TYPES.RowVector)
                    { size = this.Columns; }
                return size;
            }
        }
        #endregion

        #region Constructors definition
        public Vector(int size, TYPES type)
        {
           this._type = type;

           switch (type)
           {
                case TYPES.ColumnVector:
                    this._innerMatrix = new double[size, 1];
                    break;
                case TYPES.RowVector:
                    this._innerMatrix = new double[1, size];
                    break;
           }
        }
        #endregion

        #region Methods definition
        public new double Sum()
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
        #endregion
    }
}
