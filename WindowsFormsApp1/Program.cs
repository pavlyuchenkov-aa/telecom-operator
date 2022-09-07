using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
using System.Net.Mail;


using System.Text.RegularExpressions;

namespace WindowsFormsApp1
{

    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        /// 

        public static int currentClientId = -1;
        public static int currentDirectorId = -1;
        public static int currentManagerIf = -1;
       

        //[STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            FoxForms = new FormsFox();

            sqlConnection.Open();

            if(sqlConnection.State != System.Data.ConnectionState.Open)
            {
                MessageBox.Show("Could not connect to database");
                return;
            }
   
            Application.Run(FoxForms.Form1);
        }
        public static SqlConnection sqlConnection = new SqlConnection("Server=localhost\\sqlexpress;Database=Coursework;Integrated Security=True;MultipleActiveResultSets=True;");

        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

        public static bool isDigit(char dig)
        {
            if (dig < '0' || dig > '9')
                return false;
            return true;
        }

        public static bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }
        public static bool IsEmailValid(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        //^(\+([0-9]{1,4})([0-9]){10})$
        public static bool IsPhoneValid(string phonenumber)
        {

            return Regex.Match(phonenumber, @"^(\+([0-9]{1,4})([0-9]){10}\s{0,})$").Success;


        }

        public static bool IsFIOValid(string FIO)
        {
            return Regex.Match(FIO, @"^([А-ЯA-Z]{1}[а-яa-z]{1,}\s[А-ЯA-Z]{1}[а-яa-z]{1,}(\s[А-ЯA-Z]{1}[а-яa-z]{1,}){0,1})$").Success;
        }

        public static FormsFox FoxForms;
    }
   
    class FormsFox
    {
        public Form1 Form1;
        public ClientFunctions ClientFunctions;
        public Forms.ClientViewTarifs ClientViewTarifs;
        public Forms.Client.ClientViewCurrentTarifs clientViewCurrentTarifs;
        public Forms.Client.ClientViewPersonalData clientViewPersonalData;
        public Forms.Client.ClientChangeTariff clientChangeTariff;
        public Forms.Client.ClientViewAvailableNumbers clientViewAvailableNumbers;
        public Forms.Client.ClientBuyViewAdditionalService clientBuyViewAdditionalService;

        public Forms.Director.DirectorFunctions directorFunctions;
        public Forms.Director.DirectorTariffManage directorTariffManage;
        public Forms.Director.DirectorTariffManageAddNew directorTariffManageAddNew;
        public Forms.Director.DirectorTariffManageChangeTariff directorTariffManageChangeTariff;
        public Forms.Director.DirectorManagerPersonalDataEditView directorManagerPersonalDataEditView;
        public Forms.Director.DirectorPersonalDataEditView directorPersonalDataEditView;
        public Forms.Director.DirectorRegisterManager directorRegisterManager;
        public Forms.Director.DirectorEditPhoneNumbers directorEditPhoneNumbers;
        public Forms.Director.DirectorManageAdditionalService directorManageAdditionalService;
        public Forms.Director.DirectorManageAdditionalServiceNewSerice directorManageAdditionalServiceNewSerice;


        public Forms.Manager.ManagerFunctions managerFunctions;
        public Forms.Manager.ManagerViewTariff managerViewTariff;
        public Forms.Manager.ManagerEditViewClientPersonal managerEditViewClientPersonal;
        public Forms.Manager.ManagerViewPersonal managerViewPersonal;
        public Forms.Manager.ManagerEditNumbers managerEditNumbers;
        public Forms.Manager.ManagerAddClientPhoneNumber managerAddClientPhoneNumber;
        public Forms.Manager.ManagerRegisterClient managerRegisterClient;
        public Forms.Manager.ManagerBlockUnblockAbonent managerBlockUnblockAbonent;

        public FormsFox()
        {
            //main
            Form1 = new Form1();

            //Client forms
            ClientFunctions = new ClientFunctions();
            ClientViewTarifs = new Forms.ClientViewTarifs();
            clientViewCurrentTarifs = new Forms.Client.ClientViewCurrentTarifs();
            clientViewPersonalData = new Forms.Client.ClientViewPersonalData();
            clientChangeTariff = new Forms.Client.ClientChangeTariff();
            clientViewAvailableNumbers = new Forms.Client.ClientViewAvailableNumbers();
            clientBuyViewAdditionalService = new Forms.Client.ClientBuyViewAdditionalService();
            



            Form1.StartPosition = FormStartPosition.Manual ;
            Form1.Left = 100;
            Form1.Top = 100;

            ClientFunctions.StartPosition = FormStartPosition.Manual;
            ClientFunctions.Left = 100;
            ClientFunctions.Top = 100;

            ClientViewTarifs.StartPosition = FormStartPosition.Manual;
            ClientViewTarifs.Left = 100;
            ClientViewTarifs.Top = 100;

            //director forms
            directorManagerPersonalDataEditView = new Forms.Director.DirectorManagerPersonalDataEditView();
            directorPersonalDataEditView = new Forms.Director.DirectorPersonalDataEditView();
            directorFunctions = new Forms.Director.DirectorFunctions();
            directorTariffManage = new Forms.Director.DirectorTariffManage();
            directorTariffManageAddNew = new Forms.Director.DirectorTariffManageAddNew();
            directorTariffManageChangeTariff = new Forms.Director.DirectorTariffManageChangeTariff();
            directorRegisterManager = new Forms.Director.DirectorRegisterManager();
            directorEditPhoneNumbers = new Forms.Director.DirectorEditPhoneNumbers();
            directorManageAdditionalService = new Forms.Director.DirectorManageAdditionalService();
            directorManageAdditionalServiceNewSerice = new Forms.Director.DirectorManageAdditionalServiceNewSerice();

            managerFunctions = new Forms.Manager.ManagerFunctions();
            managerViewTariff = new Forms.Manager.ManagerViewTariff();
            managerEditViewClientPersonal = new Forms.Manager.ManagerEditViewClientPersonal();
            managerViewPersonal = new Forms.Manager.ManagerViewPersonal();
            managerEditNumbers = new Forms.Manager.ManagerEditNumbers();
            managerAddClientPhoneNumber = new Forms.Manager.ManagerAddClientPhoneNumber();
            managerRegisterClient = new Forms.Manager.ManagerRegisterClient();
            managerBlockUnblockAbonent = new Forms.Manager.ManagerBlockUnblockAbonent();
            //ClientFunctions.StartPosition = FormStartPosition.CenterScreen;
            //ClientViewTarifs.StartPosition = FormStartPosition.CenterScreen;
        }
    }
}
