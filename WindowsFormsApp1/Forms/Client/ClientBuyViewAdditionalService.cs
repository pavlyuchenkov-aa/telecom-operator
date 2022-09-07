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
    public partial class ClientBuyViewAdditionalService : Form
    {
        
        public ClientBuyViewAdditionalService()
        {
            InitializeComponent();
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
            public AdditionalServiceData(int id, string sername, string serdesc)
            {
                serviceId = id;
                serviceName = sername;
                serviceDescription = serdesc;
            }
            public int serviceId;
            public string serviceName;
            public string serviceDescription;
        }

        public List<AdditionalServiceData> AdditionalServiceDataCollection = new List<AdditionalServiceData>();
        public List<ClientPhoneNumbers> clientPhoneNumbersCollection = new List<ClientPhoneNumbers>();

        private void ClientBuyViewAdditionalService_Load(object sender, EventArgs e)
        {

        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            clientPhoneNumbersCollection.Clear();
            AdditionalServiceDataCollection.Clear();
            listBox2.Items.Clear();
            dataGridView1.Rows.Clear();

            SqlCommand command = new SqlCommand("EXEC getClientNumbersWithServiceStatus @ClientId", Program.sqlConnection);
            command.Parameters.Add("@ClientId", System.Data.SqlDbType.Int).Value = Program.currentClientId;
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


            dataGridView2.Rows.Clear();

            SqlCommand command1 = new SqlCommand("EXEC getAdditionalServiceList", Program.sqlConnection);
            SqlDataReader reader1 = command1.ExecuteReader();

            if (reader1.Read() == false)
            {
                return;
            }

            do
            {
                if (reader1.GetBoolean(3) == true)
                {
                    AdditionalServiceDataCollection.Add(new AdditionalServiceData(reader1.GetInt32(0), reader1.GetString(1), reader1.GetString(2)));

                    string[] data = { reader1.GetString(1), reader1.GetString(2) };
                    dataGridView2.Rows.Add(data);

                }
            } while (reader1.Read());

            reader1.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.FoxForms.ClientFunctions.Show();
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listBox2.SelectedItem == null)
            {
                return;
            }
            //AdditionalServiceDataCollection.Clear();
            dataGridView1.Rows.Clear();

            SqlCommand command1 = new SqlCommand("SELECT AdditionalService.AdditionalServiceId, AdditionalService.Title, AdditionalService.Description, ClientAdditionalServices.Date, AdditionalService.Avalability FROM ClientPhoneNumber JOIN PhoneNumber ON ClientPhoneNumber.PhoneNumberId=PhoneNumber.PhoneNumberId AND PhoneNumber.PhoneNumber=@FoxNumberText JOIN ClientAdditionalServices ON ClientPhoneNumber.ClientPhoneNumberId=ClientAdditionalServices.ClientPhoneNumberId JOIN AdditionalService ON ClientAdditionalServices.AdditionalServiceId=AdditionalService.AdditionalServiceId", Program.sqlConnection);
            command1.Parameters.Add("@FoxNumberText", System.Data.SqlDbType.VarChar).Value = listBox2.SelectedItem.ToString();
            SqlDataReader reader1 = command1.ExecuteReader();


            while (reader1.Read())
            {
                //additionalServiceCollection.Add(reader1.GetString(1), reader1.GetInt32(0));

                string[] data = { reader1.GetString(1), reader1.GetString(2), reader1.GetDateTime(3).ToString(), reader1.GetBoolean(4).ToString() };

                dataGridView1.Rows.Add(data);
                //AdditionalServiceDataCollection.Add(new AdditionalServiceData(reader1.GetInt32(0), reader1.GetString(1), reader1.GetString(2)));
                //string[] dataTarif = { reader.GetString(1), reader.GetString(3) };
            }

            reader1.Close();



        }

        private void button2_Click(object sender, EventArgs e)
        {

            if(listBox2.SelectedItem == null)
            {
                MessageBox.Show("Выберите нужный номер");
                return;
            }
            if(dataGridView2.SelectedRows == null)
            {
                MessageBox.Show("Выберите доп услугу");
                return;
            }


            SqlCommand command1 = new SqlCommand("EXEC AddAdditionalServices @ClientPhoneNumberId, @NewAdditionalServId, @FoxDate", Program.sqlConnection);
            command1.Parameters.Add("@ClientPhoneNumberId", System.Data.SqlDbType.Int).Value = clientPhoneNumbersCollection[listBox2.SelectedIndex].ClientPhoneNumberId;
            command1.Parameters.Add("@NewAdditionalServId", System.Data.SqlDbType.Int).Value = AdditionalServiceDataCollection[dataGridView2.SelectedRows[0].Index].serviceId;
            command1.Parameters.Add("@FoxDate", System.Data.SqlDbType.DateTime).Value = System.DateTime.Now;
            //System.DateTime.Now


            command1.ExecuteNonQuery();

            listBox2_SelectedIndexChanged(null, null);

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
