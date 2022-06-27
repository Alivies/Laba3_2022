using System;

namespace lab3 {
    public class MatrixCalculator {

        private static MatrixCalculator Instance;

        private MatrixCalculator() {

        }

        public static MatrixCalculator GetInstance {

            get {

                if (Instance == null) {

                    Instance = new MatrixCalculator();
                }

                return Instance;
            }
        }

        private SquareMatrixClone CreateSquareMatrix() {

            var NotSet = true;

            Console.WriteLine("Введите имя матрицы: ");
            var Name = Console.ReadLine();

            Console.WriteLine("\n");
            Console.WriteLine("Создать рандомную матрицу? \n");
            Console.WriteLine("Нет        0");
            Console.WriteLine("Да        1");

            while (NotSet) {

                switch (Console.ReadLine()) {

                    case "0":
                        NotSet = false;
                        break;
                    case "1":
                        return new SquareMatrixClone(Name);
                    default:
                        Console.WriteLine("Ошибка, попробуйте снова.");
                        break;
                }
            }

            NotSet = true;

            var Size = 0;

            while (NotSet) {

                Console.WriteLine("\n");
                Console.WriteLine("Введите длину матрицы: ");

                if (!int.TryParse(Console.ReadLine(), out Size) || Size <= 1) {

                    Console.WriteLine("Неправильное значение, или формат, попробуйте снова.");
                } else {

                    NotSet = false;
                }
            }

            NotSet = true;

            Console.WriteLine("\n");
            Console.WriteLine("Генерация рандомных элементов? \n");
            Console.WriteLine("Нет        0");
            Console.WriteLine("Да        1");

            while (NotSet) {

                switch (Console.ReadLine()) {

                    case "0":
                        NotSet = false;
                        break;
                    case "1":
                        return new SquareMatrixClone(Size, Name);
                    default:
                        Console.WriteLine("Ошибка 2.");
                        break;
                }
            }

            NotSet = true;

            var Elements = new double[Size, Size];
            double currentElement;
            for (var rowIndex = 0; rowIndex < Size; ++rowIndex) {

                for (var columnIndex = 0; columnIndex < Size; ++columnIndex) {

                    while (NotSet) {

                        Console.WriteLine($"Enter element {rowIndex}{columnIndex}: ");

                        if (!double.TryParse(Console.ReadLine(), out currentElement)) {

                            Console.WriteLine("Ошибка значения.");
                        } else {

                            Elements[rowIndex, columnIndex] = currentElement;

                            NotSet = false;
                        }
                    }

                    NotSet = true;
                }
            }

            return new SquareMatrixClone(Size, Name, Elements);
        }

        private void GetMatrixInfo(SquareMatrixClone Matrix) {

            Console.WriteLine($"Матрица {Matrix.Name}");
            Console.WriteLine($"Определитель: {Matrix.Determinant()}");
            Console.WriteLine($"Хеш: {Matrix.GetHashCode()}");
            Console.WriteLine($"Сумма элементов: {Matrix.SumOfElements()}");
            Console.WriteLine($"Строка: {Matrix}");
        }

        private string Comparison(SquareMatrixClone Left, SquareMatrixClone Right) {

            if (Left > Right) {

                return $"{Left.Name} > {Right.Name}";
            } else if (Left < Right) {

                return $"{Left.Name} < {Right.Name}";
            } else {

                return $"{Left.Name} = {Right.Name}";
            }
        }

        public void Calculator() {

            Console.WriteLine("Создать 1ю матрицу:\n");
            var Left = CreateSquareMatrix();

            Console.Clear();

            Console.WriteLine("Создать 2ю матирцу:\n");
            var Right = CreateSquareMatrix();

            Console.Clear();

            Left.PrintMatrix();
            Console.WriteLine("\n");
            Right.PrintMatrix();
            Console.WriteLine("\n");

            Console.WriteLine("Создать           0");
            Console.WriteLine("Вычесть           1");
            Console.WriteLine("Умножение         2");
            Console.WriteLine("Сравнение         3");
            Console.WriteLine("Данные            4");
            Console.WriteLine("Транспонировать   5");
            Console.WriteLine("Выйти             6");

            var Option = true;

            while (true) {
                while (Option) {

                    Console.WriteLine("\n");
                    Console.WriteLine("Выберите вариант");

                    switch (Console.ReadLine()) {

                        case "0":
                            try {
                                var Result = (SquareMatrix)Left.Clone();
                                Result += Right;

                                Result.PrintMatrix();
                            }
                            catch (SquareMatrixSizeException Exception) {
                                Console.WriteLine(Exception.Message);

                                break;
                            }

                            Option = false;
                            break;
                        case "1":
                            try {
                                var Result = (SquareMatrix)Left.Clone();
                                Result -= Right;

                                Result.PrintMatrix();
                            }
                            catch (SquareMatrixSizeException Exception) {
                                Console.WriteLine(Exception.Message);

                                break;
                            }

                            Option = false;
                            break;
                        case "2":
                            try {
                                var Result = (SquareMatrix)Left.Clone();
                                Result *= Right;

                                Result.PrintMatrix();
                            }
                            catch (SquareMatrixSizeException Exception) {
                                Console.WriteLine(Exception.Message);

                                break;
                            }

                            Option = false;
                            break;
                        case "3":
                            Console.WriteLine(Comparison(Left, Right));

                            Option = false;
                            break;
                        case "4":
                            Console.WriteLine("\n");
                            GetMatrixInfo(Left);
                            Console.WriteLine();
                            GetMatrixInfo(Right);

                            Option = false;
                            break;
                        case "5":
                            var tMatrix = (SquareMatrix)Left.Clone();
                            tMatrix = tMatrix.Transpose();
                            tMatrix.PrintMatrix();

                            tMatrix = (SquareMatrix)Right.Clone();
                            tMatrix = tMatrix.Transpose();
                            tMatrix.PrintMatrix();

                            Option = false;
                            break;
                        case "6":
                            return;
                        default:
                            Console.WriteLine("Ошибка 3");
                            break;
                    }

                    Option = true;
                }
            }
        }
    }
}
