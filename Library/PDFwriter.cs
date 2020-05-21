using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using MigraDoc;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace Library
{
    public static class PDFwriter
    {
        // Открывать ли файл сразу
        public static bool ToOpen { get; set; }
        // Размер текста, установленный пользователем
        public static int FontSize { get; set; }
        public static int FileNum { get; set; }

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

            // Вызываем функцию записи этого текста в файл
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
            string path = Path.Combine(docpath, FileNum == 0 ? "Task26.pdf" : 
                $"Task26({FileNum}).pdf");

            // Произойдет, если удалить html-файл из пары html+pdf и создать новый с таким же номером
            if (File.Exists(path))
            {
                MessageBox.Show($"Файл с названием, аналогичным создаваемому ({path}), уже существует. " +
                    "Он будет перезаписан.");
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
            {
                Process.Start(path);
            }
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

            // Стиль для заголовка
            style = document.Styles.AddStyle("Heading", "Normal");
            style.Font.Size = 18;
            style.Font.Bold = true;
            style.ParagraphFormat.PageBreakBefore = true;
            style.ParagraphFormat.SpaceAfter = 12;
            style.ParagraphFormat.LeftIndent = -15;

            // Стиль основного текста
            style = document.Styles.AddStyle("BaseText", "Normal");
            if (FontSize == 0)
                style.Font.Size = 10;
            else style.Font.Size = FontSize;
            style.ParagraphFormat.PageBreakBefore = false;
            style.ParagraphFormat.SpaceBefore = 20;
            style.ParagraphFormat.SpaceAfter = 6;
            style.ParagraphFormat.LineSpacingRule = LineSpacingRule.AtLeast;
            style.ParagraphFormat.LineSpacing = 15;
            style.ParagraphFormat.LeftIndent = -15;

            // Верхний колонтитул
            style = document.Styles[StyleNames.Header];
            style.ParagraphFormat.AddTabStop("3cm", MigraDoc.DocumentObjectModel.TabAlignment.Right);

            // Нижний колонтитул
            style = document.Styles[StyleNames.Footer];
            style.ParagraphFormat.AddTabStop("5cm", MigraDoc.DocumentObjectModel.TabAlignment.Center);
        }
    }
}
