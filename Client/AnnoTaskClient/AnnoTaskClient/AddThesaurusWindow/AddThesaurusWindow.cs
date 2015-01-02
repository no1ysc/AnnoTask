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
		private int selectedTabofMainWindow;

        public AddThesaurusWindow(List<string> selectedTerms, int selectedTabofMainWindow)
        {
            InitializeComponent();
			// 컨트롤 속성 사용자 정의.
			this.Controls.Add(ConceptToTextBox);
			this.Controls.Add(ConceptToComboBox);

            UIHandler.Instance.runUIHandler_Thesaurus(this);
			
			//이승철 추가 20141222.
			// ConceptFrom Setting. 선택된 탭 알아오기.
			foreach (string term in selectedTerms)
			{
				this.ConceptFromComboBox.Items.Add(new ConceptFrom(term));
			}
			// 첫번째 아이템 선택. (아이템 가져오는 방법을,,,모르겠음)
			foreach (ConceptFrom item in this.ConceptFromComboBox.Items)
			{
				this.ConceptFromComboBox.SelectedItem = item;
				break;
			}

			this.selectedTabofMainWindow = selectedTabofMainWindow;
        }

		private void AddThesaurusWindow_FormClosed(object sender, FormClosedEventArgs e)
		{
			//logic.emptyTermList();
			//UIHandler.Instance.CommonUI.setAddThesaurusWindowButtonEnable();
			UIHandler.Instance.CommonUI.setMainWindowEnableStatus(true);
		}

		/// <summary>
		/// 작성자 : 이승철, 20141223
		/// 메인 윈도우에서 어떤 탭을 누르고 이 창으로 건너왔는지.
		/// </summary>
		public int SelectedTabofMainWindow
		{
			get { return selectedTabofMainWindow; }
		}

		/// <summary>
		/// 작성자 : ????
		/// 수정자 : 이승철, 20141223
		/// 수정내용 : 시소러스 추가에 필요한 정보를 모두 적었는지 확인, 클릭 후 다른 입력이 없도록 창 비활성화 - 활성화는 추가로직이 끝나면.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void addThesaurusButton_Click_1(object sender, EventArgs e)
        {
			// 버튼 막이 먼저
			this.Enabled = false;

			if (this.ConceptFromComboBox.Text.Trim().Equals("") ||
				this.ConceptToTextBox.Text.Trim().Equals("") ||
				this.MetaOntologyComboBox.Text.Trim().Equals(""))
			{
				MessageBox.Show("시소러스 추가에 필요한 모든 필드를 적어주셔야 합니다.");
				this.Enabled = true;
				return;
			}
			logic.clickedAddThesaurus(this.ConceptFromComboBox.Text, this.ConceptToTextBox.Text, this.MetaOntologyComboBox.Text);
        }


        private void ConceptFromComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
			//if (this.SupressSelectIndexChanged) { return; }
			//ComboBox comb = (ComboBox)sender;
			//string text = comb.Text;
            logic.treeViewForThesaurusWindow(this.ConceptFromComboBox.SelectedItem.ToString());
			//showSelectedTerm(text);
        }

        private void treeView1_DoubleClick(object sender, EventArgs e)
        {
			UIHandler.Instance.UtilForUI.AfterDocListClickHandler((sender as TreeView).SelectedNode, this.richTextBox1);
        }

        private void ConceptToComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
			//ComboBox comb = (ComboBox)sender;
			//string text = comb.Text;

			//logic.getLinkedList(text);
        }

        private void ConceptToComboBox_KeyDown(object sender, KeyEventArgs e)
        {
			//ComboBox comb = (ComboBox)sender;
			//string text = comb.Text;

			//if(e.KeyCode == Keys.Enter)
			//{
			//	logic.getLinkedList(text);
			//}            
        }

		/// <summary>
		/// 작성자 : 이승철, 20141223
		/// 매순간 타자를 입력할때마다, 컨셉투 리스트를 가져올것임. 갯수 및 쿼리정책은 서버에 따름.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ConceptToComboBox_TextChanged(object sender, EventArgs e)
		{
			//ComboBox comb = (ComboBox)sender;
			//comb.EndUpdate();

			//if (!ConceptToChangedEventControl)
			//{
			//	return;
			//}

			//string text = comb.Text;
			//LastInputConceptTo = text;
			//ConceptToChangedEventControl = false; // 이벤트 막이.
			////comb.DroppedDown = true;	// 펼치기.
			//logic.getConceptToList(text);
			//comb.EndUpdate();
		}
		public string LastInputConceptTo;	//위함수에서만 쓰는 변수임, 마지막 입력을 기준으로 이벤트 제어할것임.
		public bool ConceptToChangedEventControl = true;

		/// <summary>
		/// 작성자 : 이승철, 20141223
		/// 기능구현 하고 싶은데,,,
		/// 안필요함. 20141229
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void linkedList_Click(object sender, EventArgs e)
		{
			//ListBox listBox = sender as ListBox;
			//string term = (listBox.SelectedItem as LinkedListItem).LinkedTerm;
			//logic.DocListUpdate(term, 0);
		}

		private void ConceptToTextBox_TextChanged(object sender, EventArgs e)
		{
			string text = (sender as TextBox).Text;

			if (!ConceptToChangedEventControl)
			{
				return;
			}
			if (text.Equals(""))
			{
				return;
			}

			ConceptToChangedEventControl = false; // 이벤트 막이.
			logic.getConceptToList(text);
		}

		private void ConceptToTextBox_KeyUp(object sender, KeyEventArgs e)
		{

			switch (e.KeyCode)
			{
				case Keys.Enter:
					if (ConceptToComboBox.Items.Count == 0)
					{
						return;
					}
					_selectConceptTo();
					break;
				case Keys.Up:
					if (this.ConceptToComboBox.SelectedIndex <= 0)
					{
						break;
					}
					this.ConceptToComboBox.SelectedIndex--;
					ConceptToChangedEventControl = false;
					this.ConceptToTextBox.Text = this.ConceptToComboBox.SelectedItem.ToString();
					ConceptToChangedEventControl = true;
					break;
				case Keys.Down:
					if (this.ConceptToComboBox.SelectedIndex >= this.ConceptToComboBox.Items.Count - 1)
					{
						break;
					}
					this.ConceptToComboBox.SelectedIndex++;
					ConceptToChangedEventControl = false;
					this.ConceptToTextBox.Text = this.ConceptToComboBox.SelectedItem.ToString();
					ConceptToChangedEventControl = true;
					break;
				default:
					break;
			}
		}

		private void ConceptToComboBox_MouseClick(object sender, MouseEventArgs e)
		{
			if (ConceptToComboBox.Items.Count == 0)
			{
				return;
			}
			_selectConceptTo();
		}
		
		/// <summary>
		/// ConceptTo를 선택했을때 처리루틴.
		/// </summary>
		private void _selectConceptTo()
		{
			ConceptToChangedEventControl = false;
			string selectedTerm = this.ConceptToComboBox.SelectedItem.ToString();
			this.ConceptToTextBox.Text = selectedTerm;		// 선택된 텀 적어줌.
			__changeMetaOntology(this.ConceptToComboBox.SelectedItem as ConceptTo);
			ConceptToComboBox.DroppedDown = false;
			ConceptToChangedEventControl = true;
			logic.getLinkedList(selectedTerm);
		}

		private void __changeMetaOntology(ConceptTo conceptTo)
		{
			int metaIndexInCombobox = this.MetaOntologyComboBox.Items.IndexOf(conceptTo.MetaOntology);
			this.MetaOntologyComboBox.SelectedIndex = metaIndexInCombobox;
		}



    }
}
