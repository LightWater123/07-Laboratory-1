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
using MySql.Data.MySqlClient;


namespace _07_Laboratory
{
    public partial class FrmClubRegistration : Form
    {
        private string sqlCon = "server=127.0.0.1; user=root; database=clubdb; password=";
        public FrmClubRegistration()
        {
            InitializeComponent();
        }
        
        // UPDATE
        private void button2_Click(object sender, EventArgs e)
        {
            FrmUpdateMember frmUpdateMember = new FrmUpdateMember();
           
            frmUpdateMember.Show();
        }

        // REGISTER
private void button1_Click(object sender, EventArgs e)
{
    try
    {
        // Get the values from the form
        string studentId = txtStudentID.Text.Trim(); // Use string for StudentID
        string firstName = txtFirstName.Text.Trim();
        string middleName = txtMiddleName.Text.Trim();
        string lastName = txtLastName.Text.Trim();
        int age = Convert.ToInt32(txtAge.Text.Trim());
        string gender = cb_Gender.Text.Trim();
        string program = cb_Program.Text.Trim();

        // Call the RegisterStudent method
        bool isRegistered = RegisterStudent(studentId, firstName, middleName, lastName, age, gender, program);

        if (isRegistered)
        {
            MessageBox.Show("Registration successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            clearFields(); 
            displayMembers(); 
        }
        else
        {
            MessageBox.Show("Failed to register. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}


        // register method
        public bool RegisterStudent(string StudentID, string FirstName, string MiddleName, string LastName, int Age, string Gender, string Program)
        {
            string connectionString = @"server=127.0.0.1; user=root; database=clubdb; password=";

            try
            {
                // create query
                string query = "INSERT INTO clubmembers (StudentId, FirstName, MiddleName, LastName, Age, Gender, Program) " +
                               "VALUES (@StudentId, @FirstName, @MiddleName, @LastName, @Age, @Gender, @Program)";

                // open connection
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        // add values to the parameters
                        cmd.Parameters.Add("@StudentId", MySqlDbType.Int64).Value = StudentID;
                        cmd.Parameters.Add("@FirstName", MySqlDbType.VarChar).Value = FirstName;
                        cmd.Parameters.Add("@MiddleName", MySqlDbType.VarChar).Value = MiddleName;
                        cmd.Parameters.Add("@LastName", MySqlDbType.VarChar).Value = LastName;
                        cmd.Parameters.Add("@Age", MySqlDbType.Int32).Value = Age;
                        cmd.Parameters.Add("@Gender", MySqlDbType.VarChar).Value = Gender;
                        cmd.Parameters.Add("@Program", MySqlDbType.VarChar).Value = Program;

                        // execute query
                        int rowsAffected = cmd.ExecuteNonQuery();

                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


        // reset fields
        private void clearFields()
        {
            txtStudentID.Text = string.Empty;
            txtFirstName.Text = string.Empty;
            txtMiddleName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtAge.Text = string.Empty;
            cb_Gender.SelectedIndex = -1;
            cb_Program.SelectedIndex = -1;
        }
        // display club members
        private void displayMembers()
        {
            using (MySqlConnection conn = new MySqlConnection(sqlCon))
            {
                try
                {
                    conn.Open();
                    string selectQuery = " SELECT * FROM clubmembers";
                    MySqlCommand cmd = new MySqlCommand(selectQuery, conn);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // gets the data from the database to the datagridview
                    dataGridView1.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        // REFRESH
        private void button3_Click(object sender, EventArgs e)
        {
            displayMembers();
        }

        
        private void txtStudentID_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtLastName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtAge_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtFirstName_TextChanged(object sender, EventArgs e)
        {

        }

        private void cb_Gender_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cb_Program_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtMiddleName_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void FrmClubRegistration_Load(object sender, EventArgs e)
        {

            displayMembers();
            
        }

        
    }
}
