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
    public partial class ManagerEditViewClientPersonal : Form
    {
        public struct ClientData
        {
            public ClientData(string nm, int id, int idlogin)
            {
                name = nm;
                ClientId = id;
                idLoginDetailsId = idlogin;
            }
            public string name;
            public int ClientId;
            public int idLoginDetailsId;
        }
        public List<ClientData> managersCollection = new List<ClientData>();


        public ManagerEditViewClientPersonal()
        {
            InitializeComponent();
        }
        protected override void OnVisibleChanged(EventArgs e)
        {
            listBox1.Items.Clear();
            textBox1.Text = "";
            textBox2.Text = "";

            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";

            managersCollection.Clear();
            SqlCommand command = new SqlCommand("EXEC getClientList", Program.sqlConnection);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read() == false)
            {
                return;
            }

            do
            {
                managersCollection.Add(new ClientData(reader.GetString(0), reader.GetInt32(1), reader.GetInt32(2)));
                listBox1.Items.Add(reader.GetString(0));
            } while (reader.Read());



        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            

        }
        private void ManagerEditViewClientPersonal_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            string fio = textBox1.Text;
            string login = textBox2.Text;

            string email = textBox4.Text;
            string passport = textBox5.Text;
            string address = textBox6.Text;
            string password = textBox7.Text;

            if (fio == "" || login == "" || email == "" || passport == "" || address == "")
            {
                MessageBox.Show("Одно или несколько полей пустые");
                return;
            }

            if (fio.Length > 100 || !Program.IsFIOValid(fio))
            {
                MessageBox.Show("Поле ФИО должно быть менее 100 символов и соответствовать формату");
                return;
            }

            if (login.Length > 32)
            {
                MessageBox.Show("Поле логин должно быть менее 32 символов");
                return;
            }
            if (password.Length > 32)
            {
                MessageBox.Show("Поле пароль должно быть менее 32 символов");
                return;
            }

            if (!Program.IsEmailValid(email) || email.Length > 100)
            {
                MessageBox.Show("Поле email должно быть в правильном формате и быть менее 100 символов");
                return;
            }
            if (address.Length > 100)
            {
                MessageBox.Show("Поле адрес должно быть быть менее 100 символов");
                return;
            }

            if (!Program.IsDigitsOnly(passport) || passport.Length != 10)
            {
                MessageBox.Show("Поле паспорт должно быть в правильном формате и должно состоять из 10 цифр");
                return;
            }



            if (password != "")
                password = Program.CreateMD5(password);

            SqlCommand command = new SqlCommand("EXEC setClientPersonalData @DirectorIdToUpdate, @NewDirectorName, @NewDirectorLogin, @NewDirectorPhoneNumber, @NewDirectorEmail, @NewDirectorPassport, @NewDirectorAddress, @NewDirectorPassword", Program.sqlConnection);
            command.Parameters.Add("@DirectorIdToUpdate", System.Data.SqlDbType.Int).Value = managersCollection[listBox1.SelectedIndex].ClientId;
            command.Parameters.Add("@NewDirectorName", System.Data.SqlDbType.NVarChar).Value = fio;
            command.Parameters.Add("@NewDirectorLogin", System.Data.SqlDbType.VarChar).Value = login;
            command.Parameters.Add("@NewDirectorPhoneNumber", System.Data.SqlDbType.NVarChar).Value = "1";
            command.Parameters.Add("@NewDirectorEmail", System.Data.SqlDbType.NVarChar).Value = email;
            command.Parameters.Add("@NewDirectorPassport", System.Data.SqlDbType.VarChar).Value = passport;

            command.Parameters.Add("@NewDirectorAddress", System.Data.SqlDbType.NVarChar).Value = address;

            if (password == "")
            {
                command.Parameters.Add("@NewDirectorPassword", System.Data.SqlDbType.VarChar).Value = DBNull.Value;
            }
            else
            {
                command.Parameters.Add("@NewDirectorPassword", System.Data.SqlDbType.VarChar).Value = password;
            }



            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read() == false)
            {
                MessageBox.Show("Одно из полей логин/паспорт/email уже существуют");
            }

            reader.Close();




            listBox1.Items.Clear();
            textBox1.Text = "";
            textBox2.Text = "";

            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";

            managersCollection.Clear();
            SqlCommand command1 = new SqlCommand("EXEC getClientList", Program.sqlConnection);
            SqlDataReader reader1 = command1.ExecuteReader();

            if (reader1.Read() == false)
            {
                return;
            }

            do
            {
                managersCollection.Add(new ClientData(reader1.GetString(0), reader1.GetInt32(1), reader1.GetInt32(2)));
                listBox1.Items.Add(reader1.GetString(0));
            } while (reader1.Read());

            reader1.Close();

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Program.FoxForms.managerFunctions.Show();
        }

        private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("EXEC getClientPersonalData @ManagerId", Program.sqlConnection);
            command.Parameters.Add("@ManagerId", System.Data.SqlDbType.Int).Value = managersCollection[listBox1.SelectedIndex].ClientId;

            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read() == false)
            {
                MessageBox.Show("No personal data on client");
                return;
            }

            textBox1.Text = reader.GetString(0);
            textBox2.Text = reader.GetString(1);

            textBox4.Text = reader.GetString(3);
            textBox5.Text = reader.GetString(4);
            textBox6.Text = reader.GetString(5);

            reader.Close();
        }
    }
}
