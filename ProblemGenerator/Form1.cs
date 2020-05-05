using System;
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
        }
        private void NumTextBox_TextChanged(object sender, EventArgs e)
        {
            // Проверяем, чтобы было введено положительное целое число
            if (numTextBox.Text.Length > 0 && numTextBox.Text[0] == '0')
            {
                numTextBox.Text = numTextBox.Text.Substring(1);
                errorLabel.Visible = true;
                return;
            }
            if (!int.TryParse(numTextBox.Text, out int num) || num < 1)
            {
                // Очищаем поле
                numTextBox.Text = string.Empty;
                errorLabel.Visible = true;
                return;
            }
            // И оно было меньше 100
            if (num > 100)
            {
                // Обрезаем число, оставляем только первые две цифры
                numTextBox.Text = numTextBox.Text.Substring(0, 2);
                // Ставим курсор в конец
                numTextBox.SelectionStart = 2;
                numTextBox.SelectionLength = 0;
                errorLabel.Visible = true;
                return;
            }
            Generator.ProblemsNum = num;
            errorLabel.Visible = false;
        }

        private void TypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Передаем тип в класс генератора, делаем видимой кнопку запуска
            Generator.ProblemType = typeComboBox.SelectedIndex;
            genButton.Visible = true;
            infoLabel.Visible = true;
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
            // Если галочка стоит, делаем видимой кнопку запуска
            if (randBox.Checked)
            {
                genButton.Visible = true;
                infoLabel.Visible = true;
            }
            // Если галочка не стоит и тип задач не выбран, скрываем ее
            if (!randBox.Checked && typeComboBox.SelectedItem is null)
            {
                genButton.Visible = false;
                infoLabel.Visible = false;
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
