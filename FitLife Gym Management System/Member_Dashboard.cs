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
    public partial class Member_Dashboard : Form
    {
        public Member_Dashboard()
        {
            InitializeComponent();
        }

        //System Exit Button Code
        private void exitbtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //Go to the MainMenu Form
        private void backbtn_Click(object sender, EventArgs e)
        {
            frmmainmenu MainMenu = new frmmainmenu();
            MainMenu.Show();
            this.Close();
        }

        private void viewprogramsbtn_Click(object sender, EventArgs e)
        {

        }

        //Manage Bookings Button Code
        private void managebookingdbtn_Click(object sender, EventArgs e)
        {
            Manage_Bookings manageBookingsForm = new Manage_Bookings();
            manageBookingsForm.Show();
            this.Hide();
        }

        private void Member_Dashboard_Load(object sender, EventArgs e)
        {

        }

        private void viewprogramfrmbtn_Click(object sender, EventArgs e)
        {
            ViewProgram viewprogram = new ViewProgram();
            viewprogram.Show();
            this.Hide();
        }
    }
}



