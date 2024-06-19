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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            LoadRegNumbers();
        }

        private void LoadRegNumbers()
        {
            SqlCommand cmd = new SqlCommand("SELECT regNo FROM Registration", con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                cmbRegNo.Items.Add(reader["regNo"].ToString());
            }
            con.Close();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO Registration (regNo, firstName, lastName, dateOfBirth, gender, address, email, mobilePhone, homePhone, parentName, nic, contactNo) VALUES (@regNo, @firstName, @lastName, @dateOfBirth, @gender, @address, @email, @mobilePhone, @homePhone, @parentName, @nic, @contactNo)", con);
            cmd.Parameters.AddWithValue("@regNo", int.Parse(cmbRegNo.Text));
            cmd.Parameters.AddWithValue("@firstName", txtFirstName.Text);
            cmd.Parameters.AddWithValue("@lastName", txtLastName.Text);
            cmd.Parameters.AddWithValue("@dateOfBirth", dtpDateOfBirth.Value);
            cmd.Parameters.AddWithValue("@gender", rbtnMale.Checked ? "Male" : "Female");
            cmd.Parameters.AddWithValue("@address", txtAddress.Text);
            cmd.Parameters.AddWithValue("@email", txtEmail.Text);
            cmd.Parameters.AddWithValue("@mobilePhone", int.Parse(txtMobilePhone.Text));
            cmd.Parameters.AddWithValue("@homePhone", int.Parse(txtHomePhone.Text));
            cmd.Parameters.AddWithValue("@parentName", txtParentName.Text);
            cmd.Parameters.AddWithValue("@nic", txtNIC.Text);
            cmd.Parameters.AddWithValue("@contactNo", int.Parse(txtContactNo.Text));

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            MessageBox.Show("Registration Successful!");
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("UPDATE Registration SET firstName=@firstName, lastName=@lastName, dateOfBirth=@dateOfBirth, gender=@gender, address=@address, email=@email, mobilePhone=@mobilePhone, homePhone=@homePhone, parentName=@parentName, nic=@nic, contactNo=@contactNo WHERE regNo=@regNo", con);
            cmd.Parameters.AddWithValue("@regNo", int.Parse(cmbRegNo.Text));
            cmd.Parameters.AddWithValue("@firstName", txtFirstName.Text);
            cmd.Parameters.AddWithValue("@lastName", txtLastName.Text);
            cmd.Parameters.AddWithValue("@dateOfBirth", dtpDateOfBirth.Value);
            cmd.Parameters.AddWithValue("@gender", rbtnMale.Checked ? "Male" : "Female");
            cmd.Parameters.AddWithValue("@address", txtAddress.Text);
            cmd.Parameters.AddWithValue("@email", txtEmail.Text);
            cmd.Parameters.AddWithValue("@mobilePhone", int.Parse(txtMobilePhone.Text));
            cmd.Parameters.AddWithValue("@homePhone", int.Parse(txtHomePhone.Text));
            cmd.Parameters.AddWithValue("@parentName", txtParentName.Text);
            cmd.Parameters.AddWithValue("@nic", txtNIC.Text);
            cmd.Parameters.AddWithValue("@contactNo", int.Parse(txtContactNo.Text));

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            MessageBox.Show("Update Successful!");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Are you sure to delete this item?", "Confirm Delete", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Registration WHERE regNo=@regNo", con);
                cmd.Parameters.AddWithValue("@regNo", int.Parse(cmbRegNo.Text));

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Delete Successful!");
            }
        }

        private void cmbRegNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Registration WHERE regNo=@regNo", con);
            cmd.Parameters.AddWithValue("@regNo", int.Parse(cmbRegNo.Text));

            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                txtFirstName.Text = reader["firstName"].ToString();
                txtLastName.Text = reader["lastName"].ToString();
                dtpDateOfBirth.Value = DateTime.Parse(reader["dateOfBirth"].ToString());
                if (reader["gender"].ToString() == "Male")
                {
                    rbtnMale.Checked = true;
                }
                else
                {
                    rbtnFemale.Checked = true;
                }
                txtAddress.Text = reader["address"].ToString();
                txtEmail.Text = reader["email"].ToString();
                txtMobilePhone.Text = reader["mobilePhone"].ToString();
                txtHomePhone.Text = reader["homePhone"].ToString();
                txtParentName.Text = reader["parentName"].ToString();
                txtNIC.Text = reader["nic"].ToString();
                txtContactNo.Text = reader["contactNo"].ToString();
            }
            con.Close();
        }

        private void lnkLogout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Form1 loginForm = new Form1();
            loginForm.Show();
        }

        private void lnkExit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var confirmResult = MessageBox.Show("Are you sure to exit?", "Confirm Exit", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
