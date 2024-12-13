using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _07_Laboratory
{
    public partial class FrmUpdateMember : Form
    {
        public FrmUpdateMember()
        {
            InitializeComponent();
        }

        // Update button - Confirm
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string studentId = comboBox1.Text.Trim(); // choose id to update
                if(string.IsNullOrEmpty(studentId) )
                {
                    MessageBox.Show("Please select a Student ID to update.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    string lastName = textBox2.Text.Trim();
                    string firstName = textBox1.Text.Trim();
                    string middleName = textBox3.Text.Trim();
                    int age = Convert.ToInt32(textBox4.Text.Trim());
                    string gender = comboBox2.Text.Trim();
                    string program = comboBox3.Text.Trim();

                    // update database
                    using (MySqlConnection conn = new MySqlConnection("server=127.0.0.1; user=root; database=clubdb; password="))
                    {
                        conn.Open();

                        string query = @"UPDATE clubmembers
                                        SET 
                                            
                                            FirstName = @FirstName,
                                            MiddleName = @MiddleName,
                                            LastName = @LastName,
                                            Age = @Age,
                                            Gender = @Gender,
                                            Program = @Program
                                            WHERE StudentId = @StudentId";
                                        

                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@StudentId", studentId);
                            cmd.Parameters.AddWithValue("@FirstName", firstName);                            
                            cmd.Parameters.AddWithValue("@MiddleName", middleName);
                            cmd.Parameters.AddWithValue("@LastName", lastName);
                            cmd.Parameters.AddWithValue("@Age", age);
                            cmd.Parameters.AddWithValue("@Gender", gender);
                            cmd.Parameters.AddWithValue("@Program", program);

                            // execute query
                            int rowsAffected = cmd.ExecuteNonQuery();



                        }
                    }
                        MessageBox.Show("Student Updated Successfully!", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // student ID list
        private void studentList()
        {
            using (MySqlConnection conn = new MySqlConnection("server=127.0.0.1; user=root; database=clubdb; password="))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT StudentId FROM clubmembers";
                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        comboBox1.Items.Clear();
                        while (reader.Read())
                        {
                            comboBox1.Items.Add(reader["StudentId"]).ToString();
                        }
                    }
                }catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    
                }
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedStudentId = comboBox1.Text.Trim();

            if (!string.IsNullOrEmpty(selectedStudentId))
            {
                using (MySqlConnection conn = new MySqlConnection("server=127.0.0.1; user=root; database=clubdb; password="))
                {
                    try
                    {
                        conn.Open();
                        string query = "SELECT * FROM clubmembers WHERE StudentId = @StudentId";
                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        cmd.Parameters.Add("@StudentId", MySqlDbType.VarChar).Value = selectedStudentId;

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                textBox1.Text = reader["FirstName"].ToString();
                                textBox2.Text = reader["LastName"].ToString();
                                textBox3.Text = reader["MiddleName"].ToString();
                                textBox4.Text = reader["Age"].ToString();
                                comboBox2.Text = reader["Gender"].ToString();
                                comboBox3.Text = reader["Program"].ToString();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void FrmUpdateMember_Load(object sender, EventArgs e)
        {
            studentList(); // display student ID list
        }
    }
}
