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

namespace WindowsFormsApp1.Forms.Client
{
    public partial class ClientViewAvailableNumbers : Form
    {
        public ClientViewAvailableNumbers()
        {
            InitializeComponent();
        }

        private void ClientViewAvailableNumbers_Load(object sender, EventArgs e)
        {

        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            dataGridView1.Rows.Clear();

            SqlCommand command = new SqlCommand("SELECT pn.PhoneNumber FROM PhoneNumber AS pn WHERE pn.Availability = 'TRUE'", Program.sqlConnection);
            //command.Parameters.Add("@ClientFoxId", System.Data.SqlDbType.Int).Value = Program.currentClientId;
            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read() == false)
            {
                return;
            }

            do
            {
                dataGridView1.Rows.Add(reader.GetString(0));

            } while (reader.Read());

            reader.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.FoxForms.ClientFunctions.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
