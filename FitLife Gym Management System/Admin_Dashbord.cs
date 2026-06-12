using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FitLife_Gym_Management_System
{
    public partial class Admin_Dashbord : Form
    {
        public Admin_Dashbord()
        {
            InitializeComponent();
        }

        private void Admin_Dashbord_Load(object sender, EventArgs e)
        {

        }

        //Back Button
        private void backbtn_Paint(object sender, PaintEventArgs e)
        {
            Admin_Dashbord admindash = new Admin_Dashbord();
            admindash.Show();
            this.Hide();
        }

        //System Exit Button
        private void exitbtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnback_Paint(object sender, PaintEventArgs e)
        {

        }

        //Go to the Main Menu Form
        private void backbtn_Click(object sender, EventArgs e)
        {
            frmmainmenu MainMenu = new frmmainmenu();
            MainMenu.Show();
            this.Close();
        }

        //Go to Manage Staff Form 
        private void managestaffbtn_Click(object sender, EventArgs e)
        {
            Manage_Staff manageStaffForm = new Manage_Staff();
            manageStaffForm.Show();
            this.Close();
        }

        //Go to Manage Programs Form 
        private void manageprogramsbtn_Click(object sender, EventArgs e)
        {
            Manage_Programs manageprograms = new Manage_Programs();
            manageprograms.Show();
            this.Close();
        }
    }
}
