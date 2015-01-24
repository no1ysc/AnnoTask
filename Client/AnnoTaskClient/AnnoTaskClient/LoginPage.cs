using AnnoTaskClient.Logic;
using AnnoTaskClient.UIController;
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
    public partial class LoginPage : Form
    {
        private MainLogic logic = UIHandler.Instance.logic;

        // 워터마크 설정
        protected string waterMarkTextUserName = "이메일";
        protected string waterMarkTextPassword = "비밀번호";
        private char passwordChar = '*';
        private char initChar = '\0';
        protected Color waterMarkColor = Color.Gray;
        protected Color fontColor = Color.Black;

        public LoginPage()
        {
            InitializeComponent();
            // 텍스트박스 초기 포커스 설정
            passwordTextBox.Select();

            userIDTextBox.ForeColor = waterMarkColor;
            userIDTextBox.Text = waterMarkTextUserName;

            passwordTextBox.ForeColor = waterMarkColor;
            passwordTextBox.Text = waterMarkTextPassword;

            this.userIDTextBox.Leave += new System.EventHandler(this.userNameTextBox_Leave);
            this.userIDTextBox.Enter += new System.EventHandler(this.userNameTextBox_Enter);

            this.passwordTextBox.Leave += new System.EventHandler(this.passwordTextBox_Leave);
            this.passwordTextBox.Enter += new System.EventHandler(this.passwordTextBox_Enter);

        }

        // 유저네임 텍스트박스에서 다른 곳으로 옮겨 갔을 경우
        private void userNameTextBox_Leave(object sender, EventArgs e)
        {
            if (userIDTextBox.Text == "")
            {
                userIDTextBox.Text = waterMarkTextUserName;
                userIDTextBox.ForeColor = waterMarkColor;
            }
        }

        // 유저네임 텍스트박스가 클릭 됐을 경우
        private void userNameTextBox_Enter(object sender, EventArgs e)
        {
            if (userIDTextBox.Text == waterMarkTextUserName)
            {
                userIDTextBox.Text = "";
                userIDTextBox.ForeColor = fontColor;
            }
        }

        // 패스워드 텍스트박스에서 다른 곳으로 옮겨 갔을 경우
        private void passwordTextBox_Enter(object sender, EventArgs e)
        {
            if (passwordTextBox.Text.Equals(waterMarkTextPassword))
            {
                passwordTextBox.Text = "";
                passwordTextBox.ForeColor = fontColor;
                passwordTextBox.PasswordChar = passwordChar;
            }   
        }

        // 패스워드 텍스트박스에서 다른 곳으로 옮겨 갔을 경우
        private void passwordTextBox_Leave(object sender, EventArgs e)
        {
            if (passwordTextBox.Text == "")
            {
                passwordTextBox.Text = waterMarkTextPassword;
                passwordTextBox.ForeColor = waterMarkColor;
                passwordTextBox.PasswordChar = initChar;               
            }
        }

        // 유저가 창을 끌 경우
        private void LoginPage_FormClosed(object sender, FormClosedEventArgs e)
        {
            UIHandler.Instance.CommonUI.setMainWindowEnableStatus(true);
            UIHandler.Instance.CommonUI.setButtonEnableState(false);
        }

        // 유저 계정 생성을 위한 회원가입 페이지 이동
        private void registerNewUserButton_Click(object sender, EventArgs e)
        {
            RegisterPage registerPage = new RegisterPage();
            registerPage.Owner = this;
            registerPage.Show();
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            string userID = this.userIDTextBox.Text.Trim();
            string password = this.passwordTextBox.Text.Trim();
            if (!this.userIDTextBox.Text.Trim().Equals("") && !this.passwordTextBox.Text.Trim().Equals(""))
            {
                logic.DoLogin(userID, password);
            }
        }
    }
}
