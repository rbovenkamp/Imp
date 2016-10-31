using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace Reversi
{
    public partial class Menu : UserControl
    {
        
        public int bordBreedte
        {
            get
            {
                return (int)numericUpDown1.Value;
            }
        }

        public int bordHoogte
        {
            get
            {
                return (int)numericUpDown2.Value;
            }
        }

        public string TypeP1
        {
            get
            {
                return (string)comboBox1.SelectedValue;
            }
        }

        public string TypeP2
        {
            get
            {
                return (string)comboBox2.SelectedValue;
            }
        }

        public string NameP1
        {
            get
            {
                return textBox1.Text;
            }
        }

        public string NameP2
        {
            get
            {
                return textBox2.Text;
            }
        }

        public Color KleurP1
        {
            get
            {
                return button1.BackColor;
            }
        }

        public Color KleurP2
        {
            get
            {
                return button2.BackColor;
            }
        }


        public Menu()
        {
            InitializeComponent();
            voegOptiesToeAanComboBoxes();
        }

        private void voegOptiesToeAanComboBoxes()
        {
            var opties = Assembly.GetAssembly(typeof(Speler)).GetTypes()
                .Where(t => typeof(Speler).IsAssignableFrom(t))
                .ToList()
                .Select(x => new { Name = x.Name, AssemblyName = x.AssemblyQualifiedName })
                .OrderByDescending(x => x.Name)
                .ToList();

            opties.Remove(opties.FirstOrDefault(x => x.Name == "Speler"));

            comboBox1.DataSource = opties.ToList();
            comboBox2.DataSource = opties.ToList();

            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "AssemblyName";

            comboBox2.DisplayMember = "Name";
            comboBox2.ValueMember = "AssemblyName";

            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                button1.BackColor = colorDialog1.Color;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (colorDialog2.ShowDialog() == DialogResult.OK)
            {
                button2.BackColor = colorDialog2.Color;
            }
        }
        

    }
}
