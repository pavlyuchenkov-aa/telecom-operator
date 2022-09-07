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
    public partial class DirectorTariffManageAddNew : Form
    {
        public DirectorTariffManageAddNew()
        {
            InitializeComponent();
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
        }

        private void DirectorTariffManageAddNew_Load(object sender, EventArgs e)
        {
            
            //string abon
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(!Program.IsDigitsOnly(textBox2.Text) || !Program.IsDigitsOnly(textBox3.Text) || !Program.IsDigitsOnly(textBox4.Text) || !Program.IsDigitsOnly(textBox5.Text) || !Program.IsDigitsOnly(textBox6.Text))
            {
                MessageBox.Show("Вводите только числа (положительные) во всех полях кроме названия");
                return;
            }

            if(textBox1.Text.Length > 200)
            {
                MessageBox.Show("Название содержит более 200 символов");
                return;
            }

            string name = textBox1.Text;
            int subsfee = Int32.Parse(textBox2.Text);
            int amountminutes = Int32.Parse(textBox3.Text);
            int amountsms = Int32.Parse(textBox4.Text);
            int amountgb = Int32.Parse(textBox5.Text);
            int extensubsfee = Int32.Parse(textBox6.Text);




            SqlCommand command = new SqlCommand("EXEC createNewTarif @name, @subsfee, @amountminutes, @amountsms, @amountgb, @extensubsfee, @avalav", Program.sqlConnection);
            command.Parameters.Add("@name", System.Data.SqlDbType.NVarChar).Value = name;
            command.Parameters.Add("@subsfee", System.Data.SqlDbType.Int).Value = subsfee;
            command.Parameters.Add("@amountminutes", System.Data.SqlDbType.Int).Value = amountminutes;
            command.Parameters.Add("@amountsms", System.Data.SqlDbType.Int).Value = amountsms;
            command.Parameters.Add("@amountgb", System.Data.SqlDbType.Int).Value = amountgb;
            command.Parameters.Add("@extensubsfee", System.Data.SqlDbType.Int).Value = extensubsfee;
            command.Parameters.Add("@avalav", System.Data.SqlDbType.Bit).Value = true;
            SqlDataReader reader = command.ExecuteReader();


            if(reader.Read() == false)
            {
                reader.Close();
                MessageBox.Show("Такой тариф уже есть");
                return;
            }


            reader.Close();

            this.Hide();

            Program.FoxForms.directorTariffManage.Show();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.FoxForms.directorTariffManage.Show();
        }
    }
}
