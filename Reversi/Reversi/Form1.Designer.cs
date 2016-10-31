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
            this.spelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aantalSteentjesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aanDeBeurtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hintToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.laatHintsZienToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.snelNieuwSpelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aantalSteentjesP1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aantalSteentjesP2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.spelerAanZetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optiesToolStripMenuItem,
            this.spelToolStripMenuItem,
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
            // spelToolStripMenuItem
            // 
            this.spelToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aantalSteentjesToolStripMenuItem,
            this.aantalSteentjesP1ToolStripMenuItem,
            this.aantalSteentjesP2ToolStripMenuItem,
            this.toolStripSeparator1,
            this.aanDeBeurtToolStripMenuItem,
            this.spelerAanZetToolStripMenuItem});
            this.spelToolStripMenuItem.Name = "spelToolStripMenuItem";
            this.spelToolStripMenuItem.Size = new System.Drawing.Size(50, 24);
            this.spelToolStripMenuItem.Text = "Spel";
            // 
            // aantalSteentjesToolStripMenuItem
            // 
            this.aantalSteentjesToolStripMenuItem.Name = "aantalSteentjesToolStripMenuItem";
            this.aantalSteentjesToolStripMenuItem.Size = new System.Drawing.Size(189, 26);
            this.aantalSteentjesToolStripMenuItem.Text = "Aantal steentjes";
            // 
            // aanDeBeurtToolStripMenuItem
            // 
            this.aanDeBeurtToolStripMenuItem.Name = "aanDeBeurtToolStripMenuItem";
            this.aanDeBeurtToolStripMenuItem.Size = new System.Drawing.Size(189, 26);
            this.aanDeBeurtToolStripMenuItem.Text = "Aan de beurt";
            // 
            // hintToolStripMenuItem
            // 
            this.hintToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.laatHintsZienToolStripMenuItem});
            this.hintToolStripMenuItem.Name = "hintToolStripMenuItem";
            this.hintToolStripMenuItem.Size = new System.Drawing.Size(49, 24);
            this.hintToolStripMenuItem.Text = "Hint";
            this.hintToolStripMenuItem.Click += new System.EventHandler(this.hintToolStripMenuItem_Click);
            // 
            // laatHintsZienToolStripMenuItem
            // 
            this.laatHintsZienToolStripMenuItem.Name = "laatHintsZienToolStripMenuItem";
            this.laatHintsZienToolStripMenuItem.Size = new System.Drawing.Size(178, 26);
            this.laatHintsZienToolStripMenuItem.Text = "Laat hints zien";
            // 
            // snelNieuwSpelToolStripMenuItem
            // 
            this.snelNieuwSpelToolStripMenuItem.Name = "snelNieuwSpelToolStripMenuItem";
            this.snelNieuwSpelToolStripMenuItem.Size = new System.Drawing.Size(195, 26);
            this.snelNieuwSpelToolStripMenuItem.Text = "Snel nieuw spel";
            this.snelNieuwSpelToolStripMenuItem.Click += new System.EventHandler(this.snelNieuwSpelToolStripMenuItem_Click);
            // 
            // aantalSteentjesP1ToolStripMenuItem
            // 
            this.aantalSteentjesP1ToolStripMenuItem.Name = "aantalSteentjesP1ToolStripMenuItem";
            this.aantalSteentjesP1ToolStripMenuItem.Size = new System.Drawing.Size(189, 26);
            this.aantalSteentjesP1ToolStripMenuItem.Text = "P1: 2";
            // 
            // aantalSteentjesP2ToolStripMenuItem
            // 
            this.aantalSteentjesP2ToolStripMenuItem.Name = "aantalSteentjesP2ToolStripMenuItem";
            this.aantalSteentjesP2ToolStripMenuItem.Size = new System.Drawing.Size(189, 26);
            this.aantalSteentjesP2ToolStripMenuItem.Text = "P2: 2";
            // 
            // spelerAanZetToolStripMenuItem
            // 
            this.spelerAanZetToolStripMenuItem.Name = "spelerAanZetToolStripMenuItem";
            this.spelerAanZetToolStripMenuItem.Size = new System.Drawing.Size(189, 26);
            this.spelerAanZetToolStripMenuItem.Text = "spelerAanZet";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(186, 6);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(661, 490);
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

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem optiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem spelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hintToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem laatHintsZienToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aantalSteentjesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aanDeBeurtToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem terugNaarMenuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetSpelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem snelNieuwSpelToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        public System.Windows.Forms.ToolStripMenuItem aantalSteentjesP1ToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem aantalSteentjesP2ToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem spelerAanZetToolStripMenuItem;
    }
}

