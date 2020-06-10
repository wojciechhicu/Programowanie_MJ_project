using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;


namespace projektV2
{

    public partial class RegisterForm : Form
    {
        /// <summary>
        /// funcions needed for validating forms
        /// matching if length of two passwords are between 8 and 15 digits and if yes then matching if they are the same
        /// </summary>
        /// <param name="pass1"></param>
        /// <param name="pass2"></param>
        /// <returns></returns>
        Boolean matchPass(string pass1, string pass2)
        {
            if (pass1.Length >= 8 && pass2.Length <= 15)
            {
                if (pass1.Equals(pass2))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Hasło o niepoprawnej długości. Poprawna długość to od 8 do 15 znaków");
                return false;
            }
        }
        /// <summary>
        /// trying to match email to regular expression (any digit + @ + any digit + . 2-3 digits)
        /// </summary>
        /// <param name="mail"></param>
        /// <returns></returns>
        Boolean matchMail(string mail)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(mail);
            if (match.Success)
                return true;
            else
                return false;
        }

        /// <summary>
        /// regular expression of PIN field 4 numbers only
        /// </summary>
        /// <param name="pin"></param>
        /// <returns></returns>
        public Boolean matchPIN(string pin)
        {
            Regex regex = new Regex(@"^\d{4,4}$");
            Match match = regex.Match(pin);
            if (match.Success)
                return true;
            else
                return false;
        }

        /// <summary>
        /// regular expression of age field 4 numbers only
        /// </summary>
        /// <param name="age"></param>
        /// <returns></returns>
        public Boolean matchAge(string age)
        {
            Regex regex = new Regex(@"^\d{4,4}$");
            Match match = regex.Match(age);
            if (match.Success)
                return true;
            else
                return false;
        }
        //end of functions


        public RegisterForm()
        {
            InitializeComponent();
        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {

        }

        private void close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void haveAcc_Click(object sender, EventArgs e)
        {
            this.Hide();
            LogInForm LogInForm = new LogInForm();
            LogInForm.Show();
        }

        /// <summary>
        /// sending correct values to db as user information after validating
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void registerButton_Click(object sender, EventArgs e)
        {

            
            if(matchAge(ageBox.Text) && matchPIN(pinBox.Text) && matchMail(emailBox.Text) && matchPass(passBox.Text, pass2Box.Text))
            {
                DB db = new DB();
                MySqlCommand command = new MySqlCommand("INSERT INTO `usersinfo`(`UserEMAIL`, `UserPASS`, `UserPIN`, `UserAge`) VALUES (@mail, @pass, @pin, @age)", db.getConnection());

                command.Parameters.Add("@mail", MySqlDbType.VarChar).Value = emailBox.Text;
                command.Parameters.Add("@pass", MySqlDbType.VarChar).Value = passBox.Text;
                command.Parameters.Add("@pin", MySqlDbType.VarChar).Value = pinBox.Text;
                command.Parameters.Add("@age", MySqlDbType.VarChar).Value = ageBox.Text;

                // open the connection
                db.openConnection();
                if (command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Konto zostało założone", "Konto założone", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Błąd z bazą danych");
                }

                // close the connection
                db.closeConnection();
            }
            else
            {
                MessageBox.Show("Sprawdź poprawność danych ponownie");
            }


        }
    }
}
