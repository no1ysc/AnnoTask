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
        public LoginPage()
        {
            InitializeComponent();
        }

        // 유저가 창을 끌 경우
        private void LoginPage_FormClosed(object sender, FormClosedEventArgs e)
        {
            UIHandler.Instance.CommonUI.setMainWindowEnableStatus(true);
        }

        // 유저 계정 생성을 위한 회원가입 페이지 이동
        private void registerNewUserButton_Click(object sender, EventArgs e)
        {
            RegisterPage registerPage = new RegisterPage();
            registerPage.Show();
            //UIHandler.Instance.CommonUI.setLoginPageEnableStatus(false);
        }

        private void loginButton_Click(object sender, EventArgs e)
        {

        }
    }
}
