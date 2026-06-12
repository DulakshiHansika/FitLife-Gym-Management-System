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
    public partial class Manage_Bookings : Form
    {
        // Database Connection 
        private String connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\dulak\OneDrive\Documents\GymDB.mdf;Integrated Security=True;Connect Timeout=30";

        public Manage_Bookings()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void guna2CustomGradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        // Event handler for when a cell in the DataGridView is clicked
        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = DataGridView1.Rows[e.RowIndex];

                txtBookingID.Text = row.Cells["BookingID"].Value.ToString();
                txtFullName.Text = row.Cells["FullName"].Value.ToString();
                txtcontact.Text = row.Cells["ContactNo"].Value.ToString();
                cmbMembership.SelectedItem = row.Cells["MembershipType"].Value.ToString();
                cmbProgram.SelectedItem = row.Cells["Program"].Value.ToString();
                dtpStartDate.Value = Convert.ToDateTime(row.Cells["StartDate"].Value);
          
                txtSessions.Text = row.Cells["Sessions"].Value.ToString();

                txtTotalCost.Text = row.Cells["TotalCost"].Value.ToString();

                txtBookingID.ReadOnly = true;
            }
        }

        // Exit button to close the form
        private void exitbtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void gendercombo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void cmbProgram_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }


        // Create button to add new booking to the database
        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            string query = "INSERT INTO Bookings (BookingID, FullName, ContactNo, MembershipType, Program, StartDate, Sessions, TotalCost) " +
                           "VALUES (@BookingID, @FullName, @ContactNo, @MembershipType, @Program, @StartDate, @Sessions, @TotalCost)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@BookingID", txtBookingID.Text.Trim());
                        cmd.Parameters.AddWithValue("@FullName", txtFullName.Text.Trim());
                        cmd.Parameters.AddWithValue("@ContactNo", txtcontact.Text.Trim());
                        cmd.Parameters.AddWithValue("@MembershipType", cmbMembership.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@Program", cmbProgram.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@StartDate", dtpStartDate.Value.Date);
                        cmd.Parameters.AddWithValue("@Sessions", int.Parse(txtSessions.Text));
                        cmd.Parameters.AddWithValue("@TotalCost", decimal.Parse(txtTotalCost.Text));

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Booking Create Successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ResetFields();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Database Error: " + ex.Message, "SQL Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Manage_Bookings_Load(object sender, EventArgs e)
        {
            ResetFields();
        }


        // Method to calculate total cost based on the number of sessions and the defined rates

        private void CalculateTotalCost()
        {
            if (string.IsNullOrEmpty(txtSessions.Text))
            {
                txtTotalCost.Text = "0.00";
                return;
            }

            if (int.TryParse(txtSessions.Text, out int sessions))
            {
                double rate = 0;

                if (sessions <= 10)
                {
                    rate = 1000; // ≤10 sessions → Rs. 1000
                }
                else if (sessions <= 30)
                {
                    rate = 800;  // ≤30 sessions → Rs. 800
                }
                else if (sessions <= 50)
                {
                    rate = 600;  // ≤50 sessions → Rs. 600
                }
                else
                {
                    rate = 500;  // Else → Rs. 500
                }

                double totalCost = sessions * rate;
                txtTotalCost.Text = totalCost.ToString("F2"); 
            }
            else
            {
                MessageBox.Show("Please enter only digits for the number of Sessions.", "Invalid input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSessions.Text = "";
                txtTotalCost.Text = "0.00";
            }
        }

        private void txtTotalCost_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetFields();
        }

        private void ResetFields()
        {
            txtFullName.Text = "";
            txtcontact.Text = "";
            cmbMembership.SelectedIndex = -1;
            cmbProgram.SelectedIndex = -1;
            dtpStartDate.Value = DateTime.Now;
            txtSessions.Text = "";
            txtTotalCost.Text = "0.00";

            txtBookingID.ReadOnly = true;
            txtTotalCost.ReadOnly = true;

            AutoGenerateBookingID();

            LoadGridData();
        }

        // Method to validate user inputs before performing database operations
        private bool ValidateInputs()
        {
            if (string.IsNullOrEmpty(txtBookingID.Text) || txtBookingID.Text.Trim() == "" || txtBookingID.Text == "B000")
            {
                MessageBox.Show("Booking ID was not generated properly. Please click Reset or restart the form.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (string.IsNullOrEmpty(txtFullName.Text) || string.IsNullOrEmpty(txtcontact.Text))
            {
                MessageBox.Show("Please Enter Full Name and Contact Number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (cmbMembership.SelectedIndex == -1)
            {
                MessageBox.Show("Please Select Membership Type.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (cmbProgram.SelectedIndex == -1)
            {
                MessageBox.Show("Please Select Program.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (string.IsNullOrEmpty(txtSessions.Text))
            {
                MessageBox.Show("Please Enter Sessions count.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        // Edit button to update existing booking details in the database
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            string query = "UPDATE Bookings SET FullName=@FullName, ContactNo=@ContactNo, MembershipType=@MembershipType, " +
                           "Program=@Program, StartDate=@StartDate, Sessions=@Sessions, TotalCost=@TotalCost WHERE BookingID=@BookingID";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@BookingID", txtBookingID.Text.Trim());
                        cmd.Parameters.AddWithValue("@FullName", txtFullName.Text.Trim());
                        cmd.Parameters.AddWithValue("@ContactNo", txtcontact.Text.Trim());
                        cmd.Parameters.AddWithValue("@MembershipType", cmbMembership.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@Program", cmbProgram.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@StartDate", dtpStartDate.Value.Date);
                        cmd.Parameters.AddWithValue("@Sessions", int.Parse(txtSessions.Text));
                        cmd.Parameters.AddWithValue("@TotalCost", decimal.Parse(txtTotalCost.Text));

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show($"Booking {txtBookingID.Text} Updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ResetFields();
                        }
                        else
                        {
                            MessageBox.Show("A booking with this ID cannot be found in the system.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Database update error: " + ex.Message, "SQL Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Cancel button to delete a booking from the database
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBookingID.Text))
            {
                MessageBox.Show("Please select the Booking ID you want to cancel from the system.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show($"Do you really want to remove the booking from the system?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                string query = "DELETE FROM Bookings WHERE BookingID=@BookingID";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@BookingID", txtBookingID.Text.Trim());

                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Booking Cancel Successfully!", "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                ResetFields();
                            }
                            else
                            {
                                MessageBox.Show("This ID cannot be found in the system..", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Database error occurred while deleting:" + ex.Message, "SQL Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // Link label to refresh the DataGridView with the latest data from the database
        private void displaylbl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LoadGridData();
        }

        private void LoadGridData()
        {
            string query = "SELECT * FROM Bookings";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    DataGridView1.DataSource = dt;

                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("Emty Database!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Grid data load error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Method to automatically generate a unique Booking ID 

        private void AutoGenerateBookingID()
        {
            string query = "SELECT MAX(BookingID) FROM Bookings";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        object result = cmd.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            string lastID = result.ToString().Trim();

                            if (lastID.StartsWith("B") && lastID.Length > 1)
                            {
                                int numberPart = int.Parse(lastID.Substring(1));
                                numberPart++; 
                                txtBookingID.Text = "B" + numberPart.ToString("D3");                  
                            }
                            else
                            {
                                txtBookingID.Text = "B001";
                            }
                        }
                        else
                        {
                            txtBookingID.Text = "B001";
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Auto ID Generating Error: " + ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }


        }

        private void txtSessions_TextChanged_1(object sender, EventArgs e)
        {
            CalculateTotalCost();
        }

        private void back_Click(object sender, EventArgs e)
        {
            Admin_Dashbord admindash = new Admin_Dashbord();
            admindash.Show();
            this.Hide();
        }
    }
}
 