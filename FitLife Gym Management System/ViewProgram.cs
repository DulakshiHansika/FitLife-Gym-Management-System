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
    public partial class ViewProgram : Form
    {
        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\dulak\OneDrive\Documents\GymDB.mdf;Integrated Security=True;Connect Timeout=30";
        public ViewProgram()
        {
            InitializeComponent();
        }

        private void ViewProgram_Load(object sender, EventArgs e)
        {
            LoadPrograms();
        }

        private void LoadPrograms()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string query = "SELECT * FROM Programs";

                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    DataGridView1.DataSource = dt;

                    DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    DataGridView1.ReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message);
            }
        }

        //Search Program
        private void searchbtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtprogramname.Text))
            {
                LoadPrograms();
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string query = "SELECT * FROM Programs WHERE Name LIKE @name";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@name", "%" + txtprogramname.Text.Trim() + "%");

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        DataGridView1.DataSource = dt;

                        if (dt.Rows.Count == 0)
                        {
                            MessageBox.Show("No programs found!", "Info",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Error: " + ex.Message);
            }
        }

        private void clearbtn_Click(object sender, EventArgs e)
        {
            txtprogramname.Clear();
            txtprogramname.Focus();

            LoadPrograms();      
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = DataGridView1.Rows[e.RowIndex];

                txtprogramname.Text = row.Cells["Name"].Value.ToString();
            }
        }

        private void exitbtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void back_Click(object sender, EventArgs e)
        {
            Member_Dashboard member_Dashboard = new Member_Dashboard();
            member_Dashboard.Show();
            this.Close();
        }
    }
}


