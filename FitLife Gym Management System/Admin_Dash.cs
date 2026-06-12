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
    public partial class Admin_Dash : Form
    {
        public Admin_Dash()
        {
            InitializeComponent();
        }

        //Go to Manage Staff Form Button Code
        private void managestaffbtn_Click(object sender, EventArgs e)
        {

        }

        //Go to Manage Programs Form Button Code
        private void manageprogramsbtn_Click(object sender, EventArgs e)
        {

        }

        //Back to Main Menu Button Code
        private void backbtn_Paint(object sender, PaintEventArgs e)
        {
            frmmainmenu frmmainmenu = new frmmainmenu();
            frmmainmenu.Show();
            this.Hide();
        }

        //System Exit Button Code
        private void exitbtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Admin_Dash_Load(object sender, EventArgs e)
        {

        }
    }
}
