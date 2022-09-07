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
    public partial class DirectorTariffManage : Form
    {
        public int selectedTarifId = -1;

        public DirectorTariffManage()
        {
            InitializeComponent();
        }

        private void DirectorTariffManage_Load(object sender, EventArgs e)
        {

        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            if(dataGridView1.SelectedRows == null)
            {
                selectedTarifId = -1;
            }


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
                string[] dataTarif = { reader.GetString(0), reader.GetInt32(1).ToString(), reader.GetInt32(2).ToString(), reader.GetInt32(3).ToString(), reader.GetInt32(4).ToString(), reader.GetInt32(5).ToString(), reader.GetBoolean(6).ToString() };
                dataGridView1.Rows.Add(dataTarif);
            }


            reader.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("EXEC getTarifPopularWithAvalability", Program.sqlConnection);
            //command.Parameters.Add("@Loginfox", System.Data.SqlDbType.VarChar).Value = login;
            SqlDataReader reader = command.ExecuteReader();

            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();

            while (reader.Read())
            {
                string[] dataTarif = { reader.GetString(0), reader.GetInt32(1).ToString(), reader.GetInt32(2).ToString(), reader.GetInt32(3).ToString(), reader.GetInt32(4).ToString(), reader.GetInt32(5).ToString(), reader.GetBoolean(6).ToString() };
                dataGridView1.Rows.Add(dataTarif);
            }


            reader.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 0 || textBox2.Text.Length == 0)
            {
                MessageBox.Show("Пожалуйса, укажите минимальную и маскимальную цену");
                return;
            }
            if (!Program.IsDigitsOnly(textBox1.Text) || !Program.IsDigitsOnly(textBox2.Text))
            {
                MessageBox.Show("Пожалуйса, укажите минимальную и маскимальную цену в цифрах");
                return;
            }

            if(Int32.Parse(textBox2.Text) > Int32.Parse(textBox1.Text))
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
                string[] dataTarif = { reader.GetString(0), reader.GetInt32(1).ToString(), reader.GetInt32(2).ToString(), reader.GetInt32(3).ToString(), reader.GetInt32(4).ToString(), reader.GetInt32(5).ToString(), reader.GetBoolean(6).ToString() };
                dataGridView1.Rows.Add(dataTarif);
            }


            reader.Close();
        }

        private int updateSelectedTarifIf()
        {
            if (dataGridView1.SelectedRows == null)
            {
                MessageBox.Show("Выберите тариф из списка");
                return -1;
            }
            SqlCommand command = new SqlCommand("EXEC getTarifId @TariffName", Program.sqlConnection);
            command.Parameters.Add("@TariffName", System.Data.SqlDbType.NVarChar).Value = dataGridView1.SelectedRows[0].Cells[0].Value;

            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read() == false)
            {
                MessageBox.Show("Tariff error");
                return -1;
            }


            this.selectedTarifId = reader.GetInt32(0);

            reader.Close();
            return 0;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if(updateSelectedTarifIf() == -1)
            {
                return;
            }
            if(selectedTarifId == 3)
            {
                MessageBox.Show("Данный тариф является тарифом по-умолчанию. Его нельзя деактивировать");
                return;
            }

            SqlCommand command1 = new SqlCommand("EXEC deactivateTariff @TariffId", Program.sqlConnection);
            command1.Parameters.Add("@TariffId", System.Data.SqlDbType.Int).Value = selectedTarifId;

            command1.ExecuteNonQuery();

            button2_Click(null, null);

            //getTarifId

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.FoxForms.directorFunctions.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (updateSelectedTarifIf() == -1)
            {
                return;
            }
            this.Hide();
            Program.FoxForms.directorTariffManageChangeTariff.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.FoxForms.directorTariffManageAddNew.Show();
        }
    }
}
