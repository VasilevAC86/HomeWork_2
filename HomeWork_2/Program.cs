using System.Numerics;
using System.Runtime.ExceptionServices;
using System.Runtime.Serialization.Formatters;
using System.Timers;
using System.Transactions;

namespace HomeWork_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // ------------------------ Задача 1 - Данные о массивах -------------------------
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Задача 1 - Данные о массивах");
            Console.ForegroundColor = ConsoleColor.White;
            int[] a = new int[3];
            int[,] b = new int[3, 4];
            Console.ForegroundColor= ConsoleColor.Yellow;
            Console.WriteLine("\nЗаполнение одномерного массива");
            Console.ForegroundColor = ConsoleColor.White;
            for (int i = 0; i < a.Length; i++)
            {
                Console.Write("Введете значение элемента массива под номером " + i + " -> ");
                a[i] = Convert.ToInt32(Console.ReadLine());
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\nУкажите диапазон чисел для рандомного заполнения двумерного массива");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("\nУкажите начало диапазона (целое число) -> ");
            int start = Convert.ToInt32(Console.ReadLine());
            Console.Write("Укажите конец диапазона (целое число) -> ");
            int end = Convert.ToInt32(Console.ReadLine());
            while (end <= start) // Цикл проверки корректности ввода пользователем начала и конца диапазона
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ошибка ввода! Конец диапазона не может быть меньше, чем начало!");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Введите данные ещё раз.\nВведите начало диапазона -> ");
                start = Convert.ToInt32(Console.ReadLine());
                Console.Write("Введите конец диапазона -> ");
                end = Convert.ToInt32(Console.ReadLine());
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nОдномерный массив:");
            Console.ForegroundColor = ConsoleColor.White;
            foreach(int el in a) // Цикл вывода одномерного массива в консоль
                Console.Write(el + "  ");            
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\nДвумерный массив:");
            Console.ForegroundColor = ConsoleColor.White;
            Random r = new Random(); 
            for (int i = 0; i < b.GetLength(0); ++i) // Цикл формирования и вывода в консоль двумерного масива
            {
                for (int j = 0; j < b.GetLength(1); ++j)
                {
                    b[i, j] = r.Next(start, end);
                    Console.Write(b[i, j] + "\t");
                }
                Console.WriteLine();
            }            
            // Сортируем одномерный массив для нахождения максимального (последний) и минимального (первый) элементов
            Array.Sort(a);                       
            // Переменные для хранения максимальных и минимальных элементов двумерного массива
            int b_max = b[0, 0], b_min = b[0, 0];
            foreach (var el in b) // Цикл поиска максимального и минимального элемента в двумерном массиве
            {
                if (el > b_max)
                    b_max = el;
                if (el < b_min)
                    b_min = el;
            }
            Console.Write("\nОбщий максимальный элемент двух массивов = ");
            Console.ForegroundColor = ConsoleColor.Green;
            if (b_max > a[a.Length - 1])                            
                Console.WriteLine(b_max);
            else
                Console.WriteLine(a[a.Length-1]);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Общий минимальный элемент двух массивов = ");
            Console.ForegroundColor = ConsoleColor.Green;
            if (b_min < a[0])
                Console.WriteLine(b_min);
            else
                Console.WriteLine(a[0]);
            Console.ForegroundColor = ConsoleColor.White;
            // Переменные для хранения общей суммы, суммы чётных эл. одномерного массива, суммы эл. нечётных столбцов двумерного массива 
            int sum = 0, sum_a = 0, sum_b = 0;
            int mult = 1; // Переменная для хранения общего произведения всех элементов обоих массивов
            for (int i = 0; i < b.GetLength(0); i++)
            {
                for (int j = 0; j < b.GetLength(1); j++)
                {
                    sum += b[i, j];
                    mult *= b[i, j];
                    if (j % 2 != 0)
                        sum_b += b[i, j];
                }
            }
            foreach (var el in a)
            {
                sum += el;
                mult *= el;
                if (el % 2 == 0)
                    sum_a += el;
            }             
            Console.Write("Общая сумма всех элементов двух массивов = ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(sum);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Общее произведение всех элементов двух массивов = ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(mult);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Сумма чётных элементов одномерного массива = ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(sum_a);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Сумма элементов нечётных столбцов двумерного массива = ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(sum_b);
            Console.ForegroundColor = ConsoleColor.White;

            // ------Задача 2 - Сумма элементов матрицы 5х5, расположенных между минимальным и максимальным элементами -----------
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nЗадача 2 - Сумма элементов матрицы 5х5, расположенных между минимальным и максимальным элементами");
            Console.ForegroundColor = ConsoleColor.White;            
            int[,] matrix = new int[5, 5];
            Random r2= new Random(); // Генератор случайных чисел                      
            for (int i = 0; i < matrix.GetLength(0); ++i) // Цикл формирования матрицы по условиям задачи
                for (int j = 0; j < matrix.GetLength(1); ++j)            
                    matrix[i, j] = r2.Next(-100, 100);                 
            int[] tmp2 = new int[matrix.GetLength(0) * matrix.GetLength(1)]; // Временный массив для перезаписи элементов матрицы в одномерный массив
            int it = 0; // Итератор для временного одномерного массива tmp2
            foreach (var el in matrix) // Цикл перезаписи матрицы в одномерный массив
            {
                tmp2[it] = el;
                it++;
            }                
            int max = matrix[0,0], min = matrix[0,0]; // Переменные для хранения максимального и минимального элементов матрицы
            int index_max = 0, index_min = 0; // Переменные для хранения индексов максимального и минимального элементов матрицы
            int sum_matrix = 0; // Переменная для хранения суммы эл. матрицы между максимальным и минимальным элементами
            for (int i = 0; i < tmp2.Length; ++i) // Цикл поиска индексов максимального и минимального элементов матрицы
            {
                if (tmp2[i] > max)
                {
                    max = tmp2[i];
                    index_max = i;
                }
                if (tmp2[i] < min)
                {
                    min = tmp2[i];
                    index_min = i;
                }
            }            
            Console.WriteLine("\nИсодная матрица (максимальный и минимальный элементы выделены цветом):");
            // Цикл вывода матрицы с закрашенными максимальным и минимальным элементами
            for (int i = 0; i < matrix.GetLength(0); ++i)
            {
                for (int j = 0; j < matrix.GetLength(1); ++j)
                {
                    if ((i == index_max / matrix.GetLength(0) && j == index_max % matrix.GetLength(0)) || (i == index_min / matrix.GetLength(0) && j == index_min % matrix.GetLength(0)))
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write(matrix[i, j] + "\t");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                        Console.Write(matrix[i, j] + "\t");                    
                }
                Console.WriteLine();
            }
            // Расчёт суммы элементов матрицы между максимальным и минимальным элементами
            if (index_max < index_min) 
                for (int i = index_max + 1; i < index_min; ++i)
                    sum_matrix += tmp2[i];
            else 
                for (int i = index_min + 1; i < index_max; ++i)      
                    sum_matrix += tmp2[i];
            Console.Write("\nСумма элементов матрицы между минимальным и максимальным эл. (не включая максимальный и минимальный эл.) = ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(sum_matrix);
            Console.ForegroundColor = ConsoleColor.White;

            // --------------------------- Задача 3 - Шифр Цезаря ----------------------------
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nЗадача 3 - Шифр Цезаря");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("\nВведите код шифра подстановки -> ");
            int ciper = Convert.ToInt32(Console.ReadLine()); // Переменная для хранения шифра
            Console.Write("Введите строку -> ");
            string text = Console.ReadLine(); // Переменная для хранения строки            
            string text_tmp = ""; // Временная строка для хранения зашифрованных/дешифрованных символов исходной строки
            foreach (char el in text) // Шифрующий цикл         
                text_tmp += Convert.ToChar(Convert.ToInt16(el) + ciper);
            text = text_tmp;
            text_tmp = "";
            Console.WriteLine("Зашифрованная строка -> " + text);
            foreach (char el in text) // Расшифрующий цикл
                text_tmp += Convert.ToChar(Convert.ToInt16(el) - ciper);
            text = text_tmp;
            Console.WriteLine("Расшифрованная строка -> " + text);

            // --------------------------- Задача 4 - Операции с матрицами ----------------------------
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nЗадача 4 - Операции с матрицами");
            Console.ForegroundColor = ConsoleColor.White;
            int[,] matrix_1 = new int[3, 3]; // Первая матрица
            int[,] matrix_2 = new int[3, 3]; // Вторая матрица
            Random r4 = new Random();
            for (int i = 0; i < matrix_1.GetLength(0); i++) // Вложенный цикл заполнения матриц случайными числами
                for (int j = 0; j < matrix_2.GetLength(1); j++)
                {
                    matrix_1[i, j] = r4.Next(-50, 50);
                    matrix_2[i, j] = r4.Next(-100, 100);
                }
            Console.WriteLine("\nИсходные матрицы:\n\nМатрица 1:");
            Print(matrix_1);
            Console.WriteLine("\nМатрица 2:");
            Print(matrix_2);
            Console.WriteLine();
            Console.Write("Введите число целое число -> ");
            int number = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("\n" + number + " * матрицу 1:");
            Print(matrix_1, number);            
            Console.WriteLine("\n" + number + " * матрицу 2:");
            Print(matrix_2, number);
            Console.WriteLine("\nМатрица 1 + матрица 2:");
            for (int i = 0; i < matrix_1.GetLength(0); i++)
            {
                for (int j = 0; j < matrix_1.GetLength(1); j++)
                    Console.Write(matrix_1[i, j] + matrix_2[i, j] + "\t");
                Console.WriteLine();
            }            
            // Для переменожения матриц создаём матрицу-результат умножения
            int[,] matrix_mult = new int[matrix_1.GetLength(0), matrix_2.GetLength(1)];
            for (int i = 0; i < matrix_1.GetLength(0); ++i) // Вложенный цикл переменожения матриц 1 и 2       
                for (int j = 0; j < matrix_2.GetLength(1); ++j)
                    for (int k = 0; k < matrix_2.GetLength(0); ++k)
                        matrix_mult[i, j] += matrix_1[i, k] * matrix_2[k, j];
            Console.WriteLine("\nМатрица 1 * матрица 2:");
            Print(matrix_mult);

            // --------------------------- Задача 5 - Арифметические выражения + и - в консоли ----------------------------
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nЗадача 5 - Арифметические выражения + и - в консоли");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("\nВведите выражение (сложение и вычитание любого количества любых чисел) -> ");
            string str = Console.ReadLine(); // Переменная для хранения введённого пользователем выражения            
            // Цикл очистки строки от всех лишних символов, акромя цифр, +, -, . и , по коду ASCII-таблицы
            for (int i = 0; i < str.Length; i++)
                if ((str[i] < 48 || str[i] > 57) && str[i] != 43 && str[i] != 44 && str[i] != 45 && str[i] != 46)
                    str = str.Remove(i,1);                                    
            string[] operand_tmp = str.Split(new char[] { '+', '-' }); // Массив операндов (слагаемых) в str в формате строки
            double[] operand = new double[operand_tmp.Length]; // Массив операндов в формате double
            for (int i = 0; i < operand_tmp.Length; ++i)
            {
                bool key = true; // Тип числа (true - целое, false - вещественное)
                foreach (char el in operand_tmp[i])
                    if (el == '.' || el == ',')
                        key = false;
                if (key)
                    operand[i] = Convert.ToDouble(operand_tmp[i]);
                else
                {
                    // Разбиваем строку на части - до и после символа . или ,
                    string[] fraction = operand_tmp[i].Split(new char[] { '.', ',' });
                    operand[i] = Convert.ToDouble(fraction[0]) + Convert.ToDouble(fraction[1]) / Math.Pow(10, fraction[1].Length);
                }               
            }
            string symbols = ""; // Строка операторов в str (массив символов - и +)
            foreach (char el in str) // Цикл формирования массива операторов
                if (el == '+' || el == '-')
                    symbols += el;
            double result = operand[0]; // Переменная для хранения результата выражения
            for (int i = 0; i < symbols.Length; i++)
                if (symbols[i] == '+')
                    result += operand[i + 1];
                else
                    result -= operand[i + 1];
            Console.WriteLine("Результат = " + result);

            // --------------------------- Задача 6 - Большие буквы в предложении ----------------------------
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nЗадача 6 - Большие буквы в предложении");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\nВведите текст:");
            string my_text = Console.ReadLine();
            char symbol; // Переменная для хранения возводимого в верхний регистр символа
            // Если первый символ теста не в верхнем регистре, то переводим его в верхний регистр
            if (char.IsLower(my_text[0]))
            {
                symbol = char.ToUpper(my_text[0]);
                my_text = my_text.Remove(0,1);
                my_text = my_text.Insert(0, Convert.ToString(symbol));
            }
            my_text = Change(my_text, '.'); // Возводим в верхний регистр буквы после символа '.'
            my_text = Change(my_text, '!'); // Возводим в верхний регистр буквы после символа '!'
            my_text = Change(my_text, '?'); // Возводим в верхний регистр буквы после символа '?'
            Console.WriteLine(my_text);

            // --------------------------- Задача 7 - Запретное слово ----------------------------
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nЗадача 7 - Запретное слово");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nИсходное стихотворение:");
            Console.ForegroundColor = ConsoleColor.White;
            string poem = "\nУсачёв А. - Выбрал папа ёлочку.\nВыбрал папа елочку\nСамую пушистую,\nСамую пушистую,\nСамую душистую…\nЕлочка так пахнет —\nМама сразу ахнет!";
            Console.WriteLine(poem);
            Console.Write("\nЗапретное слово = ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Самую");            
            Console.ForegroundColor = ConsoleColor.Green;
            string taboo = "Самую"; // Запретное слово            
            int count_source = -1; // Счётчик вхождения запретного слова до замены (-1 для первой итерации цикла while)
            int count_final = -1; // Счётчик вхождения запретного слова после замены
            int pos = 0; // Индекс первого вхождения запретного слова в строку
            while (pos != -1) // Пока в строке не будет найдено ни одного запретного слова (метод IndexOf = -1 если ничего не найдено)
            {
                pos = poem.IndexOf(taboo, pos + 1);                
                count_source++;
            }            
            string change = ""; // Замена запретного слова
            foreach (char el in taboo) // Цикл формирования запретного слова
                change += '*';
            poem = poem.Replace(taboo, change);
            pos = 0;
            while (pos != -1) // Пока в строке не будет найдено ни одного запретного слова (метод IndexOf = -1 если ничего не найдено)
            {
                pos = poem.IndexOf(taboo, pos + 1);
                count_final++;
            }
            Console.WriteLine("\nСтихотворение после замены запретного слова:");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(poem);
            Console.Write("\nЗапретное слово ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("Самую");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(" заменено " + (count_source - count_final) + " раз(-а).");
        }
        static void Print(int[,] matrix, int n = 1) // Функция с параметром по умолчанию для вывода матрицы в консоль (для задачи 4)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                    Console.Write(matrix[i, j] * n + "\t");
                Console.WriteLine();
            }
        }
        static string Change(string my_text, char sym)
        {
            char symbol; // Переменная для хранения возводимого в верхний регистр символа
            string my_text_tmp = my_text.Remove(0, 1); // Подстрока для поиска символа . которую будем обрезать по ходу поиска
            int counter_pos = 1; // Счётчик индекса символа исходного текста
            int index; // Индекс первого символа предложения в подстроке поиска            
            // Пока в подстроке есть символ . возводим первую букву каждого предложения в верхний регистр
            while (my_text_tmp.Contains(sym) && my_text_tmp.IndexOf(sym) != my_text_tmp.Length - 1)
            {
                index = my_text_tmp.IndexOf(sym) + 1;
                counter_pos += index;
                while (my_text_tmp[index] == ' ') // Определяем первый символ после sym который не пробел
                {
                    counter_pos++;
                    index++;
                }
                if (char.IsLower(my_text[counter_pos])) // Если первая буква предложения не заглавная, то возводим её в верхний регистр
                {
                    symbol = char.ToUpper(my_text[counter_pos]);
                    my_text = my_text.Remove(counter_pos, 1);
                    my_text = my_text.Insert(counter_pos, Convert.ToString(symbol));
                }
                my_text_tmp = my_text_tmp.Remove(0, index); // Вырезаем из подстроки поиска найденное предложение с sym                
            }
            return my_text;
        }
    }
}