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
        public static void BuildText(string[,] problems)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < problems.GetLength(1); i++)
            {
                sb.Append(problems[0, i].Replace("<br>", "\n") + "\n\n");
            }
            WritePdf(sb.ToString());
        }
        public static void WritePdf(string text)
        {
//            string text = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas pharetra tellus enim, eu condimentum sapien tempus nec. Duis quis lorem eget sapien posuere aliquet non quis quam. Aliquam eleifend lorem eget augue ultricies, at congue purus gravida. Maecenas metus neque, tristique in aliquet eu, accumsan ut quam. Sed mattis, lorem in lobortis laoreet, leo ipsum suscipit neque, quis euismod tellus risus eu quam. Quisque nec feugiat ligula. Aenean consequat neque non efficitur cursus." +
//                "Phasellus blandit pellentesque diam, fringilla ultricies risus luctus in. Quisque quis massa eu dui molestie tincidunt. Integer eu aliquet nulla, eget tempor massa. Donec mollis orci eget felis tincidunt, in maximus orci varius. Proin ut auctor dolor. Ut elementum pharetra pharetra. Cras sagittis urna nec tempor efficitur. In at lorem sit amet dolor viverra dignissim. Proin tempor, leo sit amet efficitur venenatis, lectus orci bibendum sem, ut tempor sapien dolor sed erat. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Donec sed odio id massa consectetur eleifend. Maecenas sagittis justo lectus, nec fringilla enim eleifend in. Curabitur facilisis euismod velit a dapibus. Sed quis placerat orci." +
//                "Aenean accumsan malesuada tellus, congue semper felis sagittis vitae. Quisque varius venenatis sem, pretium pharetra justo vulputate vel. Duis a finibus nulla. Etiam in enim finibus, gravida nisl nec, aliquam neque. Donec vitae purus tristique, auctor tortor nec, ullamcorper elit. Aliquam varius elit sit amet justo cursus vulputate ut id tellus. Ut vel tellus eu diam fermentum faucibus. Duis id sollicitudin augue. Vestibulum et condimentum felis, non fringilla enim. Nulla facilisi. Morbi in neque dignissim, dapibus lectus et, luctus magna." +

//"Nunc in rhoncus odio. Proin consectetur eget erat id aliquet. Donec porta tortor non consectetur ullamcorper. Praesent tincidunt fermentum lorem, vel varius ante bibendum porttitor. Duis tempus, tellus in tincidunt molestie, est odio bibendum neque, nec pretium lacus libero quis felis. Proin sed bibendum justo. Vestibulum ac tempus leo, nec eleifend dolor." +

//"Mauris eu ante nec lacus pellentesque varius sit amet sed urna. Nulla maximus, elit id egestas vehicula, velit nibh rutrum diam, in posuere nisi nisi vel urna. Aliquam varius diam a neque ornare convallis. Nulla facilisi. Mauris eget ultrices elit. Praesent eget sem at lacus tincidunt convallis vitae ac felis. Suspendisse sit amet posuere metus. Nullam ullamcorper posuere urna, nec feugiat tellus efficitur id. Vivamus et odio lacinia nunc efficitur sagittis sit amet vitae magna. Morbi a dui iaculis, vestibulum nunc sit amet, venenatis tortor. Etiam sagittis eros ut leo pretium, eu vestibulum massa blandit. Pellentesque sit amet imperdiet nunc. Duis auctor nunc vitae lacus mollis, at lacinia mauris scelerisque." +

//"Nam eget mi tempor, tristique mauris a, cursus ipsum. Ut ut feugiat nibh, nec auctor dolor. Fusce massa turpis, ornare nec lacinia nec, accumsan ornare augue. Ut non felis sit amet tortor commodo viverra. Curabitur a volutpat libero, vel dignissim arcu. Integer vel vestibulum sapien. Cras ipsum elit, bibendum dictum euismod vitae, posuere mollis ipsum. Etiam eu eros commodo, faucibus lorem et, tincidunt velit. Vivamus interdum sem nec imperdiet varius. Nam sodales mi eros, vel imperdiet felis scelerisque in. Etiam nec metus nec massa accumsan dapibus vitae ac erat. Integer eu justo sed ex pharetra lobortis tempor et nulla. Cras viverra, dolor sit amet rutrum eleifend, ipsum lectus elementum leo, vitae ullamcorper erat dolor vestibulum odio. Quisque finibus, nisi at porta venenatis, sapien felis convallis nibh, eget fringilla risus mauris vel neque. Aenean et metus placerat, pretium lorem sit amet, ultricies enim. Vestibulum nec ullamcorper neque, nec molestie ex.";


            // Добываем путь к файлу, в который будем записывать
            string docpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDoc‌​uments);
            string path = Path.Combine(docpath, "task26.pdf");
            int count = 1;

            // При существовании файла создаем новый, а не перезаписываем
            while (File.Exists(path))
            {
                path = Path.Combine(docpath, $"task26({count++}).pdf");
            }

            Document doc = new Document();

            DefineStyles(doc);
            //Section section = doc.AddSection();
            doc.AddSection();
            doc.LastSection.AddParagraph("Задание №26 ЕГЭ по информатике", "Heading");
            doc.LastSection.AddParagraph(text, "BaseText");
            //Paragraph paragraph;
            //int a = 2;
            //doc.LastSection.AddParagraph("Justified", "Heading3");
            //paragraph = doc.LastSection.AddParagraph(text, "BaseText");
            //paragraph.Format.SpaceAfter = "3cm";


            PdfDocumentRenderer renderer = new PdfDocumentRenderer(true)
            {
                Document = doc
            };
            renderer.RenderDocument();

            /*
            PdfDocument doc = new PdfDocument();
            PdfPage page = doc.AddPage();
            XGraphics graphics = XGraphics.FromPdfPage(page);
            XTextFormatter formatter = new XTextFormatter(graphics);

            //XPdfFontOptions options = new XPdfFontOptions(PdfFontEncoding.WinAnsi, PdfFontEmbedding.Default);

            XFont headerFont = new XFont("Georgia", 18, XFontStyle.Bold);
            XFont baseFont = new XFont("Georgia", 9, XFontStyle.Regular);

            formatter.DrawString("Задание №26 ЕГЭ по информатике", headerFont, XBrushes.Black,
                new XRect(40, 30, page.Width - 60, page.Height - 100), XStringFormats.TopLeft);

            formatter.DrawString(text, baseFont, XBrushes.Black,
                new XRect(40, 80, page.Width - 80, page.Height - 100), XStringFormats.TopLeft);

            //formatter.DrawString(text, headerFont, XBrushes.Black,
            //    new XRect(40, 30, page.Width-60, page.Height-60), XStringFormats.TopLeft);

            */

            renderer.PdfDocument.Save(path);

            // Открываем файл, если стоит галочка
            if (ToOpen)
                Process.Start(path);

            //string pdfTemplate = filePath;
            //string newFile = outputFilePath;
            //PdfReader PDFWriter = new PdfReader(pdfTemplate);
            //PdfStamper pdfStampDocument = new PdfStamper(PDFWriter, new FileStream(newFile, FileMode.Create));
            //AcroFields pdfFormFields = pdfStampDocument.AcroFields;
            ////For Text field
            //pdfFormFields.SetField("txtTextFieldName", "First Text");
            ////For Check Box Field 
            //pdfFormFields.SetField("chkSomeCheckBox", "Yes");
            //PDFWriter.Close();
            //pdfStampDocument.Close();
        }

        public static void DefineStyles(Document document)
        {
            // Get the predefined style Normal.
            Style style = document.Styles["Normal"];
            // Because all styles are derived from Normal, the next line changes the 
            // font of the whole document. Or, more exactly, it changes the font of
            // all styles and paragraphs that do not redefine the font.
            style.Font.Name = "Times New Roman";

            // Heading1 to Heading9 are predefined styles with an outline level. An outline level
            // other than OutlineLevel.BodyText automatically creates the outline (or bookmarks) 
            // in PDF.

            //style = document.Styles["Heading1"];
            style = document.Styles.AddStyle("Heading", "Normal");
            //style.Font.Name = "Georgia";
            style.Font.Size = 18;
            style.Font.Bold = true;
            //style.Font.Color = Colors.Black;
            style.ParagraphFormat.PageBreakBefore = true;
            style.ParagraphFormat.SpaceAfter = 12;
            style.ParagraphFormat.LeftIndent = -15;

            //style = document.Styles["Heading2"];
            style = document.Styles.AddStyle("BaseText", "Normal");
            style.Font.Size = 9;
            //style.Font.Bold = false;
            style.ParagraphFormat.PageBreakBefore = false;
            style.ParagraphFormat.SpaceBefore = 20;
            style.ParagraphFormat.SpaceAfter = 6;
            style.ParagraphFormat.LineSpacingRule = LineSpacingRule.AtLeast;
            style.ParagraphFormat.LineSpacing = 15;
            style.ParagraphFormat.LeftIndent = -15;
            //style.ParagraphFormat.Alignment = ParagraphAlignment.Justify;

            style = document.Styles[StyleNames.Header];
            style.ParagraphFormat.AddTabStop("3cm", TabAlignment.Right);

            style = document.Styles[StyleNames.Footer];
            style.ParagraphFormat.AddTabStop("5cm", TabAlignment.Center);

            //// Create a new style called TextBox based on style Normal
            //style = document.Styles.AddStyle("TextBox", "Normal");
            //style.ParagraphFormat.Alignment = ParagraphAlignment.Justify;
            //style.ParagraphFormat.Borders.Width = 2;
            //style.ParagraphFormat.Borders.Distance = "3pt";
            //style.ParagraphFormat.Shading.Color = Colors.SkyBlue;

            //// Create a new style called TOC based on style Normal
            //style = document.Styles.AddStyle("TOC", "Normal");
            //style.ParagraphFormat.AddTabStop("16cm", TabAlignment.Right, TabLeader.Dots);
            //style.ParagraphFormat.Font.Color = Colors.Blue;
        }
    }
}
