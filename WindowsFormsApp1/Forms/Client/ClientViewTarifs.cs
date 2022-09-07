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

namespace WindowsFormsApp1.Forms
{
    public partial class ClientViewTarifs : Form
    {
        public ClientViewTarifs()
        {
            InitializeComponent();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.FoxForms.ClientFunctions.Show();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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

        private void button3_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("EXEC getTarifPopular", Program.sqlConnection);
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

        private void button4_Click(object sender, EventArgs e)
        {
            if(textBox1.Text.Length == 0 || textBox2.Text.Length == 0)
            {
                MessageBox.Show("Пожалуйса, укажите минимальную и маскимальную цену");
                return;
            }
            if(!Program.IsDigitsOnly(textBox1.Text) || !Program.IsDigitsOnly(textBox2.Text))
            {
                MessageBox.Show("Пожалуйса, укажите минимальную и маскимальную цену в цифрах");
                return;
            }
            if (Int32.Parse(textBox2.Text) > Int32.Parse(textBox1.Text))
            {
                MessageBox.Show("Введите корректные данные. Минимальная цена больше максимальной!");
                return;
            }

            //SELECT t.TariffId, t.TariffName, t.SubscriptionFee, t.AmountMinutes, t.AmountSMS, t.AmountInternetGB, t.ExtendedSubsFee FROM Tariff AS t WHERE t.SubscriptionFee BETWEEN @MinSubscriptionFee AND @MaxSubscriptionFee
            SqlCommand command = new SqlCommand("EXEC getTarifByPrice @MinSubscriptionFee, @MaxSubscriptionFee", Program.sqlConnection);
            command.Parameters.Add("@MinSubscriptionFee", System.Data.SqlDbType.Int).Value = textBox2.Text;
            command.Parameters.Add("@MaxSubscriptionFee", System.Data.SqlDbType.Int).Value = textBox1.Text;
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

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void ClientViewTarifs_Load(object sender, EventArgs e)
        {

        }

        private void ClientViewTarifs_Shown(object sender, EventArgs e)
        {
            
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
        }
    }
}
