﻿namespace ProblemGenerator
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.difLabel = new System.Windows.Forms.Label();
            this.typeComboBox = new System.Windows.Forms.ComboBox();
            this.numTextBox = new System.Windows.Forms.TextBox();
            this.numLabel = new System.Windows.Forms.Label();
            this.errorLabel = new System.Windows.Forms.Label();
            this.genButton = new System.Windows.Forms.Button();
            this.title1 = new System.Windows.Forms.Label();
            this.title2 = new System.Windows.Forms.Label();
            this.infoLabel = new System.Windows.Forms.Label();
            this.randBox = new System.Windows.Forms.CheckBox();
            this.seedLabel = new System.Windows.Forms.Label();
            this.seedBox = new System.Windows.Forms.TextBox();
            this.escButton = new System.Windows.Forms.Button();
            this.htmlCheckBox = new System.Windows.Forms.CheckBox();
            this.pdfCheckBox = new System.Windows.Forms.CheckBox();
            this.textSizeComboBox = new System.Windows.Forms.ComboBox();
            this.textSizeLabel = new System.Windows.Forms.Label();
            this.createPdfCheckBox = new System.Windows.Forms.CheckBox();
            this.waitLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // difLabel
            // 
            this.difLabel.AutoSize = true;
            this.difLabel.Font = new System.Drawing.Font("Comic Sans MS", 13.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.difLabel.Location = new System.Drawing.Point(40, 305);
            this.difLabel.Name = "difLabel";
            this.difLabel.Size = new System.Drawing.Size(389, 49);
            this.difLabel.TabIndex = 0;
            this.difLabel.Text = "Выберите тип задачи:";
            this.difLabel.Click += new System.EventHandler(this.DifLabel_Click);
            // 
            // typeComboBox
            // 
            this.typeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.typeComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.typeComboBox.FormattingEnabled = true;
            this.typeComboBox.Items.AddRange(new object[] {
            "одна куча камней",
            "две кучи камней",
            "два слова"});
            this.typeComboBox.Location = new System.Drawing.Point(529, 309);
            this.typeComboBox.Name = "typeComboBox";
            this.typeComboBox.Size = new System.Drawing.Size(304, 39);
            this.typeComboBox.TabIndex = 1;
            this.typeComboBox.SelectedIndexChanged += new System.EventHandler(this.TypeComboBox_SelectedIndexChanged);
            // 
            // numTextBox
            // 
            this.numTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.numTextBox.Location = new System.Drawing.Point(529, 390);
            this.numTextBox.Name = "numTextBox";
            this.numTextBox.Size = new System.Drawing.Size(100, 38);
            this.numTextBox.TabIndex = 2;
            this.numTextBox.Text = "1";
            this.numTextBox.TextChanged += new System.EventHandler(this.NumTextBox_TextChanged);
            // 
            // numLabel
            // 
            this.numLabel.AutoSize = true;
            this.numLabel.Font = new System.Drawing.Font("Comic Sans MS", 13.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.numLabel.Location = new System.Drawing.Point(40, 386);
            this.numLabel.Name = "numLabel";
            this.numLabel.Size = new System.Drawing.Size(474, 49);
            this.numLabel.TabIndex = 3;
            this.numLabel.Text = "Введите количество задач:";
            this.numLabel.Click += new System.EventHandler(this.NumLabel_Click);
            // 
            // errorLabel
            // 
            this.errorLabel.AutoSize = true;
            this.errorLabel.Font = new System.Drawing.Font("Comic Sans MS", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.errorLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.errorLabel.Location = new System.Drawing.Point(638, 396);
            this.errorLabel.Name = "errorLabel";
            this.errorLabel.Size = new System.Drawing.Size(613, 38);
            this.errorLabel.TabIndex = 4;
            this.errorLabel.Text = "Количество задач - целое число от 1 до 1000.";
            this.errorLabel.Visible = false;
            // 
            // genButton
            // 
            this.genButton.AutoSize = true;
            this.genButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.genButton.Font = new System.Drawing.Font("Comic Sans MS", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.genButton.ForeColor = System.Drawing.Color.Green;
            this.genButton.Location = new System.Drawing.Point(485, 624);
            this.genButton.Name = "genButton";
            this.genButton.Size = new System.Drawing.Size(285, 59);
            this.genButton.TabIndex = 6;
            this.genButton.Text = "Сгенерировать!";
            this.genButton.UseVisualStyleBackColor = false;
            this.genButton.Visible = false;
            this.genButton.Click += new System.EventHandler(this.GenButton_Click);
            // 
            // title1
            // 
            this.title1.Font = new System.Drawing.Font("Comic Sans MS", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.title1.ForeColor = System.Drawing.Color.Green;
            this.title1.Location = new System.Drawing.Point(289, 40);
            this.title1.Name = "title1";
            this.title1.Size = new System.Drawing.Size(701, 135);
            this.title1.TabIndex = 6;
            this.title1.Text = "Генератор задач\r\n";
            // 
            // title2
            // 
            this.title2.Font = new System.Drawing.Font("Comic Sans MS", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.title2.ForeColor = System.Drawing.Color.Green;
            this.title2.Location = new System.Drawing.Point(165, 162);
            this.title2.Name = "title2";
            this.title2.Size = new System.Drawing.Size(931, 125);
            this.title2.TabIndex = 7;
            this.title2.Text = "№26 ЕГЭ по информатике";
            // 
            // infoLabel
            // 
            this.infoLabel.Font = new System.Drawing.Font("Comic Sans MS", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.infoLabel.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.infoLabel.Location = new System.Drawing.Point(260, 697);
            this.infoLabel.Name = "infoLabel";
            this.infoLabel.Size = new System.Drawing.Size(733, 94);
            this.infoLabel.TabIndex = 8;
            this.infoLabel.Text = "Файлы с готовыми задачами (html и pdf) будут сохранены в папке «Документы».\r\n";
            this.infoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.infoLabel.Visible = false;
            // 
            // randBox
            // 
            this.randBox.AutoSize = true;
            this.randBox.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.randBox.Location = new System.Drawing.Point(908, 307);
            this.randBox.Name = "randBox";
            this.randBox.Size = new System.Drawing.Size(317, 49);
            this.randBox.TabIndex = 9;
            this.randBox.Text = "Рандомные типы";
            this.randBox.UseVisualStyleBackColor = true;
            this.randBox.CheckedChanged += new System.EventHandler(this.RandBox_CheckedChanged);
            // 
            // seedLabel
            // 
            this.seedLabel.Font = new System.Drawing.Font("Comic Sans MS", 9.4F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.seedLabel.Location = new System.Drawing.Point(12, 456);
            this.seedLabel.Name = "seedLabel";
            this.seedLabel.Size = new System.Drawing.Size(490, 81);
            this.seedLabel.TabIndex = 10;
            this.seedLabel.Text = "Ключ генерации (любое 4-значное число, необязательно):";
            this.seedLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // seedBox
            // 
            this.seedBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.seedBox.Location = new System.Drawing.Point(529, 468);
            this.seedBox.Name = "seedBox";
            this.seedBox.Size = new System.Drawing.Size(100, 38);
            this.seedBox.TabIndex = 3;
            this.seedBox.TextChanged += new System.EventHandler(this.SeedBox_TextChanged);
            // 
            // escButton
            // 
            this.escButton.BackColor = System.Drawing.Color.LemonChiffon;
            this.escButton.Font = new System.Drawing.Font("Comic Sans MS", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.escButton.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.escButton.Location = new System.Drawing.Point(521, 805);
            this.escButton.Name = "escButton";
            this.escButton.Size = new System.Drawing.Size(210, 60);
            this.escButton.TabIndex = 7;
            this.escButton.Text = "Выход";
            this.escButton.UseVisualStyleBackColor = false;
            this.escButton.Click += new System.EventHandler(this.EscButton_Click);
            // 
            // htmlCheckBox
            // 
            this.htmlCheckBox.AutoSize = true;
            this.htmlCheckBox.Checked = true;
            this.htmlCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.htmlCheckBox.Font = new System.Drawing.Font("Comic Sans MS", 9.4F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.htmlCheckBox.Location = new System.Drawing.Point(55, 560);
            this.htmlCheckBox.Name = "htmlCheckBox";
            this.htmlCheckBox.Size = new System.Drawing.Size(215, 40);
            this.htmlCheckBox.TabIndex = 4;
            this.htmlCheckBox.Text = "Открыть html";
            this.htmlCheckBox.UseVisualStyleBackColor = true;
            this.htmlCheckBox.CheckedChanged += new System.EventHandler(this.HtmlCheckBox_CheckedChanged);
            // 
            // pdfCheckBox
            // 
            this.pdfCheckBox.AutoSize = true;
            this.pdfCheckBox.Font = new System.Drawing.Font("Comic Sans MS", 9.4F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pdfCheckBox.Location = new System.Drawing.Point(594, 560);
            this.pdfCheckBox.Name = "pdfCheckBox";
            this.pdfCheckBox.Size = new System.Drawing.Size(203, 40);
            this.pdfCheckBox.TabIndex = 5;
            this.pdfCheckBox.Text = "Открыть pdf";
            this.pdfCheckBox.UseVisualStyleBackColor = true;
            this.pdfCheckBox.Visible = false;
            this.pdfCheckBox.CheckedChanged += new System.EventHandler(this.PdfCheckBox_CheckedChanged);
            // 
            // textSizeComboBox
            // 
            this.textSizeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.textSizeComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textSizeComboBox.FormattingEnabled = true;
            this.textSizeComboBox.Items.AddRange(new object[] {
            "8",
            "10",
            "11",
            "12",
            "13",
            "14",
            "16",
            "18",
            "20"});
            this.textSizeComboBox.Location = new System.Drawing.Point(1114, 556);
            this.textSizeComboBox.Name = "textSizeComboBox";
            this.textSizeComboBox.Size = new System.Drawing.Size(80, 39);
            this.textSizeComboBox.TabIndex = 11;
            this.textSizeComboBox.Visible = false;
            this.textSizeComboBox.SelectedIndexChanged += new System.EventHandler(this.TextSizeComboBox_SelectedIndexChanged);
            // 
            // textSizeLabel
            // 
            this.textSizeLabel.Font = new System.Drawing.Font("Comic Sans MS", 9.4F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textSizeLabel.Location = new System.Drawing.Point(837, 560);
            this.textSizeLabel.Name = "textSizeLabel";
            this.textSizeLabel.Size = new System.Drawing.Size(265, 40);
            this.textSizeLabel.TabIndex = 12;
            this.textSizeLabel.Text = "Размер текста в pdf";
            this.textSizeLabel.Visible = false;
            // 
            // createPdfCheckBox
            // 
            this.createPdfCheckBox.AutoSize = true;
            this.createPdfCheckBox.Font = new System.Drawing.Font("Comic Sans MS", 9.4F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.createPdfCheckBox.Location = new System.Drawing.Point(343, 560);
            this.createPdfCheckBox.Name = "createPdfCheckBox";
            this.createPdfCheckBox.Size = new System.Drawing.Size(192, 40);
            this.createPdfCheckBox.TabIndex = 13;
            this.createPdfCheckBox.Text = "Создать pdf";
            this.createPdfCheckBox.UseVisualStyleBackColor = true;
            this.createPdfCheckBox.CheckedChanged += new System.EventHandler(this.CreatePdfCheckBox_CheckedChanged);
            // 
            // waitLabel
            // 
            this.waitLabel.BackColor = System.Drawing.Color.LemonChiffon;
            this.waitLabel.Font = new System.Drawing.Font("Comic Sans MS", 13.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.waitLabel.Location = new System.Drawing.Point(395, 388);
            this.waitLabel.Name = "waitLabel";
            this.waitLabel.Size = new System.Drawing.Size(472, 163);
            this.waitLabel.TabIndex = 14;
            this.waitLabel.Text = "Пожалуйста, подождите, пока создается pdf-файл...";
            this.waitLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.waitLabel.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Ivory;
            this.ClientSize = new System.Drawing.Size(1273, 1029);
            this.Controls.Add(this.waitLabel);
            this.Controls.Add(this.createPdfCheckBox);
            this.Controls.Add(this.textSizeLabel);
            this.Controls.Add(this.textSizeComboBox);
            this.Controls.Add(this.pdfCheckBox);
            this.Controls.Add(this.htmlCheckBox);
            this.Controls.Add(this.escButton);
            this.Controls.Add(this.seedBox);
            this.Controls.Add(this.seedLabel);
            this.Controls.Add(this.randBox);
            this.Controls.Add(this.infoLabel);
            this.Controls.Add(this.title2);
            this.Controls.Add(this.title1);
            this.Controls.Add(this.genButton);
            this.Controls.Add(this.errorLabel);
            this.Controls.Add(this.numLabel);
            this.Controls.Add(this.numTextBox);
            this.Controls.Add(this.typeComboBox);
            this.Controls.Add(this.difLabel);
            this.Name = "Form1";
            this.Text = "Генератор задач №26 ЕГЭ по информатике";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label difLabel;
        private System.Windows.Forms.ComboBox typeComboBox;
        private System.Windows.Forms.TextBox numTextBox;
        private System.Windows.Forms.Label numLabel;
        private System.Windows.Forms.Label errorLabel;
        private System.Windows.Forms.Button genButton;
        private System.Windows.Forms.Label title1;
        private System.Windows.Forms.Label title2;
        private System.Windows.Forms.Label infoLabel;
        private System.Windows.Forms.CheckBox randBox;
        private System.Windows.Forms.Label seedLabel;
        private System.Windows.Forms.TextBox seedBox;
        private System.Windows.Forms.Button escButton;
        private System.Windows.Forms.CheckBox htmlCheckBox;
        private System.Windows.Forms.CheckBox pdfCheckBox;
        private System.Windows.Forms.ComboBox textSizeComboBox;
        private System.Windows.Forms.Label textSizeLabel;
        private System.Windows.Forms.CheckBox createPdfCheckBox;
        private System.Windows.Forms.Label waitLabel;
    }
}

