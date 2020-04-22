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
        public static string OneHeap(Generator.Adder[] actions, int winMin, int winMax)
        {
            int[] table = new int[winMin];
            // Для каждого количества камней, для каждого действия
            for (int i = winMin - 1; i > 0; i--)
            {
                foreach (var action in actions)
                {
                    // Если ходом мы попадаем в зону выигрыша, то в этой клетке 1
                    if ((action(i) >= winMin) && (action(i) <= winMax))
                    {
                        table[i] = 1;
                        break;
                    }
                    else
                    {
                        // Если следующим ходом мы не попадаем в зону выигрыша, 
                        // то этим попадаем вторым (???)
                        if (action(i) <= winMin && table[action(i)] < 0)
                        {
                            table[i] = 1; //a[action(i)] * (-1) + 1;
                            break;
                        }
                    }
                    // Если никак не попадаем, то это зона проигрыша
                    table[i] = -1;
                }
            }

            string res = "";
            for (int i = 0; i < table.Length; i++)
            {
                res += $"{i}: {table[i]}\n";
            }
            return res;
        }

        public static void TwoHeaps(int add, int mult, int toWin)
        {
            string path = "../../result.txt";
            File.WriteAllText(path, string.Empty);

            bool IsWin1(string[] cells)
            {
                int k = 0;
                for (int i = 0; i < cells.Length; i++)
                {
                    if (cells[i] == "!") { k++; break; }
                }
                return k != 0;
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
                int k = 0;
                for (int i = 0; i < cells.Length; i++)
                {
                    if (cells[i] == "-1") { k++; break; }
                }
                return k != 0;
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
                int k = 0;
                for (int i = 0; i < cells.Length; i++)
                {
                    if (cells[i] == "-") { k++; break; } // было cells[i, 0]
                }
                return k != 0;
            }

            string GetValue(int row, int col, string[,] tbl)
            {
                //int row = int.Parse(rowS); int col = int.Parse(colS);
                if (row + col + 2 >= toWin) // +2 из-за нумерации с нуля?
                {
                    if (row < toWin && col < toWin)
                    {
                        tbl[row, col] = "!";
                    }
                    return "!";
                }
                else
                {
                    if (tbl[row, col] == "?")
                    {
                        var one_move_cells = new string[] {
                            GetValue(row + add, col, tbl), GetValue(row, col + add, tbl),
                            GetValue(row * mult, col, tbl), GetValue(row, col * mult, tbl) };
                        if (IsWin1(one_move_cells)) { tbl[row, col] = "+1"; }
                        else if (IsLoss1(one_move_cells)) { tbl[row, col] = "-1"; }
                        else if (IsWin2(one_move_cells)) { tbl[row, col] = "+2"; }
                        else if (IsLoss2(one_move_cells)) { tbl[row, col] = "-2"; }
                        else if (IsWin(one_move_cells)) { tbl[row, col] = "+"; }
                        else tbl[row, col] = "-";
                    }
                }
                return tbl[row, col];
            }

            //    function select(rr, cc)
            //    {
            //        //alert(r);
            //        //alert(c);
            //        var tb = document.getElementById("table");
            //        for (r = 0; r < tb.rows.length; r++)
            //        {
            //            for (c = 0; c < tb.rows[r].cells.length; c++)
            //            {
            //                tb.rows[r].cells[c].classList.remove("Selected");
            //            }
            //        }

            //        tb.rows[rr].cells[cc].classList.add("Selected");
            //        var heap_1 = tb.rows[0].cells[cc].innerText;
            //        var heap_2 = tb.rows[rr].cells[0].innerText;

            //        r = Number(rr) + Number(p);
            //        if (r > tb.rows.length - 1)
            //        {
            //            r = tb.rows.length - 1;
            //        }
            //        c = cc;
            //        tb.rows[r].cells[c].classList.add("Selected");

            //        r = rr;
            //        c = Number(cc) + Number(p);
            //        if (c > tb.rows[r].cells.length - 1)
            //        {
            //            c = tb.rows[r].cells.length - 1;
            //        }
            //        tb.rows[r].cells[c].classList.add("Selected");

            //        var new_c;
            //        for (c = cc; c < tb.rows[0].cells.length; c++)
            //        {
            //            new_c = c;
            //            if (tb.rows[0].cells[c].innerText == heap_1 * m)
            //            {
            //                break;
            //            }
            //        }
            //        tb.rows[r].cells[new_c].classList.add("Selected");

            //        var new_r;
            //        for (r = rr; r < tb.rows.length; r++)
            //        {
            //            new_r = r;
            //            if (tb.rows[r].cells[0].innerText == heap_2 * m)
            //            {
            //                break;
            //            }
            //        }
            //        tb.rows[new_r].cells[cc].classList.add("Selected");

            //    }


            //    function solve(this_form)
            //    {

            //        p = this_form.p.value;
            //        m = this_form.m.value;
            //        start_r = this_form.start_r.value;
            //        start_c = this_form.start_c.value;
            //        vi = this_form.victory.value;
            //        end_r = vi - start_c + 1;
            //        end_c = vi - start_r + 1;
            //        V = this_form.V.value;
            //        P = this_form.P.value;


            string[,] table = new string[toWin-1, toWin-1];

            for (int r = 0; r < toWin-1; r++)
            {
                for (int c = 0; c < toWin-1; c++)
                {
                    table[r, c] = "?";
                }
            }

            //for (int r = 0; r < toWin; r++)
            //{
            //    for (int c = 0; c < toWin; c++)
            //    {
            //        GetValue(r, c, table);
            //    }
            //}

            //MessageBox.Show($"{table.Length}");

            for (int i = 0; i < toWin - 1; i++)
            {
                for (int j = 0; j < toWin - 1; j++)
                {
                    File.AppendAllText(path, table[i, j] + " ");
                }
                File.AppendAllText(path, Environment.NewLine);
            }

            //for (r = start_r; r < end_r; r++)
            //{
            //    for (c = start_c; c < end_c; c++)
            //    {
            //        res[r][c] = "?";
            //    }
            //}

            //        var print = document.getElementById("res");
            //        var output = "";
            //        output += '<table id = "table">';
            //        output += '<TH>';

            //        for (c = start_c; c < end_c; c++)
            //        {
            //            output += '<TD>';
            //            output += c;
            //            output += '</TD>';
            //            output += '</TH>';
            //        }

            //        for (r = start_r; r < end_r; r++)
            //        {
            //            output += '<TR>';
            //            output += '<TD>';
            //            output += r;
            //            output += '</TD>';
            //            for (c = start_c; c < end_c; c++)
            //            {
            //                var cl = "";
            //                if (res[r][c][0] == V)
            //                {
            //                    cl = "V";
            //                }

            //                output += '<TD class = "' + cl + '"';
            //                output += ' onclick="select(this.parentNode.rowIndex,this.cellIndex)">'



            //        output += res[r][c];
            //                output += '</TD>';
            //            }
            //            output += '</TR>';
            //        }
            //        output += '</table>';
            //        print.innerHTML = output;

            //    }
        }
    }
}
