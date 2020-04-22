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
            this.difComboBox = new System.Windows.Forms.ComboBox();
            this.numTextBox = new System.Windows.Forms.TextBox();
            this.numLabel = new System.Windows.Forms.Label();
            this.errorLabel = new System.Windows.Forms.Label();
            this.genButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // difLabel
            // 
            this.difLabel.AutoSize = true;
            this.difLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.difLabel.Location = new System.Drawing.Point(49, 100);
            this.difLabel.Name = "difLabel";
            this.difLabel.Size = new System.Drawing.Size(291, 31);
            this.difLabel.TabIndex = 0;
            this.difLabel.Text = "Выберите тип задачи:";
            this.difLabel.Click += new System.EventHandler(this.DifLabel_Click);
            // 
            // difComboBox
            // 
            this.difComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.difComboBox.FormattingEnabled = true;
            this.difComboBox.Items.AddRange(new object[] {
            "одна куча камней",
            "две кучи камней",
            "два слова"});
            this.difComboBox.Location = new System.Drawing.Point(432, 100);
            this.difComboBox.Name = "difComboBox";
            this.difComboBox.Size = new System.Drawing.Size(206, 33);
            this.difComboBox.TabIndex = 1;
            this.difComboBox.SelectedIndexChanged += new System.EventHandler(this.DifComboBox_SelectedIndexChanged);
            // 
            // numTextBox
            // 
            this.numTextBox.Location = new System.Drawing.Point(432, 150);
            this.numTextBox.Name = "numTextBox";
            this.numTextBox.Size = new System.Drawing.Size(100, 31);
            this.numTextBox.TabIndex = 2;
            this.numTextBox.TextChanged += new System.EventHandler(this.NumTextBox_TextChanged);
            // 
            // numLabel
            // 
            this.numLabel.AutoSize = true;
            this.numLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.numLabel.Location = new System.Drawing.Point(49, 150);
            this.numLabel.Name = "numLabel";
            this.numLabel.Size = new System.Drawing.Size(358, 31);
            this.numLabel.TabIndex = 3;
            this.numLabel.Text = "Введите количество задач:";
            this.numLabel.Click += new System.EventHandler(this.NumLabel_Click);
            // 
            // errorLabel
            // 
            this.errorLabel.AutoSize = true;
            this.errorLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.errorLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.errorLabel.Location = new System.Drawing.Point(550, 150);
            this.errorLabel.Name = "errorLabel";
            this.errorLabel.Size = new System.Drawing.Size(595, 31);
            this.errorLabel.TabIndex = 4;
            this.errorLabel.Text = "Количество задач - целое число не меньше 1.";
            this.errorLabel.Visible = false;
            // 
            // genButton
            // 
            this.genButton.AutoSize = true;
            this.genButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.genButton.Location = new System.Drawing.Point(512, 231);
            this.genButton.Name = "genButton";
            this.genButton.Size = new System.Drawing.Size(222, 41);
            this.genButton.TabIndex = 5;
            this.genButton.Text = "Сгенерировать!";
            this.genButton.UseVisualStyleBackColor = true;
            this.genButton.Click += new System.EventHandler(this.GenButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1273, 738);
            this.Controls.Add(this.genButton);
            this.Controls.Add(this.errorLabel);
            this.Controls.Add(this.numLabel);
            this.Controls.Add(this.numTextBox);
            this.Controls.Add(this.difComboBox);
            this.Controls.Add(this.difLabel);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label difLabel;
        private System.Windows.Forms.ComboBox difComboBox;
        private System.Windows.Forms.TextBox numTextBox;
        private System.Windows.Forms.Label numLabel;
        private System.Windows.Forms.Label errorLabel;
        private System.Windows.Forms.Button genButton;
    }
}

