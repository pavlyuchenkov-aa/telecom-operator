using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1.Forms.Director
{
    public partial class DirectorFunctions : Form
    {
        public DirectorFunctions()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.FoxForms.directorTariffManage.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.FoxForms.directorPersonalDataEditView.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.FoxForms.directorManagerPersonalDataEditView.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.FoxForms.directorRegisterManager.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.FoxForms.directorEditPhoneNumbers.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.FoxForms.directorManageAdditionalService.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.FoxForms.Form1.Show();
        }
    }
}
