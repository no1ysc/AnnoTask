using AnnoTaskClient.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnnoTaskClient.UIController
{
	class UIHandler
	{
		private static UIHandler thisClass = new UIHandler();
        public static UIHandler Instance
		{
			get { return thisClass; }
		}

		private MainWindow mainWindow;
        public void runUIHandler(MainWindow value)
		{
			mainWindow = value;

			commonUI = new CommonUI(mainWindow);
			ngram1 = new NGramUI1(mainWindow);
			ngram2 = new NGramUI2(mainWindow);
			ngram3 = new NGramUI3(mainWindow);
			ngram4 = new NGramUI4(mainWindow);

			utilForUI = new UtilForUI();
		}

		private AddThesaurusWindow addThesaurusWindow;
		public void runUIHandler_Thesaurus(AddThesaurusWindow value)
		{
			addThesaurusWindow = value;

			thesaurusUI = new ThesaurusUI(addThesaurusWindow);
		}

		private CommonUI commonUI;
		private NGramUI1 ngram1;
		private NGramUI2 ngram2;
		private NGramUI3 ngram3;
		private NGramUI4 ngram4;
		private ThesaurusUI thesaurusUI;
		private UtilForUI utilForUI;

		public CommonUI CommonUI { get { return commonUI; } }
		public NGramUI1 NGram1 { get{return ngram1;}	}
		public NGramUI2 NGram2 { get{return ngram2;}	}
		public NGramUI3 NGram3 { get{return ngram3;}	}
		public NGramUI4 NGram4 { get{return ngram4;} }
		public ThesaurusUI ThesaurusUI { get { return thesaurusUI;  } }
		public UtilForUI UtilForUI { get { return utilForUI; } }

        public MainLogic logic = new MainLogic();

	}
}
