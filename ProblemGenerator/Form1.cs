using System;
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

namespace ProblemGenerator
{
    public partial class Form1 : Form
    {
        private static readonly Random rand = new Random();
        public Form1()
        {
            InitializeComponent();
            FormClosing += new FormClosingEventHandler(Form1_Closing);
            Size = new Size(650, 470);
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
            if (!int.TryParse(numTextBox.Text, out int num) || num > 1000)
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
            if (seedBox.Text.Length == 0)
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
            if (!int.TryParse(seedBox.Text, out int num) || num > 9999)
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
                if ((randBox.Checked || !(typeComboBox.SelectedItem is null)))
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
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при создании html-файла.\n" + ex.Message);
                Environment.Exit(0);
            }

            Close();
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
    }
}
