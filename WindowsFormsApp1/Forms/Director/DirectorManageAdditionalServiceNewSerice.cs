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

namespace WindowsFormsApp1.Forms.Director
{
    public partial class DirectorManageAdditionalServiceNewSerice : Form
    {
        public DirectorManageAdditionalServiceNewSerice()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.FoxForms.directorManageAdditionalService.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //newAdditionalService
            SqlCommand command = new SqlCommand("EXEC newAdditionalService @Name, @Desc", Program.sqlConnection);
            command.Parameters.Add("@Name", System.Data.SqlDbType.NVarChar).Value = textBox1.Text;
            command.Parameters.Add("@Desc", System.Data.SqlDbType.NVarChar).Value = textBox2.Text;

            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read() == false)
            {
                MessageBox.Show("Такая услуга/описание уже есть");
                reader.Close();
                return;
            }

            reader.Close();

            this.Hide();
            Program.FoxForms.directorManageAdditionalService.Show();
        }
    }
}
