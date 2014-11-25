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
			mainWindow.btnImportDoc.Enabled = bState;
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
		public bool ButtonEnable
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
	}
}
