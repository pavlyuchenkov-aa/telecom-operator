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
    public partial class DirectorRegisterManager : Form
    {
        public DirectorRegisterManager()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.FoxForms.directorFunctions.Show();
        }
        protected override void OnVisibleChanged(EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
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

           

            if (fio == "" || login == "" || phonenumber == "" || email == "" || passport == "" || address == "" || password == "")
            {
                MessageBox.Show("Одно или несколько полей пустые");
                return;
            }

            if(fio.Length > 100 || !Program.IsFIOValid(fio))
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
            if(!Program.IsEmailValid(email) || email.Length > 100)
            {
                MessageBox.Show("Поле email должно быть в правильном формате и быть менее 100 символов");
                return;
            }
            if(address.Length > 100)
            {
                MessageBox.Show("Поле адрес должно быть быть менее 100 символов");
                return;
            }

            if(!Program.IsDigitsOnly(passport) || passport.Length != 10)
            {
                MessageBox.Show("Поле паспорт должно быть в правильном формате и должно состоять из 10 цифр");
                return;
            }
            

            password = Program.CreateMD5(password);

            SqlCommand command = new SqlCommand("EXEC Register_Client @Login, @Password, @PassportData, @Name, @PhoneNumber, @Address, @Email, 2", Program.sqlConnection);
            //command.Parameters.Add("@DirectorIdToUpdate", System.Data.SqlDbType.Int).Value = Program.currentDirectorId;
            command.Parameters.Add("@Name", System.Data.SqlDbType.NVarChar).Value = fio;
            command.Parameters.Add("@Login", System.Data.SqlDbType.VarChar).Value = login;
            command.Parameters.Add("@PhoneNumber", System.Data.SqlDbType.NVarChar).Value = phonenumber;
            command.Parameters.Add("@Email", System.Data.SqlDbType.NVarChar).Value = email;
            command.Parameters.Add("@PassportData", System.Data.SqlDbType.VarChar).Value = passport;

            command.Parameters.Add("@Address", System.Data.SqlDbType.NVarChar).Value = address;

            
            command.Parameters.Add("@Password", System.Data.SqlDbType.VarChar).Value = password;
            



            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read() == false)
            {
                MessageBox.Show("Одно из полей логин/паспорт/email/телефон уже существуют в бд");
                return;
            }

            reader.Close();

            this.Hide();
            Program.FoxForms.directorFunctions.Show();

        }

        private void DirectorRegisterManager_Load(object sender, EventArgs e)
        {

        }
    }
}
