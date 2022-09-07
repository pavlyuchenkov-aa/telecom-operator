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
    public partial class ManagerAddClientPhoneNumber : Form
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

        public List<ClientData> clientsCollection = new List<ClientData>();
        public List<FreeNumbers> freeNumbersCollection = new List<FreeNumbers>();

        public ManagerAddClientPhoneNumber()
        {
            InitializeComponent();
        }

        private void ManagerAddClientPhoneNumber_Load(object sender, EventArgs e)
        {

        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();

            clientsCollection.Clear();
            freeNumbersCollection.Clear();
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
            }
            while (reader1.Read());
            

            reader1.Close();





            //EXEC getTarifList
            SqlCommand command2 = new SqlCommand("EXEC getTarifList", Program.sqlConnection);
            SqlDataReader reader2 = command2.ExecuteReader();

            if (reader2.Read() == false)
            {
                return;
            }

            do
            {
                 listBox3.Items.Add(reader2.GetString(0));
            } while (reader2.Read());

            reader2.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.FoxForms.managerFunctions.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //AddNumberToClient

            if(listBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите клиента");
                return;
            }
            if (listBox2.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите свободный номер");
                return;
            }

            SqlCommand command2 = new SqlCommand("EXEC AddNumberToClient @PhoneNumberId, @ClientId, @TariffId, 'TRUE'", Program.sqlConnection);
            command2.Parameters.Add("@PhoneNumberId", System.Data.SqlDbType.Int).Value = freeNumbersCollection[listBox2.SelectedIndex].PhoneNumberId;
            command2.Parameters.Add("@ClientId", System.Data.SqlDbType.Int).Value = clientsCollection[listBox1.SelectedIndex].ClientId;

            int tarifid = -1;

            if (listBox3.SelectedIndex != -1)
            {
                SqlCommand commandGET = new SqlCommand("EXEC getTarifId @TarifName", Program.sqlConnection);
                commandGET.Parameters.Add("@TarifName", System.Data.SqlDbType.NVarChar).Value = listBox3.Text;
                SqlDataReader readerGET = commandGET.ExecuteReader();
                if (readerGET.Read() == false)
                {
                    MessageBox.Show("readerGET.Read() returned false");
                    return;
                }

                tarifid = readerGET.GetInt32(0);
                readerGET.Close();
            }
            else
            {
                
                tarifid = -1;
            }

            

            if(tarifid == -1)
            {
                command2.Parameters.Add("@TariffId", System.Data.SqlDbType.Int).Value = DBNull.Value;
            }
            else
            {
                command2.Parameters.Add("@TariffId", System.Data.SqlDbType.Int).Value = tarifid;
            }


            SqlDataReader readres = command2.ExecuteReader();
            if(readres.Read() == false)
            {
                MessageBox.Show("Не удалось добавить номер - такой номер уже есть у другого клиента");
                return;
            }
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

            

            MessageBox.Show("Номер успешно добавлен клиенту");
        }
    }
}
