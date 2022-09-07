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
    public partial class ClientViewPersonalData : Form
    {
        public ClientViewPersonalData()
        {
            InitializeComponent();
        }

        private void ClientViewPersonalData_Load(object sender, EventArgs e)
        {

        }

        protected override void OnVisibleChanged(EventArgs e)
        {


            //MessageBox.Show("id: " + Program.currentClientId);

            SqlCommand command = new SqlCommand("SELECT Login, ClientName, Address, Email, PassportData FROM Client JOIN LoginDetails ON Client.IdLoginDetails=LoginDetails.LoginDetailsId AND Client.ClientId=@ClientId", Program.sqlConnection);
            command.Parameters.Add("@ClientId", System.Data.SqlDbType.Int).Value = Program.currentClientId;
            SqlDataReader reader = command.ExecuteReader();


            if(reader.Read() == false)
            {
                MessageBox.Show("Client doesnt has personal data");
            }

            labelLogin.Text = reader.GetString(0);
            labelFio.Text = reader.GetString(1);
            labelAddress.Text = reader.GetString(2);
            labelEmail.Text = reader.GetString(3);
            labelPassport.Text = reader.GetString(4);


            reader.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.FoxForms.ClientFunctions.Show();
        }
    }
}
