using System;
using System.Diagnostics;
using System.IO;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProblemGenerator
{
    public static class HTMLWriter
    {
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
                input1.SetAttributeValue("onclick", $@"showAnswer('{problems[1, i] + Create2DTable(problems[2, i])}', 'answer{i}')");
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

                //table = doc.CreateElement("table");
                //table.SetAttributeValue("id", "table");
                //table.SetAttributeValue("border", "1");
                //if (problems[2, i][problems[2, i].Length-1] == '0')
                //{
                //    table.InnerHtml = Create1DTable(problems[2, i]);
                //}
                //else
                    //table.InnerHtml = Create2DTable(problems[2, i]);
            }
            doc.Save(path);

            // Открываем страницу в браузере
            Process.Start(new ProcessStartInfo(path) { UseShellExecute = true });
        }

        /// <summary>
        /// Создает код для однострочной html-таблицы из строки 
        /// </summary>
        /// <param name="str">Строка</param>
        /// <returns>Код для создания таблицы</returns>
        //static string Create1DTable(string str)
        //{
        //    string[] arr = str.Split(' ');
        //    StringBuilder table = new StringBuilder();
        //    //table.Append(@"<table id = ""table"" border = ""1""><tr>");
        //    table.Append("<tr>");

        //    for (int i = 1; i < arr.Length; i++)
        //    {
        //        table.Append($@"<th class=""h"">{i}</th>");
        //    }
        //    table.Append("</tr><tr>");

        //    for (int i = 1; i < arr.Length; i++)
        //    {
        //        if (arr[i] == "+") table.Append(@"<td class=""w"">+</td>");
        //        else table.Append("<td>-</td>");
        //    }

        //    //table.Append("</tr></table><br>");
        //    table.Append("</tr>");
        //    return table.ToString();
        //}

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
            table.Append(@"<table id = \""table\"" border = '1'><tr>"); /////////тут лажа//////////////////

            // Добавляем пустую верхнюю левую ячейку
            table.Append("<th> </th>");

            // Делаем верхнюю шапку
            for (int i = 1; i <= lenHor; i++)
            {
                table.Append($"<th class='h'>{i}</th>");
            }
            // Заканчиваем строку
            table.Append("</tr>");

            string[] row;
            // Создаем основную часть таблицы
            for (int j = 1; j <= lenVert; j++)
            {
                // Новая строка и первый элемент - индекс для боковой шапки
                table.Append($"<tr><th class='h'>{j}</th>");
                // Следующая строка таблицы
                row = arrOfRows[j-1].Split(' ');

                // Пишем саму строку
                for (int i = 0; i < lenHor; i++)
                {
                    // Если выигрышная клетка, красим
                    if (row[i] == "+") table.Append("<td class='w'>+</td>");
                    else table.Append("<td>-</td>");
                }
                // Заканчиваем строку
                table.Append("</tr>");
            }

            // Заканчиваем таблицу
            table.Append("</table><br>");
            return table.ToString();
        }
    }
}
