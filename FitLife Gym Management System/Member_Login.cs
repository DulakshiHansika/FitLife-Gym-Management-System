using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FitLife_Gym_Management_System
{
    public partial class Member_Login : Form
    {
        public Member_Login()
        {
            InitializeComponent();
        }


        private void Member_Login_Load(object sender, EventArgs e)
        {

        }

        //System Exit Button Code
        private void exitbtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Go to the Register Form
        private void registerfrmbtn_Click(object sender, EventArgs e)
        {
            Member_Registration memberRegisterForm = new Member_Registration();
            memberRegisterForm.Show();
            this.Hide();
        }

        private void memberpasswordcheck_CheckedChanged(object sender, EventArgs e)
        {
            if (memberpasswordcheck.Checked)
            {
                txtpassword.UseSystemPasswordChar = false;
            }
            else
            {
                txtpassword.UseSystemPasswordChar = true;
            }
        }

        //Back Button Code
        private void backbtn_Click(object sender, EventArgs e)
        {
            frmmainmenu MainMenu = new frmmainmenu();
            MainMenu.Show();
            this.Close();
        }

        private void txtusername_TextChanged(object sender, EventArgs e)
        {

        }

        //User Login Button Code
        private void loginbtn_Click(object sender, EventArgs e)
        {
            if (txtusername.Text == "" || txtpassword.Text == "")
            {
                MessageBox.Show("Please enter both Username and Password!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\dulak\OneDrive\Documents\GymDB.mdf;Integrated Security=True;Connect Timeout=30";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT COUNT(*) FROM Login WHERE username = @user AND password = @pass";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@user", txtusername.Text.Trim());
                        command.Parameters.AddWithValue("@pass", txtpassword.Text.Trim());

                        int count = Convert.ToInt32(command.ExecuteScalar());

                        if (count > 0)
                        {
                            MessageBox.Show("Member Login Successful! Welcome to FitLife Gym.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            Member_Dashboard memDash = new Member_Dashboard();
                            memDash.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Invalid Member Username or Password!", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            txtusername.Clear();
                            txtpassword.Clear();
                            txtpassword.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
