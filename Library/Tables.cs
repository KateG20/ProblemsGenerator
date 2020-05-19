using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Library
{
    /// <summary>
    /// Класс, содержащий методы для создания таблиц для разных видов задач
    /// </summary>
    public static class Tables
    {
        /// <summary>
        /// Делает 2Д таблицу строковой
        /// </summary>
        /// <param name="arr">Таблица</param>
        /// <returns>Таблица в виде одной строки</returns>
        static string ArrToStr(string[,] arr)
        {
            string[] joinedRows = new string[arr.GetLength(0)];
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                joinedRows[i] = string.Join(" ", GetRow(arr, i));
            }
            return string.Join("\n", joinedRows);
        }

        /// <summary>
        /// Возвращает строку 2Д таблицы из стрингов
        /// </summary>
        /// <param name="arr">Таблица</param>
        /// <param name="rowNum">Номер строки</param>
        /// <returns>Массив - строка</returns>
        static string[] GetRow(string[,] arr, int rowNum)
        {
            return Enumerable.Range(0, arr.GetLength(1))
                    .Select(x => arr[rowNum, x])
                    .ToArray();
        }

        /// <summary>
        /// Создает таблицу с исходами для любого количества элементов,
        /// для одной кучи
        /// </summary>
        /// <param name="actions">Список арифметических действий</param>
        /// <param name="winMin">Минимальное количество камней для выигрыша</param>
        /// <param name="winMax">Максимальное количество камней для выигрыша</param>
        /// <returns>Таблица с исходами в виде строки</returns>
        public static string OneHeap(int[] toAdd, int toMult, int winMin, int winMax)
        {
            string[,] table = new string[1, winMin];
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
                        table[0, i] = "+";
                        ok = true;
                        break;
                    }
                    else
                    {
                        // Если мы попадаем в зону проигрыша, то это тоже зона выигрыша
                        if (i + addend < winMin && table[0, i + addend] == "-")
                        {
                            table[0, i] = "+";
                            ok = true;
                            break;
                        }
                    }
                    // Иначе это зона проигрыша
                    table[0, i] = "-";
                }
                if (ok) continue;
                // Теперь так же для множителя
                // Если ходом мы попадаем в зону выигрыша, то в этой клетке 1
                if ((i * toMult >= winMin) && (i * toMult <= winMax))
                {
                    table[0, i] = "+";
                    continue;
                }
                else
                {
                    // Если мы попадаем в зону проигрыша, то это тоже зона выигрыша
                    if (i * toMult < winMin && table[0, i * toMult] == "-")
                    {
                        table[0, i] = "+";
                        continue;
                    }
                }
                // Иначе это зона проигрыша
                table[0, i] = "-";
            }

            // Заменяем много выигрышных клеток подряд на точки.
            int k = (int)Math.Ceiling((double)winMin / toMult);
            while (table[0, k] == "+")
            {
                table[0, k++] = ".";
            }

            string tableStr = ArrToStr(table);   

            return tableStr;
        }

        /// <summary>
        /// Создает таблицу с исходами для любого количества элементов,
        /// для двух куч
        /// </summary>
        /// <param name="add">Сколько камней добавляется</param>
        /// <param name="mult">Во сколько раз умножается количество камней</param>
        /// <param name="toWin">Сколько камней нужно для выигрыша</param>
        /// <returns>Таблица с исходами в виде строки</returns>
        public static string TwoHeaps(int add, int mult, int toWin)
        {
            // Методы, принимающие на вход четыре варианта развития событий 
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
                    if (cells[i] == "+2") { k++; break; }
                }
                return k == 4;
            }

            bool IsWin(string[] cells)
            {
                for (int i = 0; i < cells.Length; i++)
                {
                    if (cells[i] == "-") return true;
                }
                return false;
            }

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
                    else tbl[row, col] = "-";
                }
                return tbl[row, col];
            }

            // Создаем пустую таблицу
            string[,] table = new string[toWin, toWin];

            // Определяем значение для каждой клетки
            for (int row = 1; row < toWin; row++)
            {
                for (int col = 1; col < toWin; col++)
                {
                    GetValue(row, col, table);
                }
            }

            // Место, где начинается много плюсов
            int startWins = (int)Math.Ceiling((double)toWin / mult);
            // Меняем много плюсов на точки
            for (int row = 1; row < toWin - 1; row++)
            {
                for (int col = 1; col < toWin - 1; col++)
                {
                    if (col >= startWins || row >= startWins) table[row, col] = ".";
                }
            }

            string tableStr = ArrToStr(table);

            return tableStr;
        }

        /// <summary>
        /// Создает таблицу с исходами для любого количества букв,
        /// для двух слов
        /// </summary>
        /// <param name="actionsX">Действия для первого слова</param>
        /// <param name="actionsY">Действия для второго слова</param>
        /// <param name="toWin">Количество букв для победы</param>
        /// <returns>Таблица с исходами в виде строки</returns>
        public static string TwoWords(Generator.Adder[] actionsX, Generator.Adder[] actionsY, int toWin)
        {
            string[,] table = new string[toWin, toWin];

            for (var y = toWin - 1; y > 0; y--)
            {
                for (var x = toWin - 1; x > 0; x--)
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

            // Применяем действия умножения к единице, если получим 2 - 
            // значит минимум происходит умножение на 2
            int minMult = Math.Min(actionsX[1](1), actionsY[1](1));

            // Место, где начинается много плюсов, с запасом
            int startWins = (int)Math.Ceiling((double)toWin / minMult);
            // Меняем много плюсов на точки
            for (int row = 1; row < toWin - 1; row++)
            {
                for (int col = 1; col < toWin - 1; col++)
                {
                    if (col >= startWins || row >= startWins) table[row, col] = ".";
                }
            }

            string tableStr = ArrToStr(table);

            return tableStr;
        }
    }
}

