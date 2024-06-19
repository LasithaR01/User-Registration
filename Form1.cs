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

namespace SkillsInternational
{
    public partial class Form1 : Form
    {
        //sql part
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Logins WHERE username=@username AND password=@password", con);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", password);

            con.Open();
            int result = (int)cmd.ExecuteScalar();
            con.Close();

            if (result > 0)
            {
                this.Hide();
                Form2 registrationForm = new Form2();
                registrationForm.Show();
            }
            else
            {
                MessageBox.Show("Invalid Username or Password");
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtUsername.Clear();
            txtPassword.Clear();
            txtUsername.Focus();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Are you sure to exit?", "Confirm Exit", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
