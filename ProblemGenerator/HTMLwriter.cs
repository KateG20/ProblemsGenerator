using System;
using System.Diagnostics;
using System.IO;
using HtmlAgilityPack;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemGenerator
{
    public static class HTMLWriter
    {
        public static void WriteHTML(string[,] problems)
        {
            // Добываем путь к файлу, в который будем записывать
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDoc‌​uments) + "//task26.html";
            // Добываем текст из шаблончика и записываем его в файл
            string baseText = File.ReadAllText("../../HtmlTemplate.txt");
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
                input1.SetAttributeValue("onclick", $"showAnswer('{problems[1, i]}', 'answer{i}')");
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

            // Открываем страничку в браузере
            Process.Start(new ProcessStartInfo(path) { UseShellExecute = true });
            //Process.Start(new ProcessStartInfo(@"C:\Users\Lenovo X1\source\repos\ProblemsGeneratorBeta\ProblemGenerator\template.html") { UseShellExecute = true });
        }
    }
}
