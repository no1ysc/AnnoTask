using AnnoTaskClient.Logic;
using AnnoTaskClient.UIController;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnnoTaskClient
{
    
    public partial class RegisterPage : Form
    {
        private MainLogic logic = UIHandler.Instance.logic;
        
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

        // 유저가 창을 끌 경우
        private void RegisterPage_FormClosed(object sender, FormClosedEventArgs e)
        {
            UIHandler.Instance.CommonUI.setLoginPageEnableStatus(true);
        }




        // (기흥) 계정 등록 버튼 클릭
        private void registerButton_Click(object sender, EventArgs e)
        {
            this.Enabled = false;

            if (this.userNameTextBox.Text.Trim().Equals("") ||
                this.userIdTextBox.Text.Trim().Equals("") ||
                this.passwordTextBox.Text.Trim().Equals("") ||
                this.passwordConfirmTextBox.Text.Trim().Equals(""))
            {
                MessageBox.Show("항목들을 모두 채워주셔야 계정 등록이 가능합니다.");
                this.Enabled = true;
                return;
            }
            else if (!isValidUserName(this.userNameTextBox.Text.Trim()))
            {
                MessageBox.Show("2~5글자의 한글 이름을 입력해 주시길 바랍니다.");
                this.Enabled = true;
                clearInputs("userName");
                return;
            }
            else if (!isValidUserID(this.userIdTextBox.Text.Trim()))
            {
                MessageBox.Show("올바른 이메일 주소를 입력해 주시길 바랍니다.");
                this.Enabled = true;
                clearInputs("userID");
                return;
            }
            else if(!isValidPassword(this.passwordTextBox.Text.Trim())){
                MessageBox.Show("비밀번호에는 반드시 1개 이상의 영문과 숫자를 포함시켜 주셔야 합니다.");
                this.Enabled = true;
                clearInputs("password");
                return;
            }
            else if (!isValidPasswordConfirm(this.passwordConfirmTextBox.Text.Trim()))
            {
                MessageBox.Show("비밀번호 확인이 비밀번호와 일치하지 않습니다.");
                this.Enabled = true;
                clearInputs("pass_confirm");
                return;
            }

            string userName, userID, password;
            userName = this.userNameTextBox.Text.Trim();
            userID = this.userIdTextBox.Text.Trim();
            password = this.passwordTextBox.Text.Trim();
                
            logic.registerNewUser(userName, userID, password);
        }

        private void clearInputs(string whichTextBox)
        {
            switch (whichTextBox)
            {
                case "userName":
                    this.userName.Text = string.Empty;
                    break;
                case "userID":
                    this.userIdTextBox.Text = string.Empty;
                    break;
                case "password":
                    this.passwordTextBox.Text = string.Empty;
                    this.passwordConfirmTextBox.Text = string.Empty;
                    break;
                case "pass_confirm":
                    this.passwordConfirmTextBox.Text = string.Empty;
                    break;
                default:
                    break;
            }
        }

        private bool isValidPasswordConfirm(string pass)
        {
            if (pass == null)
            {
                return false;
            }

            if (!this.passwordTextBox.Text.Trim().Equals(this.passwordConfirmTextBox.Text.Trim()))
            {
                return false;
            }

            return true;
        }

        private bool isValidPassword(string pass)
        {
            if (pass == null)
            {
                return false;
            }

            if (!Regex.IsMatch(pass, "(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{6,20})$"))
            {
                return false;
            }

            return true;
        }

        private bool isValidUserID(string userID)
        {
            if (userID == null)
            {
                return false;
            }

            // 이메일 주소 체크
            if (!Regex.IsMatch(userID, "[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?"))
            {
                return false;
            }

            return true;
        }

        private bool isValidUserName(string userName)
        {
            if (userName == null)
            {
                return false;
            }

            if (!Regex.IsMatch(userName, "[가-힣]{2,5}"))
            {
                return false;
            }

            return true;
        }

    }
}
