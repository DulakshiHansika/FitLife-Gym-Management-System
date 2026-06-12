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
    public partial class View_Programs : Form
    {
        public View_Programs()
        {
            InitializeComponent();
        }

        // Load Event to Populate DataGridView with Programs Data
        private void View_Programs_Load(object sender, EventArgs e)
        {
            LoadPrograms();
        }

        // Method to Load Programs Data into DataGridView
        private void LoadPrograms()
        {
            
        }

        //System Exit Button
        private void guna2CustomGradientPanel1_Paint(object sender, PaintEventArgs e)
        {
            this.Close();
        }

        //Go to the Member Dashboard Form
        private void backbtn_Click(object sender, EventArgs e)
        {
            Member_Dashboard frm = new Member_Dashboard();
            frm.Show();
            this.Hide();
        }

        private void loginbtn_Click(object sender, EventArgs e)
        {

        }

        //DataGridView Cell Click Event to Show Program Details
        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        //Clear Button Code
        private void clearbtn_Click(object sender, EventArgs e)
        {

        }

        //Search Button Code
        private void searchbtn_Click(object sender, EventArgs e)
        {
           
        }

        private void View_Programs_Load_1(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void exitbtn_Click(object sender, EventArgs e)
        {

        }

        private void txtprogramname_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
