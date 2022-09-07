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
    public partial class ClientViewCurrentTarifs : Form
    {
        public ClientViewCurrentTarifs()
        {
            InitializeComponent();
        }

        private void ClientViewCurrentTarifs_Load(object sender, EventArgs e)
        {

        }
        private void ClientViewCurrentTarifs_Shown(object sender, EventArgs e)
        {
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();

            //MessageBox.Show("id: " + Program.currentClientId);

            SqlCommand command = new SqlCommand("EXEC PrintAllClientTariffs @ClientId", Program.sqlConnection);
            command.Parameters.Add("@ClientId", System.Data.SqlDbType.Int).Value = Program.currentClientId;
            SqlDataReader reader = command.ExecuteReader();
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();

            while (reader.Read())
            {
                string[] dataTarif = { reader.GetString(1), reader.GetString(3) };
                dataGridView1.Rows.Add(dataTarif);
            }


            reader.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.FoxForms.ClientFunctions.Show();
        }
    }
}
