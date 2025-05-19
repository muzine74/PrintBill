using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DBConnection.Entity.UsersEntity;

namespace SendBillWF
{
    public partial class LoginForm : Form
    {
        public static User CurrentUser { get; private set; }

        public LoginForm()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, EventArgs e)
        {
            CurrentUser = new User { Username = UserNameLogin.Text ,password = PasswordLogin.Text };

            // Assign groups based on "role" (in real app, this would come from DB)
            if (UserNameLogin.Text == "admin")
            {
                CurrentUser.Groups.Add(PermissionManager.GetAvailableGroups().First(g => g.Name == "Administrators"));
            }
            else if (UserNameLogin.Text == "editor")
            {
                CurrentUser.Groups.Add(PermissionManager.GetAvailableGroups().First(g => g.Name == "Editors"));
            }
            else
            {
                CurrentUser.Groups.Add(PermissionManager.GetAvailableGroups().First(g => g.Name == "Viewers"));
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
