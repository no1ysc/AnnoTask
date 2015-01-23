namespace AnnoTaskClient
{
    partial class RegisterPage
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
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.usernameTextBox = new System.Windows.Forms.TextBox();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.confirmPasswordTextBox = new System.Windows.Forms.TextBox();
            this.registerButton = new System.Windows.Forms.Button();
            this.userID = new System.Windows.Forms.Label();
            this.password_lable = new System.Windows.Forms.Label();
            this.passwordConfirm_lable = new System.Windows.Forms.Label();
            this.userName = new System.Windows.Forms.Label();
            this.RegisterTitle_Lable = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // nameTextBox
            // 
            this.nameTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.nameTextBox.Location = new System.Drawing.Point(186, 84);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(207, 20);
            this.nameTextBox.TabIndex = 0;
            // 
            // usernameTextBox
            // 
            this.usernameTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.usernameTextBox.Location = new System.Drawing.Point(186, 124);
            this.usernameTextBox.Name = "usernameTextBox";
            this.usernameTextBox.Size = new System.Drawing.Size(207, 20);
            this.usernameTextBox.TabIndex = 1;
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.passwordTextBox.Location = new System.Drawing.Point(186, 166);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.Size = new System.Drawing.Size(207, 20);
            this.passwordTextBox.TabIndex = 2;
            // 
            // confirmPasswordTextBox
            // 
            this.confirmPasswordTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.confirmPasswordTextBox.Location = new System.Drawing.Point(186, 207);
            this.confirmPasswordTextBox.Name = "confirmPasswordTextBox";
            this.confirmPasswordTextBox.Size = new System.Drawing.Size(207, 20);
            this.confirmPasswordTextBox.TabIndex = 3;
            // 
            // registerButton
            // 
            this.registerButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.registerButton.Location = new System.Drawing.Point(156, 252);
            this.registerButton.Name = "registerButton";
            this.registerButton.Size = new System.Drawing.Size(137, 39);
            this.registerButton.TabIndex = 4;
            this.registerButton.Text = "계정 등록";
            this.registerButton.UseVisualStyleBackColor = true;
            this.registerButton.Click += new System.EventHandler(this.registerButton_Click);
            // 
            // userID
            // 
            this.userID.AutoSize = true;
            this.userID.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.userID.Location = new System.Drawing.Point(34, 124);
            this.userID.Name = "userID";
            this.userID.Size = new System.Drawing.Size(146, 17);
            this.userID.TabIndex = 5;
            this.userID.Text = "Username (email) :";
            // 
            // password_lable
            // 
            this.password_lable.AutoSize = true;
            this.password_lable.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.password_lable.Location = new System.Drawing.Point(113, 164);
            this.password_lable.Name = "password_lable";
            this.password_lable.Size = new System.Drawing.Size(67, 20);
            this.password_lable.TabIndex = 6;
            this.password_lable.Text = "비밀번호 :";
            // 
            // passwordConfirm_lable
            // 
            this.passwordConfirm_lable.AutoSize = true;
            this.passwordConfirm_lable.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.passwordConfirm_lable.Location = new System.Drawing.Point(84, 205);
            this.passwordConfirm_lable.Name = "passwordConfirm_lable";
            this.passwordConfirm_lable.Size = new System.Drawing.Size(96, 20);
            this.passwordConfirm_lable.TabIndex = 7;
            this.passwordConfirm_lable.Text = "비밀번호 확인 :";
            // 
            // userName
            // 
            this.userName.AutoSize = true;
            this.userName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.userName.Location = new System.Drawing.Point(127, 82);
            this.userName.Name = "userName";
            this.userName.Size = new System.Drawing.Size(53, 20);
            this.userName.TabIndex = 8;
            this.userName.Text = "이  름 :";
            // 
            // RegisterTitle_Lable
            // 
            this.RegisterTitle_Lable.AutoSize = true;
            this.RegisterTitle_Lable.Font = new System.Drawing.Font("Verdana", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RegisterTitle_Lable.Location = new System.Drawing.Point(101, 23);
            this.RegisterTitle_Lable.Name = "RegisterTitle_Lable";
            this.RegisterTitle_Lable.Size = new System.Drawing.Size(222, 26);
            this.RegisterTitle_Lable.TabIndex = 12;
            this.RegisterTitle_Lable.Text = "AnnoTask 계정 등록";
            // 
            // RegisterPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(454, 312);
            this.Controls.Add(this.RegisterTitle_Lable);
            this.Controls.Add(this.userName);
            this.Controls.Add(this.passwordConfirm_lable);
            this.Controls.Add(this.password_lable);
            this.Controls.Add(this.userID);
            this.Controls.Add(this.registerButton);
            this.Controls.Add(this.confirmPasswordTextBox);
            this.Controls.Add(this.passwordTextBox);
            this.Controls.Add(this.usernameTextBox);
            this.Controls.Add(this.nameTextBox);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Name = "RegisterPage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sign Up";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.TextBox usernameTextBox;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.TextBox confirmPasswordTextBox;
        private System.Windows.Forms.Button registerButton;
        private System.Windows.Forms.Label userID;
        private System.Windows.Forms.Label password_lable;
        private System.Windows.Forms.Label passwordConfirm_lable;
        private System.Windows.Forms.Label userName;
        private System.Windows.Forms.Label RegisterTitle_Lable;
    }
}