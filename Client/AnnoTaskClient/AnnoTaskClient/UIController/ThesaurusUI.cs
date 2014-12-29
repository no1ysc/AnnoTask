using AnnoTaskClient.Logic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AnnoTaskClient.UIController
{
    class ThesaurusUI
    {
        private AddThesaurusWindow addThesaurusWindow;
        

        public ThesaurusUI(AddThesaurusWindow addThesaurusWindow)
        {
            this.addThesaurusWindow = addThesaurusWindow;
        }


		#region ConceptTo, ConceptFrom, Meta  get, set
		// getter, setter, delegate 전반적 수정, 이승철, 20141220
		private delegate string ConceptFromGet();
		private string getConceptFrom()
		{
			return addThesaurusWindow.ConceptFromComboBox.SelectedText;
		}
		private delegate void ConceptFromSet(List<ConceptFrom> conceptFrom);
		private void setConceptFrom(List<ConceptFrom> conceptFrom)
		{
			if (conceptFrom != null)
			{
				addThesaurusWindow.ConceptFromComboBox.Items.AddRange(conceptFrom.ToArray());
			}
			else
			{
				// null 입력시 클린동작.
				addThesaurusWindow.ConceptFromComboBox.Items.Clear();
			}
		}

		private delegate string ConceptToGet();
		private string getConceptTo()
		{
			return addThesaurusWindow.ConceptToComboBox.Text;
		}
		private delegate void ConceptToSet(string conceptToTerm);
		private void setConceptTo(string conceptToTerm)
		{
			addThesaurusWindow.ConceptToComboBox.Text = conceptToTerm;
			// 이승철 20141223, 코드에서 입력한 택스트는 이벤트가 발생하면 아니됨.
			addThesaurusWindow.ConceptToChangedEventControl = true;
		}
		private delegate void ConceptToListSet(List<ConceptTo> conceptTo);
		private void setConceptToList(List<ConceptTo> conceptTo)
		{
			if (conceptTo != null)
			{
				addThesaurusWindow.ConceptToComboBox.Items.Clear();
				addThesaurusWindow.ConceptToComboBox.Items.AddRange(conceptTo.ToArray());
				//// 리스트 대신 오토컴플리션할 수 있게 붙여줌.
				//AutoCompleteStringCollection stringCollection = new AutoCompleteStringCollection();
				//foreach (ConceptTo concept in conceptTo)
				//{
				//	stringCollection.Add(concept.ConceptToTerm);
				//}
				////addThesaurusWindow.ConceptToComboBox.AutoCompleteCustomSource.Remove("");
				////addThesaurusWindow.ConceptToComboBox.AutoCompleteCustomSource = stringCollection;
				addThesaurusWindow.ConceptToComboBox.DroppedDown = true;
			}
			else
			{
				// null 입력시 클린동작.
				addThesaurusWindow.ConceptToComboBox.Items.Clear();
			}

			addThesaurusWindow.ConceptToChangedEventControl = true;
		}
		
		private delegate string MetaOntologyGet();
		private string getMetaOntology()
		{
			return addThesaurusWindow.MetaOntologyComboBox.SelectedText;
		}
				

		// 아래 get, set 설명
		// 일반, 선택된 아이템 또는 유져가 작성한 스트링 가져옴.
		// List들은 각 컨트롤에 아이템들을 통째로 추가하는 동작임.
        public string ConceptTo
        {
			get
			{
				ConceptToGet conceptToGet = new ConceptToGet(getConceptTo);
				return (string)addThesaurusWindow.Invoke(conceptToGet, new object[] { });
			}
			set
			{
				ConceptToSet conceptToSet = new ConceptToSet(setConceptTo);
				addThesaurusWindow.Invoke(conceptToSet, new object[] { value });
			}
			
        }
		private List<ConceptTo> conceptToList;
		public List<ConceptTo> ConceptToList
		{
			set 
			{
				conceptToList = value;
				ConceptToListSet conceptToSet = new ConceptToListSet(setConceptToList);
				addThesaurusWindow.Invoke(conceptToSet, new object[] { value });
			}
		}

		public string ConceptFrom
        {
			get
			{
				ConceptFromGet conceptFromGet = new ConceptFromGet(getConceptFrom);
				return (string)addThesaurusWindow.Invoke(conceptFromGet, new object[] { });
			}
        }
		public List<ConceptFrom> ConceptFromList
		{
			set
			{
				ConceptFromSet conceptFromSet = new ConceptFromSet(setConceptFrom);
				addThesaurusWindow.Invoke(conceptFromSet, new object[] { value });
			}
		}

		public string MetaOntology
		{
			get
			{
				MetaOntologyGet metaOntologyGet = new MetaOntologyGet(getMetaOntology);
				return (String)addThesaurusWindow.Invoke(metaOntologyGet, new object[] { });
			}
		}
		#endregion

		private delegate string LinkedListGet();
		private string getLinkedList()
		{
			return (addThesaurusWindow.linkedList.SelectedItem as LinkedListItem).LinkedTerm;
		}
		private delegate void LinkedListSet(List<LinkedListItem> item);
		private void setLinkedList(List<LinkedListItem> item)
		{
			if (item != null)
			{
				addThesaurusWindow.linkedList.Items.AddRange(item.ToArray());
			}
			else
			{
				// null 입력시 클린동작.
				addThesaurusWindow.linkedList.Items.Clear();
			}
		}

		public string LinkedListBox
		{
			get
			{
				LinkedListGet linkedListGet = new LinkedListGet(getLinkedList);
				return (string)addThesaurusWindow.Invoke(linkedListGet, new object[] { });
			}
		}
		public List<LinkedListItem> LinkedListBoxList
		{
			set
			{
				LinkedListSet linkedListSet = new LinkedListSet(setLinkedList);
				addThesaurusWindow.Invoke(linkedListSet, new object[] { value });
			}
		}
		
		/// <summary>
		/// 작성자 : 이승철,  20141223
		/// ConceptFrom 에서 입력된 문자를 지움.
		/// </summary>
		/// <param name="target"></param>
		public void RefreshAfterAddThesaurus()
		{
			RefreshAfterThesaurusWindow delegateFunction = new RefreshAfterThesaurusWindow(refreshAfterAddThesaurus);
			addThesaurusWindow.Invoke(delegateFunction, new object[] { });
		}
		private delegate void RefreshAfterThesaurusWindow();
		private void refreshAfterAddThesaurus()
		{
			// Concept from 처리다했던 상황, 사용자가 할일 없음.
			if (addThesaurusWindow.ConceptFromComboBox.Items.Count == 0)
			{
				return;
			}

			ConceptFrom refferenceItem = addThesaurusWindow.ConceptFromComboBox.SelectedItem as ConceptFrom;
	
			// Concept From 다음 아이템 선택,
			// 버그나면 여기확인 필요, 선택된 아이템을 삭제하면 곧바로 다음아이템이 선택된걸로 나올까?
			addThesaurusWindow.ConceptFromComboBox.Items.Remove(refferenceItem);

			// ConceptTo 클리어
			ConceptToList = null;
			addThesaurusWindow.ConceptToComboBox.Text = "";

			// MetaOntology 제자리로
			addThesaurusWindow.MetaOntologyComboBox.SelectedItem = " ";

			// LinkedList 클리어
			LinkedListBoxList = null;

			// 문서리스트 클리어
			addThesaurusWindow.treeView1.Nodes.Clear();

			// 기사전문 클리어
			addThesaurusWindow.richTextBox1.Text = " ";

			// 메인윈도우 탭에서 작업한 단어 삭제할 수 있도록.
			UIHandler.Instance.CommonUI.UpdateTerm(refferenceItem.ConceptFromTerm, addThesaurusWindow.SelectedTabofMainWindow);

			// 시소러스창 활성화
			addThesaurusWindow.Enabled = true;
		}

		private delegate int FindConceptToID(string term);
		public int findConceptToID(string term)
		{
			FindConceptToID delegateFunction = new FindConceptToID(_findConceptToID);
			return (int)addThesaurusWindow.Invoke(delegateFunction, new object[] { term });
		}
		private int _findConceptToID(string term)
		{
			int ret = -1;

			foreach(ConceptTo conceptTo in addThesaurusWindow.ConceptToComboBox.Items)
			{
				if (conceptTo.ConceptToTerm == term)
				{
					ret = Int32.Parse(conceptTo.ConceptToID);
					break;
				}
			}

			return ret;
		}

        private delegate void ThesaurusWindowConceptFromRefresh(List<String> termList);
        public void RefreshConceptFromCombo(List<String> termList)
        {
            ThesaurusWindowConceptFromRefresh conceptFromRefresh = new ThesaurusWindowConceptFromRefresh(refreshThesaurusWindowConceptFrom);
            addThesaurusWindow.Invoke(conceptFromRefresh, new object[] { termList });
        }
        private void refreshThesaurusWindowConceptFrom(List<String> termList)
        {

            if (termList.Count == 0)
            {
                addThesaurusWindow.ConceptFromComboBox.Items.Clear();
                return;
            }
            
            {
                String selectedTerm = termList[0];

                addThesaurusWindow.ConceptFromComboBox.Items.Clear();
                addThesaurusWindow.ConceptFromComboBox.Text = selectedTerm;

                for (int i = 0; i < termList.Count; ++i)
                {
                    addThesaurusWindow.ConceptFromComboBox.Items.Add(termList[i]);
                }
            }
        }

		//private delegate void ThesaurusWindowConceptToRefresh(List<String> termList);
		//public void RefreshConceptToCombo(List<String> termList)
		//{
		//	ThesaurusWindowConceptToRefresh conceptToRefresh = new ThesaurusWindowConceptToRefresh(refreshThesaurusWindowConceptTo);
		//	addThesaurusWindow.Invoke(conceptToRefresh, new object[] { termList });
		//}
		//private void refreshThesaurusWindowConceptTo(List<String> termList)
		//{
		//	addThesaurusWindow.ConceptToComboBox.Items.Clear();
		//	//addThesaurusWindow.ConceptToComboBox.Text = termList[0];

		//	for (int i = 0; i < termList.Count; ++i)
		//	{
		//		addThesaurusWindow.ConceptToComboBox.Items.Add(termList[i]);
		//	}

		//	//addThesaurusWindow.ConceptToComboBox.DroppedDown = true;
		//}

		/// <summary>
		/// 작성자 : 신효정
		/// // (기흥) 트리뷰 생성하는 동작 수정
		/// 수정자 : 이승철, 20141223
		/// 수정내용 : 재사용을 위해 공통부를 UIUtil로 이동.
		/// </summary>
		/// <param name="term"></param>
		/// <param name="docMeta"></param>
		private delegate void DocListRefresh(string term, Dictionary<string, Dictionary<int, string>> docMeta);
		public void RefreshDocList(string str, Dictionary<string, Dictionary<int, string>> docMeta)
		{
			//DocListRefresh docListRefresh = new DocListRefresh(refreshDocList);
			//addThesaurusWindow.Invoke(docListRefresh, new object[] { str, docMeta });
			UIHandler.Instance.UtilForUI.RefreshDocList(str, docMeta, addThesaurusWindow.treeView1);
		}
		private void refreshDocList(string str, Dictionary<string, Dictionary<int, string>> docMeta)
		{
			UIHandler.Instance.UtilForUI.RefreshDocList(str, docMeta, addThesaurusWindow.treeView1);
		}


        

		//private delegate void MetaRefresh(LinkedList[] LinkedList);
		//public void RefreshMeta(LinkedList[] LinkedList)
		//{
		//	MetaRefresh metaRefresh = new MetaRefresh(refreshMeta);
		//	addThesaurusWindow.Invoke(metaRefresh, new object[] { LinkedList });
		//}
		//private void refreshMeta(LinkedList[] LinkedList)
		//{
		//	addThesaurusWindow.MetaOntologyComboBox.Text = "";
		//	if (LinkedList.Count() != 0)
		//	{ 
		//		addThesaurusWindow.MetaOntologyComboBox.Text = LinkedList[0].metaInfos;
		//	}
		//	addThesaurusWindow.linkedList.Clear();

		//	foreach (LinkedList temp in LinkedList)
		//	{
		//		addThesaurusWindow.linkedList.Text += temp.linkedTerms+Environment.NewLine;
		//	}
		//}        
    }
}
