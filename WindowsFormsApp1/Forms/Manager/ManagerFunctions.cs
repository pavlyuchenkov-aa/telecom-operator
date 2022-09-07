using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1.Forms.Manager
{
    public partial class ManagerFunctions : Form
    {
        public ManagerFunctions()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.FoxForms.Form1.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.FoxForms.managerViewTariff.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.FoxForms.managerEditViewClientPersonal.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.FoxForms.managerViewPersonal.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.FoxForms.managerEditNumbers.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.FoxForms.managerAddClientPhoneNumber.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.FoxForms.managerBlockUnblockAbonent.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.FoxForms.managerRegisterClient.Show();
        }
    }
}
