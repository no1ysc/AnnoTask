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
        private MainLogic logic = UIHandler.Instance.logic;
        public MainLogic getMainLogic()
        {
            return this.logic;
        }

        public AddThesaurusWindow()
        {
            InitializeComponent();
            UIHandler.Instance.runUIHandler_Thesaurus(this);
        }

        private void addThesaurusButton_Click(object sender, EventArgs e)
        {
            logic.clickedAddThesaurus();
        }

        private void ConceptFromComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comb = (ComboBox)sender;
            string text = comb.Text;
            logic.thesaurusWindowTermChanged(text);
            logic.treeViewForThesaurusWindow(text);
        }

        private void ConceptToComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int a = 0;
            }
        }

        private void treeView1_DoubleClick(object sender, EventArgs e)
        {
            string article = getArticle(sender);
            if (article != null)
            {
                logic.getColoredArticle(article, sender, this.richTextBox1);
            }
        }

        private string getArticle(object sender)
        {
            TreeNode node = (sender as TreeView).SelectedNode;

            if (node.Level != 2)
            {
                return null;
            }

            string title = node.Text;
            string category = node.Parent.Text.Substring(0, node.Parent.Text.IndexOf('('));
            string term = node.Parent.Parent.Text;

            return logic.loadArticle(term, category, title);
        }

    }
}
