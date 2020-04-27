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
            if (!int.TryParse(numTextBox.Text, out int num) || num < 1)
            {
                numTextBox.Text = string.Empty;
                errorLabel.Visible = true;
                return;
            }
            Generator.ProblemsNum = num;
            errorLabel.Visible = false;
        }

        private void DifComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Generator.storagePath = difComboBox.Text == "одна куча камней" ? "../../storage1.txt" : "../../storage2.txt";
            Generator.ProblemType = difComboBox.SelectedIndex;
        }

        private void GenButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Generator.Generate());
            Close();
        } 

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
