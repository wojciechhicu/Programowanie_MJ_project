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

namespace projektV2
{
    public partial class LoggedIn : Form
    {
        public LoggedIn()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Hide();
            LogInForm LogInForm = new LogInForm();
            LogInForm.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        /// <summary>
        /// adding and showing tasks for other users
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addButton_Click(object sender, EventArgs e)
        {
            string conString = "Server=localhost;Database=users;User Id=root;Password=''";
            MySqlConnection conDataBase = new MySqlConnection(conString);
            MySqlCommand cmdDataBase = new MySqlCommand(" SELECT * from usertasks",conDataBase);


            try
            {
                // add task
                DB db = new DB();
                MySqlCommand command = new MySqlCommand("INSERT INTO `usertasks`(`UserName`, `UserTask`) VALUES (@UserName, @UserTask)", db.getConnection());
                command.Parameters.Add("@UserName", MySqlDbType.VarChar).Value = userNames.Text;
                command.Parameters.Add("@UserTask", MySqlDbType.VarChar).Value = taskField.Text;

                // open the connection
                db.openConnection();
                if (command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Zadanie dodane", "Zadanie dodane", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show("Błąd w dodawaniu zadania");
                }

                // close the connection
                db.closeConnection();

                //show list of tasks
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cmdDataBase;
                DataTable dbdataset = new DataTable();
                sda.Fill(dbdataset);
                BindingSource bSource = new BindingSource();

                bSource.DataSource = dbdataset;
                dataGridView1.DataSource = bSource;
                sda.Update(dbdataset);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
