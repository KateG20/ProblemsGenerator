using System;
using System.Linq;
using System.Collections.Generic;
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
        //public static string[,] TwoWords(int[] add, int[] mult, int toWin)
        public static string[,] TwoWords(Generator.Adder[] actionsX, Generator.Adder[] actionsY, int toWin)
        {
            //actionsX = [];
            //actionsY = [];

            //var print = document.getElementById("res");
            //print.innerHTML = "Вычисляю..";

            //for (var row of this_form['actionsX'].value.split(" "))
            //{
            //    row = row.trim();
            //    if (row != '')
            //    {
            //        var actionX = new Function('x', 'return x ' + row + ';');
            //        actionsX[actionsX.length] = actionX;
            //    }
            //}
            //for (var row of this_form['actionsY'].value.split(" "))
            //{
            //    row = row.trim();
            //    if (row != '')
            //    {
            //        var actionY = new Function('x', 'return x ' + row + ';');
            //        actionsY[actionsY.length] = actionY;
            //    }
            //}

            //win = +this_form['win'].value;
            //startX = +this_form['startX'].value;
            //startY = +this_form['startY'].value;
            //a = new Array(win).fill(0);
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

            // Что это???
            //var xx = 1;
            //var x0 = 0;
            //while ((xx + 1) < toWin)
            //{
            //    x0 += 1;
            //    foreach (var actionX in actionsX)
            //        if (actionX(x0) > xx) xx = actionX(x0);
            //}
            //int finishX = x0;

            //var yy = 1;
            //var y0 = 0;
            //while (yy + 1 < toWin)
            //{
            //    y0 += 1;
            //    foreach (var actionY in actionsY)
            //        if (actionY(y0) > yy) yy = actionY(y0);
            //}
            //int finishY = y0;

            //string res = "";
            //for (int i = 1; i < toWin; i++)
            //{
            //    for (int j = 0; j < toWin; j++)
            //    {
            //        res += table[i, j] + " ";
            //    }
            //    res += "\n";
            //}
            //MessageBox.Show(res);

            return table;
        }
    }
}

