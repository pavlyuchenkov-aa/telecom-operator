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
    public partial class ManagerBlockUnblockAbonent : Form
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

        public List<ClientData> clientsCollection = new List<ClientData>();
        public List<ClientPhoneNumbers> clientPhoneNumbersCollection = new List<ClientPhoneNumbers>();
        public ManagerBlockUnblockAbonent()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.FoxForms.managerFunctions.Show();
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            dataGridView1.Rows.Clear();

            listBox1.Items.Clear();

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

        }

        private void ManagerBlockUnblockAbonent_Load(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listBox1.SelectedIndex == -1)
            {
                return;
            }

            clientPhoneNumbersCollection.Clear();
            dataGridView1.Rows.Clear();

            SqlCommand command = new SqlCommand("EXEC getClientNumbersWithServiceStatus @ClientId", Program.sqlConnection);
            command.Parameters.Add("@ClientId", SqlDbType.Int).Value = clientsCollection[listBox1.SelectedIndex].ClientId;
            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read() == false)
            {
                return;
            }

            do
            {
                clientPhoneNumbersCollection.Add(new ClientPhoneNumbers(reader.GetString(0), reader.GetInt32(1)));

                string[] data = { reader.GetString(0), reader.GetBoolean(2).ToString() };
                dataGridView1.Rows.Add(data);

            } while (reader.Read());

            reader.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //setClientPhoneNumberServiceStatus

            if(listBox1.SelectedIndex == -1 || dataGridView1.SelectedRows == null)
            {
                MessageBox.Show("Выберите номер для изменения статуса");
                return;
            }
            SqlCommand command = new SqlCommand("EXEC setClientPhoneNumberServiceStatus @ClientPhoneNumberId, @ServiceStatus", Program.sqlConnection);
            command.Parameters.Add("@ClientPhoneNumberId", SqlDbType.Int).Value = clientPhoneNumbersCollection[dataGridView1.SelectedRows[0].Index].ClientPhoneNumberId;
            command.Parameters.Add("@ServiceStatus", SqlDbType.Bit).Value = false;

            command.ExecuteNonQuery();

            listBox1_SelectedIndexChanged(null, null);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //setClientPhoneNumberServiceStatus

            if (listBox1.SelectedIndex == -1 || dataGridView1.SelectedRows == null)
            {
                MessageBox.Show("Выберите номер для изменения статуса");
                return;
            }
            SqlCommand command = new SqlCommand("EXEC setClientPhoneNumberServiceStatus @ClientPhoneNumberId, @ServiceStatus", Program.sqlConnection);
            command.Parameters.Add("@ClientPhoneNumberId", SqlDbType.Int).Value = clientPhoneNumbersCollection[dataGridView1.SelectedRows[0].Index].ClientPhoneNumberId;
            command.Parameters.Add("@ServiceStatus", SqlDbType.Bit).Value = true;

            command.ExecuteNonQuery();

            listBox1_SelectedIndexChanged(null, null);
        }
    }
}
