
namespace Geoinformatika
{
    partial class GridCreationForm
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
            this.XMinTextBox = new System.Windows.Forms.TextBox();
            this.XMaxTextBox = new System.Windows.Forms.TextBox();
            this.YMinTextBox = new System.Windows.Forms.TextBox();
            this.YMaxTextBox = new System.Windows.Forms.TextBox();
            this.CountX = new System.Windows.Forms.TextBox();
            this.CountY = new System.Windows.Forms.TextBox();
            this.CellTextBox = new System.Windows.Forms.TextBox();
            this.NearestPointsRadioButton = new System.Windows.Forms.RadioButton();
            this.SearchRadiusRadiuButton = new System.Windows.Forms.RadioButton();
            this.NearestCountTextBox = new System.Windows.Forms.TextBox();
            this.RadiusSearchTextBox = new System.Windows.Forms.TextBox();
            this.SaveButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.comboBoxLayer = new System.Windows.Forms.ComboBox();
            this.PowerComboBox = new System.Windows.Forms.ComboBox();
            this.CalculateTimeRadius = new System.Windows.Forms.Button();
            this.CalculateAccuracyButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // XMinTextBox
            // 
            this.XMinTextBox.Location = new System.Drawing.Point(36, 161);
            this.XMinTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.XMinTextBox.Name = "XMinTextBox";
            this.XMinTextBox.Size = new System.Drawing.Size(132, 22);
            this.XMinTextBox.TabIndex = 0;
            this.XMinTextBox.Text = "Xmin";
            // 
            // XMaxTextBox
            // 
            this.XMaxTextBox.Location = new System.Drawing.Point(203, 161);
            this.XMaxTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.XMaxTextBox.Name = "XMaxTextBox";
            this.XMaxTextBox.Size = new System.Drawing.Size(132, 22);
            this.XMaxTextBox.TabIndex = 1;
            this.XMaxTextBox.Text = "XMax";
            // 
            // YMinTextBox
            // 
            this.YMinTextBox.Location = new System.Drawing.Point(36, 204);
            this.YMinTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.YMinTextBox.Name = "YMinTextBox";
            this.YMinTextBox.Size = new System.Drawing.Size(132, 22);
            this.YMinTextBox.TabIndex = 2;
            this.YMinTextBox.Text = "Ymin";
            // 
            // YMaxTextBox
            // 
            this.YMaxTextBox.Location = new System.Drawing.Point(203, 204);
            this.YMaxTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.YMaxTextBox.Name = "YMaxTextBox";
            this.YMaxTextBox.Size = new System.Drawing.Size(132, 22);
            this.YMaxTextBox.TabIndex = 3;
            this.YMaxTextBox.Text = "YMax";
            // 
            // CountX
            // 
            this.CountX.Location = new System.Drawing.Point(535, 161);
            this.CountX.Margin = new System.Windows.Forms.Padding(4);
            this.CountX.Name = "CountX";
            this.CountX.Size = new System.Drawing.Size(132, 22);
            this.CountX.TabIndex = 4;
            this.CountX.Text = "CountX";
            // 
            // CountY
            // 
            this.CountY.Location = new System.Drawing.Point(535, 204);
            this.CountY.Margin = new System.Windows.Forms.Padding(4);
            this.CountY.Name = "CountY";
            this.CountY.Size = new System.Drawing.Size(132, 22);
            this.CountY.TabIndex = 5;
            this.CountY.Text = "CountY";
            // 
            // CellTextBox
            // 
            this.CellTextBox.Location = new System.Drawing.Point(370, 160);
            this.CellTextBox.Multiline = true;
            this.CellTextBox.Name = "CellTextBox";
            this.CellTextBox.Size = new System.Drawing.Size(139, 23);
            this.CellTextBox.TabIndex = 7;
            this.CellTextBox.Text = "Cell";
            this.CellTextBox.TextChanged += new System.EventHandler(this.CellTextBox_TextChanged);
            // 
            // NearestPointsRadioButton
            // 
            this.NearestPointsRadioButton.AutoSize = true;
            this.NearestPointsRadioButton.Checked = true;
            this.NearestPointsRadioButton.Location = new System.Drawing.Point(370, 268);
            this.NearestPointsRadioButton.Name = "NearestPointsRadioButton";
            this.NearestPointsRadioButton.Size = new System.Drawing.Size(186, 20);
            this.NearestPointsRadioButton.TabIndex = 8;
            this.NearestPointsRadioButton.TabStop = true;
            this.NearestPointsRadioButton.Text = "Ближайшее число точек";
            this.NearestPointsRadioButton.UseVisualStyleBackColor = true;
            // 
            // SearchRadiusRadiuButton
            // 
            this.SearchRadiusRadiuButton.AutoSize = true;
            this.SearchRadiusRadiuButton.Location = new System.Drawing.Point(370, 314);
            this.SearchRadiusRadiuButton.Name = "SearchRadiusRadiuButton";
            this.SearchRadiusRadiuButton.Size = new System.Drawing.Size(125, 20);
            this.SearchRadiusRadiuButton.TabIndex = 9;
            this.SearchRadiusRadiuButton.Text = "Радиус поиска";
            this.SearchRadiusRadiuButton.UseVisualStyleBackColor = true;
            // 
            // NearestCountTextBox
            // 
            this.NearestCountTextBox.Location = new System.Drawing.Point(567, 268);
            this.NearestCountTextBox.Name = "NearestCountTextBox";
            this.NearestCountTextBox.Size = new System.Drawing.Size(100, 22);
            this.NearestCountTextBox.TabIndex = 10;
            // 
            // RadiusSearchTextBox
            // 
            this.RadiusSearchTextBox.Location = new System.Drawing.Point(567, 314);
            this.RadiusSearchTextBox.Name = "RadiusSearchTextBox";
            this.RadiusSearchTextBox.Size = new System.Drawing.Size(100, 22);
            this.RadiusSearchTextBox.TabIndex = 11;
            // 
            // SaveButton
            // 
            this.SaveButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.SaveButton.Location = new System.Drawing.Point(370, 361);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(109, 41);
            this.SaveButton.TabIndex = 12;
            this.SaveButton.Text = "OK";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButton.Location = new System.Drawing.Point(497, 361);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(109, 41);
            this.CancelButton.TabIndex = 13;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            // 
            // comboBoxLayer
            // 
            this.comboBoxLayer.FormattingEnabled = true;
            this.comboBoxLayer.Location = new System.Drawing.Point(36, 71);
            this.comboBoxLayer.Name = "comboBoxLayer";
            this.comboBoxLayer.Size = new System.Drawing.Size(299, 24);
            this.comboBoxLayer.TabIndex = 0;
            this.comboBoxLayer.SelectedIndexChanged += new System.EventHandler(this.comboBoxLayer_SelectedIndexChanged);
            // 
            // PowerComboBox
            // 
            this.PowerComboBox.FormattingEnabled = true;
            this.PowerComboBox.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.PowerComboBox.Location = new System.Drawing.Point(36, 310);
            this.PowerComboBox.Name = "PowerComboBox";
            this.PowerComboBox.Size = new System.Drawing.Size(121, 24);
            this.PowerComboBox.TabIndex = 14;
            // 
            // CalculateTimeRadius
            // 
            this.CalculateTimeRadius.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.CalculateTimeRadius.Location = new System.Drawing.Point(36, 391);
            this.CalculateTimeRadius.Name = "CalculateTimeRadius";
            this.CalculateTimeRadius.Size = new System.Drawing.Size(117, 24);
            this.CalculateTimeRadius.TabIndex = 15;
            this.CalculateTimeRadius.Text = "CalculeteRadiusTime";
            this.CalculateTimeRadius.UseVisualStyleBackColor = true;
            this.CalculateTimeRadius.Click += new System.EventHandler(this.CalculateTimeRadius_Click);
            // 
            // CalculateAccuracyButton
            // 
            this.CalculateAccuracyButton.Location = new System.Drawing.Point(36, 361);
            this.CalculateAccuracyButton.Name = "CalculateAccuracyButton";
            this.CalculateAccuracyButton.Size = new System.Drawing.Size(132, 24);
            this.CalculateAccuracyButton.TabIndex = 16;
            this.CalculateAccuracyButton.Text = "CalculateAccuracy";
            this.CalculateAccuracyButton.UseVisualStyleBackColor = true;
            this.CalculateAccuracyButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 16);
            this.label1.TabIndex = 17;
            this.label1.Text = "Название слоя";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 141);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 16);
            this.label2.TabIndex = 18;
            this.label2.Text = "Xmin";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(36, 187);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 16);
            this.label3.TabIndex = 19;
            this.label3.Text = "Ymin";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(200, 141);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 16);
            this.label4.TabIndex = 20;
            this.label4.Text = "Xmax";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(200, 187);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 16);
            this.label5.TabIndex = 21;
            this.label5.Text = "Ymax";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(367, 141);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(138, 16);
            this.label6.TabIndex = 22;
            this.label6.Text = "Размер ячейки (cell)";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(532, 141);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(179, 16);
            this.label7.TabIndex = 23;
            this.label7.Text = "Количество ячеек (CountX)";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(532, 184);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(180, 16);
            this.label8.TabIndex = 24;
            this.label8.Text = "Количество ячеек (CountY)";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(564, 249);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(126, 16);
            this.label9.TabIndex = 25;
            this.label9.Text = "Количество точек";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(564, 296);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(104, 16);
            this.label10.TabIndex = 26;
            this.label10.Text = "Радиус поиска";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(33, 288);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(197, 16);
            this.label11.TabIndex = 27;
            this.label11.Text = "Степень влияния расстояния";
            // 
            // GridCreationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(754, 414);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CalculateAccuracyButton);
            this.Controls.Add(this.CalculateTimeRadius);
            this.Controls.Add(this.PowerComboBox);
            this.Controls.Add(this.comboBoxLayer);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.RadiusSearchTextBox);
            this.Controls.Add(this.NearestCountTextBox);
            this.Controls.Add(this.SearchRadiusRadiuButton);
            this.Controls.Add(this.NearestPointsRadioButton);
            this.Controls.Add(this.CellTextBox);
            this.Controls.Add(this.CountY);
            this.Controls.Add(this.CountX);
            this.Controls.Add(this.YMaxTextBox);
            this.Controls.Add(this.YMinTextBox);
            this.Controls.Add(this.XMaxTextBox);
            this.Controls.Add(this.XMinTextBox);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "GridCreationForm";
            this.Text = "GridCreationForm";
            this.Load += new System.EventHandler(this.GridCreationForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox XMinTextBox;
        private System.Windows.Forms.TextBox XMaxTextBox;
        private System.Windows.Forms.TextBox YMinTextBox;
        private System.Windows.Forms.TextBox YMaxTextBox;
        private System.Windows.Forms.TextBox CountX;
        private System.Windows.Forms.TextBox CountY;
        private System.Windows.Forms.TextBox CellTextBox;
        private System.Windows.Forms.RadioButton NearestPointsRadioButton;
        private System.Windows.Forms.RadioButton SearchRadiusRadiuButton;
        private System.Windows.Forms.TextBox NearestCountTextBox;
        private System.Windows.Forms.TextBox RadiusSearchTextBox;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.ComboBox comboBoxLayer;
        private System.Windows.Forms.ComboBox PowerComboBox;
        private System.Windows.Forms.Button CalculateTimeRadius;
        private System.Windows.Forms.Button CalculateAccuracyButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
    }
}