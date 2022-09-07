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

namespace WindowsFormsApp1.Forms.Client
{
    public partial class ClientChangeTariff : Form
    {
        public ClientChangeTariff()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.FoxForms.ClientFunctions.Show();
        }
        //
        private void ClientChangeTariff_Load(object sender, EventArgs e)
        {

        }

        Dictionary<string, int> ClientPhoneNumbersCollections = new Dictionary<string, int>();
        Dictionary<string, int> TariffCollections = new Dictionary<string, int>();
        protected override void OnVisibleChanged(EventArgs e)
        {
            listBox1PhoneNumber.Items.Clear();
            listBox2Tarif.Items.Clear();
            ClientPhoneNumbersCollections.Clear();
            TariffCollections.Clear();

            labelCurremtTariffInfo.Text = "";
            SqlCommand command = new SqlCommand("SELECT ClientPhoneNumber.ClientPhoneNumberId, PhoneNumber.PhoneNumber, Tariff.TariffName FROM ClientPhoneNumber JOIN Tariff ON ClientPhoneNumber.TariffId=Tariff.TariffId AND ClientPhoneNumber.ClientId=@ClientFoxId AND ClientPhoneNumber.ServiceStatus='TRUE' JOIN PhoneNumber ON ClientPhoneNumber.PhoneNumberId=PhoneNumber.PhoneNumberId", Program.sqlConnection);
            command.Parameters.Add("@ClientFoxId", System.Data.SqlDbType.Int).Value = Program.currentClientId;
            SqlDataReader reader = command.ExecuteReader();

            if(reader.Read() == false)
            {
                return;
            }

            do
            {
                ClientPhoneNumbersCollections.Add(reader.GetString(1), reader.GetInt32(0));

            } while (reader.Read());

            reader.Close();

            foreach (var a in ClientPhoneNumbersCollections)
            {
                listBox1PhoneNumber.Items.Add(a.Key);
            }

            /*SqlCommand command1 = new SqlCommand("SELECT Tariff.TariffName FROM ClientPhoneNumber JOIN Tariff ON ClientPhoneNumber.TariffId=Tariff.TariffId AND ClientPhoneNumber.ClientId=@FoxClientId JOIN PhoneNumber ON PhoneNumber.PhoneNumberId=ClientPhoneNumber.PhoneNumberId AND PhoneNumber.PhoneNumber=@FoxPhoneNumber", Program.sqlConnection);
            command1.Parameters.Add("@FoxClientId", System.Data.SqlDbType.Int).Value = Program.currentClientId;
            command1.Parameters.Add("@FoxPhoneNumber", System.Data.SqlDbType.VarChar).Value = ClientPhoneNumbersCollections.ElementAt(0).Key;

            SqlDataReader reader1 = command1.ExecuteReader();
            if(reader1.Read() == false)
            {
                MessageBox.Show("command1 failed");
                return;
            }

            labelCurremtTariffInfo.Text = "Тариф на выбранном номере телефона: " + reader1.GetString(0);

            reader1.Close();*/

            //SELECT Tariff.TariffName FROM ClientPhoneNumber JOIN Tariff ON ClientPhoneNumber.TariffId=Tariff.TariffId AND ClientPhoneNumber.ClientId=21 JOIN PhoneNumber ON PhoneNumber.PhoneNumberId=ClientPhoneNumber.PhoneNumberId AND PhoneNumber.PhoneNumber='86669996666'

            //SELECT Tariff.TariffId, Tariff.TariffName FROM Tariff WHERE Tariff.Availability = 'TRUE'


            SqlCommand command2 = new SqlCommand("SELECT Tariff.TariffId, Tariff.TariffName FROM Tariff WHERE Tariff.Availability = 'TRUE'", Program.sqlConnection);

            SqlDataReader reader2 = command2.ExecuteReader();
            if (reader2.Read() == false)
            {
                MessageBox.Show("No available tarifs");
                return;
            }

            do
            {

                TariffCollections.Add(reader2.GetString(1), reader2.GetInt32(0));
            }
            while (reader2.Read());

            reader2.Close();

            foreach (var a in TariffCollections)
            {
                listBox2Tarif.Items.Add(a.Key);
            }



            //labelCurremtTariffInfo.Text = "Тариф на выбранном номере телефона: " + reader2.GetString(0);
        }

        private void listBox2Tarif_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void listBox1PhoneNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listBox1PhoneNumber.SelectedItem == null)
            {
                return;
            }

            SqlCommand command1 = new SqlCommand("SELECT Tariff.TariffName FROM ClientPhoneNumber JOIN Tariff ON ClientPhoneNumber.TariffId=Tariff.TariffId AND ClientPhoneNumber.ClientId=@FoxClientId JOIN PhoneNumber ON PhoneNumber.PhoneNumberId=ClientPhoneNumber.PhoneNumberId AND PhoneNumber.PhoneNumber=@FoxPhoneNumber", Program.sqlConnection);
            command1.Parameters.Add("@FoxClientId", System.Data.SqlDbType.Int).Value = Program.currentClientId;
            command1.Parameters.Add("@FoxPhoneNumber", System.Data.SqlDbType.VarChar).Value = listBox1PhoneNumber.SelectedItem.ToString();

            SqlDataReader reader1 = command1.ExecuteReader();
            if (reader1.Read() == false)
            {
                MessageBox.Show("NULL tarif on current phone number");
                return;
            }

            labelCurremtTariffInfo.Text = "Тариф на выбранном номере телефона: " + reader1.GetString(0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(listBox1PhoneNumber.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, веберите номер");
                return;
            }

            if (listBox2Tarif.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, веберите тариф");
                return;
            }

            //MessageBox.Show(Program.currentClientId + " " + ClientPhoneNumbersCollections[listBox1PhoneNumber.SelectedItem.ToString()] + " " + TariffCollections[listBox2Tarif.SelectedItem.ToString()]);

            SqlCommand commandF = new SqlCommand("SELECT PhoneNumber.PhoneNumberId FROM PhoneNumber WHERE PhoneNumber.PhoneNumber=@FoxPhoneNumber", Program.sqlConnection);
            commandF.Parameters.Add("@FoxPhoneNumber", System.Data.SqlDbType.VarChar).Value = listBox1PhoneNumber.SelectedItem.ToString();

            SqlDataReader readerF = commandF.ExecuteReader();
            if (readerF.Read() == false)
            {
                MessageBox.Show("commandF failed");
                return;
            }

            int phoneNumberId = readerF.GetInt32(0);

            readerF.Close();


            SqlCommand command = new SqlCommand("UPDATE ClientPhoneNumber SET TariffId=@FoxTarifId WHERE ClientPhoneNumber.ClientId=@FoxClientId AND ClientPhoneNumber.PhoneNumberId=@FoxPhoneNumberId", Program.sqlConnection);
            command.Parameters.Add("@FoxClientId", System.Data.SqlDbType.Int).Value = Program.currentClientId;
            command.Parameters.Add("@FoxPhoneNumberId", System.Data.SqlDbType.Int).Value = phoneNumberId;//ClientPhoneNumbersCollections[listBox1PhoneNumber.SelectedItem.ToString()];
            command.Parameters.Add("@FoxTarifId", System.Data.SqlDbType.Int).Value = TariffCollections[listBox2Tarif.SelectedItem.ToString()];

            command.ExecuteNonQuery();

            labelCurremtTariffInfo.Text = "Тариф на выбранном номере телефона: " + listBox2Tarif.SelectedItem.ToString();

        }
    }
}
