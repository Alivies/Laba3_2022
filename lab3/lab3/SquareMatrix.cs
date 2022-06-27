using System;

namespace lab3 {

    public class SquareMatrix : IComparable {
        public int Size { get; set; }
        public string Name { get; set; }
        public double[,] Matrix { get; set; }
        public SquareMatrix() {

        }

        public SquareMatrix(string Name) {

            var Rand = new Random();
            this.Name = Name;
            Size = Rand.Next(2, 5);
            Matrix = new double[Size, Size];

            for (var rowIndex = 0; rowIndex < Size; ++rowIndex) {

                for (var columnIndex = 0; columnIndex < Size; ++columnIndex) {

                    Matrix[rowIndex, columnIndex] = Rand.Next(-250, 250);
                }
            }
        }

        public SquareMatrix(int Size, string Name) {

            this.Name = Name;
            this.Size = Size;
            Matrix = new double[Size, Size];
            var Rand = new Random();

            for (var rowIndex = 0; rowIndex < Size; ++rowIndex) {
                for (var columnIndex = 0; columnIndex < Size; ++columnIndex) {
                    Matrix[rowIndex, columnIndex] = Rand.Next(-250, 250);
                }
            }
        }

        public SquareMatrix(int Size, string Name, double[ , ] Elements) {

            this.Name = Name;
            this.Size = Size;
            Matrix = new double[Size, Size];
            var Rand = new Random();

            for (var rowIndex = 0; rowIndex < Size; ++rowIndex) {
                for (var columnIndex = 0; columnIndex < Size; ++columnIndex) {
                    Matrix[rowIndex, columnIndex] = Elements[rowIndex, columnIndex];
                }
            }
        }

        public double SumOfElements() {
            double sum = 0;

            for (var rowIndex = 0; rowIndex < Size; ++rowIndex) {
                for (var columnIndex = 0; columnIndex < Size; ++columnIndex) {
                    sum += Matrix[rowIndex, columnIndex];
                }
            }

            return sum;
        }

        public double Determinant() {

            if (this.Size == 2) {
                return (this.Matrix[0, 0] * this.Matrix[1, 1] - this.Matrix[0, 1] * this.Matrix[1, 0]);
            }
            var Matrix = new SquareMatrix(this.Size - 1, "Result");
            return Matrix.SumOfElements();
        }

        public SquareMatrix Transpose() {

            var tMatrix = new SquareMatrix(this.Size, $"{this.Name} transposed");
            for (var rowIndex = 0; rowIndex < this.Size; ++rowIndex) {
                for (var columnIndex = 0; columnIndex < this.Size; ++columnIndex) {
                    tMatrix.Matrix[columnIndex, rowIndex] = this.Matrix[rowIndex, columnIndex];
                }
            }

            return tMatrix;
        }

        public override string ToString() {

            var elementCount = 1;
            var Result = "";
            for (var rowIndex = 0; rowIndex < this.Size; ++rowIndex) {
                for (var columnIndex = 0; columnIndex < this.Size; ++columnIndex) {
                    Result += ($"Element {elementCount}: {this.Matrix[rowIndex, columnIndex]}  ");
                    ++elementCount;
                }
            }

            return Result;
        }

        public int CompareTo(object obj) {

            if (obj is SquareMatrix) {
                var param = obj as SquareMatrix;
                if (param.SumOfElements() > this.SumOfElements()) {
                    return -1;
                }
                if (param.SumOfElements() < this.SumOfElements()) {
                    return 1;
                }
                if (param.SumOfElements() == this.SumOfElements()) {
                    return 0;
                }
            }
            return -1;
        }

        public override bool Equals(object obj) {
            
            if (obj is SquareMatrix) {
                var param = obj as SquareMatrix;
                if (param.Size != this.Size) {
                    return false;
                }

                for (var rowIndex = 0; rowIndex < param.Size; ++rowIndex) {
                    for (var columnIndex = 0; columnIndex < param.Size; ++columnIndex) {
                        if (param.Matrix[rowIndex, columnIndex] != this.Matrix[rowIndex, columnIndex]) {
                            return false;
                        }
                    }
                }
                return true;
            }
            return false;
        }
        public override int GetHashCode() {
            return (Int32)this.SumOfElements();
        }

        public static SquareMatrix operator + (SquareMatrix Left, SquareMatrix Right) {

            if (Left.Size != Right.Size) {
                throw new SquareMatrixSizeException("Матрицы должны быть одинакового размера");
            }

            var ElementsAmount = Left.Size * Left.Size;
            double[ , ] Elements = new double[Left.Size, Left.Size];
            var ElementsCount = 0;
            for (var rowIndex = 0; rowIndex < Left.Size; ++rowIndex) {
                for (var columnIndex = 0; columnIndex < Left.Size; ++columnIndex) {
                    Elements[rowIndex, columnIndex] = Left.Matrix[rowIndex, columnIndex] + Right.Matrix[rowIndex, columnIndex];
                    ++ElementsCount;
                }
            }
            var Name = "Result";
            return new SquareMatrix(Left.Size, Name, Elements);
        }

        public static SquareMatrix operator - (SquareMatrix Left, SquareMatrix Right) {
            if (Left.Size != Right.Size) {
                throw new SquareMatrixSizeException("Матрицы должны быть одинакового размера");
            }

            var ElementsAmount = Left.Size * Left.Size;
           
            double[,] Elements = new double[Left.Size, Left.Size];
            var ElementsCount = 0;
            for (var rowIndex = 0; rowIndex < Left.Size; ++rowIndex) {
                for (var columnIndex = 0; columnIndex < Left.Size; ++columnIndex) {
                    Elements[rowIndex, columnIndex] = Left.Matrix[rowIndex, columnIndex] - Right.Matrix[rowIndex, columnIndex];
                    ++ElementsCount;
                }
            }
            var Name = "Result";
            return new SquareMatrix(Left.Size, Name, Elements);
        }

        public static SquareMatrix operator * (SquareMatrix Left, SquareMatrix Right) {
            if (Left.Size != Right.Size) {
                throw new SquareMatrixSizeException("Матрицы должны быть одинакового размера");
            }

            var ElementsAmount = Left.Size * Left.Size;

            double[,] Elements = new double[Left.Size, Left.Size];
            var ElementsCount = 0;
            for (var rowIndex = 0; rowIndex < Left.Size; ++rowIndex) {
                for (var columnIndex = 0; columnIndex < Left.Size; ++columnIndex) {
                    Elements[rowIndex, columnIndex] = Left.Matrix[rowIndex, columnIndex] * Right.Matrix[rowIndex, columnIndex];
                    ++ElementsCount;
                }
            }
            var Name = "Result";
            return new SquareMatrix(Left.Size, Name, Elements);
        }

        public static bool operator > (SquareMatrix Left, SquareMatrix Right) {
            if (Left.SumOfElements() > Right.SumOfElements()) {
                return true;
            }
            return false;
        }

        public static bool operator < (SquareMatrix Left, SquareMatrix Right) {
            if (Left.SumOfElements() < Right.SumOfElements()) {
                return true;
            }
            return false;
        }

        public static bool operator >= (SquareMatrix Left, SquareMatrix Right) {
            if (Left.SumOfElements() >= Right.SumOfElements()) {
                return true;
            }
            return false;
        }

        public static bool operator <= (SquareMatrix Left, SquareMatrix Right) {

            if (Left.SumOfElements() <= Right.SumOfElements()) {
                return true;
            }
            return false;
        }

        public static bool operator == (SquareMatrix Left, SquareMatrix Right) {

            if (Left.Size != Right.Size) {
                return false;
            }

            for (var rowIndex = 0; rowIndex < Left.Size; ++rowIndex) {
                for (var columnIndex = 0; columnIndex < Left.Size; ++columnIndex) {
                    if (Left.Matrix[rowIndex, columnIndex] != Right.Matrix[rowIndex, columnIndex]) {
                        return false;
                    }
                }
            }
            return true;
        }

        public static bool operator != (SquareMatrix Left, SquareMatrix Right) {

            if (Left.Size != Right.Size) {
                return true;
            }

            for (var rowIndex = 0; rowIndex < Left.Size; ++rowIndex) {
                for (var columnIndex = 0; columnIndex < Left.Size; ++columnIndex) {
                    if (Left.Matrix[rowIndex, columnIndex] != Right.Matrix[rowIndex, columnIndex]) {
                        return true;
                    }
                }
            }

            return false;
        }

        public static bool operator true (SquareMatrix Matrix) {

            return (Matrix.Determinant() != 0);
        }

        public static bool operator false (SquareMatrix Matrix) {

            return (Matrix.Determinant() == 0);
        }

        public static implicit operator string (SquareMatrix Matrix) {

            var elementCount = 1;
            var Result = "";
            for (var rowIndex = 0; rowIndex < Matrix.Size; ++rowIndex) {
                for (var columnIndex = 0; columnIndex < Matrix.Size; ++columnIndex) {
                    Result += ($"Element {elementCount}: {Matrix.Matrix[rowIndex, columnIndex]}  ");
                    ++elementCount;
                }
            }

            return Result;
        }

        public static implicit operator SquareMatrix (double[ , ] Elements) {

            var ElementsAmount = Elements.Length;
            if (ElementsAmount % 2 == 0) {
                var Size = ElementsAmount / 2;
                return new SquareMatrix(Size, "Result", Elements);
            } else {
                throw new SquareMatrixSizeException("Размеры массива должны быть одинаковой длины.");
            }
        }

        public void PrintMatrix() {

            var row = "";
            Console.WriteLine($"Matrix {this.Name}:\n");
            for (var rowIndex = 0; rowIndex < this.Size; ++rowIndex) {
                for (var columnIndex = 0; columnIndex < this.Size; ++columnIndex) {
                    row += $"{this.Matrix[rowIndex, columnIndex]}\t";
                }

                Console.WriteLine(row);
                Console.WriteLine();

                row = "";
            }
        }
    }
}
