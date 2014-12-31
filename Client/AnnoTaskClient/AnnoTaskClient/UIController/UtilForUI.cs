using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnnoTaskClient.UIController
{
	/// <summary>
	/// @author JS
	/// UI 에서 사용하는 공통함수들
	/// 메인 윈도우, 시소러스 추가윈도우에서 공통으로 사용할 부분들 정의
	/// </summary>
	class UtilForUI
	{
		/// <summary>
		/// 문서에서 선택된 텀을 하이라이트하여 리치박스에 바로 적용하는 함수.
		/// 최초 작성자 : 신효정
		/// 리펙토링 : 이승철
		/// </summary>
		/// <param name="article"></param>
		/// <param name="term"></param>
		/// <param name="articleView"></param>
		private void getColoredArticle(String article, string term, RichTextBox articleView)
		{
			articleView.Clear();

			int Ncnt = 1;
			int line = 0;
			bool lineF = false;
			for (int i = 0; i < term.Length; ++i)
			{
				char temp = term[i];
				if (temp.Equals(' '))
				{
					Ncnt++;
				}
			}

			String[] lineTokenList = article.Split('\n');
			string word = "";
			string word1 = "";
			string word2 = "";
			ArrayList list = new ArrayList();
			foreach (string lineToken in lineTokenList)
			{
				String[] wordtokenList = lineToken.Split(' ');

				foreach (string wordToken in wordtokenList)
				{
					if (word.Contains(term))
					{
						if (!lineF)
						{
							String[] lineList = word.Split('\n');
							line = lineList.Count();
							lineF = true;
						}

						String[] result = word.Split(' ');

						for (int i = 0; i < (result.Count() - Ncnt); ++i)
						{
							word1 += result[i] + " ";
						}

						for (int j = (result.Count() - Ncnt); j < result.Count(); ++j)
						{
							word2 += result[j] + " ";
						}

						articleView.SelectionColor = System.Drawing.Color.Black;
						articleView.SelectionBackColor = System.Drawing.Color.White;
						articleView.SelectedText = word1;

						articleView.SelectionColor = System.Drawing.Color.Black;
						articleView.SelectionBackColor = System.Drawing.Color.Yellow;
						articleView.SelectedText = word2;

						word = "";
						word1 = "";
						word2 = "";
					}
					word += " " + wordToken;
				}
				word += "\n";
			}

			if (word.Contains(term))
			{
				if (!lineF)
				{
					String[] lineList = word.Split('\n');
					line = lineList.Count();
					lineF = true;
				}
				word1 = "";
				word2 = "";

				String[] result = word.Split(' ');
				for (int i = 0; i < (result.Count() - Ncnt); ++i)
				{
					word1 += result[i] + " ";
				}

				for (int j = (result.Count() - Ncnt); j < result.Count(); ++j)
				{
					word2 += result[j] + " ";
				}

				articleView.SelectionColor = System.Drawing.Color.Black;
				articleView.SelectionBackColor = System.Drawing.Color.White;
				articleView.SelectedText = word1;

				articleView.SelectionColor = System.Drawing.Color.Black;
				articleView.SelectionBackColor = System.Drawing.Color.Yellow;
				articleView.SelectedText = word2;

				articleView.SelectionStart = line;
				articleView.ScrollToCaret();

				return;
			}

			articleView.SelectionColor = System.Drawing.Color.Black;
			articleView.SelectionBackColor = System.Drawing.Color.White;
			articleView.SelectedText = word;

			articleView.SelectionStart = line;
			articleView.ScrollToCaret();
		}

		/// <summary>
		/// 작성자 : 이승철, 20141222
		/// 문서리스트에서 선택된 노드와 표시할 리치박스를 입력하면 선택된노드에 해당되는 단어를 리치박스에서 하이라이트 해줌,
		/// 1차수정: 아티클 로드함수 변경으로 인한 변수 및 로드방법 수정. 정의된 선택된 노드 타입 사용.
		/// </summary>
		/// <param name="selectedNode"></param>
		/// <param name="articleView"></param>
		internal void AfterDocListClickHandler(TreeNode selectedNode, RichTextBox articleView)
		{
			if (selectedNode.Level != 2)
			{
				return;
			}

			//string title = selectedNode.Text;
			//string category = selectedNode.Parent.Text.Substring(0, selectedNode.Parent.Text.IndexOf('('));
			string term = selectedNode.Parent.Parent.Text;

			string article = UIHandler.Instance.logic.loadArticle((selectedNode as LastDocListNode).DocID);

			if (article != null)
			{
				getColoredArticle(article, term, articleView);
			}
		}

		/// <summary>
		/// 트리뷰 인스턴스에 입력한 파라메터들 반영.
		/// </summary>
		/// <param name="str"></param>
		/// <param name="docMeta"></param>
		/// <param name="treeView"></param>
		internal void RefreshDocList(string str, Dictionary<string, Dictionary<int, string>> docMeta, TreeView treeView)
		{
			treeView.Nodes.Clear();

			TreeNode root = new TreeNode(str);
			TreeNode child = new TreeNode();
			root.ExpandAll();

			foreach (string category in docMeta.Keys)
			{
				string content = category + "(" + docMeta[category].Count + ")";

				child = root.Nodes.Add(content);
				foreach (int doc_id in docMeta[category].Keys)
				{
					LastDocListNode lastNode = new LastDocListNode(docMeta[category][doc_id]);
					lastNode.DocID = doc_id;
					child.Nodes.Add(lastNode);
				}
			}

			treeView.Nodes.Add(root);
		}
	
	}
}
