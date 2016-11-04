using SchetsEditor.Dialog;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SchetsEditor
{
    public class Hoofdscherm : Form
    {
        MenuStrip menuStrip;
        string belangrijk = "Schets editorinator Mega Premium ++ .net # Proffesional Edition ++ voordeel";

        public Hoofdscherm()
        {   this.ClientSize = new Size(800, 600);
            menuStrip = new MenuStrip();
            this.Controls.Add(menuStrip);
            this.maakFileMenu();
            this.maakHelpMenu();
            this.Text = belangrijk;
            this.IsMdiContainer = true;
            this.MainMenuStrip = menuStrip;
        }
        private void maakFileMenu()
        {   ToolStripDropDownItem menu;
            menu = new ToolStripMenuItem("File");
            menu.DropDownItems.Add("Nieuw", null, this.nieuw);
            menu.DropDownItems.Add("Open", null, this.open);
            menu.DropDownItems.Add("Exit", null, this.afsluiten);
            menuStrip.Items.Add(menu);
        }
        private void maakHelpMenu()
        {   ToolStripDropDownItem menu;
            menu = new ToolStripMenuItem("Help");
            menu.DropDownItems.Add("Over \"Schets\"", null, this.about);
            menuStrip.Items.Add(menu);
        }
        private void about(object o, EventArgs ea)
        {   MessageBox.Show(belangrijk + "versie System.DivideByZeroException \n    (c) UU Informatica en Natuurkunde 2000 + 420"
                           , "Over \"" + belangrijk + "\""
                           , MessageBoxButtons.OK
                           , MessageBoxIcon.Information
                           );
        }

        private void nieuw(object sender, EventArgs e)
        {   SchetsWin s = new SchetsWin();
            s.MdiParent = this;
            s.Show();
        }
        private void open(object sender, EventArgs e)
        {
            OpenImageDialog dialog = new OpenImageDialog();
            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                SchetsWin s;

                if (dialog.FileName.EndsWith(".schets"))
                {
                    s = new SchetsWin(File.ReadAllText(dialog.FileName));
                }
                else
                {
                    Image img = Image.FromFile(dialog.FileName);
                    Bitmap bmp = new Bitmap(img);
                    s = new SchetsWin(bmp);
                }
                
                s.MdiParent = this;
                s.Show();
            }
        }
        private void afsluiten(object sender, EventArgs e)
        {   this.Close();
        }
    }
}
