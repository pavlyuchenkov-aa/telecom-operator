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
    public partial class DirectorTariffManageChangeTariff : Form
    {
        public DirectorTariffManageChangeTariff()
        {
            InitializeComponent();
        }
        protected override void OnVisibleChanged(EventArgs e)
        {
            SqlCommand command = new SqlCommand("EXEC getTarifDataById @TarifId", Program.sqlConnection);
            command.Parameters.Add("@TarifId", System.Data.SqlDbType.Int).Value = Program.FoxForms.directorTariffManage.selectedTarifId;
            SqlDataReader reader = command.ExecuteReader();

            if(reader.Read() == false)
            {
                MessageBox.Show("Couldnt fetch tarif data, id:" + Program.FoxForms.directorTariffManage.selectedTarifId);
                return;
            }

            
            string[] dataTarif = { reader.GetString(0), reader.GetInt32(1).ToString(), reader.GetInt32(2).ToString(), reader.GetInt32(3).ToString(), reader.GetInt32(4).ToString(), reader.GetInt32(5).ToString(), reader.GetBoolean(6).ToString() };
            textBox1.Text = reader.GetString(0);
            textBox2.Text = reader.GetInt32(1).ToString();
            textBox3.Text = reader.GetInt32(2).ToString();
            textBox4.Text = reader.GetInt32(3).ToString();
            textBox5.Text = reader.GetInt32(4).ToString();
            textBox6.Text = reader.GetInt32(5).ToString();



            reader.Close();
        }
        private void DirectorTariffManageChangeTariff_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!Program.IsDigitsOnly(textBox2.Text) || !Program.IsDigitsOnly(textBox3.Text) || !Program.IsDigitsOnly(textBox4.Text) || !Program.IsDigitsOnly(textBox5.Text) || !Program.IsDigitsOnly(textBox6.Text))
            {
                MessageBox.Show("Вводите только числа (положительные) во всех полях кроме названия");
                return;
            }

            if (textBox1.Text.Length > 200)
            {
                MessageBox.Show("Название содержит более 200 символов");
                return;
            }

            string name = textBox1.Text;
            int subsfee = Int32.Parse(textBox2.Text);
            int amountminutes = Int32.Parse(textBox3.Text);
            int amountsms = Int32.Parse(textBox4.Text);
            int extensubsfee = Int32.Parse(textBox5.Text);
            int amountgb = Int32.Parse(textBox6.Text);

            SqlCommand command = new SqlCommand("EXEC changeTariff @TarifId, @name, @subsfee, @amountminutes, @amountsms, @amountgb, @extensubsfee, @avalav", Program.sqlConnection);
            command.Parameters.Add("@TarifId", System.Data.SqlDbType.Int).Value = Program.FoxForms.directorTariffManage.selectedTarifId;
            command.Parameters.Add("@name", System.Data.SqlDbType.NVarChar).Value = name;
            command.Parameters.Add("@subsfee", System.Data.SqlDbType.Int).Value = subsfee;
            command.Parameters.Add("@amountminutes", System.Data.SqlDbType.Int).Value = amountminutes;
            command.Parameters.Add("@amountsms", System.Data.SqlDbType.Int).Value = amountsms;
            command.Parameters.Add("@amountgb", System.Data.SqlDbType.Int).Value = amountgb;
            command.Parameters.Add("@extensubsfee", System.Data.SqlDbType.Int).Value = extensubsfee;
            command.Parameters.Add("@avalav", System.Data.SqlDbType.Bit).Value = true;
            SqlDataReader reader = command.ExecuteReader();


            if (reader.Read() == false)
            {
                reader.Close();
                MessageBox.Show("Такой тариф уже есть");
                return;
            }


            reader.Close();


            this.Hide();
            Program.FoxForms.directorTariffManage.Show();
        }
    }
}
