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
        private static AddThesaurusWindow addThesaurusWindow;
        

        public ThesaurusUI(AddThesaurusWindow addThesaurusWindow)
        {
            //this.addThesaurusWindow = addThesaurusWindow;
            ThesaurusUI.addThesaurusWindow = addThesaurusWindow;
        }


		#region ConceptTo, ConceptFrom, Meta  get, set
		// getter, setter, delegate 전반적 수정, 이승철, 20141220
		private delegate String ConceptFromGet();
		private String getConceptFrom()
		{
			return addThesaurusWindow.ConceptFromComboBox.Text;
		}

		private delegate String MetaOntologyGet();
		private String getMetaOntology()
		{
			return addThesaurusWindow.MetaOntologyComboBox.Text;
		}

		private delegate String ConceptToGet();
		private String getConceptTo()
		{
			return addThesaurusWindow.ConceptToComboBox.Text;
		}

        public string ConceptTo
        {
			get
			{
				ConceptToGet conceptToGet = new ConceptToGet(getConceptTo);
				return (string)addThesaurusWindow.Invoke(conceptToGet, new object[] { });
			}
			set { addThesaurusWindow.ConceptToComboBox.Text = value; }
        }
		public string ConceptFrom
        {
			get
			{
				ConceptFromGet conceptFromGet = new ConceptFromGet(getConceptFrom);
				return (string)addThesaurusWindow.Invoke(conceptFromGet, new object[] { });
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

		//public String conceptTo
		//{
		//	get { return (String)addThesaurusWindow.ConceptToComboBox.SelectedItem; }
		//	set { addThesaurusWindow.ConceptToComboBox.Text = value; }
		//}

		#endregion
        

        

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

        private delegate void ThesaurusWindowConceptToRefresh(List<String> termList);
        public void RefreshConceptToCombo(List<String> termList)
        {
            ThesaurusWindowConceptToRefresh conceptToRefresh = new ThesaurusWindowConceptToRefresh(refreshThesaurusWindowConceptTo);
            addThesaurusWindow.Invoke(conceptToRefresh, new object[] { termList });
        }
        private void refreshThesaurusWindowConceptTo(List<String> termList)
        {
            addThesaurusWindow.ConceptToComboBox.Items.Clear();
            //addThesaurusWindow.ConceptToComboBox.Text = termList[0];

            for (int i = 0; i < termList.Count; ++i)
            {
                addThesaurusWindow.ConceptToComboBox.Items.Add(termList[i]);
            }

            //addThesaurusWindow.ConceptToComboBox.DroppedDown = true;
        }


        private delegate void DocListRefresh(Frequency freq);
        public void RefreshDocList(Frequency term)
        {
            DocListRefresh docListRefresh = new DocListRefresh(refreshDocList);
            addThesaurusWindow.Invoke(docListRefresh, new object[] { term });
        }
        private void refreshDocList(Frequency term)
        {
            addThesaurusWindow.treeView1.Nodes.Clear();

            TreeNode root = new TreeNode(term.Term);

            root.ExpandAll();

            foreach (string category in term.Category.Keys)
            {
                string content = category + "(" + term.Category[category].Count.ToString() + ")";

                TreeNode child = root.Nodes.Add(content);
                foreach (int docID in term.Category[category].Keys)
                {
                    child.Nodes.Add(term.Category[category][docID]);
                }
            }
            addThesaurusWindow.treeView1.Nodes.Add(root);
        }

        private delegate void MetaRefresh(LinkedList[] LinkedList);
        public void RefreshMeta(LinkedList[] LinkedList)
        {
            MetaRefresh metaRefresh = new MetaRefresh(refreshMeta);
            addThesaurusWindow.Invoke(metaRefresh, new object[] { LinkedList });
        }
        private void refreshMeta(LinkedList[] LinkedList)
        {
            addThesaurusWindow.MetaOntologyComboBox.Text = "";
            if (LinkedList.Count() != 0)
            { 
                addThesaurusWindow.MetaOntologyComboBox.Text = LinkedList[0].metaInfos;
            }
            addThesaurusWindow.linkedList.Clear();

            foreach (LinkedList temp in LinkedList)
            {
                addThesaurusWindow.linkedList.Text += temp.linkedTerms+Environment.NewLine;
            }
        }        
    }
}
