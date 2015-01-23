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
            this.userIdTextBox = new System.Windows.Forms.TextBox();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.passwordConfirmTextBox = new System.Windows.Forms.TextBox();
            this.userNameTextBox = new System.Windows.Forms.TextBox();
            this.registerButton = new System.Windows.Forms.Button();
            this.userID = new System.Windows.Forms.Label();
            this.password_lable = new System.Windows.Forms.Label();
            this.passwordConfirm_lable = new System.Windows.Forms.Label();
            this.userName = new System.Windows.Forms.Label();
            this.RegisterTitle_Lable = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // userIdTextBox
            // 
            this.userIdTextBox.Location = new System.Drawing.Point(164, 129);
            this.userIdTextBox.Multiline = true;
            this.userIdTextBox.Name = "userIdTextBox";
            this.userIdTextBox.Size = new System.Drawing.Size(229, 30);
            this.userIdTextBox.TabIndex = 0;
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Location = new System.Drawing.Point(164, 169);
            this.passwordTextBox.Multiline = true;
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.Size = new System.Drawing.Size(229, 30);
            this.passwordTextBox.TabIndex = 1;
            this.passwordTextBox.UseSystemPasswordChar = true;
            // 
            // passwordConfirmTextBox
            // 
            this.passwordConfirmTextBox.Location = new System.Drawing.Point(164, 208);
            this.passwordConfirmTextBox.Multiline = true;
            this.passwordConfirmTextBox.Name = "passwordConfirmTextBox";
            this.passwordConfirmTextBox.Size = new System.Drawing.Size(229, 30);
            this.passwordConfirmTextBox.TabIndex = 2;
            this.passwordConfirmTextBox.UseSystemPasswordChar = true;
            // 
            // userNameTextBox
            // 
            this.userNameTextBox.Location = new System.Drawing.Point(164, 90);
            this.userNameTextBox.Multiline = true;
            this.userNameTextBox.Name = "userNameTextBox";
            this.userNameTextBox.Size = new System.Drawing.Size(229, 30);
            this.userNameTextBox.TabIndex = 3;
            // 
            // registerButton
            // 
            this.registerButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.registerButton.Location = new System.Drawing.Point(207, 252);
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
            this.userID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.userID.Location = new System.Drawing.Point(23, 134);
            this.userID.Name = "userID";
            this.userID.Size = new System.Drawing.Size(135, 20);
            this.userID.TabIndex = 5;
            this.userID.Text = "UserID (email) :";
            // 
            // password_lable
            // 
            this.password_lable.AutoSize = true;
            this.password_lable.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.password_lable.Location = new System.Drawing.Point(71, 176);
            this.password_lable.Name = "password_lable";
            this.password_lable.Size = new System.Drawing.Size(87, 20);
            this.password_lable.TabIndex = 6;
            this.password_lable.Text = "비밀번호 :";
            // 
            // passwordConfirm_lable
            // 
            this.passwordConfirm_lable.AutoSize = true;
            this.passwordConfirm_lable.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.passwordConfirm_lable.Location = new System.Drawing.Point(32, 217);
            this.passwordConfirm_lable.Name = "passwordConfirm_lable";
            this.passwordConfirm_lable.Size = new System.Drawing.Size(126, 20);
            this.passwordConfirm_lable.TabIndex = 7;
            this.passwordConfirm_lable.Text = "비밀번호 확인 :";
            // 
            // userName
            // 
            this.userName.AutoSize = true;
            this.userName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.userName.Location = new System.Drawing.Point(95, 94);
            this.userName.Name = "userName";
            this.userName.Size = new System.Drawing.Size(63, 20);
            this.userName.TabIndex = 8;
            this.userName.Text = "이  름 :";
            // 
            // RegisterTitle_Lable
            // 
            this.RegisterTitle_Lable.AutoSize = true;
            this.RegisterTitle_Lable.Font = new System.Drawing.Font("Verdana", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RegisterTitle_Lable.Location = new System.Drawing.Point(12, 9);
            this.RegisterTitle_Lable.Name = "RegisterTitle_Lable";
            this.RegisterTitle_Lable.Size = new System.Drawing.Size(293, 32);
            this.RegisterTitle_Lable.TabIndex = 12;
            this.RegisterTitle_Lable.Text = "AnnoTask 계정 등록";
            // 
            // RegisterPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(456, 319);
            this.Controls.Add(this.RegisterTitle_Lable);
            this.Controls.Add(this.userName);
            this.Controls.Add(this.passwordConfirm_lable);
            this.Controls.Add(this.password_lable);
            this.Controls.Add(this.userID);
            this.Controls.Add(this.registerButton);
            this.Controls.Add(this.userNameTextBox);
            this.Controls.Add(this.passwordConfirmTextBox);
            this.Controls.Add(this.passwordTextBox);
            this.Controls.Add(this.userIdTextBox);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Name = "RegisterPage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sign Up";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox userIdTextBox;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.TextBox passwordConfirmTextBox;
        private System.Windows.Forms.TextBox userNameTextBox;
        private System.Windows.Forms.Button registerButton;
        private System.Windows.Forms.Label userID;
        private System.Windows.Forms.Label password_lable;
        private System.Windows.Forms.Label passwordConfirm_lable;
        private System.Windows.Forms.Label userName;
        private System.Windows.Forms.Label RegisterTitle_Lable;
    }
}