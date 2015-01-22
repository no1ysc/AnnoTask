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
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.registerButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.userIdStatus = new System.Windows.Forms.Label();
            this.passwordStatus = new System.Windows.Forms.Label();
            this.passwordConfirmStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // userIdTextBox
            // 
            this.userIdTextBox.Location = new System.Drawing.Point(211, 56);
            this.userIdTextBox.Name = "userIdTextBox";
            this.userIdTextBox.Size = new System.Drawing.Size(229, 20);
            this.userIdTextBox.TabIndex = 0;
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Location = new System.Drawing.Point(211, 104);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.Size = new System.Drawing.Size(229, 20);
            this.passwordTextBox.TabIndex = 1;
            this.passwordTextBox.UseSystemPasswordChar = true;
            // 
            // passwordConfirmTextBox
            // 
            this.passwordConfirmTextBox.Location = new System.Drawing.Point(211, 147);
            this.passwordConfirmTextBox.Name = "passwordConfirmTextBox";
            this.passwordConfirmTextBox.Size = new System.Drawing.Size(229, 20);
            this.passwordConfirmTextBox.TabIndex = 2;
            this.passwordConfirmTextBox.UseSystemPasswordChar = true;
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(211, 192);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(229, 20);
            this.nameTextBox.TabIndex = 3;
            // 
            // registerButton
            // 
            this.registerButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.registerButton.Location = new System.Drawing.Point(264, 234);
            this.registerButton.Name = "registerButton";
            this.registerButton.Size = new System.Drawing.Size(119, 30);
            this.registerButton.TabIndex = 4;
            this.registerButton.Text = "확  인";
            this.registerButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(90, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "UserID (email) :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.Location = new System.Drawing.Point(136, 105);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "비밀번호 :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.Location = new System.Drawing.Point(108, 148);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 15);
            this.label3.TabIndex = 7;
            this.label3.Text = "비밀번호 확인 :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.Location = new System.Drawing.Point(152, 193);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 15);
            this.label4.TabIndex = 8;
            this.label4.Text = "이  름 :";
            // 
            // userIdStatus
            // 
            this.userIdStatus.AutoSize = true;
            this.userIdStatus.Location = new System.Drawing.Point(461, 59);
            this.userIdStatus.Name = "userIdStatus";
            this.userIdStatus.Size = new System.Drawing.Size(167, 15);
            this.userIdStatus.TabIndex = 9;
            this.userIdStatus.Text = "사용할 수 있는 아이디입니다.";
            // 
            // passwordStatus
            // 
            this.passwordStatus.AutoSize = true;
            this.passwordStatus.Location = new System.Drawing.Point(461, 107);
            this.passwordStatus.Name = "passwordStatus";
            this.passwordStatus.Size = new System.Drawing.Size(163, 15);
            this.passwordStatus.TabIndex = 10;
            this.passwordStatus.Text = "사용 가능한 비밀번호입니다.";
            // 
            // passwordConfirmStatus
            // 
            this.passwordConfirmStatus.AutoSize = true;
            this.passwordConfirmStatus.Location = new System.Drawing.Point(461, 150);
            this.passwordConfirmStatus.Name = "passwordConfirmStatus";
            this.passwordConfirmStatus.Size = new System.Drawing.Size(83, 15);
            this.passwordConfirmStatus.TabIndex = 11;
            this.passwordConfirmStatus.Text = "비밀번호 일치";
            // 
            // RegisterPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(661, 300);
            this.Controls.Add(this.passwordConfirmStatus);
            this.Controls.Add(this.passwordStatus);
            this.Controls.Add(this.userIdStatus);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.registerButton);
            this.Controls.Add(this.nameTextBox);
            this.Controls.Add(this.passwordConfirmTextBox);
            this.Controls.Add(this.passwordTextBox);
            this.Controls.Add(this.userIdTextBox);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Name = "RegisterPage";
            this.Text = "회 원 가 입";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox userIdTextBox;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.TextBox passwordConfirmTextBox;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.Button registerButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label userIdStatus;
        private System.Windows.Forms.Label passwordStatus;
        private System.Windows.Forms.Label passwordConfirmStatus;
    }
}