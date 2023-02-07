
namespace Geoinformatika
{
    partial class MiniGISSavchenko
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MiniGISSavchenko));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.Pan = new System.Windows.Forms.ToolStripButton();
            this.ZoomIn = new System.Windows.Forms.ToolStripButton();
            this.ZoomOut = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.Center = new System.Windows.Forms.ToolStripButton();
            this.SelectObject = new System.Windows.Forms.ToolStripButton();
            this.RulerButton = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
            this.LoadLayer = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.minColorDialog = new System.Windows.Forms.ColorDialog();
            this.button1 = new System.Windows.Forms.Button();
            this.layerControl1 = new Geoinformatika.LayerControl();
            this.map1 = new Geoinformatika.Map();
            this.CalcGrid = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Pan,
            this.ZoomIn,
            this.ZoomOut,
            this.toolStripButton1,
            this.Center,
            this.SelectObject,
            this.RulerButton,
            this.toolStripSeparator1,
            this.CalcGrid});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(766, 27);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // Pan
            // 
            this.Pan.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Pan.Image = ((System.Drawing.Image)(resources.GetObject("Pan.Image")));
            this.Pan.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Pan.Name = "Pan";
            this.Pan.Size = new System.Drawing.Size(24, 24);
            this.Pan.Text = "Pen";
            this.Pan.Click += new System.EventHandler(this.Pan_Click);
            // 
            // ZoomIn
            // 
            this.ZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ZoomIn.Image = ((System.Drawing.Image)(resources.GetObject("ZoomIn.Image")));
            this.ZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ZoomIn.Name = "ZoomIn";
            this.ZoomIn.Size = new System.Drawing.Size(24, 24);
            this.ZoomIn.Text = "Zoom In";
            this.ZoomIn.Click += new System.EventHandler(this.ZoomIn_Click);
            // 
            // ZoomOut
            // 
            this.ZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ZoomOut.Image = ((System.Drawing.Image)(resources.GetObject("ZoomOut.Image")));
            this.ZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ZoomOut.Name = "ZoomOut";
            this.ZoomOut.Size = new System.Drawing.Size(24, 24);
            this.ZoomOut.Text = "Zoom Out";
            this.ZoomOut.Click += new System.EventHandler(this.ZoomOut_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(24, 24);
            this.toolStripButton1.Text = "toolStripButton1";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // Center
            // 
            this.Center.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Center.Image = ((System.Drawing.Image)(resources.GetObject("Center.Image")));
            this.Center.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Center.Name = "Center";
            this.Center.Size = new System.Drawing.Size(24, 24);
            this.Center.Text = "Center";
            this.Center.Click += new System.EventHandler(this.Center_Click);
            // 
            // SelectObject
            // 
            this.SelectObject.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SelectObject.Image = global::Geoinformatika.Properties.Resources.SelectObhect;
            this.SelectObject.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SelectObject.Name = "SelectObject";
            this.SelectObject.Size = new System.Drawing.Size(24, 24);
            this.SelectObject.Click += new System.EventHandler(this.SelectObject_Click);
            // 
            // RulerButton
            // 
            this.RulerButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.RulerButton.Image = ((System.Drawing.Image)(resources.GetObject("RulerButton.Image")));
            this.RulerButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RulerButton.Name = "RulerButton";
            this.RulerButton.Size = new System.Drawing.Size(24, 24);
            this.RulerButton.Text = "Ruler";
            this.RulerButton.Click += new System.EventHandler(this.RulerButton_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel3,
            this.toolStripStatusLabel4,
            this.toolStripStatusLabel5});
            this.statusStrip1.Location = new System.Drawing.Point(0, 361);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(766, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            this.statusStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.statusStrip1_ItemClicked);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel2.Text = "toolStripStatusLabel2";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel3.Text = "toolStripStatusLabel3";
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel4.Text = "toolStripStatusLabel4";
            // 
            // toolStripStatusLabel5
            // 
            this.toolStripStatusLabel5.Name = "toolStripStatusLabel5";
            this.toolStripStatusLabel5.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel5.Text = "toolStripStatusLabel5";
            // 
            // LoadLayer
            // 
            this.LoadLayer.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.LoadLayer.Location = new System.Drawing.Point(622, 354);
            this.LoadLayer.Margin = new System.Windows.Forms.Padding(2);
            this.LoadLayer.Name = "LoadLayer";
            this.LoadLayer.Size = new System.Drawing.Size(77, 28);
            this.LoadLayer.TabIndex = 4;
            this.LoadLayer.Text = "LoadLayer";
            this.LoadLayer.UseVisualStyleBackColor = true;
            this.LoadLayer.Click += new System.EventHandler(this.LoadLayer_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(526, 364);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(56, 19);
            this.button1.TabIndex = 5;
            this.button1.Text = "minColor";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // layerControl1
            // 
            this.layerControl1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.layerControl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.layerControl1.Location = new System.Drawing.Point(526, 24);
            this.layerControl1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.layerControl1.Name = "layerControl1";
            this.layerControl1.Size = new System.Drawing.Size(232, 335);
            this.layerControl1.TabIndex = 3;
            // 
            // map1
            // 
            this.map1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.map1.Location = new System.Drawing.Point(0, 28);
            this.map1.Margin = new System.Windows.Forms.Padding(4);
            this.map1.Name = "map1";
            this.map1.Size = new System.Drawing.Size(520, 330);
            this.map1.TabIndex = 0;
            this.map1.Load += new System.EventHandler(this.map1_Load);
            this.map1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.map1_MouseMove);
            this.map1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.map1_MouseUp);
            // 
            // CalcGrid
            // 
            this.CalcGrid.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.CalcGrid.Image = ((System.Drawing.Image)(resources.GetObject("CalcGrid.Image")));
            this.CalcGrid.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CalcGrid.Name = "CalcGrid";
            this.CalcGrid.Size = new System.Drawing.Size(24, 24);
            this.CalcGrid.Text = "Рассчитать сеть";
            this.CalcGrid.Click += new System.EventHandler(this.CalcGrid_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // MiniGISSavchenko
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(766, 383);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.LoadLayer);
            this.Controls.Add(this.layerControl1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.map1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MiniGISSavchenko";
            this.Text = "MiniGISSavchenko";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripButton Pan;
        private System.Windows.Forms.ToolStripButton ZoomIn;
        private System.Windows.Forms.ToolStripButton ZoomOut;
        private System.Windows.Forms.ToolStripButton Center;
        private System.Windows.Forms.ToolStripButton SelectObject;
        private Map map1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ToolStripButton RulerButton;
        private LayerControl layerControl1;
        private System.Windows.Forms.Button LoadLayer;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ColorDialog minColorDialog;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton CalcGrid;
    }
}

