using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Mandelbrot
{
    public partial class MandelForm : Form
    {
        private MandelbrotState _state;
        // Eigenschap met coordinaten en dergelijke die plaats/status van de mandelbrot aangeeft
        public MandelbrotState CurrentState
        {
            get
            {
                return _state;
            }
            // Wanneer deze waarde verandert wordt de UI geupdate
            set
            {
                _state = value;
                tbX.Text = _state.Center.X.ToString();
                tbY.Text = _state.Center.Y.ToString();
                tbScale.Text = _state.Scale.ToString();
                tbIterations.Text = _state.MaxIterations.ToString();
                this.DrawMandelBrot(_state);
            }
        }

        // Hier staan de presets met naam en de status die geladen wordt wanneer ze aangeklikt worden
        public Dictionary<string, MandelbrotState> Presets
        {
            get
            {
                return new Dictionary<string, MandelbrotState>
                {
                    { "Default", new MandelbrotState(100, 0.01, new PointD(0, 0)) },
                    { "Electricity", new MandelbrotState(100, 0.0000792281625142645, new PointD(-1.37518835894696, 0.0919723496064265)) },
                    { "Spirals", new MandelbrotState(100, 0.0000106338239662794, new PointD(-0.00642178511886223, 0.72326835393239)) },
                    { "Leaves", new MandelbrotState(100, 0.0000132922799578492, new PointD(-0.333040903671303, -0.613026246536015)) }
                };
            }
        }

        // Voeg de presets aan het menu toe
        private void addPresetsToMenu(MenuStrip menuStrip)
        {
            ToolStripMenuItem presets = new ToolStripMenuItem("Presets");
            foreach (KeyValuePair<string, MandelbrotState> Preset in Presets)
            {
                ToolStripMenuItem preset = new ToolStripMenuItem(Preset.Key);
                // Lambda expressie die aangeeft wat er moet gebeuren wanneer je op de preset klikt
                preset.Click += ((o, e) =>
                {
                    this.CurrentState = Preset.Value;
                });
                presets.DropDownItems.Add(preset);
            }
            menuStrip.Items.Add(presets);
        }

        public MandelForm()
        {
            InitializeComponent();
            addPresetsToMenu(this.menuStrip1);
            // Mousewheel staat niet als standaard property in de designer dus die wordt hier toegevoegd
            pictureBox1.MouseWheel += zoomOnMandelbrot;

            CurrentState = this.Presets["Default"];
        }

        // Teken de mandelbrot met een lager aantal iteraties zolang de gebruiker aan het resizen is
        private void resizeScreen(object sender, EventArgs e)
        {
            DrawMandelBrot(this.CurrentState, 20);
        }

        // Wanneer de gebruiker klaar is met resizen teken de mandelbrot met origineel aantal iteraties
        private void resizeScreenEnd(object sender, EventArgs e)
        {
            DrawMandelBrot(this.CurrentState);
        }

        // Inzoomen op de mandelbrot wanneer de gebruiker scrollt
        private void zoomOnMandelbrot(object sender, MouseEventArgs e)
        {
            // Nieuwe schaal afhankelijk van hoe ver er gescrollt is
            double newScale = this.CurrentState.Scale * Math.Pow(0.999, e.Delta);
            int width = pictureBox1.Width;
            int height = pictureBox1.Height;

            // Om 1 of andere reden kan er ook een delta van 0 optreden, in dit geval verandert er niks aan de coordinaten
            if (e.Delta > 0)
            {
                // Zorg dat het punt waar je op inzoomt onder de muis blijft met de nieuwe schaal
                double selectedX = ((e.X - width / 2)) * this.CurrentState.Scale + this.CurrentState.Center.X;
                double leftBorder = selectedX - e.X * newScale;
                double xOffset = leftBorder + (width / 2 * newScale);

                double selectedY = ((e.Y - height / 2)) * this.CurrentState.Scale + this.CurrentState.Center.Y;
                double topBorder = selectedY - e.Y * newScale;
                double yOffset = topBorder + (height / 2 * newScale);

                CurrentState = new MandelbrotState(CurrentState.MaxIterations, newScale, new PointD(xOffset, yOffset));
            }
            else
            {
                CurrentState = new MandelbrotState(CurrentState.MaxIterations, newScale, CurrentState.Center);
            }
        }

        // Inzoomen wanneer de gebruiker op de mandelbrot klikt, plaats ook de geklikte locatie in het midden
        private void clickOnMandelbrot(object sender, MouseEventArgs e)
        {
            int mouseX = e.X;
            int mouseY = e.Y;
            int width = pictureBox1.Width;
            int height = pictureBox1.Height;

            double selectedX = ((mouseX - width / 2)) * this.CurrentState.Scale + this.CurrentState.Center.X;
            double selectedY = ((mouseY - height / 2)) * this.CurrentState.Scale + this.CurrentState.Center.Y;

            CurrentState = new MandelbrotState(CurrentState.MaxIterations, this.CurrentState.Scale / 2, new PointD(selectedX, selectedY));
        }

        // Wanneer een gebruiker op enter drukt in 1 van de tekstvelden
        private void userChangedValue(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                double x;
                double y;
                double scale;
                uint iterations;

                // Controleer of alle velden correct zijn ingevuld
                if (
                    Double.TryParse(tbX.Text, out x)
                    && Double.TryParse(tbY.Text, out y)
                    && Double.TryParse(tbScale.Text, out scale)
                    && UInt32.TryParse(tbIterations.Text, out iterations))
                {
                    // Schrijf de nieuwe waarden
                    e.Handled = true;
                    this.CurrentState = new MandelbrotState(
                        iterations,
                        scale,
                        new PointD(x, y)
                    );
                }
                else
                {
                    MessageBox.Show("Invalid input");
                }
            }
        }

        public void DrawMandelBrot(MandelbrotState state, uint? iterations = null)
        {
            pictureBox1.Image = state.ToImage(this.pictureBox1.Size, iterations ?? state.MaxIterations);
        }
    }
}
