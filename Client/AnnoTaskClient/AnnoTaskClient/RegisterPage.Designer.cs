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
            this.userNameTextBox = new System.Windows.Forms.TextBox();
            this.userIDTextBox = new System.Windows.Forms.TextBox();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.passwordConfirmTextBox = new System.Windows.Forms.TextBox();
            this.registerButton = new System.Windows.Forms.Button();
            this.userID_lable = new System.Windows.Forms.Label();
            this.password_lable = new System.Windows.Forms.Label();
            this.passwordConfirm_lable = new System.Windows.Forms.Label();
            this.userName_lable = new System.Windows.Forms.Label();
            this.RegisterTitle_Lable = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // userNameTextBox
            // 
            this.userNameTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userNameTextBox.Location = new System.Drawing.Point(206, 84);
            this.userNameTextBox.Name = "userNameTextBox";
            this.userNameTextBox.Size = new System.Drawing.Size(187, 21);
            this.userNameTextBox.TabIndex = 0;
            this.userNameTextBox.TabStop = false;
            // 
            // userIDTextBox
            // 
            this.userIDTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userIDTextBox.Location = new System.Drawing.Point(206, 124);
            this.userIDTextBox.Name = "userIDTextBox";
            this.userIDTextBox.Size = new System.Drawing.Size(187, 21);
            this.userIDTextBox.TabIndex = 1;
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.passwordTextBox.Location = new System.Drawing.Point(206, 166);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.Size = new System.Drawing.Size(187, 21);
            this.passwordTextBox.TabIndex = 2;
            // 
            // passwordConfirmTextBox
            // 
            this.passwordConfirmTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.passwordConfirmTextBox.Location = new System.Drawing.Point(206, 205);
            this.passwordConfirmTextBox.Name = "passwordConfirmTextBox";
            this.passwordConfirmTextBox.Size = new System.Drawing.Size(187, 21);
            this.passwordConfirmTextBox.TabIndex = 3;
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
            // userID_lable
            // 
            this.userID_lable.AutoSize = true;
            this.userID_lable.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userID_lable.Location = new System.Drawing.Point(54, 123);
            this.userID_lable.Name = "userID_lable";
            this.userID_lable.Size = new System.Drawing.Size(140, 18);
            this.userID_lable.TabIndex = 5;
            this.userID_lable.Text = "Username (email) :";
            // 
            // password_lable
            // 
            this.password_lable.AutoSize = true;
            this.password_lable.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.password_lable.Location = new System.Drawing.Point(113, 166);
            this.password_lable.Name = "password_lable";
            this.password_lable.Size = new System.Drawing.Size(81, 20);
            this.password_lable.TabIndex = 6;
            this.password_lable.Text = "비밀번호 :";
            // 
            // passwordConfirm_lable
            // 
            this.passwordConfirm_lable.AutoSize = true;
            this.passwordConfirm_lable.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.passwordConfirm_lable.Location = new System.Drawing.Point(77, 205);
            this.passwordConfirm_lable.Name = "passwordConfirm_lable";
            this.passwordConfirm_lable.Size = new System.Drawing.Size(117, 20);
            this.passwordConfirm_lable.TabIndex = 7;
            this.passwordConfirm_lable.Text = "비밀번호 확인 :";
            // 
            // userName_lable
            // 
            this.userName_lable.AutoSize = true;
            this.userName_lable.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.userName_lable.Location = new System.Drawing.Point(137, 84);
            this.userName_lable.Name = "userName_lable";
            this.userName_lable.Size = new System.Drawing.Size(57, 20);
            this.userName_lable.TabIndex = 8;
            this.userName_lable.Text = "이  름 :";
            // 
            // RegisterTitle_Lable
            // 
            this.RegisterTitle_Lable.AutoSize = true;
            this.RegisterTitle_Lable.Font = new System.Drawing.Font("Verdana", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RegisterTitle_Lable.Location = new System.Drawing.Point(101, 23);
            this.RegisterTitle_Lable.Name = "RegisterTitle_Lable";
            this.RegisterTitle_Lable.Size = new System.Drawing.Size(242, 26);
            this.RegisterTitle_Lable.TabIndex = 12;
            this.RegisterTitle_Lable.Text = "AnnoTask 계정 등록";
            // 
            // RegisterPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(454, 312);
            this.Controls.Add(this.RegisterTitle_Lable);
            this.Controls.Add(this.userName_lable);
            this.Controls.Add(this.passwordConfirm_lable);
            this.Controls.Add(this.password_lable);
            this.Controls.Add(this.userID_lable);
            this.Controls.Add(this.registerButton);
            this.Controls.Add(this.passwordConfirmTextBox);
            this.Controls.Add(this.passwordTextBox);
            this.Controls.Add(this.userIDTextBox);
            this.Controls.Add(this.userNameTextBox);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Name = "RegisterPage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sign Up";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox userNameTextBox;
        private System.Windows.Forms.TextBox userIDTextBox;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.TextBox passwordConfirmTextBox;
        private System.Windows.Forms.Button registerButton;
        private System.Windows.Forms.Label userID_lable;
        private System.Windows.Forms.Label password_lable;
        private System.Windows.Forms.Label passwordConfirm_lable;
        private System.Windows.Forms.Label userName_lable;
        private System.Windows.Forms.Label RegisterTitle_Lable;
    }
}