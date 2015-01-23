using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnnoTaskClient
{
    
    public partial class RegisterPage : Form
    {
        protected string waterMarkTextName = "실명 입력";
        protected string waterMarkTextUserName = "사용하실 이메일 주소 입력";
        protected string waterMarkTextPassword = "알파벳+숫자 조합으로 6~10자리 입력";
        protected string waterMarkTextConfirmPassword = "비밀번호 재입력";
        private char passwordChar = '*';
        private char initChar = '\0';
        protected Color waterMarkColor = Color.Gray;
        protected Color fontColor = Color.Black;

        public RegisterPage()
        {
            InitializeComponent();
            usernameTextBox.Select();

            nameTextBox.ForeColor = waterMarkColor;
            nameTextBox.Text = waterMarkTextName;

            usernameTextBox.ForeColor = waterMarkColor;
            usernameTextBox.Text = waterMarkTextUserName;

            passwordTextBox.ForeColor = waterMarkColor;
            passwordTextBox.Text = waterMarkTextPassword;

            confirmPasswordTextBox.ForeColor = waterMarkColor;
            confirmPasswordTextBox.Text = waterMarkTextConfirmPassword;

            this.nameTextBox.Leave += new System.EventHandler(this.nameTextBox_Leave);
            this.nameTextBox.Enter += new System.EventHandler(this.nameTextBox_Enter);

            this.usernameTextBox.Leave += new System.EventHandler(this.usernameTextBox_Leave);
            this.usernameTextBox.Enter += new System.EventHandler(this.usernameTextBox_Enter);

            this.passwordTextBox.Leave += new System.EventHandler(this.passwordTextBox_Leave);
            this.passwordTextBox.Enter += new System.EventHandler(this.passwordTextBox_Enter);

            this.confirmPasswordTextBox.Leave += new System.EventHandler(this.confirmPasswordTextBox_Leave);
            this.confirmPasswordTextBox.Enter += new System.EventHandler(this.confirmPasswordTextBox_Enter);
        }

        private void nameTextBox_Leave(object sender, EventArgs e)
        {
            if (nameTextBox.Text == "")
            {
                nameTextBox.Text = waterMarkTextName;
                nameTextBox.ForeColor = waterMarkColor;
            }
        }

        private void nameTextBox_Enter(object sender, EventArgs e)
        {
            if (nameTextBox.Text == waterMarkTextName)
            {
                nameTextBox.Text = "";
                nameTextBox.ForeColor = fontColor;
            }
        }

        private void usernameTextBox_Leave(object sender, EventArgs e)
        {
            if (usernameTextBox.Text == "")
            {
                usernameTextBox.Text = waterMarkTextUserName;
                usernameTextBox.ForeColor = waterMarkColor;
            }
        }

        private void usernameTextBox_Enter(object sender, EventArgs e)
        {
            if (usernameTextBox.Text == waterMarkTextUserName)
            {
                usernameTextBox.Text = "";
                usernameTextBox.ForeColor = fontColor;
            }
        }

        // 
        private void passwordTextBox_Enter(object sender, EventArgs e)
        {
            if (passwordTextBox.Text == waterMarkTextPassword)
            {
                passwordTextBox.Text = "";
                passwordTextBox.ForeColor = fontColor;
                passwordTextBox.PasswordChar = passwordChar;
            }
        }

        // 
        private void passwordTextBox_Leave(object sender, EventArgs e)
        {
            if (passwordTextBox.Text == "")
            {
                passwordTextBox.Text = waterMarkTextPassword;
                passwordTextBox.ForeColor = waterMarkColor;
                passwordTextBox.PasswordChar = initChar;
            }
        }

        // 
        private void confirmPasswordTextBox_Enter(object sender, EventArgs e)
        {
            if (confirmPasswordTextBox.Text == waterMarkTextConfirmPassword)
            {
                confirmPasswordTextBox.Text = "";
                confirmPasswordTextBox.ForeColor = fontColor;
                confirmPasswordTextBox.PasswordChar = passwordChar;
            }
        }

        // 
        private void confirmPasswordTextBox_Leave(object sender, EventArgs e)
        {
            if (confirmPasswordTextBox.Text == "")
            {
                confirmPasswordTextBox.Text = waterMarkTextConfirmPassword;
                confirmPasswordTextBox.ForeColor = waterMarkColor;
                confirmPasswordTextBox.PasswordChar = initChar;
            }
        }

        // (기흥) 계정 등록 버튼 클릭
        private void registerButton_Click(object sender, EventArgs e)
        {

        }
    }
}
