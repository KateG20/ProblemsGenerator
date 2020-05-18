using System;
using System.Diagnostics;
using System.IO;
using MigraDoc;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
//using PdfSharp;
//using PdfSharp.Drawing;
//using PdfSharp.Drawing.Layout;
//using PdfSharp.Pdf;
//using PdfSharp.Pdf.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public static class PDFwriter
    {
        public static bool ToOpen { get; set; }

        /// <summary>
        /// Создает одну строку текста с условиями из данных о задачах
        /// </summary>
        /// <param name="problems">Сгенерированные данные о задачах</param>
        public static void BuildText(string[,] problems)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < problems.GetLength(1); i++)
            {
                sb.Append(problems[0, i].Replace("<br>", "\n") + "\n\n");
            }
            WritePdf(sb.ToString());
        }

        /// <summary>
        /// Записывает строку текста (условия задач) в pdf с заголовком
        /// </summary>
        /// <param name="text">Условия задач</param>
        public static void WritePdf(string text)
        {
            // Добываем путь к файлу, в который будем записывать
            string docpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDoc‌​uments);
            string path = Path.Combine(docpath, "task26.pdf");
            int count = 1;

            // При существовании файла создаем новый, а не перезаписываем
            while (File.Exists(path))
            {
                path = Path.Combine(docpath, $"task26({count++}).pdf");
            }

            // Создаем документ
            Document doc = new Document();

            // Определяем стили
            DefineStyles(doc);

            // Создаем секцию текста
            doc.AddSection();
            // Пишем заголовок
            doc.LastSection.AddParagraph("Задание №26 ЕГЭ по информатике", "Heading");
            // Пишем основной текст
            doc.LastSection.AddParagraph(text, "BaseText");

            PdfDocumentRenderer renderer = new PdfDocumentRenderer(true)
            {
                Document = doc
            };
            renderer.RenderDocument();
            // Сохраняем документ
            renderer.PdfDocument.Save(path);

            // Открываем файл, если стоит галочка
            if (ToOpen)
                Process.Start(path);
        }

        /// <summary>
        /// Определяет стили документа
        /// </summary>
        /// <param name="document">PDF-документ</param>
        public static void DefineStyles(Document document)
        {
            // Базовый стиль
            Style style = document.Styles["Normal"];

            // Стиль по умолчанию
            style.Font.Name = "Times New Roman";

            style = document.Styles.AddStyle("Heading", "Normal");
            style.Font.Size = 18;
            style.Font.Bold = true;
            style.ParagraphFormat.PageBreakBefore = true;
            style.ParagraphFormat.SpaceAfter = 12;
            style.ParagraphFormat.LeftIndent = -15;

            style = document.Styles.AddStyle("BaseText", "Normal");
            style.Font.Size = 9;
            style.ParagraphFormat.PageBreakBefore = false;
            style.ParagraphFormat.SpaceBefore = 20;
            style.ParagraphFormat.SpaceAfter = 6;
            style.ParagraphFormat.LineSpacingRule = LineSpacingRule.AtLeast;
            style.ParagraphFormat.LineSpacing = 15;
            style.ParagraphFormat.LeftIndent = -15;

            style = document.Styles[StyleNames.Header];
            style.ParagraphFormat.AddTabStop("3cm", TabAlignment.Right);

            style = document.Styles[StyleNames.Footer];
            style.ParagraphFormat.AddTabStop("5cm", TabAlignment.Center);
        }
    }
}
