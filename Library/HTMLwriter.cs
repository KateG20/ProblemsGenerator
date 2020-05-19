using System;
using System.Diagnostics;
using System.IO;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library
{
    public static class HTMLWriter
    {
        // Открывать ли файл сразу
        public static bool ToOpen { get; set; }

        /// <summary>
        /// Записывает сгенерированные задачи в html-файл
        /// </summary>
        /// <param name="problems">Данные о задачах</param>
        public static void WriteHTML(string[,] problems)
        {
            // Добываем путь к файлу, в который будем записывать
            string docpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDoc‌​uments);
            string path = Path.Combine(docpath, "task26.html");
            int count = 1;

            // При существовании файла создаем новый, а не перезаписываем
            while (File.Exists(path))
            {
                path = Path.Combine(docpath, $"task26({count++}).html");
            }

            // Добываем текст из шаблона и записываем его в файл
            string baseText = File.ReadAllText("HtmlTemplate.txt");
            File.WriteAllText(path, baseText, Encoding.UTF8);

            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.Load(path);
            HtmlNode body = doc.DocumentNode.SelectSingleNode("//html/body"),
                task, button, input1, input2, answer, span;

            // Проходимся по каждой задаче из списка
            for (int i = 0; i <= problems.GetUpperBound(1); i++)
            {
                // Создаем тег для условия задачи и записываем его
                task = doc.CreateElement("p");
                task.InnerHtml = problems[0, i];
                body.AppendChild(task);
                button = doc.CreateElement("p");

                // Создаем два тега для кнопочек, записываем туда атрибуты
                input1 = doc.CreateElement("input");
                input1.SetAttributeValue("class", "colored");
                input1.SetAttributeValue("type", "button");
                input1.SetAttributeValue("value", "Решение");
                input1.SetAttributeValue("onclick", $"showAnswer(\"{problems[1, i] + Create2DTable(problems[2, i])}\", 'answer{i}')");
                button.AppendChild(input1);

                input2 = doc.CreateElement("input");
                input2.SetAttributeValue("class", "colored");
                input2.SetAttributeValue("type", "button");
                input2.SetAttributeValue("value", "Спрятать решение");
                input2.SetAttributeValue("onclick", $"hideAnswer('answer{i}')");
                button.AppendChild(input2);

                // Добавляем кнопочки в body
                body.AppendChild(button);

                // Создаем тег для ответа и записываем
                answer = doc.CreateElement("p");
                span = doc.CreateElement("span");
                span.SetAttributeValue("id", $"answer{i}");
                answer.AppendChild(span);
                body.AppendChild(answer);
            }
            doc.Save(path);

            // Открываем страницу в браузере, если стоит галочка
            if (ToOpen)
                Process.Start(new ProcessStartInfo(path) { UseShellExecute = true });
        }

        /// <summary>
        /// Создает код для двумерной html-таблицы из строки 
        /// </summary>
        /// <param name="str">Строка</param>
        /// <returns>Код таблицы</returns>
        static string Create2DTable(string str)
        {
            // Делаем массив строк таблицы, разделяя по переносу строки
            string[] arrOfRows = str.Split('\n');
            // Длина по горизонтали - длина первой строки
            int lenHor = arrOfRows[0].Split(' ').Length;
            // Длина по вертикали - количество строк
            int lenVert = arrOfRows.Length;

            StringBuilder table = new StringBuilder();
            // Добавляем тег таблицы и начинаем новую строку
            table.Append("<table id = 'table' border = '1'><tr>");

            // Добавляем пустую верхнюю левую ячейку
            table.Append("<th> </th>");

            // Для удобства делаем первую строку массивом
            string[] firstRow = lenVert == 1 ? arrOfRows[0].Split(' ') : arrOfRows[1].Split(' ');
            // Длина шапки с учетом пропусков
            int headLen = 0;
            // Делаем верхнюю шапку
            for (int i = 1; i < lenHor; i++)
            {
                // Пропускаем все колонки, в которых в исходной таблице стоит точка,
                // единожды пишем знак пропуска ".."
                if (firstRow[i] == ".")
                {
                    //MessageBox.Show(res);
                    if (firstRow[i - 1] != ".")
                        table.Append($"<th class='h'>..</th>");
                    continue;
                }
                headLen++;
                if (i < 10) table.Append($"<th class='h'>{i}&nbsp;</th>");
                else table.Append($"<th class='h'>{i}</th>");
            }

            // Заканчиваем строку
            table.Append("</tr>");

            // Добавляем сами строки
            // Для однострочной таблицы
            if (lenVert == 1)
            {
                table.Append($"<tr><th class='h'>&nbsp;</th>");
                AddRow(firstRow, ref table);
            }
            // Для квадратных таблиц
            else
            {
                bool flag = true;
                string[] cells;
                // Создаем основную часть таблицы
                for (int j = 1; j < lenVert; j++)
                {
                    // Следующая строка таблицы
                    cells = arrOfRows[j].Split(' ');

                    // Если строка непустая (второй элемент непустой (первый может 
                    // быть пустой даже у непустой строки))
                    if (cells[1].Length > 0)
                    {
                        // Если состоит из точек, её надо сокращать
                        if (cells[1] == ".")
                        {
                            // Первый раз, пока флаг еще true, создаем строку из пропусков
                            if (flag)
                            {
                                table.Append($"<tr><th class='h'>..</th>");
                                for (int i = 0; i < headLen; i++)
                                {
                                    table.Append("<td class='w'>..</td>");
                                }
                                // В конце - восклицательный знак и конец строки
                                table.Append("<td class='r'>!</td></tr>");
                                // Меняем флаг, чтобы больше строк не создавалось
                                flag = false;
                            }
                            // И в любом случае - continue
                            continue;
                        }
                        // Если состоит из восклицательных знаков, ее надо сокращать
                        else if (cells[1] == "!")
                        {
                            table.Append($"<tr><th class='h'>{cells.Length - 1}</th>");
                            for (int i = 0; i < headLen; i++)
                            {
                                table.Append("<td class='r'>!</td>");
                            }
                            // В конце - восклицательный знак и конец строки
                            table.Append("<td class='r'>!</td></tr>");
                            // Цикл закончен
                            break;
                        }
                    }
                    // Первый элемент - индекс для боковой шапки
                    table.Append($"<tr><th class='h'>{j}</th>");
                    AddRow(cells, ref table);

                    // Заканчиваем строку
                    table.Append("</tr>");
                }
            }
            // Заканчиваем таблицу
            table.Append("</table><br>");
            return table.ToString();
        }

        /// <summary>
        /// Добавляет в таблицу строку
        /// </summary>
        /// <param name="cells">Массив элементов строки</param>
        /// <param name="table">Ссылка на таблицу</param>
        static void AddRow(string[] cells, ref StringBuilder table)
        {
            for (int i = 0; i < cells.Length; i++)
            {
                if (cells[i].Length < 1) continue;
                // Красим клетки
                // Если это клетка пропуска, "сокращаем" их до одной
                if (cells[i][0] == '.')
                {
                    if (cells[i - 1][0] != '.')
                        table.Append("<td class='w'>..</td>");
                    continue;
                }
                else if (cells[i][0] == '+')
                    table.Append("<td class='w'>+</td>");
                else if (cells[i][0] == '-')
                    table.Append("<td class='l'>-</td>");
                else
                    table.Append("<td class='r'>!</td>");
            }
        }
    }
}
