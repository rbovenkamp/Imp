namespace Reversi
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.optiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.terugNaarMenuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetSpelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.snelNieuwSpelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hintToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optiesToolStripMenuItem,
            this.hintToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(661, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // optiesToolStripMenuItem
            // 
            this.optiesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.terugNaarMenuToolStripMenuItem,
            this.resetSpelToolStripMenuItem,
            this.snelNieuwSpelToolStripMenuItem});
            this.optiesToolStripMenuItem.Name = "optiesToolStripMenuItem";
            this.optiesToolStripMenuItem.Size = new System.Drawing.Size(64, 24);
            this.optiesToolStripMenuItem.Text = "Opties";
            // 
            // terugNaarMenuToolStripMenuItem
            // 
            this.terugNaarMenuToolStripMenuItem.Name = "terugNaarMenuToolStripMenuItem";
            this.terugNaarMenuToolStripMenuItem.Size = new System.Drawing.Size(195, 26);
            this.terugNaarMenuToolStripMenuItem.Text = "Terug naar menu";
            this.terugNaarMenuToolStripMenuItem.Click += new System.EventHandler(this.terugNaarMenuToolStripMenuItem_Click);
            // 
            // resetSpelToolStripMenuItem
            // 
            this.resetSpelToolStripMenuItem.Name = "resetSpelToolStripMenuItem";
            this.resetSpelToolStripMenuItem.Size = new System.Drawing.Size(195, 26);
            this.resetSpelToolStripMenuItem.Text = "Reset spel";
            this.resetSpelToolStripMenuItem.Click += new System.EventHandler(this.resetSpel);
            // 
            // snelNieuwSpelToolStripMenuItem
            // 
            this.snelNieuwSpelToolStripMenuItem.Name = "snelNieuwSpelToolStripMenuItem";
            this.snelNieuwSpelToolStripMenuItem.Size = new System.Drawing.Size(195, 26);
            this.snelNieuwSpelToolStripMenuItem.Text = "Snel nieuw spel";
            this.snelNieuwSpelToolStripMenuItem.Click += new System.EventHandler(this.snelNieuwSpelToolStripMenuItem_Click);
            // 
            // hintToolStripMenuItem
            // 
            this.hintToolStripMenuItem.Name = "hintToolStripMenuItem";
            this.hintToolStripMenuItem.Size = new System.Drawing.Size(72, 24);
            this.hintToolStripMenuItem.Text = "Hints ☑";
            this.hintToolStripMenuItem.Click += new System.EventHandler(this.hintToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Location = new System.Drawing.Point(0, 31);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(661, 447);
            this.panel1.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(661, 490);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem optiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hintToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem terugNaarMenuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetSpelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem snelNieuwSpelToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
    }
}

