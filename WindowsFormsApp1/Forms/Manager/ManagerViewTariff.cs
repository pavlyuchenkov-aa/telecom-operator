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
    public partial class ManagerViewTariff : Form
    {
        public ManagerViewTariff()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("EXEC getTarifList", Program.sqlConnection);
            //command.Parameters.Add("@Loginfox", System.Data.SqlDbType.VarChar).Value = login;
            SqlDataReader reader = command.ExecuteReader();
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();

            while (reader.Read())
            {
                if (reader.GetBoolean(6) == true)
                {
                    string[] dataTarif = { reader.GetString(0), reader.GetInt32(1).ToString(), reader.GetInt32(2).ToString(), reader.GetInt32(3).ToString(), reader.GetInt32(4).ToString(), reader.GetInt32(5).ToString() };
                    dataGridView1.Rows.Add(dataTarif);
                }
            }


            reader.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.FoxForms.managerFunctions.Show();
        }
    }
}
