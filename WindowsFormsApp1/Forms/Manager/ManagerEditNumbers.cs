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
    public partial class ManagerEditNumbers : Form
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
        

        public struct ClientPhoneNumbers
        {
            public ClientPhoneNumbers(string phoneNumber, int clientPhoneNumberId)
            {
                PhoneNumber = phoneNumber;
                ClientPhoneNumberId = clientPhoneNumberId;
            }
            public string PhoneNumber;
            public int ClientPhoneNumberId;
        }

        public struct AdditionalServiceData
        {
            public AdditionalServiceData(int id, string sername, string serdesc, bool serstat)
            {
                serviceId = id;
                serviceName = sername;
                serviceDescription = serdesc;
                serviceStatus = serstat;
            }
            public int serviceId;
            public string serviceName;
            public string serviceDescription;
            public bool serviceStatus;
        }

        public List<AdditionalServiceData> AdditionalServiceDataCollection = new List<AdditionalServiceData>();

        public List<ClientData> clientsCollection = new List<ClientData>();
        public List<ClientPhoneNumbers> clientPhoneNumbersCollection = new List<ClientPhoneNumbers>();

        public ManagerEditNumbers()
        {
            InitializeComponent();
        }

        private void ManagerEditNumbers_Load(object sender, EventArgs e)
        {

        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();

            clientsCollection.Clear();
            SqlCommand command = new SqlCommand("EXEC getClientList", Program.sqlConnection);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read() == false)
            {
                return;
            }

            do
            {
                clientsCollection.Add(new ClientData(reader.GetString(0), reader.GetInt32(1)));
                listBox1.Items.Add(reader.GetString(0));
            } while (reader.Read());

            reader.Close();











            dataGridView2.Rows.Clear();
            //dataGridView1.Refresh();
            AdditionalServiceDataCollection.Clear();

            SqlCommand command1 = new SqlCommand("EXEC getAdditionalServiceList", Program.sqlConnection);
            //command.Parameters.Add("@idToChange", System.Data.SqlDbType.Int).Value = phoneNumbersCollection[listBox1.SelectedIndex].PhoneNumberId;
            //command.Parameters.Add("@NewName", System.Data.SqlDbType.Char).Value = textBox1.Text;

            SqlDataReader reader1 = command1.ExecuteReader();

            if (reader1.Read() == false)
            {

                reader1.Close();
                return;
            }

            do
            {
                string[] data = { reader1.GetString(1), reader1.GetString(2),reader1.GetBoolean(3).ToString() };
                dataGridView2.Rows.Add(data);

                AdditionalServiceDataCollection.Add(new AdditionalServiceData(reader1.GetInt32(0), reader1.GetString(1), reader1.GetString(2), reader1.GetBoolean(3)));
            }
            while (reader1.Read());

            reader1.Close();


        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.FoxForms.managerFunctions.Show();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            clientPhoneNumbersCollection.Clear();

            SqlCommand command = new SqlCommand("EXEC getClientNumbersWithServiceStatus @ClientId", Program.sqlConnection);
            command.Parameters.Add("@ClientId", System.Data.SqlDbType.Int).Value = clientsCollection[listBox1.SelectedIndex].ClientId;
            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read() == false)
            {
                return;
            }

            do
            {
                if (reader.GetBoolean(2) == true)
                {
                    clientPhoneNumbersCollection.Add(new ClientPhoneNumbers(reader.GetString(0), reader.GetInt32(1)));
                    listBox2.Items.Add(reader.GetString(0));
                }
            } while (reader.Read());

            reader.Close();
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите клиента");
                return;
            }
            if (listBox2.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите номер у клиента");
                return;
            }
            if (dataGridView2.SelectedRows.Count != 1)
            {
                MessageBox.Show("Выберите услугу");
                return;
            }

            if(AdditionalServiceDataCollection[dataGridView2.SelectedRows[0].Index].serviceStatus == false)
            {
                MessageBox.Show("Данная услуга не поддерживается. Выберите другую");
                return;
            }


            SqlCommand command1 = new SqlCommand("EXEC AddAdditionalServices @ClientPhoneNumberId, @NewAdditionalServId, @FoxDate", Program.sqlConnection);
            command1.Parameters.Add("@ClientPhoneNumberId", System.Data.SqlDbType.Int).Value = clientPhoneNumbersCollection[listBox2.SelectedIndex].ClientPhoneNumberId;
            command1.Parameters.Add("@NewAdditionalServId", System.Data.SqlDbType.Int).Value = AdditionalServiceDataCollection[dataGridView2.SelectedRows[0].Index].serviceId;
            command1.Parameters.Add("@FoxDate", System.Data.SqlDbType.DateTime).Value = System.DateTime.Now;
            //System.DateTime.Now


            command1.ExecuteNonQuery();

            MessageBox.Show("Услуга успешно добавлена");
        }
    }
}
