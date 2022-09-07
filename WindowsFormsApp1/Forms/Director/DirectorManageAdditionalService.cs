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
    //EXEC newAdditionalService 'МЕГА СУПЕР ДОП УСЛАГА', '+ -900 минут разговора'

    public partial class DirectorManageAdditionalService : Form
    {
        public int selectedServiceId = -1;
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
        public DirectorManageAdditionalService()
        {
            InitializeComponent();
        }

        private void DirectorManageAdditionalService_Load(object sender, EventArgs e)
        {

        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            dataGridView1.Rows.Clear();
            //dataGridView1.Refresh();
            AdditionalServiceDataCollection.Clear();

            SqlCommand command = new SqlCommand("EXEC getAdditionalServiceList", Program.sqlConnection);
            //command.Parameters.Add("@idToChange", System.Data.SqlDbType.Int).Value = phoneNumbersCollection[listBox1.SelectedIndex].PhoneNumberId;
            //command.Parameters.Add("@NewName", System.Data.SqlDbType.Char).Value = textBox1.Text;

            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read() == false)
            {
                
                reader.Close();
                return;
            }

            do
            {
                string[] data = { reader.GetString(1), reader.GetString(2), reader.GetBoolean(3).ToString() };
                dataGridView1.Rows.Add(data);

                AdditionalServiceDataCollection.Add(new AdditionalServiceData(reader.GetInt32(0), reader.GetString(1), reader.GetString(2)));
            }
            while (reader.Read());

            reader.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.FoxForms.directorManageAdditionalServiceNewSerice.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DirectorManageAdditionalService_SelectionChanged(object sender, EventArgs e)
        {
            if(AdditionalServiceDataCollection.Count != 0 && dataGridView1.SelectedRows.Count != 0)
            {
                textBox1.Text = AdditionalServiceDataCollection[dataGridView1.SelectedRows[0].Index].serviceName;
                textBox2.Text = AdditionalServiceDataCollection[dataGridView1.SelectedRows[0].Index].serviceDescription;
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("EXEC changeAdditionalService @ServiceId, @ServiceName, @ServiceDesc", Program.sqlConnection);
            command.Parameters.Add("@ServiceId", System.Data.SqlDbType.Int).Value = AdditionalServiceDataCollection[dataGridView1.SelectedRows[0].Index].serviceId;
            command.Parameters.Add("@ServiceName", System.Data.SqlDbType.NVarChar).Value = textBox1.Text;
            command.Parameters.Add("@ServiceDesc", System.Data.SqlDbType.NVarChar).Value = textBox2.Text;

            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read() == false)
            {
                MessageBox.Show("Такая услуга/такое описание уже есть");
                reader.Close();
                return;
            }

            reader.Close();

            OnVisibleChanged(null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.FoxForms.directorFunctions.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //changeAdditionalServiceAvalability

            SqlCommand command = new SqlCommand("EXEC changeAdditionalServiceAvalability @ServiceId, @aval", Program.sqlConnection);
            command.Parameters.Add("@ServiceId", System.Data.SqlDbType.Int).Value = AdditionalServiceDataCollection[dataGridView1.SelectedRows[0].Index].serviceId;
            command.Parameters.Add("@aval", System.Data.SqlDbType.Bit).Value = false;

            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read() == false)
            {
                MessageBox.Show("не удалось деактивировать услугу");
                reader.Close();
                return;
            }

            reader.Close();

            OnVisibleChanged(null);
        }
    }
}
