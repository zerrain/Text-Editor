using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Assignment_2
{
    public partial class Form1 : Form
    {
        private string[] usernames;
        private string[] passwords;
        private User[] users;

        public Form1()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            this.AcceptButton = loginBtn;

            try
            {
                int noOfAccounts = File.ReadAllLines("login.txt").Length;
                usernames = new string[noOfAccounts];
                passwords = new string[noOfAccounts];
                users = new User[noOfAccounts];
                int counter = 0;
                using (StreamReader readAccounts = new StreamReader("login.txt"))
                {
                    while (!readAccounts.EndOfStream)
                    {
                        var credentials = readAccounts.ReadLine().Split(',');
                        usernames[counter] = credentials[0];
                        passwords[counter] = credentials[1];
                        users[counter] = new User(credentials[0], credentials[1], credentials[2], credentials[3], credentials[4], credentials[5]);
                        counter++;
                    }
                }
            } catch (FileNotFoundException e)
            {
                if (MessageBox.Show("Login text file not found", "Error") == DialogResult.OK)
                    Environment.Exit(0);
            }
        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            login();
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void CreateAccBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form3 form3 = new Form3(usernames);
            form3.Show();
        }

        private void login()
        {
            if (!usernames.Contains<string>(userNameTB.Text))
                MessageBox.Show("This user does not exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (!passwordTB.Text.Equals(passwords[Array.IndexOf(usernames, userNameTB.Text)]))
                MessageBox.Show("Incorrect password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                string currentUser = userNameTB.Text;
                string userPermissions = users[Array.IndexOf(usernames, currentUser)].getUserPermissions();
                this.Hide();
                Form2 form2 = new Form2(currentUser, userPermissions);
                form2.Show();
            }
        }
    }

    public class User
    {
        private string userName;
        private string password;
        private string userPermissions;
        private string firstName;
        private string lastName;
        private string dOB;

        public User(string userName, string password, string userPermissions, string firstName, string lastName, string dOB)
        {
            this.userName = userName;
            this.password = password;
            this.userPermissions = userPermissions;
            this.firstName = firstName;
            this.lastName = lastName;
            this.dOB = dOB;
        }

        public string getUserName()
        {
            return userName;
        }

        public string getUserPermissions()
        {
            return userPermissions;
        }
    }
}
