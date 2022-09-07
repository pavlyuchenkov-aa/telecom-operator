using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class ClientFunctions : Form
    {
        public ClientFunctions()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.FoxForms.ClientViewTarifs.Show();

        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.FoxForms.Form1.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.FoxForms.clientViewCurrentTarifs.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.FoxForms.clientViewPersonalData.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.FoxForms.clientChangeTariff.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.FoxForms.clientViewAvailableNumbers.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.FoxForms.clientBuyViewAdditionalService.Show();
        }
    }
}
