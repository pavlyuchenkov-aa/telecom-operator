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
    public partial class ManagerRegisterClient : Form
    {
        public struct ClientData
        {
            public ClientData(string nm, int id)
            {
                name = nm;
                ClientId = id;
            }
            public string name;
            public int ClientId;
        }

        public struct FreeNumbers
        {
            public FreeNumbers(string phoneNumber, int phoneNumberId)
            {
                PhoneNumber = phoneNumber;
                PhoneNumberId = phoneNumberId;
            }
            public string PhoneNumber;
            public int PhoneNumberId;
        }

        public struct Tariff
        {
            public Tariff(string tariffName, int subscriptionFee, int amountMinutes, int amountSMS, int extendedSubsFee, int amountInternetGB, bool availability, int tariffId)
            {
                TariffName = tariffName;
                SubscriptionFee = subscriptionFee;
                AmountMinutes = amountMinutes;
                AmountSMS = amountSMS;
                ExtendedSubsFee = extendedSubsFee;
                AmountInternetGB = amountInternetGB;
                Availability = availability;
                TariffId = tariffId;
        }
            public string TariffName;
            public int SubscriptionFee;
            public int AmountMinutes;
            public int AmountSMS;
            public int ExtendedSubsFee;
            public int AmountInternetGB;
            public bool Availability;
            public int TariffId;
        }


        public List<ClientData> clientsCollection = new List<ClientData>();
        public List<FreeNumbers> freeNumbersCollection = new List<FreeNumbers>();
        public List<Tariff> tariffCollection = new List<Tariff>();

        public ManagerRegisterClient()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string fio = textBox1.Text;
            string login = textBox2.Text;
            string email = textBox4.Text;
            string passport = textBox5.Text;
            string address = textBox6.Text;
            string password = textBox7.Text;

            if (fio == "" || login == "" || email == "" || passport == "" || address == "" || password == "")
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

            if(listBox2.SelectedIndex == -1)
            {
                MessageBox.Show("Нужно выбрать доступный номер клиенту");
                return;
            }

            password = Program.CreateMD5(password);

            SqlCommand command = new SqlCommand("EXEC Register_Client @Login, @Password, @PassportData, @Name, @PhoneNumber, @Address, @Email, 1", Program.sqlConnection);
            //command.Parameters.Add("@DirectorIdToUpdate", System.Data.SqlDbType.Int).Value = Program.currentDirectorId;
            command.Parameters.Add("@Name", System.Data.SqlDbType.NVarChar).Value = fio;
            command.Parameters.Add("@Login", System.Data.SqlDbType.VarChar).Value = login;
            command.Parameters.Add("@PhoneNumber", System.Data.SqlDbType.NVarChar).Value = "1";
            command.Parameters.Add("@Email", System.Data.SqlDbType.NVarChar).Value = email;
            command.Parameters.Add("@PassportData", System.Data.SqlDbType.VarChar).Value = passport;

            command.Parameters.Add("@Address", System.Data.SqlDbType.NVarChar).Value = address;


            command.Parameters.Add("@Password", System.Data.SqlDbType.VarChar).Value = password;




            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read() == false)
            {
                MessageBox.Show("Одно из полей логин/паспорт/email уже существуют в бд");
                return;
            }

            int ClientId = reader.GetInt32(0);

            reader.Close();


            SqlCommand command2 = new SqlCommand("EXEC AddNumberToClient @PhoneNumberId, @ClientId, @TariffId, 'TRUE'", Program.sqlConnection);
            command2.Parameters.Add("@PhoneNumberId", System.Data.SqlDbType.Int).Value = freeNumbersCollection[listBox2.SelectedIndex].PhoneNumberId;
            command2.Parameters.Add("@ClientId", System.Data.SqlDbType.Int).Value = ClientId;
            if(listBox3.SelectedIndex == -1)
            {
                command2.Parameters.Add("@TariffId", System.Data.SqlDbType.Int).Value = DBNull.Value;
            }
            else
            {
                command2.Parameters.Add("@TariffId", System.Data.SqlDbType.Int).Value = tariffCollection[listBox3.SelectedIndex].TariffId;
            }
            command2.ExecuteNonQuery();

            MessageBox.Show("Клиент успешно добавлен");

            this.Hide();
            Program.FoxForms.managerFunctions.Show();
        }
        protected override void OnVisibleChanged(EventArgs e)
        {
            freeNumbersCollection.Clear();
            listBox2.Items.Clear();
            


            SqlCommand command1 = new SqlCommand("EXEC getPhoneNumbers", Program.sqlConnection);
            SqlDataReader reader1 = command1.ExecuteReader();

            if (reader1.Read() == false)
            {
                return;
            }

            do
            {
                if (reader1.GetBoolean(2) == true)
                {
                    freeNumbersCollection.Add(new FreeNumbers(reader1.GetString(1), reader1.GetInt32(0)));
                    listBox2.Items.Add(reader1.GetString(1));
                }
            } while (reader1.Read());

            reader1.Close();


            tariffCollection.Clear();
            listBox3.Items.Clear();
            //EXEC getTarifList
            SqlCommand command2 = new SqlCommand("EXEC getTarifList", Program.sqlConnection);
            SqlDataReader reader2 = command2.ExecuteReader();

            if (reader2.Read() == false)
            {
                return;
            }

            do
            {
                if (reader2.GetInt32(7) != 3)
                {
                    tariffCollection.Add(new Tariff(reader2.GetString(0), reader2.GetInt32(1), reader2.GetInt32(2), reader2.GetInt32(3), reader2.GetInt32(4), reader2.GetInt32(5), reader2.GetBoolean(6), reader2.GetInt32(7)));
                    listBox3.Items.Add(reader2.GetString(0));
                }
            }
            while (reader2.Read());

            reader2.Close();
        }
        private void ManagerRegisterClient_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.FoxForms.managerFunctions.Show();
        }
    }
}
