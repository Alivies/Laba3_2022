using System;

namespace lab3 {
    public class SquareMatrixClone : SquareMatrix, ICloneable {

        public SquareMatrixClone() {

        }

        public SquareMatrixClone(string Name) : base(Name) {

        }

        public SquareMatrixClone(int Size, string Name) : base(Size, Name) {

        }

        public SquareMatrixClone(int Size, string Name, double[,] Elements) : base(Size, Name, Elements) {

        }

        public object Clone() {

            var clone = new SquareMatrixClone();

            clone.Size = this.Size;
            clone.Name = string.Copy(this.Name);
            clone.Matrix = this.Matrix;

            return clone;
        }
    }
}
