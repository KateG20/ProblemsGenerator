namespace ProblemGenerator
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.infoLabel = new System.Windows.Forms.Label();
            this.randBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // difLabel
            // 
            this.difLabel.AutoSize = true;
            this.difLabel.Font = new System.Drawing.Font("Comic Sans MS", 13.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.difLabel.Location = new System.Drawing.Point(40, 280);
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
            this.typeComboBox.Location = new System.Drawing.Point(529, 287);
            this.typeComboBox.Name = "typeComboBox";
            this.typeComboBox.Size = new System.Drawing.Size(304, 39);
            this.typeComboBox.TabIndex = 1;
            this.typeComboBox.SelectedIndexChanged += new System.EventHandler(this.TypeComboBox_SelectedIndexChanged);
            // 
            // numTextBox
            // 
            this.numTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.numTextBox.Location = new System.Drawing.Point(529, 366);
            this.numTextBox.Name = "numTextBox";
            this.numTextBox.Size = new System.Drawing.Size(100, 38);
            this.numTextBox.TabIndex = 2;
            this.numTextBox.TextChanged += new System.EventHandler(this.NumTextBox_TextChanged);
            // 
            // numLabel
            // 
            this.numLabel.AutoSize = true;
            this.numLabel.Font = new System.Drawing.Font("Comic Sans MS", 13.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.numLabel.Location = new System.Drawing.Point(40, 361);
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
            this.errorLabel.Location = new System.Drawing.Point(643, 365);
            this.errorLabel.Name = "errorLabel";
            this.errorLabel.Size = new System.Drawing.Size(597, 38);
            this.errorLabel.TabIndex = 4;
            this.errorLabel.Text = "Количество задач - целое число от 1 до 100.";
            this.errorLabel.Visible = false;
            // 
            // genButton
            // 
            this.genButton.AutoSize = true;
            this.genButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.genButton.Font = new System.Drawing.Font("Comic Sans MS", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.genButton.ForeColor = System.Drawing.Color.Green;
            this.genButton.Location = new System.Drawing.Point(485, 463);
            this.genButton.Name = "genButton";
            this.genButton.Size = new System.Drawing.Size(285, 59);
            this.genButton.TabIndex = 5;
            this.genButton.Text = "Сгенерировать!";
            this.genButton.UseVisualStyleBackColor = false;
            this.genButton.Visible = false;
            this.genButton.Click += new System.EventHandler(this.GenButton_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Comic Sans MS", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.Green;
            this.label1.Location = new System.Drawing.Point(289, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(701, 135);
            this.label1.TabIndex = 6;
            this.label1.Text = "Генератор задач\r\n";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Comic Sans MS", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.ForeColor = System.Drawing.Color.Green;
            this.label2.Location = new System.Drawing.Point(165, 130);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(931, 125);
            this.label2.TabIndex = 7;
            this.label2.Text = "№26 ЕГЭ по информатике";
            // 
            // infoLabel
            // 
            this.infoLabel.Font = new System.Drawing.Font("Comic Sans MS", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.infoLabel.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.infoLabel.Location = new System.Drawing.Point(328, 546);
            this.infoLabel.Name = "infoLabel";
            this.infoLabel.Size = new System.Drawing.Size(597, 84);
            this.infoLabel.TabIndex = 8;
            this.infoLabel.Text = "Вы будете перенаправлены в браузер на страницу с готовыми задачами.";
            this.infoLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.infoLabel.Visible = false;
            // 
            // randBox
            // 
            this.randBox.AutoSize = true;
            this.randBox.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.randBox.Location = new System.Drawing.Point(908, 283);
            this.randBox.Name = "randBox";
            this.randBox.Size = new System.Drawing.Size(317, 49);
            this.randBox.TabIndex = 9;
            this.randBox.Text = "Рандомные типы";
            this.randBox.UseVisualStyleBackColor = true;
            this.randBox.CheckedChanged += new System.EventHandler(this.RandBox_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Ivory;
            this.ClientSize = new System.Drawing.Size(1273, 738);
            this.Controls.Add(this.randBox);
            this.Controls.Add(this.infoLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label infoLabel;
        private System.Windows.Forms.CheckBox randBox;
    }
}

