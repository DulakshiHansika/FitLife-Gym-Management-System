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

namespace FitLife_Gym_Management_System
{
    public partial class Member_Registration : Form
    {
        public Member_Registration()
        {
            InitializeComponent();
        }

        private void Member_Registration_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void registerfrmbtn_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        //Password Show/Hide Code
        private void memberpasswordcheck_CheckedChanged(object sender, EventArgs e)
        {
            if (passwordcheck.Checked)
            {
                txtpassword.UseSystemPasswordChar = false;
            }
            else
            {
                txtpassword.UseSystemPasswordChar = true;
            }
        }

      
        private void clearbtn_Click(object sender, EventArgs e)
        {

        }

        private void loginbtn_Click(object sender, EventArgs e)
        {

        }

        private void txtpassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtusername_TextChanged(object sender, EventArgs e)
        {

        }

        //System Exit Button Code
        private void exitbtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //New Member Registration Button Code
        private void signupbtn_Click(object sender, EventArgs e)
        {
            if (txtemail.Text == "" || txtusername.Text == "" || txtpassword.Text == "")
            {
                MessageBox.Show("Please fill all fields (Email, Username and Password)!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\dulak\OneDrive\Documents\GymDB.mdf;Integrated Security=True;Connect Timeout=30";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Check Duplicate Username or Email
                    string checkQuery = "SELECT COUNT(*) FROM Login WHERE username = @user OR email = @email";

                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, connection))
                    {
                        checkCmd.Parameters.AddWithValue("@user", txtusername.Text.Trim());
                        checkCmd.Parameters.AddWithValue("@email", txtemail.Text.Trim());

                        int exists = Convert.ToInt32(checkCmd.ExecuteScalar());

                        if (exists > 0)
                        {
                            MessageBox.Show("Username or Email already exists! Please use another one.", "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }

                    string insertQuery = "INSERT INTO Login (email, username, password) VALUES (@email, @user, @pass)";

                    using (SqlCommand insertCmd = new SqlCommand(insertQuery, connection))
                    {
                        insertCmd.Parameters.AddWithValue("@email", txtemail.Text.Trim());
                        insertCmd.Parameters.AddWithValue("@user", txtusername.Text.Trim());
                        insertCmd.Parameters.AddWithValue("@pass", txtpassword.Text.Trim());

                        insertCmd.ExecuteNonQuery();

                        MessageBox.Show("Member Registration Successful! You can now log in.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        txtemail.Clear();
                        txtusername.Clear();
                        txtpassword.Clear();
                        txtemail.Focus();

                        Member_Dashboad memberDash = new Member_Dashboad();
                        memberDash.Show();
                        this.Hide();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during registration: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Clear Button Code
        private void clearbtn_Click_1(object sender, EventArgs e)
        {
            txtemail.Clear();
            txtusername.Clear();
            txtpassword.Clear();
        }

        private void guna2CustomGradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        //Go to the Login Form
        private void loginfrm_Click(object sender, EventArgs e)
        {
            Member_Login memberLoginForm = new Member_Login();
            memberLoginForm.Show();
            this.Hide();
        }
    }
}
