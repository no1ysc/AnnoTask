using AnnoTaskClient.UIController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;


namespace AnnoTaskClient.Logic
{
	public class MainLogic
	{
		private ClientWormHole clientWormHole = new ClientWormHole();
		private Dictionary<string, Frequency> termNFreq = new Dictionary<string, Frequency>(); // Term, Freq
		
		public MainLogic()
		{

		}

		public  void ConnectServer()
		{
			if (clientWormHole.Connect())
			{
				// 서버 연결 안내문.
				MessageBox.Show("서버 연결완료");
				UIHandler.Instance.CommonUI.ButtonEnable = true;
			}
			else
			{
				// 서버 연결 실패, 지연됨.
				MessageBox.Show("서버 연결실패");
			}
		}

		

		private void importDoc()
		{
			string startDate = UIHandler.Instance.CommonUI.StartDate + " 00:00:00";
			string endDate = UIHandler.Instance.CommonUI.EndDate + " 23:59:59";
			bool naver = UIHandler.Instance.CommonUI.isNaver;
			bool daum = UIHandler.Instance.CommonUI.isDaum;
			bool nate = UIHandler.Instance.CommonUI.isNate;
			DocByTerm[] result = clientWormHole.ImportDoc(startDate, endDate, naver, daum, nate);

			if (result == null)
			{
				MessageBox.Show("해당 기간에 문서가 존재하지 않습니다.");
				return;
			}

			// Doc -> Term
			int procCount = 0;
			int workingPercent = 40;
			foreach (DocByTerm docByTerm in result)
			{
				foreach (string term in docByTerm.Terms.Keys)
				{
					if (!termNFreq.ContainsKey(term))
					{
						termNFreq[term] = new Frequency(term, docByTerm.Ngram);
					}
					termNFreq[term].FreqInDocument++;
					termNFreq[term].TotalTermFreq += docByTerm.Terms[term];
					termNFreq[term].appendCategory(docByTerm.DocCategory, docByTerm.DocID, docByTerm.Title);
				}

				// 퍼센트 업.
				int per = (procCount++ / result.Length) * workingPercent;
				if (per >= 1)
				{
					UIHandler.Instance.CommonUI.ProgressBar += per;
				}
			}
			UIHandler.Instance.CommonUI.ProgressBar = 90;


			// 텀리스트 출력
			procCount = 0;
			workingPercent = 10;
			foreach (Frequency freqTerm in termNFreq.Values)
			{
				switch (freqTerm.NGram)
				{
					case 1:
						UIHandler.Instance.NGram1.AddTermList(freqTerm);
						break;
					case 2:
						UIHandler.Instance.NGram2.AddTermList(freqTerm);
						break;
					case 3:
						UIHandler.Instance.NGram3.AddTermList(freqTerm);
						break;
					case 4:
						UIHandler.Instance.NGram4.AddTermList(freqTerm);
						break;
					default:
						break;
				}

				// 퍼센트 업.
				int per = (procCount++ / termNFreq.Count) * workingPercent;
				if (per >= 1)
				{
					UIHandler.Instance.CommonUI.ProgressBar += per;
				}
			}

			UIHandler.Instance.CommonUI.ProgressBar = 100;
		}

		private void clear()
		{
			// TODO : 메모리 누수 가능성 점검해야함.
			termNFreq = new Dictionary<string, Frequency>();

			//foreach (Frequency freq in termNFreq.Values)
			//{

			//}

			UIHandler.Instance.CommonUI.TermCount = 0;
			UIHandler.Instance.CommonUI.DocCount = 0;
			UIHandler.Instance.CommonUI.ProgressBar = 0;
		}

		internal void clickedImportDoc()
		{
			clear();
			importDoc();

			UIHandler.Instance.CommonUI.ButtonEnable = true;
		}

		internal void cellContentDoubleClick(string p, int tabNumber)
		{
			// 트리뷰를 생성.
			switch (tabNumber)
			{
				case 1:
					UIHandler.Instance.NGram1.RefreshDocList(termNFreq[p]);
					break;
				case 2:
					UIHandler.Instance.NGram2.RefreshDocList(termNFreq[p]);
					break;
				case 3:
					UIHandler.Instance.NGram3.RefreshDocList(termNFreq[p]);
					break;
				case 4:
					UIHandler.Instance.NGram4.RefreshDocList(termNFreq[p]);
					break;
				default:
					break;
			}
		}

		internal string loadArticle(string term, string category, string title)
		{
			// find DOC ID
			int targetID = -1;
			foreach (int docID in termNFreq[term].Category[category].Keys)
			{
				if (termNFreq[term].Category[category][docID].Contains(title))
				{
					targetID = docID;
				}
			}

			return clientWormHole.getDocBodyFromID(targetID);
		}
	}
}
