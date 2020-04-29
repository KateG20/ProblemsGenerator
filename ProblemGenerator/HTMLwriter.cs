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
            //string path = Path.Combine(Environment.GetFolderPath(
            //    Environment.SpecialFolder.MyDoc‌​uments), "Task 26", "tasks.html");
            //if (!Directory.Exists(path))
            //{
            //    Directory.CreateDirectory(path);
            //}

            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDoc‌​uments) + "//task26.html";
            string baseText = File.ReadAllText("../../HtmlTemplate.txt");
            File.WriteAllText(path, baseText, Encoding.UTF8);

            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.Load(path);
            HtmlNode task, button, input1, input2, body, answer, span;
            MessageBox.Show(problems.GetUpperBound(1).ToString());
            for (int i = 0; i <= problems.GetUpperBound(1); i++)
            {
                body = doc.DocumentNode.SelectSingleNode("//html/body");
                task = doc.CreateElement("p");
                task.InnerHtml = problems[0, i];
                body.AppendChild(task);
                button = doc.CreateElement("p");

                input1 = doc.CreateElement("input");
                input1.SetAttributeValue("type", "button");
                input1.SetAttributeValue("value", "Решение");
                input1.SetAttributeValue("onclick", $"showAnswer('{problems[1, i]}')");
                button.AppendChild(input1);

                input2 = doc.CreateElement("input");
                input2.SetAttributeValue("type", "button");
                input2.SetAttributeValue("value", "Спрятать решение");
                input2.SetAttributeValue("onclick", "hideAnswer()");
                button.AppendChild(input2);

                body.AppendChild(button);

                answer = doc.CreateElement("p");
                span = doc.CreateElement("span");
                span.SetAttributeValue("id", "answer");
                answer.AppendChild(span);
                body.AppendChild(answer);



            }
            doc.Save(path);

            //foreach (var node in doc.DocumentNode.SelectNodes("//html/body"))
            //{

            //}

            //MessageBox.Show(mda.ToString());
            //doc.DocumentNode.SelectSingleNode("//html/body").SetChildNodesId(new HtmlNode())

            //HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            //doc.Load("test.htm");
            //foreach (HtmlNode docObjects in doc.DocumentNode.SelectNodes("//object[@type='text/sitemap']"))
            //{
            //    HtmlNodeCollection paramNodes = docObjects.ChildNodes;
            //    foreach (HtmlNode paramNode in paramNodes)
            //    {
            //        string paramName = paramNode.GetAttributeValue("name", null);
            //        string paramValue = paramNode.GetAttributeValue("value", null);
            //        HtmlNode findItem = paramNode.NextSibling;
            //        if (paramName == "Example")
            //        {
            //            var objectNode = paramNode.ParentNode;
            //            var li = doc.CreateElement("li");
            //            objectNode.Remove();
            //            li.AppendChild(objectNode);

            //            doc.DocumentNode.AppendChild(li);
            //        }
            //    }
            //}

            Process.Start(new ProcessStartInfo(path) { UseShellExecute = true });
            //Process.Start(new ProcessStartInfo(@"C:\Users\Lenovo X1\source\repos\ProblemsGeneratorBeta\ProblemGenerator\template.html") { UseShellExecute = true });
        }
    }
}
