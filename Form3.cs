using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment_2
{
    public partial class Form3 : Form
    {
        string[] existingUsernames;

        public Form3(string[] usernames)
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            userTypeCB.Items.Add("View");
            userTypeCB.Items.Add("Edit");

            existingUsernames = usernames;
        }

        private void submitBtn_Click(object sender, EventArgs e)
        {
            //create new
            if (!(userNameTB.Text.Equals("") || passwordTB.Text.Equals("") || userTypeCB.SelectedIndex == -1 || firstNameTB.Text.Equals("") || lastNameTB.Text.Equals("")))
            {
                if (!existingUsernames.Contains(userNameTB.Text))
                {
                    if (passwordTB.Text.Equals(rePasswordTB.Text))
                    {
                        File.AppendAllText("login.txt", "\n" + userNameTB.Text + "," + passwordTB.Text + "," + userTypeCB.SelectedItem.ToString() + "," + firstNameTB.Text + "," + lastNameTB.Text + "," + dobDP.Value.Date.ToString("dd-mm-yyyy"));
                        this.Hide();
                        Form1 form1 = new Form1();
                        form1.Show();
                    }
                    else
                        MessageBox.Show("The password has been re-entered incorrectly");
                }
                else
                    MessageBox.Show("This username already exists in the system.\n\nPlease try again with a different username", "Error");
            }
            else
                MessageBox.Show("Please complete all the fields");
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.Show();
        }
    }
}
