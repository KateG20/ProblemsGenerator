using System;
using System.Threading;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Library;

namespace ProblemGenerator
{
    public partial class Form1 : Form
    {
        private static readonly Random rand = new Random();
        //private System.Windows.Forms.Timer timer;
        public Form1()
        {
            InitializeComponent();
            FormClosing += new FormClosingEventHandler(Form1_Closing);
            Size = new Size(650, 500);
            // Заранее устанавливаем свойство true, т.к. галочка изначально стоит
            HTMLWriter.ToOpen = true;
            // Ставим единицу, которая стоит по умолчанию
            Generator.ProblemsNum = 1;
            // Загружаем иконку
            try
            {
                Icon = new Icon("favicon.ico");

            }
            catch (Exception)
            {
                MessageBox.Show("Возникли некоторые проблемы с иконкой приложения. " +
                    "Будет использована иконка по умолчанию.");
            }
        }
        private void NumTextBox_TextChanged(object sender, EventArgs e)
        {
            // Проверяем, чтобы было введено положительное целое число
            // меньше 1001
            if (numTextBox.Text.Length > 0 && numTextBox.Text[0] == '0')
            {
                numTextBox.Text = numTextBox.Text.Substring(1);
                errorLabel.Visible = true;
                return;
            }
            if (!int.TryParse(numTextBox.Text, out int num) || num > 1000 || num < 1)
            {
                // Обрезаем число, убирая введенный символ
                numTextBox.Text = numTextBox.Text.Substring(0, Math.Max(numTextBox.Text.Length - 1, 0));
                // Ставим курсор в конец
                numTextBox.SelectionStart = numTextBox.Text.Length;
                numTextBox.SelectionLength = 0;
                errorLabel.Visible = true;
                return;
            }
            Generator.ProblemsNum = num;
            errorLabel.Visible = false;
        }

        private void SeedBox_TextChanged(object sender, EventArgs e)
        {
            // Если пустота, то всё ок
            if (seedBox.Text.Length == 0 && (randBox.Checked || !(typeComboBox.SelectedItem is null)))
            {
                genButton.Visible = true;
                infoLabel.Visible = true;
            }
            // Если что-то начали писать - прячем кнопки, пока не будет
            // 4-хзначного числа
            else
            {
                genButton.Visible = false;
                infoLabel.Visible = false;
            }

            // Проверяем, чтобы на первом месте не было нуля
            if (seedBox.Text.Length > 0 && seedBox.Text[0] == '0')
            {
                seedBox.Text = seedBox.Text.Substring(1);
                return;
            }
            // Проверяем, что это число меньше 5 знаков
            if (!int.TryParse(seedBox.Text, out int num) || num > 9999 || num < 1)
            {
                // Обрезаем число, оставляем только первые четыре цифры
                seedBox.Text = seedBox.Text.Substring(0, Math.Max(seedBox.Text.Length - 1, 0));
                // Ставим курсор в конец
                seedBox.SelectionStart = seedBox.Text.Length;
                seedBox.SelectionLength = 0;
                return;
            }
            // Если оно четырехзначное, можно генерировать
            if (num > 999)
            {
                Generator.Seed = num;
                if (randBox.Checked || !(typeComboBox.SelectedItem is null))
                {
                    genButton.Visible = true;
                    infoLabel.Visible = true;
                }
            }
        }

        private void TypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Передаем тип в класс генератора, делаем видимой кнопку запуска
            Generator.ProblemType = typeComboBox.SelectedIndex;
            if (seedBox.Text.Length == 0 || seedBox.Text.Length == 4)
            {
                genButton.Visible = true;
                infoLabel.Visible = true;
            }
        }

        private void GenButton_Click(object sender, EventArgs e)
        {
            // Если количество задач не указано, сообщение об ошибке
            if (numTextBox.Text == "")
            {
                errorLabel.Visible = true;
                return;
            }

            string[,] problemsData;
            if (randBox.Checked) problemsData = Generator.RandomGenerate();
            else problemsData = Generator.Generate();
            try
            {
                HTMLWriter.WriteHTML(problemsData);
                // Если нужно создать pdf, запускаем это в новом треде, 
                // а на экране блокируем все элементы и выводим сообщение
                // об ожидании
                if (createPdfCheckBox.Checked)
                {
                    waitLabel.Visible = true;
                    StartPdfThread(problemsData);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при создании файла.\n" +
                    "Приложение принудительно завершит работу." + ex.Message);
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// Запускает отдельный тред для создания pdf-файла
        /// </summary>
        /// <param name="data">Данные для текстов задач</param>
        private void StartPdfThread(string[,] data)
        {
            void ThreadStarter()
            {
                try
                {
                    DisableAll();
                    PDFwriter.BuildText(data);
                }
                finally
                {
                    EnableAll();
                    waitLabel.Visible = false;
                }
            }
            var thread = new Thread(ThreadStarter);
            thread.Start();
        }

        /// <summary>
        /// Делает неактивными все элементы формы
        /// </summary>
        private void DisableAll()
        {
            foreach (Control con in Controls)
            {
                con.Enabled = false;
            }
        }

        /// <summary>
        /// Делает активными все элементы формы
        /// </summary>
        private void EnableAll()
        {
            foreach (Control con in Controls)
            {
                con.Enabled = true;
            }
        }

        private void RandBox_CheckedChanged(object sender, EventArgs e)
        {
            // Если галочка стоит, делаем видимой кнопку запуска и скрываем выбор типа
            if (randBox.Checked && (seedBox.Text.Length == 0 || seedBox.Text.Length == 4))
            {
                genButton.Visible = true;
                infoLabel.Visible = true;
                typeComboBox.Visible = false;
            }
            // Если галочка не стоит и тип задач не выбран, наоборот
            if (!randBox.Checked)
            {
                typeComboBox.Visible = true;
                if (typeComboBox.SelectedItem is null)
                {
                    genButton.Visible = false;
                    infoLabel.Visible = false;
                }
            }
        }

        private void HtmlCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (htmlCheckBox.Checked)
                HTMLWriter.ToOpen = true;
            else if (!htmlCheckBox.Checked)
                HTMLWriter.ToOpen = false;
        }

        private void PdfCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (pdfCheckBox.Checked)
                PDFwriter.ToOpen = true;
            else if (!pdfCheckBox.Checked)
                PDFwriter.ToOpen = false;
        }

        private void TextSizeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int.TryParse(textSizeComboBox.Text, out int size);
            if (size == 0) size = 10;
            PDFwriter.FontSize = size;
        }

        private void EscButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        // Пасхалочки
        private void DifLabel_Click(object sender, EventArgs e)
        {
            difLabel.ForeColor = Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256));
        }

        private void NumLabel_Click(object sender, EventArgs e)
        {
            numLabel.ForeColor = Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256));
        }

        private void Form1_Closing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void CreatePdfCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (createPdfCheckBox.Checked)
            {
                pdfCheckBox.Visible = true;
                textSizeLabel1.Visible = true;
                textSizeComboBox.Visible = true;
            }
            else if (!createPdfCheckBox.Checked)
            {
                pdfCheckBox.Visible = false;
                textSizeLabel1.Visible = false;
                textSizeComboBox.Visible = false;
            }
        }
    }
}
