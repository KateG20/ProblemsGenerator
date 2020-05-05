using System;
using System.Linq;
using System.Collections.Generic;
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
        /// <returns>Массив исходов для каждой клетки</returns>
        public static string[] OneHeap(int[] toAdd, int toMult, int winMin, int winMax)
        {
            string[] table = new string[winMin];
            // Флаг для выхода из массива, если уже определили значение клетки
            bool ok;
            // Для каждого количества камней, для каждого действия
            for (int i = winMin - 1; i > 0; i--)
            {
                ok = false;
                // Проверяем эту клетку для каждого слагаемого
                foreach (var addend in toAdd)
                {
                    // Если ходом мы попадаем в зону выигрыша, то в этой клетке 1
                    if ((i + addend >= winMin) && (i + addend <= winMax))
                    {
                        table[i] = "+";
                        ok = true;
                        break;
                    }
                    else
                    {
                        // Если мы попадаем в зону проигрыша, то это тоже зона выигрыша
                        if (i + addend < winMin && table[i + addend] == "-")
                        {
                            table[i] = "+";
                            ok = true;
                            break;
                        }
                    }
                    // Иначе это зона проигрыша
                    table[i] = "-";
                }
                if (ok) continue;
                // Теперь так же для множителя
                // Если ходом мы попадаем в зону выигрыша, то в этой клетке 1
                if ((i * toMult >= winMin) && (i * toMult <= winMax))
                {
                    table[i] = "+";
                    continue;
                }
                else
                {
                    // Если мы попадаем в зону проигрыша, то это тоже зона выигрыша
                    if (i * toMult < winMin && table[i * toMult] == "-")
                    {
                        table[i] = "+";
                        continue;
                    }
                }
                // Иначе это зона проигрыша
                table[i] = "-";
            }

            string tableString = "";
            for (int i = 1; i < table.Length; i++)
            {
                tableString += $"{i}{table[i]} ";
            }
            //MessageBox.Show(tableString);

            return table;
        }

        /// <summary>
        /// Создает таблицу с исходами для любого количества элементов,
        /// для двух куч
        /// </summary>
        /// <param name="add">Сколько камней добавляется</param>
        /// <param name="mult">Во сколько раз умножается количество камней</param>
        /// <param name="toWin">Сколько камней нужно для выигрыша</param>
        /// <returns>Двумерный массив (таблицу) с исходами</returns>
        public static string[,] TwoHeaps(int add, int mult, int toWin)
        {
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

            return table;
        }

        /// <summary>
        /// Создает таблицу с исходами для любого количества букв,
        /// для двух слов
        /// </summary>
        /// <param name="actionsX">Действия для первого слова</param>
        /// <param name="actionsY">Действия для второго слова</param>
        /// <param name="toWin">Количество букв для победы</param>
        /// <returns>Двумерный массив (таблицу) с исходами</returns>
        public static string[,] TwoWords(Generator.Adder[] actionsX, Generator.Adder[] actionsY, int toWin)
        {
            string[,] table = new string[toWin, toWin];

            // Заполнение шапок (не знаю, надо ли)
            //for (var r = 0; r < toWin; r++)
            //{
            //    table[r, 0] = r.ToString();
            //}
            //for (var c = 0; c < toWin; c++)
            //{
            //    table[0, c] = c.ToString();
            //}

            for (var y = toWin-1; y > 0; y--)
            {
                for (var x = toWin-1; x > 0; x--)
                {
                    table[y, x] = "-";
                    List<string> targets = new List<string>();
                    if ((y + x) >= toWin)
                        table[y, x] = "!";
                    else
                    {
                        foreach (var actionX in actionsX)
                        {
                            if (y + actionX(x) >= toWin)
                            {
                                targets.Add("!");
                                break;
                            }
                            else
                            {
                                targets.Add(table[y, actionX(x)]);
                            }
                        }
                        foreach (var actionY in actionsY)
                        {
                            if (x + actionY(y) >= toWin)
                            {
                                targets.Add("!");
                                break;
                            }
                            else
                            {
                                targets.Add(table[actionY(y), x]);
                            }
                        }
                        if (targets.IndexOf("!") != -1) table[y, x] = "+1";
                        else if (targets.IndexOf("-1") != -1) table[y, x] = "+2";
                        else if (targets.IndexOf("-1,2") != -1) table[y, x] = "+";
                        else if (targets.IndexOf("-") != -1) table[y, x] = "+";
                        else
                        {
                            table[y, x] = "-";
                            int k1 = 0;
                            int k2 = 0;
                            foreach (string target in targets)
                            {
                                if (target == "+1") k1 += 1;
                                else if (target == "+2") k2 += 1;
                            }
                            if (k1 == targets.Count) table[y, x] = "-1";
                            else if ((k1 + k2) == targets.Count) table[y, x] = "-1,2";
                        }
                    }
                }
            }

            return table;
        }
    }
}

