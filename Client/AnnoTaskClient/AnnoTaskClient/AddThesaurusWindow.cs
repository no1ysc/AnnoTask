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

        private void addThesaurusButton_Click_1(object sender, EventArgs e)
        {
            logic.clickedAddThesaurus(this.ConceptFromComboBox.Text, this.ConceptToComboBox.Text, this.MetaOntologyComboBox.Text);
            if (ConceptFromComboBox.Items.Count == 0)
            {
                ConceptFromComboBox.Text = "";
            }
        }

        internal bool SupressSelectIndexChanged { get; set; }
        private void showSelectedTerm(String text)
        {
            this.SupressSelectIndexChanged = true;
            ConceptFromComboBox.Text = text;
            this.SupressSelectIndexChanged = false;
        }

        private void ConceptFromComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.SupressSelectIndexChanged) { return; }
            ComboBox comb = (ComboBox)sender;
            string text = comb.Text;
            logic.treeViewForThesaurusWindow(text);
            showSelectedTerm(text);
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

        private void ConceptToComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comb = (ComboBox)sender;
            string text = comb.Text;

            logic.getLinkedList(text);
        }

        private void AddThesaurusWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            logic.emptyTermList();
			//UIHandler.Instance.CommonUI.setAddThesaurusWindowButtonEnable();
			UIHandler.Instance.CommonUI.setMainWindowEnableStatus(true);
        }

        private void ConceptToComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            ComboBox comb = (ComboBox)sender;
            string text = comb.Text;

            if(e.KeyCode == Keys.Enter)
            {
                logic.getLinkedList(text);
            }            
        }

    }
}
