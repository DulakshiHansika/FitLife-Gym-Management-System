using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FitLife_Gym_Management_System
{
    public partial class frmmainmenu : Form
    {
        public frmmainmenu()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //MainMenu Transparent Panel Code
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            panel1.BackColor = Color.FromArgb(170, 20, 20, 20);

            label1.Parent = panel1;
            label1.BackColor = Color.Transparent;
            label1.ForeColor = Color.White;

            label2.Parent = panel1;
            label2.BackColor = Color.Transparent;
            label2.ForeColor = Color.White;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }

        //Go to the Admin Login Form
        private void adminbtn_Click(object sender, EventArgs e)
        {
            Admin__Login_Form adminForm = new Admin__Login_Form();
            adminForm.Show();
            this.Hide();
        }

        //Go to the Member Login Form
        private void memberloginBTN_Click(object sender, EventArgs e)
        {
            Member_Login memberForm = new Member_Login();
            memberForm.Show();
            this.Hide();
        }

        //Exit Button Code
        private void exitbtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
