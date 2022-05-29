using System;

namespace lab3 {
    public class MatrixCalculator {

        private static MatrixCalculator instance;

        private MatrixCalculator() {

        }

        public static MatrixCalculator GetInstance {

            get {

                if (instance == null) {

                    instance = new MatrixCalculator();
                }

                return instance;
            }
        }

        private SquareMatrixClone CreateSquareMatrix() {

            var notSet = true;

            Console.WriteLine("Введите имя матрицы: ");
            var name = Console.ReadLine();

            Console.WriteLine("\n");
            Console.WriteLine("Создать рандомную матрицу? \n");
            Console.WriteLine("Нет        0");
            Console.WriteLine("Да        1");

            while (notSet) {

                switch (Console.ReadLine()) {

                    case "0":
                        notSet = false;
                        break;
                    case "1":
                        return new SquareMatrixClone(name);
                    default:
                        Console.WriteLine("Ошибка, попробуйте снова.");
                        break;
                }
            }

            notSet = true;

            var size = 0;

            while (notSet) {

                Console.WriteLine("\n");
                Console.WriteLine("Введите длину матрицы: ");

                if (!int.TryParse(Console.ReadLine(), out size) || size <= 1) {

                    Console.WriteLine("Неправильное значение, или формат, попробуйте снова.");
                } else {

                    notSet = false;
                }
            }

            notSet = true;

            Console.WriteLine("\n");
            Console.WriteLine("Генерация рандомных элементов? \n");
            Console.WriteLine("Нет        0");
            Console.WriteLine("Да        1");

            while (notSet) {

                switch (Console.ReadLine()) {

                    case "0":
                        notSet = false;
                        break;
                    case "1":
                        return new SquareMatrixClone(size, name);
                    default:
                        Console.WriteLine("Ошибка 2.");
                        break;
                }
            }

            notSet = true;

            var elements = new double[size, size];
            double currentElement;
            for (var rowIndex = 0; rowIndex < size; ++rowIndex) {

                for (var columnIndex = 0; columnIndex < size; ++columnIndex) {

                    while (notSet) {

                        Console.WriteLine($"Enter element {rowIndex}{columnIndex}: ");

                        if (!double.TryParse(Console.ReadLine(), out currentElement)) {

                            Console.WriteLine("Ошибка значения.");
                        } else {

                            elements[rowIndex, columnIndex] = currentElement;

                            notSet = false;
                        }
                    }

                    notSet = true;
                }
            }

            return new SquareMatrixClone(size, name, elements);
        }

        private void GetMatrixInfo(SquareMatrixClone matrix) {

            Console.WriteLine($"Матрица {matrix.Name}");
            Console.WriteLine($"Определитель: {matrix.Determinant()}");
            Console.WriteLine($"Хеш: {matrix.GetHashCode()}");
            Console.WriteLine($"Сумма элементов: {matrix.SumOfElements()}");
            Console.WriteLine($"Строка: {matrix}");
        }

        private string Comparison(SquareMatrixClone left, SquareMatrixClone right) {

            if (left > right) {

                return $"{left.Name} > {right.Name}";
            } else if (left < right) {

                return $"{left.Name} < {right.Name}";
            } else {

                return $"{left.Name} = {right.Name}";
            }
        }

        public void Calculator() {

            Console.WriteLine("Создать 1ю матрицу:\n");
            var left = CreateSquareMatrix();

            Console.Clear();

            Console.WriteLine("Создать 2ю матирцу:\n");
            var right = CreateSquareMatrix();

            Console.Clear();

            left.PrintMatrix();
            Console.WriteLine("\n");
            right.PrintMatrix();
            Console.WriteLine("\n");

            Console.WriteLine("Создать           0");
            Console.WriteLine("Вычесть           1");
            Console.WriteLine("Умножение         2");
            Console.WriteLine("Сравнение         3");
            Console.WriteLine("Данные            4");
            Console.WriteLine("Транспонировать   5");
            Console.WriteLine("Выйти             6");

            var option = true;

            while (true) {
                while (option) {

                    Console.WriteLine("\n");
                    Console.WriteLine("Выберите вариант");

                    switch (Console.ReadLine()) {

                        case "0":
                            try {
                                var result = (SquareMatrix)left.Clone();
                                result += right;

                                result.PrintMatrix();
                            }
                            catch (SquareMatrixSizeException exception) {
                                Console.WriteLine(exception.Message);

                                break;
                            }

                            option = false;
                            break;
                        case "1":
                            try {
                                var result = (SquareMatrix)left.Clone();
                                result -= right;

                                result.PrintMatrix();
                            }
                            catch (SquareMatrixSizeException exception) {
                                Console.WriteLine(exception.Message);

                                break;
                            }

                            option = false;
                            break;
                        case "2":
                            try {
                                var result = (SquareMatrix)left.Clone();
                                result *= right;

                                result.PrintMatrix();
                            }
                            catch (SquareMatrixSizeException exception) {
                                Console.WriteLine(exception.Message);

                                break;
                            }

                            option = false;
                            break;
                        case "3":
                            Console.WriteLine(Comparison(left, right));

                            option = false;
                            break;
                        case "4":
                            Console.WriteLine("\n");
                            GetMatrixInfo(left);
                            Console.WriteLine();
                            GetMatrixInfo(right);

                            option = false;
                            break;
                        case "5":
                            var tMatrix = (SquareMatrix)left.Clone();
                            tMatrix = tMatrix.Transpose();
                            tMatrix.PrintMatrix();

                            tMatrix = (SquareMatrix)right.Clone();
                            tMatrix = tMatrix.Transpose();
                            tMatrix.PrintMatrix();

                            option = false;
                            break;
                        case "6":
                            return;
                        default:
                            Console.WriteLine("Ошибка 3");
                            break;
                    }

                    option = true;
                }
            }
        }
    }
}
