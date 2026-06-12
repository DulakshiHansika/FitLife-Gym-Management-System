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
    public partial class Manage_Programs : Form
    {
        // Database Connection 
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\dulak\OneDrive\Documents\GymDB.mdf;Integrated Security=True;Connect Timeout=30");

        public Manage_Programs()
        {
            InitializeComponent();
        }

        private void Manage_Programs_Load(object sender, EventArgs e)
        {
            LoadProgramData();
            GetNextId();
            LoadTrainers();
        }

        // Method to Load Programs Data into DataGridView
        private void LoadProgramData()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Programs", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                DataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message);
            }
        }

        // Method to Get Next Available ID for New Program
        private void GetNextId()
        {
            try
            {
                string query = "SELECT ISNULL(MAX(Id), 0) + 1 FROM Programs";
                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();
                int nextId = Convert.ToInt32(cmd.ExecuteScalar());
                conn.Close();

                txtid.Text = nextId.ToString();
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open) conn.Close();
                MessageBox.Show("Error fetching next ID: " + ex.Message);
            }
        }

        // Method to Load Trainer Names into ComboBox
        private void LoadTrainers()
        {
            try
            {
                string query = "SELECT Name FROM Staff WHERE Role = 'Trainer'";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                comboTrainer.Items.Clear();
                while (dr.Read())
                {
                    comboTrainer.Items.Add(dr["Name"].ToString());
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open) conn.Close();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        //Add New Program Button Code
        private void addbtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtname.Text) || string.IsNullOrEmpty(txtcont.Text))
            {
                MessageBox.Show("Please fill all required fields (Name and Cost).");
                return;
            }

            try
            {
                conn.Open();

                string query = "INSERT INTO Programs (Name, CostPerSession, Description, Trainer) VALUES (@Name, @Cost, @Description, @Trainer)";
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@Name", txtname.Text);
                cmd.Parameters.AddWithValue("@Cost", Convert.ToDecimal(txtcont.Text));
                cmd.Parameters.AddWithValue("@Description", txtdescription.Text);
                cmd.Parameters.AddWithValue("@Trainer", comboTrainer.Text);

                cmd.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("Program Added Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadProgramData();
                clearbtn.PerformClick(); 
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open) conn.Close();
                MessageBox.Show(ex.Message);
            }
        }

        //Edit or Update Program Button Code 
        private void updatebtn_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();

                string query = @"UPDATE Programs 
                                 SET Name = @Name, 
                                     CostPerSession = @Cost, 
                                     Description = @Description, 
                                     Trainer = @Trainer 
                                 WHERE Id = @Id";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@Id", txtid.Text);
                cmd.Parameters.AddWithValue("@Name", txtname.Text);
                cmd.Parameters.AddWithValue("@Cost", Convert.ToDecimal(txtcont.Text));
                cmd.Parameters.AddWithValue("@Description", txtdescription.Text);
                cmd.Parameters.AddWithValue("@Trainer", comboTrainer.Text);

                cmd.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("Program Updated Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadProgramData();
                clearbtn.PerformClick();
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open) conn.Close();
                MessageBox.Show(ex.Message);
            }
        }

        //Delete Program Button Code
        private void deletebtn_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show(
                    "Do you want to delete this program?",
                    "Delete Program",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("DELETE FROM Programs WHERE Id=@Id", conn);
                    cmd.Parameters.AddWithValue("@Id", txtid.Text);

                    cmd.ExecuteNonQuery();
                    conn.Close();

                    MessageBox.Show("Program Deleted Successfully", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadProgramData();
                    clearbtn.PerformClick();
                }
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open) conn.Close();
                MessageBox.Show(ex.Message);
            }
        }

        //Clear Input Fields Button Code
        private void clearbtn_Click(object sender, EventArgs e)
        {
            txtname.Clear();
            txtcont.Clear();
            txtdescription.Clear();
            comboTrainer.SelectedIndex = -1;
            comboTrainer.Text = "";

            GetNextId();
        }

        // Populate Input Fields when a Row is Selected in DataGridView
        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = DataGridView1.Rows[e.RowIndex];

                txtid.Text = row.Cells["Id"].Value.ToString();
                txtname.Text = row.Cells["Name"].Value.ToString();
                txtcont.Text = row.Cells["CostPerSession"].Value.ToString();
                txtdescription.Text = row.Cells["Description"].Value.ToString();
                comboTrainer.Text = row.Cells["Trainer"].Value.ToString();
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        //System Exit Button Code
        private void exitbtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Search Program Button Code
        private void searchbtn_Click(object sender, EventArgs e)
        {

        }
    }
}
