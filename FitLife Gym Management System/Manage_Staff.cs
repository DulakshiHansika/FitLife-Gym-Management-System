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
using System.Xml.Linq;

namespace FitLife_Gym_Management_System
{
    public partial class Manage_Staff : Form
    {       
        // Database Connection 
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\dulak\OneDrive\Documents\GymDB.mdf;Integrated Security=True;Connect Timeout=30");

        public Manage_Staff()
        {
            InitializeComponent();
        }

        // Method to Load Staff Data into DataGridView
        private void LoadStaffData()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Staff", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                guna2DataGridView1.DataSource = dt; 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message);
            }
        }

        // Method to Get Next Available ID for New Staff Member
        private void GetNextId()
        {
            try
            {
                string query = "SELECT ISNULL(MAX(Id), 0) + 1 FROM Staff";
                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();
                int nextId = Convert.ToInt32(cmd.ExecuteScalar());
                conn.Close();

                txtid.Text = nextId.ToString();
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show("Error fetching next ID: " + ex.Message);
            }
        }

        private void guna2TextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        //System Exit Button Code
        private void exitbtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Add New Member Code
        private void addbtn_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(
                "INSERT INTO Staff VALUES (@name,@gender,@role,@contact,@email,@salary)", conn);

                cmd.Parameters.AddWithValue("@name", txtname.Text);
                cmd.Parameters.AddWithValue("@gender", gendercombo.Text);
                cmd.Parameters.AddWithValue("@role", rolecombobox.Text);
                cmd.Parameters.AddWithValue("@contact", txtcontact.Text);
                cmd.Parameters.AddWithValue("@email", txtemail.Text);
                cmd.Parameters.AddWithValue("@salary", txtsalary.Text);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Staff Added Successfully");

                conn.Close();

                LoadStaffData();

                GetNextId();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        private void Manage_Staff_Load(object sender, EventArgs e)
        {
                     
        }

        private void txtsalary_TextChanged(object sender, EventArgs e)
        {

        }

        //Role Combobox Selection Change Event to Auto Fill Salary
        private void rolecombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (rolecombobox.Text)
            {
                case "Trainer":
                    txtsalary.Text = "70000";
                    break;

                case "Reception":
                    txtsalary.Text = "40000";
                    break;

                case "Cleaner":
                    txtsalary.Text = "30000";
                    break;

                case "Nutritionist":
                    txtsalary.Text = "60000";
                    break;
            }
        }

        //Update Member Button Code
        private void updatebtn_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();

                string query = @"UPDATE Staff
                         SET Name=@Name,
                             Gender=@Gender,
                             Role=@Role,
                             Contact=@Contact,
                             Email=@Email,
                             Salary=@Salary
                         WHERE Id=@Id";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@Id", txtid.Text);
                cmd.Parameters.AddWithValue("@Name", txtname.Text);
                cmd.Parameters.AddWithValue("@Gender", gendercombo.Text);
                cmd.Parameters.AddWithValue("@Role", rolecombobox.Text);
                cmd.Parameters.AddWithValue("@Contact", txtcontact.Text);
                cmd.Parameters.AddWithValue("@Email", txtemail.Text);
                cmd.Parameters.AddWithValue("@Salary", txtsalary.Text);

                cmd.ExecuteNonQuery();

                conn.Close();

                MessageBox.Show("Staff Updated Successfully");

                LoadStaffData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        //Delete Member Button Code
        private void deletebtn_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show(
                    "Do you want to delete this staff member?",
                    "Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(
                        "DELETE FROM Staff WHERE Id=@Id", conn);

                    cmd.Parameters.AddWithValue("@Id", txtid.Text);

                    cmd.ExecuteNonQuery();

                    conn.Close();

                    MessageBox.Show("Deleted Successfully");

                    LoadStaffData();
                    clearbtn.PerformClick();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        //Clear Button Code
        private void clearbtn_Click(object sender, EventArgs e)
        {
            txtid.Clear();
            txtname.Clear();
            gendercombo.SelectedIndex = -1;
            rolecombobox.SelectedIndex = -1;
            txtcontact.Clear();
            txtemail.Clear();
            txtsalary.Clear();

            GetNextId();
        }

        //Search Button Code
        private void searchbtn_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "SELECT * FROM Staff WHERE Name LIKE @Search";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@Search",
                    "%" + txtsreach.Text + "%");

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();

                da.Fill(dt);

                guna2DataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void txtid_TextChanged(object sender, EventArgs e)
        {

        }

        private void rolecombobox_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void guna2CustomGradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtsreach_TextChanged(object sender, EventArgs e)
        {

        }

        //Data GridView Code
        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = guna2DataGridView1.Rows[e.RowIndex];

                txtid.Text = row.Cells["Id"].Value.ToString();
                txtname.Text = row.Cells["Name"].Value.ToString();
                gendercombo.SelectedItem = row.Cells["Gender"].Value.ToString();
                rolecombobox.SelectedItem = row.Cells["Role"].Value.ToString();
                txtcontact.Text = row.Cells["Contact"].Value.ToString();
                txtemail.Text = row.Cells["Email"].Value.ToString();
                txtsalary.Text = row.Cells["Salary"].Value.ToString();
            }

        }

        //Back Icon Code
        private void back_Click(object sender, EventArgs e)
        {
            Admin_Dashbord admindash = new Admin_Dashbord(); 
            admindash.Show();
            this.Hide();
        }

        private void txtcontact_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void gendercombo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void txtname_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
