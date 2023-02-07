
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
            this.CountX.Location = new System.Drawing.Point(527, 161);
            this.CountX.Margin = new System.Windows.Forms.Padding(4);
            this.CountX.Name = "CountX";
            this.CountX.Size = new System.Drawing.Size(132, 22);
            this.CountX.TabIndex = 4;
            this.CountX.Text = "CountX";
            // 
            // CountY
            // 
            this.CountY.Location = new System.Drawing.Point(527, 204);
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
            this.CellTextBox.Size = new System.Drawing.Size(150, 66);
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
            this.NearestPointsRadioButton.Size = new System.Drawing.Size(191, 21);
            this.NearestPointsRadioButton.TabIndex = 8;
            this.NearestPointsRadioButton.TabStop = true;
            this.NearestPointsRadioButton.Text = "Ближайшее число точек";
            this.NearestPointsRadioButton.UseVisualStyleBackColor = true;
            // 
            // SearchRadiusRadiuButton
            // 
            this.SearchRadiusRadiuButton.AutoSize = true;
            this.SearchRadiusRadiuButton.Location = new System.Drawing.Point(370, 296);
            this.SearchRadiusRadiuButton.Name = "SearchRadiusRadiuButton";
            this.SearchRadiusRadiuButton.Size = new System.Drawing.Size(126, 21);
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
            this.RadiusSearchTextBox.Location = new System.Drawing.Point(567, 296);
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
            this.PowerComboBox.Location = new System.Drawing.Point(36, 296);
            this.PowerComboBox.Name = "PowerComboBox";
            this.PowerComboBox.Size = new System.Drawing.Size(121, 24);
            this.PowerComboBox.TabIndex = 14;
            // 
            // CalculateTimeRadius
            // 
            this.CalculateTimeRadius.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.CalculateTimeRadius.Location = new System.Drawing.Point(625, 342);
            this.CalculateTimeRadius.Name = "CalculateTimeRadius";
            this.CalculateTimeRadius.Size = new System.Drawing.Size(117, 60);
            this.CalculateTimeRadius.TabIndex = 15;
            this.CalculateTimeRadius.Text = "CalculeteRadiusTime";
            this.CalculateTimeRadius.UseVisualStyleBackColor = true;
            this.CalculateTimeRadius.Click += new System.EventHandler(this.CalculateTimeRadius_Click);
            // 
            // CalculateAccuracyButton
            // 
            this.CalculateAccuracyButton.Location = new System.Drawing.Point(228, 355);
            this.CalculateAccuracyButton.Name = "CalculateAccuracyButton";
            this.CalculateAccuracyButton.Size = new System.Drawing.Size(136, 53);
            this.CalculateAccuracyButton.TabIndex = 16;
            this.CalculateAccuracyButton.Text = "CalculateAccuracy";
            this.CalculateAccuracyButton.UseVisualStyleBackColor = true;
            this.CalculateAccuracyButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // GridCreationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(754, 414);
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
    }
}