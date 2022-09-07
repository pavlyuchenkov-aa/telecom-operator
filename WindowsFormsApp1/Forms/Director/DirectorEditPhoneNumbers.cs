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
    public partial class DirectorEditPhoneNumbers : Form
    {
        public struct PhoneNumbersData
        {
            public PhoneNumbersData(int id, string displayname)
            {
                PhoneNumberId = id;
                PhoneNumberName = displayname;
            }
            public int PhoneNumberId;
            public string PhoneNumberName;
        }

        public List<PhoneNumbersData> phoneNumbersCollection = new List<PhoneNumbersData>();
        public DirectorEditPhoneNumbers()
        {
            InitializeComponent();
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            dataGridView1.Rows.Clear();
            phoneNumbersCollection.Clear();

            textBox2.Clear();

            SqlCommand command = new SqlCommand("EXEC directorGetAvailablePhoneNumbers", Program.sqlConnection);
            //command.Parameters.Add("@DirectorId", System.Data.SqlDbType.Int).Value = Program.currentDirectorId;

            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read() == false)
            {
                reader.Close();
                return;
            }

            do
            {
                phoneNumbersCollection.Add(new PhoneNumbersData(reader.GetInt32(0), reader.GetString(1)));

                string[] data = { reader.GetString(1), reader.GetBoolean(2).ToString() };
                dataGridView1.Rows.Add(data);

                //listBox1.Items.Add(reader.GetString(1));
            }
            while (reader.Read());

            reader.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();

            Program.FoxForms.directorFunctions.Show();
        }

        private void DirectorEditPhoneNumbers_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("EXEC setPhoneNumberAvalability @idToChange, @Avalability", Program.sqlConnection);
            command.Parameters.Add("@idToChange", System.Data.SqlDbType.Int).Value = phoneNumbersCollection[dataGridView1.SelectedRows[0].Index].PhoneNumberId;
            command.Parameters.Add("@Avalability", System.Data.SqlDbType.Bit).Value = true;

            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read() == false)
            {
                MessageBox.Show("Такой номер уже есть");
                reader.Close();
                return;
            }

            reader.Close();




            dataGridView1.Rows.Clear();
            phoneNumbersCollection.Clear();

            textBox2.Clear();

            SqlCommand command1 = new SqlCommand("EXEC directorGetAvailablePhoneNumbers", Program.sqlConnection);
            //command.Parameters.Add("@DirectorId", System.Data.SqlDbType.Int).Value = Program.currentDirectorId;

            SqlDataReader reader1 = command1.ExecuteReader();

            if (reader1.Read() == false)
            {
                reader1.Close();
                return;
            }

            do
            {
                phoneNumbersCollection.Add(new PhoneNumbersData(reader1.GetInt32(0), reader1.GetString(1)));

                string[] data = { reader1.GetString(1), reader1.GetBoolean(2).ToString() };
                dataGridView1.Rows.Add(data);

                //listBox1.Items.Add(reader.GetString(1));
            }
            while (reader1.Read());

            reader1.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(!Program.IsPhoneValid(textBox2.Text))
            {
                MessageBox.Show("Номер должен быть в правильном формате и быть менее 15 цифр");
                return;
            }

            SqlCommand command = new SqlCommand("EXEC newPhoneNumber @NewName", Program.sqlConnection);
            
            command.Parameters.Add("@NewName", System.Data.SqlDbType.Char).Value = textBox2.Text;

            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read() == false)
            {
                MessageBox.Show("Такой номер уже есть");
                reader.Close();
                return;
            }

            reader.Close();




            dataGridView1.Rows.Clear();
            phoneNumbersCollection.Clear();

            textBox2.Clear();


            SqlCommand command1 = new SqlCommand("EXEC directorGetAvailablePhoneNumbers", Program.sqlConnection);
            //command.Parameters.Add("@DirectorId", System.Data.SqlDbType.Int).Value = Program.currentDirectorId;

            SqlDataReader reader1 = command1.ExecuteReader();

            if (reader1.Read() == false)
            {
                reader1.Close();
                return;
            }

            do
            {
                phoneNumbersCollection.Add(new PhoneNumbersData(reader1.GetInt32(0), reader1.GetString(1)));

                string[] data = { reader1.GetString(1), reader1.GetBoolean(2).ToString() };
                dataGridView1.Rows.Add(data);

                //listBox1.Items.Add(reader.GetString(1));
            }
            while (reader1.Read());

            reader1.Close();


        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("EXEC setPhoneNumberAvalability @idToChange, @Avalability", Program.sqlConnection);
            command.Parameters.Add("@idToChange", System.Data.SqlDbType.Int).Value = phoneNumbersCollection[dataGridView1.SelectedRows[0].Index].PhoneNumberId;
            command.Parameters.Add("@Avalability", System.Data.SqlDbType.Bit).Value = false;

            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read() == false)
            {
                MessageBox.Show("err");
                reader.Close();
                return;
            }

            reader.Close();




            dataGridView1.Rows.Clear();
            phoneNumbersCollection.Clear();

            textBox2.Clear();

            SqlCommand command1 = new SqlCommand("EXEC directorGetAvailablePhoneNumbers", Program.sqlConnection);
            //command.Parameters.Add("@DirectorId", System.Data.SqlDbType.Int).Value = Program.currentDirectorId;

            SqlDataReader reader1 = command1.ExecuteReader();

            if (reader1.Read() == false)
            {
                reader1.Close();
                return;
            }

            do
            {
                phoneNumbersCollection.Add(new PhoneNumbersData(reader1.GetInt32(0), reader1.GetString(1)));

                string[] data = { reader1.GetString(1), reader1.GetBoolean(2).ToString() };
                dataGridView1.Rows.Add(data);

                //listBox1.Items.Add(reader.GetString(1));
            }
            while (reader1.Read());

            reader1.Close();
        }
    }
}
