using Add_User_To_List_Project.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Add_User_To_List_Project
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        enum enGender
        {
            Male = 0,
            Female = 1,
        }

        void DefaultSytem()
        {

            tbFirstName.Clear();
            tbLastName.Clear();
            tbEmail.Clear();
            cbGender.SelectedIndex = 0;

            pbStudentImage.Image = Resources.user;

            lbStudentID.Text = "none";
            lbFullName.Text = "none";
            lbEmail.Text = "example@example.ex";
            lbGender.Text = "Male";


        }
        void ErrorProvider(System.Windows.Forms.TextBox TB, CancelEventArgs e, string ErrorOfMessage)
        {
            if (string.IsNullOrEmpty(TB.Text))
            {
                //e.Cancel = true;
                TB.Focus();
                errorProvider1.SetError(TB, ErrorOfMessage);
            }
            else
            {
                //e.Cancel = false;
                errorProvider1.SetError(TB, "");

            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            DefaultSytem();
            tbFirstName.Focus();
        }

        private void tbFirstName_Validating(object sender, CancelEventArgs e)
        {
            ErrorProvider((System.Windows.Forms.TextBox)sender, e, "First Name should have a value");
        }

        private void tbLastName_Validating(object sender, CancelEventArgs e)
        {
            ErrorProvider((System.Windows.Forms.TextBox)sender, e, "Last Name should have a value");

        }

        private bool ContainsSpecialCharacters(string Text)
        {
            foreach (char c in Text)
            {
                if (!char.IsLetterOrDigit(c) && !char.IsWhiteSpace(c))
                {
                    if (c != '@' && c != '.')
                        return true;
                }
            }

            return false;
        }
        private void tbEmail_Validating(object sender, CancelEventArgs e)
        {

            string ErrorMessage = "";
            bool ThereAreError = false;

            if (string.IsNullOrEmpty(tbEmail.Text))
            {
                ErrorMessage += "Email should have a value!" + Environment.NewLine;
                ThereAreError = true;
            }

            if (!tbEmail.Text.Contains("gmail") && !tbEmail.Text.Contains("hotmail") && !tbEmail.Text.Contains("yahoo") && !tbEmail.Text.Contains("outlook"))
            {
                ErrorMessage += "Email must contains one of these: gmail, hotmail,yahoo,outlook ...ets." + Environment.NewLine;
                ThereAreError = true;
            }

            if (!tbEmail.Text.Contains("@"))
            {
                ErrorMessage += "Email must contains \"@\"" + Environment.NewLine;
                ThereAreError = true;
            }

            if (!tbEmail.Text.Contains(".com"))
            {
                ErrorMessage += "Email must contains \".com\"" + Environment.NewLine;
                ThereAreError = true;
            }

            if (ContainsSpecialCharacters(tbEmail.Text))
            {
                ErrorMessage += "Email must dont contains any special characters like : !, #, $, %..ets" + Environment.NewLine;
                ThereAreError = true;
            }

            if (ThereAreError)
            {
                //e.Cancel = true;
                tbEmail.Focus();
                errorProvider1.SetError(tbEmail, ErrorMessage);
            }
            else
            {
                //e.Cancel = false;
                errorProvider1.SetError(tbEmail, "");
            }


        }

        string GenerateID()
        {
            Random random = new Random();
            string ID = "";

            for (byte i = 0; i < 8; i++)
            {
                if (i < 2 || i > 2)
                {
                    if (i <= 1)
                        ID += (char)random.Next(49, 57);
                    else
                        ID += (char)random.Next(48, 57);
                }
                else
                {
                    ID += (char)random.Next(65, 71);
                }
            }

            return ID;
        }

        bool IsEmailExist(string Email)
        {
            for (byte i = 0; i < listView1.Items.Count; i++)
            {
                if (Email == listView1.Items[i].SubItems[3].Text.ToString())
                {
                    return true;
                }
            }

            return false;
        }

        void NotifyForAddNewStudent(string FirstName, string LastName)
        {
            AddNotify.Icon = SystemIcons.Information;
            AddNotify.BalloonTipIcon = ToolTipIcon.Info;
            AddNotify.BalloonTipText = $"Student {FirstName} {LastName} was added successfully!";
            AddNotify.BalloonTipTitle = "Add Successfully";

            AddNotify.ShowBalloonTip(15);
        }
        void NotifyForUpdatStudent(string prevFirstName, string prevLastName, string NewFirstName, string NewLastName)
        {
            AddNotify.Icon = SystemIcons.Information;
            AddNotify.BalloonTipIcon = ToolTipIcon.Info;
            AddNotify.BalloonTipText = $"Update Student From \"{prevFirstName} {prevLastName}\" to \"{NewFirstName} {NewLastName}\" was done!";
            AddNotify.BalloonTipTitle = "Update Successfully";

            AddNotify.ShowBalloonTip(15);
        }
        void NotifyForWrong(string Message)
        {
            notifyIcon2.Icon = SystemIcons.Warning;
            notifyIcon2.BalloonTipIcon = ToolTipIcon.Warning;
            notifyIcon2.BalloonTipText = Message;
            notifyIcon2.BalloonTipTitle = "Warning";

            notifyIcon2.ShowBalloonTip(1);

        }

        void ClearTextBox()
        {
            tbFirstName.Clear();
            tbLastName.Clear();
            tbEmail.Clear();
            cbGender.SelectedIndex = 0;

            tbFirstName.Focus();
        }

        bool IsEmailValide(string Email)
        {

            if (string.IsNullOrEmpty(tbEmail.Text))
                return false;

            if (!tbEmail.Text.Contains("gmail") && !tbEmail.Text.Contains("hotmail") && !tbEmail.Text.Contains("yahoo") && !tbEmail.Text.Contains("outlook"))
            {
                return false;
            }

            if (!tbEmail.Text.Contains("@"))
            {
                return false;
            }

            if (!tbEmail.Text.Contains(".com"))
            {
                return false;
            }

            if (ContainsSpecialCharacters(tbEmail.Text))
            {
                return false;
            }

            return true;
        }

        bool IsIdExist(string ID)
        {
           
            for (byte i = 0; i < listView1.Items.Count; i++)
            {
                if (ID == listView1.Items[0].SubItems[0].Text)
                {
                    return true;
                }
            }
            return false;
        }

        void AddNewStudent(string FirstName, string LastName, string Email, enGender Gender)
        {


            if (IsEmailExist(Email))
            {
                NotifyForWrong($"This email \"{tbEmail.Text}\" has already been used");
                return;
            }


            string ID;
            ID = GenerateID();
            do
            {
                ID = GenerateID();

            } while (IsIdExist(ID));



            ListViewItem item = new ListViewItem(ID);
            item.SubItems.Add(FirstName);
            item.SubItems.Add(LastName);
            item.SubItems.Add(Email);
            item.SubItems.Add(Gender.ToString());

            if (Gender.ToString() == "Male")
                item.ImageIndex = 0;
            else
                item.ImageIndex = 1;

            listView1.Items.Add(item);
        }
        private void tbAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbFirstName.Text) || string.IsNullOrEmpty(tbLastName.Text) || !IsEmailValide(tbEmail.Text))
            {
                NotifyForWrong("One of the fields is empty!!");
                return;
            }

            enGender Gender = (cbGender.Text == "Male") ? enGender.Male : enGender.Female;
            AddNewStudent(tbFirstName.Text.Trim(), tbLastName.Text.Trim(), tbEmail.Text.Trim(), Gender);

            NotifyForAddNewStudent(tbFirstName.Text, tbLastName.Text);

            ClearTextBox();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (listView1.Items.Count != 0)
            {
                if (listView1.SelectedItems.Count == 1)
                {
                    ListViewItem Item = listView1.SelectedItems[0];
                    Item.Remove();
                }
                else
                {
                    ListViewItem Item;
                    for (byte i = 0; i < listView1.SelectedItems.Count; i++)
                    {
                        Item = listView1.SelectedItems[i];
                        Item.Remove();
                        i--;
                    }
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            btnUpdat.Enabled = cbUpdate.Checked;
        }

        private void listView1_Click(object sender, EventArgs e)
        {
            ListViewItem item = listView1.SelectedItems[0];

            lbStudentID.Text = item.SubItems[0].Text;
            lbFullName.Text = item.SubItems[1].Text + " " + item.SubItems[2].Text;
            lbEmail.Text = item.SubItems[3].Text;
            lbGender.Text = item.SubItems[4].Text;

            if (lbGender.Text == "Male")
                pbStudentImage.Image = Resources.Male; 
            else
                pbStudentImage.Image = Resources.Female;

        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            ListViewItem item = listView1.SelectedItems[0];

            tbFirstName.Text = item.SubItems[1].Text;
            tbLastName.Text = item.SubItems[2].Text;
            tbEmail.Text = item.SubItems[3].Text;

            cbGender.SelectedIndex = (item.SubItems[4].Text == "Male") ? 0 : 1;
        }

        private void btnUpdat_Click_1(object sender, EventArgs e)
        {
            ListViewItem item = listView1.SelectedItems[0];

            string prevFirstName = item.SubItems[1].Text, prevLastName = item.SubItems[2].Text,
                   newFirstName = tbFirstName.Text, newLastName = tbLastName.Text;

            item.SubItems[1].Text = tbFirstName.Text;
            item.SubItems[2].Text = tbLastName.Text;
            item.SubItems[3].Text = tbEmail.Text;
            item.SubItems[4].Text = cbGender.Text;

            if ((prevFirstName != newFirstName) || (prevLastName != newLastName))
                NotifyForUpdatStudent(prevFirstName,prevLastName,newFirstName,newLastName);

            ClearTextBox();
            cbUpdate.Checked = false;
        }

        private void btnRandomStudent_Click(object sender, EventArgs e)
        {
            Random rdm = new Random();
            string[] MaleNames = { "Omar", "Khaled", "Youssef", "Mohammed", "Ahmed", "Ali", "Saeed",
                                   "Ibrahim", "Fahd","Yasser", "Majed", "Tariq", "Hassan", "Salim",
                                   "Sami", "Zaid", "Anas", "Adel", "Rami", "Basel"};


            string[] FemaleNames = { "Fatima", "Layla", "Maryam", "Noor", "Sarah", "Aya", "Zainab",
                                     "Huda", "Rahaf", "Reem","Nora", "Shaimaa", "Lina", "Rania",
                                      "Soumaya", "Dana", "Raghad", "Najla", "Hanan", "Yasmeen"};

            for(byte i = 0; i < 10; i++)
            {
                enGender Gender = (i % 2 == 0) ? enGender.Male : enGender.Female;

                string FirstName = (Gender == enGender.Male) ? MaleNames[rdm.Next(0,19)] : FemaleNames[rdm.Next(0, 19)];
                string LastName = MaleNames[rdm.Next(0, 19)];

                string Email = $"{FirstName.Substring(0,3).ToLower()}{LastName.Substring(0,3).ToLower()}{rdm.Next(1,9)}{rdm.Next(1,9)}@gmail.com";

                Thread.Sleep(2);
                AddNewStudent(FirstName, LastName, Email, Gender);
            }
        }
    }
}
