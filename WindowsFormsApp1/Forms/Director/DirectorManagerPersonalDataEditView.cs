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
    public struct ManagerData
    {
        public ManagerData(string nm, int id, int idlogin)
        {
            name = nm;
            ManagerId = id;
            idLoginDetailsId = idlogin;
        }
        public string name;
        public int ManagerId;
        public int idLoginDetailsId;
    }
    public partial class DirectorManagerPersonalDataEditView : Form
    {
        public List<ManagerData> managersCollection = new List<ManagerData>(); //name, ManagerId, idLoginDetails
        public DirectorManagerPersonalDataEditView()
        {
            InitializeComponent();
        }

        private void DirectorManagerPersonalDataEditView_Load(object sender, EventArgs e)
        {

        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            listBox1.Items.Clear();
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";

            managersCollection.Clear();
            SqlCommand command = new SqlCommand("EXEC getManagerList", Program.sqlConnection);
            SqlDataReader reader = command.ExecuteReader();

            if(reader.Read() == false)
            {
                return;
            }

            do
            {
                managersCollection.Add(new ManagerData(reader.GetString(0), reader.GetInt32(1), reader.GetInt32(2)));
                listBox1.Items.Add(reader.GetString(0));
            } while (reader.Read());



        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.FoxForms.directorFunctions.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string fio = textBox1.Text;
            string login = textBox2.Text;
            string phonenumber = textBox3.Text;
            string email = textBox4.Text;
            string passport = textBox5.Text;
            string address = textBox6.Text;
            string password = textBox7.Text;

            if (fio == "" || login == "" || phonenumber == "" || email == "" || passport == "" || address == "")
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

            if (!Program.IsPhoneValid(phonenumber))
            {
                MessageBox.Show("Поле номер телефона должно быть в правильном формате и быть менее 15 цифр");
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

            SqlCommand command = new SqlCommand("EXEC setManagerPersonalData @DirectorIdToUpdate, @NewDirectorName, @NewDirectorLogin, @NewDirectorPhoneNumber, @NewDirectorEmail, @NewDirectorPassport, @NewDirectorAddress, @NewDirectorPassword", Program.sqlConnection);
            command.Parameters.Add("@DirectorIdToUpdate", System.Data.SqlDbType.Int).Value = managersCollection[listBox1.SelectedIndex].ManagerId;
            command.Parameters.Add("@NewDirectorName", System.Data.SqlDbType.NVarChar).Value = fio;
            command.Parameters.Add("@NewDirectorLogin", System.Data.SqlDbType.VarChar).Value = login;
            command.Parameters.Add("@NewDirectorPhoneNumber", System.Data.SqlDbType.NVarChar).Value = phonenumber;
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
                MessageBox.Show("Одно из полей логин/паспорт/email/телефон уже существуют в бд");
            }

            reader.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("EXEC deleteManager @ManagerIdLoginDetails", Program.sqlConnection);
            command.Parameters.Add("@ManagerIdLoginDetails", System.Data.SqlDbType.Int).Value = managersCollection[listBox1.SelectedIndex].idLoginDetailsId;

            //foxes dont care about errors in requests
            command.ExecuteNonQuery();

            //update all window
            listBox1.Items.Clear();
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";

            managersCollection.Clear();
            SqlCommand command1 = new SqlCommand("EXEC getManagerList", Program.sqlConnection);
            SqlDataReader reader1 = command1.ExecuteReader();

            if (reader1.Read() == false)
            {
                return;
            }

            do
            {
                managersCollection.Add(new ManagerData(reader1.GetString(0), reader1.GetInt32(1), reader1.GetInt32(2)));
                listBox1.Items.Add(reader1.GetString(0));
            } while (reader1.Read());
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("EXEC getManagerPersonalData @ManagerId", Program.sqlConnection);
            command.Parameters.Add("@ManagerId", System.Data.SqlDbType.Int).Value = managersCollection[listBox1.SelectedIndex].ManagerId;

            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read() == false)
            {
                MessageBox.Show("No personal data on manager");
                return;
            }

            textBox1.Text = reader.GetString(0);
            textBox2.Text = reader.GetString(1);
            textBox3.Text = reader.GetString(2);
            textBox4.Text = reader.GetString(3);
            textBox5.Text = reader.GetString(4);
            textBox6.Text = reader.GetString(5);

            reader.Close();

        }
    }
}
