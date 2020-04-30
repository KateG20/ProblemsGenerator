using System;
using System.IO;
using System.Windows.Forms;

namespace ProblemGenerator
{
    /// <summary>
    /// Класс, содержащий методы для создания таблиц для разных видов задач
    /// </summary>
    public static class Tables
    {
        /// <summary>
        /// Создает таблицу с исходами для любого количества элементов,
        /// для одной кучи
        /// </summary>
        /// <param name="actions">Список арифметических действий</param>
        /// <param name="winMin">Минимальное количество камней для выигрыша</param>
        /// <param name="winMax">Максимальное количество камней для выигрыша</param>
        /// <returns>пока не знаю</returns>
        public static string[] OneHeap(Generator.Adder[] actions, int winMin, int winMax)
        {
            string[] table = new string[winMin];
            // Для каждого количества камней, для каждого действия
            for (int i = winMin - 1; i > 0; i--)
            {
                foreach (var action in actions)
                {
                    // Если ходом мы попадаем в зону выигрыша, то в этой клетке 1
                    if ((action(i) >= winMin) && (action(i) <= winMax))
                    {
                        table[i] = "+1";
                        break;
                    }
                    else
                    {
                        // Если следующим ходом мы не попадаем в зону выигрыша, 
                        // то этим попадаем вторым (???)
                        if (action(i) <= winMin && table[action(i)][0] == '-')
                        {
                            table[i] = "+" + table[action(i)].Substring(1); //a[action(i)] * (-1) + 1;
                            break;
                        }
                    }
                    // Если никак не попадаем, то это зона проигрыша
                    table[i] = "-1";
                }
            }

            //MessageBox.Show(string.Join(", ", table));
            return table;
            //string res = "";
            //for (int i = 0; i < table.Length; i++)
            //{
            //    res += $"{i}: {table[i]}\n";
            //}
            //return res;
        }

        public static string[,] TwoHeaps(int add, int mult, int toWin)
        {
            //string path = "../../result.txt";
            //File.WriteAllText(path, string.Empty);

            // Методы, принимающие на вход четыре варианты развития событий 
            // после разных ходов, и возвращающие соответствие своему названию
            bool IsWin1(string[] cells)
            {
                for (int i = 0; i < cells.Length; i++)
                {
                    if (cells[i] == "!") return true;
                }
                return false;
            }

            bool IsLoss1(string[] cells)
            {
                int k = 0;
                for (int i = 0; i < cells.Length; i++)
                {
                    if (cells[i] == "+1") k++;
                }
                return k == 4;
            }

            bool IsWin2(string[] cells)
            {
                for (int i = 0; i < cells.Length; i++)
                {
                    if (cells[i] == "-1") return true;
                }
                return false;
            }

            bool IsLoss2(string[] cells)
            {
                int k = 0;
                for (int i = 0; i < cells.Length; i++)
                {
                    if (cells[i] == "-2") { k++; break; }
                }
                return k != 0;
            }

            bool IsWin(string[] cells)
            {
                for (int i = 0; i < cells.Length; i++)
                {
                    if (cells[i] == "-") return true;
                }
                return false;
            }

            //////// Это я пытаюсь сделать так, чтобы в каждой клетке было указано 
            //////// количество ходов для выигрыша; доделаю потом
            //int IsWin0(string[] cells)
            //{
            //    int minStep = 100, step;
            //    for (int i = 0; i < cells.Length; i++)
            //    {
            //        if (cells[i][0] == '-')
            //        {
            //            step = int.Parse(cells[i].Substring(1)) + 1;
            //            if (step < minStep) minStep = step;
            //        }
            //    }
            //    return minStep;
            //}

            string GetValue(int row, int col, string[,] tbl)
            {
                // Если сумма камней >= нужной, это выигрышная клетка
                if (row + col >= toWin)
                {
                    if (row < toWin && col < toWin)
                    {
                        tbl[row, col] = "!";
                    }
                    return "!";
                }
                else if (tbl[row, col] is null)
                {
                    // Смотрим на четыре возможных варианта развития событий из 
                    // этого хода (рекурсией)
                    var cellsAfterMove = new string[] {
                        GetValue(row + add, col, tbl), GetValue(row, col + add, tbl),
                        GetValue(row * mult, col, tbl), GetValue(row, col * mult, tbl) };
                    // По этим четырем вариантам решаем, эта клеточка выигрышная или
                    // проигрышная
                    if (IsWin1(cellsAfterMove)) { tbl[row, col] = "+1"; }
                    else if (IsLoss1(cellsAfterMove)) { tbl[row, col] = "-1"; }
                    else if (IsWin2(cellsAfterMove)) { tbl[row, col] = "+2"; }
                    else if (IsLoss2(cellsAfterMove)) { tbl[row, col] = "-2"; }
                    else if (IsWin(cellsAfterMove)) { tbl[row, col] = "+"; }
                    //int ifWin = IsWin0(cellsAfterMove);
                    //if (ifWin != 100) tbl[row, col] = "+" + ifWin.ToString();
                    else tbl[row, col] = "-";
                }
                return tbl[row, col];
            }

            // Создаем пустую таблицу
            string[,] table = new string[toWin, toWin];

            // Определяем значение для каждой клетки
            for (int r = 1; r < toWin; r++)
            {
                for (int c = 1; c < toWin; c++)
                {
                    GetValue(r, c, table);
                }
            }

            //string res = "";

            //for (int i = 1; i < toWin; i++)
            //{
            //    for (int j = 1; j < toWin; j++)
            //    {
            //        //File.AppendAllText("../../result.txt", table[i, j] + " ");
            //        res += table[i, j] + " ";
            //    }
            //    //File.AppendAllText("../../result.txt", Environment.NewLine);
            //    res += "\n";
            //}
            //MessageBox.Show(res);

            return table;
            //MessageBox.Show(table.Length.ToString());
        }
    }
}
