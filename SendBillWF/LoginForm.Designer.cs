namespace SendBillWF
{
    partial class LoginForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            UserNameLogin = new TextBox();
            PasswordLogin = new TextBox();
            label1 = new Label();
            label2 = new Label();
            Login = new Button();
            LoginCancel = new Button();
            SuspendLayout();
            // 
            // UserNameLogin
            // 
            UserNameLogin.Location = new Point(271, 128);
            UserNameLogin.Name = "UserNameLogin";
            UserNameLogin.Size = new Size(256, 23);
            UserNameLogin.TabIndex = 0;
            // 
            // PasswordLogin
            // 
            PasswordLogin.Location = new Point(271, 175);
            PasswordLogin.Name = "PasswordLogin";
            PasswordLogin.PasswordChar = '*';
            PasswordLogin.Size = new Size(256, 23);
            PasswordLogin.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(162, 132);
            label1.Name = "label1";
            label1.Size = new Size(89, 15);
            label1.TabIndex = 2;
            label1.Text = "Nom utilisateur";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(162, 179);
            label2.Name = "label2";
            label2.Size = new Size(71, 15);
            label2.TabIndex = 3;
            label2.Text = "mot de Pass";
            // 
            // Login
            // 
            Login.Location = new Point(258, 284);
            Login.Name = "Login";
            Login.Size = new Size(93, 28);
            Login.TabIndex = 4;
            Login.Text = "Se connecté";
            Login.UseVisualStyleBackColor = true;
            Login.Click += Login_Click;
            // 
            // LoginCancel
            // 
            LoginCancel.Location = new Point(397, 284);
            LoginCancel.Name = "LoginCancel";
            LoginCancel.Size = new Size(93, 28);
            LoginCancel.TabIndex = 5;
            LoginCancel.Text = "Annulé";
            LoginCancel.UseVisualStyleBackColor = true;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(LoginCancel);
            Controls.Add(Login);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(PasswordLogin);
            Controls.Add(UserNameLogin);
            Name = "LoginForm";
            Text = "LoginForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox UserNameLogin;
        private TextBox PasswordLogin;
        private Label label1;
        private Label label2;
        private Button Login;
        private Button LoginCancel;
    }
}