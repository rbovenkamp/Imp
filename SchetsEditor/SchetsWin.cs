using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using System.Resources;
using SchetsEditor.Tools;
using SchetsEditor.Dialog;
using System.Drawing.Imaging;
using SchetsEditor.Historie;
using System.IO;

namespace SchetsEditor
{
    public class SchetsWin : Form
    {   
        MenuStrip menuStrip;
        public SchetsControl schetscontrol;
        ISchetsTool huidigeTool;
        Panel paneel;
        bool vast;
        ResourceManager resourcemanager
            = new ResourceManager("SchetsEditor.Properties.Resources"
                                 , Assembly.GetExecutingAssembly()
                                 );

        private void veranderAfmeting(object o, EventArgs ea)
        {
            schetscontrol.Size = new Size ( this.ClientSize.Width  - 70
                                          , this.ClientSize.Height - 50);
            paneel.Location = new Point(64, this.ClientSize.Height - 30);
        }

        private void klikToolMenu(object obj, EventArgs ea)
        {
            this.huidigeTool = (ISchetsTool)((ToolStripMenuItem)obj).Tag;
        }

        private void klikToolButton(object obj, EventArgs ea)
        {
            this.huidigeTool = (ISchetsTool)((RadioButton)obj).Tag;
        }

        private void afsluiten(object obj, EventArgs ea)
        {
            if (schetscontrol.Schets.Bewerkt == true)
            {
                DialogResult result = MessageBox.Show("Je hebt aanpassingen gedaan, wil je opslaan?", "Afsluiten", MessageBoxButtons.YesNoCancel);
                switch(result)
                {
                    case DialogResult.Yes:
                        opslaan(obj, ea);
                        schetscontrol.Schets.Bewerkt = false;
                        this.Close();
                        break;
                    case DialogResult.No:
                        schetscontrol.Schets.Bewerkt = false;
                        this.Close();
                        break;
                }
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (e.CloseReason == CloseReason.WindowsShutDown) return;

            afsluiten(this, e);
            if (schetscontrol.Schets.Bewerkt == true)
                e.Cancel = true;
        }

        private void opslaan(object obj, EventArgs ea)
        {
            SaveImageDialog dialog = new SaveImageDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                switch (dialog.SelectedImageType)
                {
                    case SaveImageDialog.ImageType.Bmp:
                        schetscontrol.Schets.Bitmap.Save(dialog.FileName);
                        break;
                    case SaveImageDialog.ImageType.Jpeg:
                        schetscontrol.Schets.Bitmap.Save(dialog.FileName, ImageFormat.Jpeg);
                        break;
                    case SaveImageDialog.ImageType.Png:
                        schetscontrol.Schets.Bitmap.Save(dialog.FileName, ImageFormat.Png);
                        break;
                    case SaveImageDialog.ImageType.Schets:
                        File.WriteAllText(dialog.FileName, schetscontrol.Schets.Historie.Serialiseer());
                        break;
                }
            }
        }

        private void undo(object obj, EventArgs ea)
        {
            SchetsHistorie h = schetscontrol.Schets.Historie;
            if (!(h.Peek() is PlaatjeObject) && h.Peek() != null)
            {
                h.Undo();
                schetscontrol.Invalidate();
            }
        }

        private void redo(object obj, EventArgs ea)
        {
            SchetsHistorie h = schetscontrol.Schets.Historie;
            if (h.PeekToekomst() != null)
            {
                h.BACK_TO_THE_FUTURE();
                schetscontrol.Invalidate();
            }
        }

        public SchetsWin()
        {
            schetscontrol = new SchetsControl();
            this.ClientSize = new Size(700, 500);
            initialiseerControls();
        }

        public SchetsWin(Bitmap bmp)
        {
            schetscontrol = new SchetsControl(bmp);
            this.ClientSize = new Size(bmp.Width, bmp.Height);
            initialiseerControls();
        }

        public SchetsWin(string schetsBestand)
        {
            schetscontrol = new SchetsControl();
            this.ClientSize = new Size(700, 500);
            initialiseerControls();
            schetscontrol.LaadHistorie(schetsBestand);
        }

        private void initialiseerControls()
        {
            ISchetsTool[] deTools = { new PenTool()
                                    , new LijnTool()
                                    , new OvaalTool()
                                    , new VolOvaalTool()
                                    , new RechthoekTool()
                                    , new VolRechthoekTool()
                                    , new TekstTool()
                                    , new GumTool()
                                    , new VerplaatsTool()
                                    };
            
            huidigeTool = deTools[0];

            schetscontrol.Location = new Point(64, 10);
            schetscontrol.MouseDown += (object o, MouseEventArgs mea) =>
            {
                vast = true;
                huidigeTool.MuisVast(schetscontrol, mea.Location);
            };
            schetscontrol.MouseMove += (object o, MouseEventArgs mea) =>
            {
                if (vast)
                    huidigeTool.MuisDrag(schetscontrol, mea.Location);
            };
            schetscontrol.MouseUp += (object o, MouseEventArgs mea) =>
            {
                if (vast)
                    huidigeTool.MuisLos(schetscontrol, mea.Location);
                vast = false;
            };
            schetscontrol.KeyPress += (object o, KeyPressEventArgs kpea) =>
            {
                huidigeTool.Letter(schetscontrol, kpea.KeyChar);
            };
            this.Controls.Add(schetscontrol);

            schetscontrol.KeyDown += schetscontrol_KeyDown;

            menuStrip = new MenuStrip();
            menuStrip.Visible = false;
            this.BackColor = Color.White;
            this.Controls.Add(menuStrip);
            this.maakFileMenu();
            this.maakToolMenu(deTools);
            this.maakAktieMenu();
            this.maakToolButtons(deTools);
            this.maakAktieButtons();
            this.Resize += this.veranderAfmeting;
            this.veranderAfmeting(null, null);
        }

        private void schetscontrol_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Z)
            {
                this.undo(sender, e);
            }
            if (e.Control && e.KeyCode == Keys.Y)
            {
                this.redo(sender, e);
            }
        }

        private void maakFileMenu()
        {   
            ToolStripMenuItem menu = new ToolStripMenuItem("File");
            menu.MergeAction = MergeAction.MatchOnly;
            menu.DropDownItems.Add("Opslaan", null, this.opslaan);
            menu.DropDownItems.Add("Sluiten", null, this.afsluiten);
            menuStrip.Items.Add(menu);
        }

        private void maakToolMenu(ICollection<ISchetsTool> tools)
        {   
            ToolStripMenuItem menu = new ToolStripMenuItem("Tool");
            foreach (ISchetsTool tool in tools)
            {   ToolStripItem item = new ToolStripMenuItem();
                item.Tag = tool;
                item.Text = tool.ToString();
                item.Image = (Image)resourcemanager.GetObject(tool.ToString());
                item.Click += this.klikToolMenu;
                menu.DropDownItems.Add(item);
            }
            menuStrip.Items.Add(menu);
        }

        private void maakAktieMenu()
        {   
            ToolStripMenuItem menu = new ToolStripMenuItem("Aktie");
            menu.DropDownItems.Add("Terug (ctrl + z)", null, undo);
            menu.DropDownItems.Add("Vooruit (ctrl + y)", null, redo);
            menu.DropDownItems.Add("Clear", null, schetscontrol.Schoon );
            menu.DropDownItems.Add("Kies kleur", null, (o, e) => schetscontrol.VeranderKleur(kleurButton, new EventArgs()));
            menuStrip.Items.Add(menu);
        }

        private void maakToolButtons(ICollection<ISchetsTool> tools)
        {
            int t = 0;
            foreach (ISchetsTool tool in tools)
            {
                RadioButton b = new RadioButton();
                b.Appearance = Appearance.Button;
                b.Size = new Size(45, 62);
                b.Location = new Point(10, 10 + t * 62);
                b.Tag = tool;
                b.Text = tool.ToString();
                b.Image = (Image)resourcemanager.GetObject(tool.ToString());
                b.TextAlign = ContentAlignment.TopCenter;
                b.ImageAlign = ContentAlignment.BottomCenter;
                b.Click += this.klikToolButton;
                this.Controls.Add(b);
                if (t == 0) b.Select();
                t++;
            }
        }
        Button kleurButton;
        NumericUpDown dikteButton;
        private void maakAktieButtons()
        {   
            paneel = new Panel();
            paneel.Size = new Size(600, 24);
            this.Controls.Add(paneel);
            
            Button b; Label l;
            b = new Button(); 
            b.Text = "Clear";  
            b.Location = new Point(  0, 0); 
            b.Click += schetscontrol.Schoon; 
            paneel.Controls.Add(b);
            
            b = new Button(); 
            b.Text = "Rotate"; 
            b.Location = new Point( 80, 0); 
            b.Click += schetscontrol.Roteer; 
            paneel.Controls.Add(b);
            
            l = new Label();  
            l.Text = "Pen kleur:"; 
            l.Location = new Point(170, 3); 
            l.AutoSize = true;               
            paneel.Controls.Add(l);

            kleurButton = new Button();
            kleurButton.BackColor = schetscontrol.PenKleur;
            kleurButton.Location = new Point(230, 0);
            kleurButton.Click += schetscontrol.VeranderKleur;
            paneel.Controls.Add(kleurButton);

            l = new Label();
            l.Text = "Pen dikte:";
            l.Location = new Point(320, 3);
            l.AutoSize = true;
            paneel.Controls.Add(l);

            dikteButton = new NumericUpDown();
            dikteButton.Value = schetscontrol.PenDikte;
            dikteButton.Location = new Point(380, 0);
            dikteButton.ValueChanged += schetscontrol.VeranderPenDikte;
            paneel.Controls.Add(dikteButton);
        }
    }
}
