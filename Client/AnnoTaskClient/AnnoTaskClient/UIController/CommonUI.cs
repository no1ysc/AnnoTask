using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnnoTaskClient.UIController
{
	class CommonUI
	{
		private MainWindow mainWindow;

		public CommonUI(MainWindow mainWindow)
		{
			this.mainWindow = mainWindow;
		}


		// Delegates
		private delegate void SetTermCountDelegate(int value);
		private delegate void SetDocCountDelegate(int value);
		private delegate void SetProgressValueDelegate(int value);
		private delegate void SetButtonEnable(bool bState);

		private void setTermCount(int value)
		{
			mainWindow.termCount.Text = value.ToString();
		}

		private void setDocCount(int value)
		{
			mainWindow.docCount.Text = value.ToString();
		}

		private void setProgressBar(int value)
		{
			mainWindow.progressBar.Value = value;
			mainWindow.progressLabel.Text = value.ToString() + "%";
		}

		private void setButtonEnableState(bool bState)
		{
			if (bState)
			{
				mainWindow.AllButtonEnable();
			}
			else
			{
				mainWindow.AllButtonDisable();
			}
		}



		
		/// <summary>
		/// @Author : JS
		/// 메인윈도우 활성화/비활성화
		/// </summary>
		/// <param name="status"></param>
		public void setMainWindowEnableStatus(bool status)
		{
			mainWindow.Enabled = status;
		}

		public int TermCount
		{
			set
			{
				SetTermCountDelegate termCount = new SetTermCountDelegate(setTermCount);
				mainWindow.Invoke(termCount, new object[] { value });
			}
			get
			{
				return Int32.Parse(mainWindow.termCount.Text);
			}
		}
		public int DocCount
		{
			set
			{
				SetDocCountDelegate docCount = new SetDocCountDelegate(setDocCount);
				mainWindow.Invoke(docCount, new object[] { value });
			}
		}
		private int progressBar = 0;
		public int ProgressBar
		{
			set
			{
				progressBar = value;
				SetProgressValueDelegate progress = new SetProgressValueDelegate(setProgressBar);
				mainWindow.Invoke(progress, new object[] { progressBar });
			}
			get { return progressBar; }
		}
		public bool AllButtonEnabledInMainWindow
		{
			set
			{
				SetButtonEnable sbt = new SetButtonEnable(setButtonEnableState);
				mainWindow.Invoke(sbt, new object[] { value });
			}
		}
		public string StartDate
		{
			get { return mainWindow.startDate.Value.Date.ToString("yyyy-MM-dd"); }
		}
		public string EndDate
		{
			//get{return mainWindow.endDate.Value.ToString();}
			get { return mainWindow.endDate.Value.Date.ToString("yyyy-MM-dd"); }
		}
		public bool isNaver
		{
			get{return mainWindow.bNaver.Checked;}
		}
		public bool isDaum
		{
			get{return mainWindow.bDaum.Checked;}
		}
		public bool isNate
		{
			get{return mainWindow.bNate.Checked;}
		}


		/// <summary>
		/// 작성자 : ???
		/// 수정자 : 이승철
		/// CommonUI로 옴김.
		/// // 이승철, 의견 : Term 관리하는데에서는 안지우나?
		/// </summary>
		/// <param name="updateList"></param>
		/// <param name="tabNumber"></param>
		internal void UpdateTermList(List<string> updateList, int tabNumber)
		{
			switch (tabNumber)
			{
				case 1:
					UIHandler.Instance.NGram1.RefreshTermList(updateList);
					break;
				case 2:
					UIHandler.Instance.NGram2.RefreshTermList(updateList);
					break;
				case 3:
					UIHandler.Instance.NGram3.RefreshTermList(updateList);
					break;
				case 4:
					UIHandler.Instance.NGram4.RefreshTermList(updateList);
					break;
			}
		}
		internal void UpdateTerm(string term, int tabNumber)
		{
			List<string> updateList = new List<string>();
			updateList.Add(term);
			UpdateTermList(updateList, tabNumber);
		}
	}
}
