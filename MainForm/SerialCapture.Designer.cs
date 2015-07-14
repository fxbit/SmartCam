using System;

namespace MainForm
{
    partial class SerialCapture
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SerialCapture));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton_save = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton_plus = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_minus = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel_fps = new System.Windows.Forms.ToolStripLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.canvas1 = new FxMaths.GUI.Canvas();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.listBox_regions = new System.Windows.Forms.ListBox();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton_AddRegion = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_RemoveRegions = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton_save,
            this.toolStripSeparator1,
            this.toolStripButton_plus,
            this.toolStripButton_minus,
            this.toolStripSeparator2,
            this.toolStripLabel1,
            this.toolStripLabel_fps});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(924, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton_save
            // 
            this.toolStripButton_save.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_save.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_save.Image")));
            this.toolStripButton_save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_save.Name = "toolStripButton_save";
            this.toolStripButton_save.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_save.Text = "toolStripButton1";
            this.toolStripButton_save.ToolTipText = "Save";
            this.toolStripButton_save.Click += new System.EventHandler(this.toolStripButton_save_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton_plus
            // 
            this.toolStripButton_plus.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_plus.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_plus.Image")));
            this.toolStripButton_plus.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_plus.Name = "toolStripButton_plus";
            this.toolStripButton_plus.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_plus.Text = "toolStripButton1";
            this.toolStripButton_plus.ToolTipText = "Add";
            this.toolStripButton_plus.Click += new System.EventHandler(this.toolStripButton_plus_Click);
            // 
            // toolStripButton_minus
            // 
            this.toolStripButton_minus.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_minus.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_minus.Image")));
            this.toolStripButton_minus.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_minus.Name = "toolStripButton_minus";
            this.toolStripButton_minus.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_minus.Text = "toolStripButton2";
            this.toolStripButton_minus.ToolTipText = "Remove";
            this.toolStripButton_minus.Click += new System.EventHandler(this.toolStripButton_minus_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(29, 22);
            this.toolStripLabel1.Text = "FPS:";
            // 
            // toolStripLabel_fps
            // 
            this.toolStripLabel_fps.Name = "toolStripLabel_fps";
            this.toolStripLabel_fps.Size = new System.Drawing.Size(13, 22);
            this.toolStripLabel_fps.Text = "0";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.canvas1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(924, 752);
            this.splitContainer1.SplitterDistance = 647;
            this.splitContainer1.TabIndex = 1;
            // 
            // canvas1
            // 
            this.canvas1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.canvas1.Location = new System.Drawing.Point(0, 0);
            this.canvas1.Name = "canvas1";
            this.canvas1.Size = new System.Drawing.Size(647, 752);
            this.canvas1.TabIndex = 0;
            this.canvas1.Zoom = new System.Drawing.SizeF(1F, 1F);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.tabControl1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.propertyGrid1);
            this.splitContainer2.Size = new System.Drawing.Size(273, 752);
            this.splitContainer2.SplitterDistance = 424;
            this.splitContainer2.TabIndex = 0;
            // 
            // listBox1
            // 
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(3, 3);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(259, 392);
            this.listBox1.TabIndex = 0;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.CategoryForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid1.Location = new System.Drawing.Point(0, 0);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(273, 324);
            this.propertyGrid1.TabIndex = 0;
            this.propertyGrid1.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid1_PropertyValueChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(273, 424);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.listBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(265, 398);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Cameras";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.listBox_regions);
            this.tabPage2.Controls.Add(this.toolStrip2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(265, 398);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Regions";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // listBox_regions
            // 
            this.listBox_regions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox_regions.FormattingEnabled = true;
            this.listBox_regions.Location = new System.Drawing.Point(3, 28);
            this.listBox_regions.Name = "listBox_regions";
            this.listBox_regions.Size = new System.Drawing.Size(259, 367);
            this.listBox_regions.TabIndex = 0;
            this.listBox_regions.SelectedIndexChanged += new System.EventHandler(this.listBox_regions_SelectedIndexChanged);
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton_AddRegion,
            this.toolStripButton_RemoveRegions});
            this.toolStrip2.Location = new System.Drawing.Point(3, 3);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(259, 25);
            this.toolStrip2.TabIndex = 1;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripButton_AddRegion
            // 
            this.toolStripButton_AddRegion.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_AddRegion.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_AddRegion.Image")));
            this.toolStripButton_AddRegion.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_AddRegion.Name = "toolStripButton_AddRegion";
            this.toolStripButton_AddRegion.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_AddRegion.Text = "toolStripButton1";
            this.toolStripButton_AddRegion.ToolTipText = "Add New Region";
            this.toolStripButton_AddRegion.Click += new System.EventHandler(this.toolStripButton_AddRegion_Click);
            // 
            // toolStripButton_RemoveRegions
            // 
            this.toolStripButton_RemoveRegions.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_RemoveRegions.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_RemoveRegions.Image")));
            this.toolStripButton_RemoveRegions.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_RemoveRegions.Name = "toolStripButton_RemoveRegions";
            this.toolStripButton_RemoveRegions.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton_RemoveRegions.Text = "toolStripButton2";
            this.toolStripButton_RemoveRegions.ToolTipText = "Remove Selected Region";
            this.toolStripButton_RemoveRegions.Click += new System.EventHandler(this.toolStripButton_RemoveRegions_Click);
            // 
            // SerialCapture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(924, 777);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.Name = "SerialCapture";
            this.Text = "SerialCapture";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.ToolStripButton toolStripButton_plus;
        private System.Windows.Forms.ToolStripButton toolStripButton_minus;
        private FxMaths.GUI.Canvas canvas1;
        private System.Windows.Forms.ToolStripButton toolStripButton_save;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel_fps;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListBox listBox_regions;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton toolStripButton_AddRegion;
        private System.Windows.Forms.ToolStripButton toolStripButton_RemoveRegions;
    }
}