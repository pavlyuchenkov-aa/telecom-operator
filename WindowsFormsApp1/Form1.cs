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

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //Как клиент
        private void button1_Click(object sender, EventArgs e)
        {
            string login = textBox1.Text;
            string password = textBox2.Text;
            string databasePassword = "-1";

            string hashedPassword = Program.CreateMD5(password);

            //MessageBox.Show("login = " + login + " pass = " + password + " hashpass=" + hashedPassword);
            try
            {

                SqlCommand command1 = new SqlCommand("SELECT LoginDetails.Login, Client.ClientId FROM LoginDetails JOIN Client ON LoginDetails.LoginDetailsId=Client.IdLoginDetails AND Login=@Loginfox", Program.sqlConnection);
                command1.Parameters.Add("@Loginfox", System.Data.SqlDbType.VarChar).Value = login;

                SqlDataReader readerLogin = command1.ExecuteReader();
                

                

                if(readerLogin.Read() == false)
                {
                    MessageBox.Show("Нет такого логина");
                    readerLogin.Close();
                    return;
                }

                Program.currentClientId = readerLogin.GetInt32(1);
                readerLogin.Close();
                


            }
            catch(SqlException exp)
            {
                MessageBox.Show("err");
                return;
            }

            try
            {
                SqlCommand command2 = new SqlCommand("SELECT LoginDetails.Password FROM LoginDetails WHERE Login=@Loginfox", Program.sqlConnection);
                command2.Parameters.Add("@Loginfox", System.Data.SqlDbType.VarChar).Value = login;
                SqlDataReader readerPassword = command2.ExecuteReader();


                if(readerPassword.Read() == false)
                {
                    MessageBox.Show("err readerPassword.Read()");
                    return;
                }

                databasePassword = readerPassword.GetString(0);
                
                readerPassword.Close();

                
            }
            catch (SqlException exp)
            {
                MessageBox.Show("Internal pass read error");
            }

            if (!databasePassword.Equals(hashedPassword))
            {
                MessageBox.Show("Неправильный пароль");
                return;
            }

            Form1.ActiveForm.Hide();
            Program.FoxForms.ClientFunctions.Show();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string login = textBox1.Text;
            string password = textBox2.Text;

            string hashedPassword = Program.CreateMD5(password);

            SqlCommand command1 = new SqlCommand("EXEC loginDirector @login, @password", Program.sqlConnection);
            command1.Parameters.Add("@login", System.Data.SqlDbType.VarChar).Value = login;
            command1.Parameters.Add("@password", System.Data.SqlDbType.VarChar).Value = hashedPassword;

            SqlDataReader readerLogin = command1.ExecuteReader();

            if(readerLogin.Read() == false)
            {
                MessageBox.Show("loginDirector(@login varchar(32), @password varchar(32)) returned nothing");
                readerLogin.Close();
                return;
            }
            
            if (readerLogin.GetInt32(0) == -1)
            {
                MessageBox.Show("Такого логина не существует");

                return;
            }
            if (readerLogin.GetInt32(0) == -2)
            {
                MessageBox.Show("Неправильный пароль");

                return;
            }

            Program.currentDirectorId = readerLogin.GetInt32(0);
            readerLogin.Close();

            Form1.ActiveForm.Hide();
            Program.FoxForms.directorFunctions.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string login = textBox1.Text;
            string password = textBox2.Text;

            string hashedPassword = Program.CreateMD5(password);

            SqlCommand command1 = new SqlCommand("EXEC loginManager @login, @password", Program.sqlConnection);
            command1.Parameters.Add("@login", System.Data.SqlDbType.VarChar).Value = login;
            command1.Parameters.Add("@password", System.Data.SqlDbType.VarChar).Value = hashedPassword;

            SqlDataReader readerLogin = command1.ExecuteReader();

            if (readerLogin.Read() == false)
            {
                MessageBox.Show("loginManager(@login varchar(32), @password varchar(32)) returned nothing");
                readerLogin.Close();
                return;
            }

            if (readerLogin.GetInt32(0) == -1)
            {
                MessageBox.Show("Такого логина не существует");

                return;
            }
            if (readerLogin.GetInt32(0) == -2)
            {
                MessageBox.Show("Неправильный пароль");

                return;
            }

            Program.currentManagerIf = readerLogin.GetInt32(0);
            readerLogin.Close();

            Form1.ActiveForm.Hide();
            Program.FoxForms.managerFunctions.Show();
        }
    }
}
