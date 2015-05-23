namespace MainForm
{
    partial class PeopleOverview
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
            this.canvas1 = new FxMaths.GUI.Canvas();
            this.SuspendLayout();
            // 
            // canvas1
            // 
            this.canvas1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.canvas1.EditBorderColor = new SharpDX.Color(((byte)(165)), ((byte)(42)), ((byte)(42)), ((byte)(255)));
            this.canvas1.Location = new System.Drawing.Point(0, 0);
            this.canvas1.Name = "canvas1";
            this.canvas1.ScreenOffset = new SharpDX.Vector2(10F, 10F);
            this.canvas1.SelectedBorderColor = new SharpDX.Color(((byte)(245)), ((byte)(245)), ((byte)(220)), ((byte)(255)));
            this.canvas1.Size = new System.Drawing.Size(642, 513);
            this.canvas1.TabIndex = 0;
            this.canvas1.Zoom = new System.Drawing.SizeF(1F, 1F);
            // 
            // PeopleOverview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(642, 513);
            this.Controls.Add(this.canvas1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.Name = "PeopleOverview";
            this.Text = "PeopleOverview";
            this.Load += new System.EventHandler(this.PeopleOverview_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private FxMaths.GUI.Canvas canvas1;
    }
}