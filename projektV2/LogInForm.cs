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
    public partial class LogInForm : Form
    {
        public LogInForm()
        {
            InitializeComponent();
        }

        private void LogInForm_Load(object sender, EventArgs e)
        {

        }

        private void close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// creating question into db with values after validating
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void submit_Click(object sender, EventArgs e)
        {
            //creating values from text boxes
            string email = emailBox.Text;
            String password = passBox.Text;

                //creating regex for an email if passed creating if statement to see that password have 8-15 digits if yes its correct
                Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                Match match = regex.Match(email);
                if (match.Success)
                {
                if (password.Length >= 8 && password.Length <= 15)
                    {
                    //correct password so creaining connection with DB and searching email and pass are in db
                        DB db = new DB();
                        DataTable table = new DataTable();
                        MySqlDataAdapter adapter = new MySqlDataAdapter();
                        MySqlCommand command = new MySqlCommand("SELECT * FROM `usersinfo` WHERE `UserEMAIL` = @use and `UserPASS` = @pass", db.getConnection());

                        //adding paremeters for command into DB
                        command.Parameters.Add("@use", MySqlDbType.VarChar).Value = email;
                        command.Parameters.Add("@pass", MySqlDbType.VarChar).Value = password;

                        adapter.SelectCommand = command;

                        adapter.Fill(table);

                        // check if the user exists or not
                        if (table.Rows.Count > 0)
                        {
                            this.Hide();
                            LoggedIn LoggedIn = new LoggedIn();
                            LoggedIn.Show();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Niepoprawny email");
                }
        }

        private void registerButt_Click(object sender, EventArgs e)
        {
            this.Hide();
            RegisterForm registerform = new RegisterForm();
            registerform.Show();
        }
    }
}
