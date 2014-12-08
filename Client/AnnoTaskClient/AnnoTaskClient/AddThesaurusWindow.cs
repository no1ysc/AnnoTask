using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using AnnoTaskClient.UIController;
using AnnoTaskClient.Logic;

namespace AnnoTaskClient
{
    public partial class AddThesaurusWindow : Form
    {
        MainLogic logic = new MainLogic();

        public AddThesaurusWindow()
        {
            InitializeComponent();
            UIHandler.Instance.runUIHandler_Thesaurus(this);
        }

        private void addThesaurusButton_Click(object sender, EventArgs e)
        {
            logic.clickedAddThesaurus();
        }
    }
}
