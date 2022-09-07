using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApp1.Forms.Manager
{
    public partial class ManagerViewPersonal : Form
    {
        public ManagerViewPersonal()
        {
            InitializeComponent();
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();

            SqlCommand command = new SqlCommand("EXEC getManagerPersonalData @DirectorId", Program.sqlConnection);
            command.Parameters.Add("@DirectorId", System.Data.SqlDbType.Int).Value = Program.currentManagerIf;

            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read() == false)
            {
                MessageBox.Show("No personal data for director");
            }

            textBox1.Text = reader.GetString(0);
            textBox2.Text = reader.GetString(1);
            textBox3.Text = reader.GetString(2);
            textBox4.Text = reader.GetString(3);
            textBox5.Text = reader.GetString(4);
            textBox6.Text = reader.GetString(5);


            reader.Close();
        }

        private void ManagerViewPersonal_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.FoxForms.managerFunctions.Show();
        }
    }
}
