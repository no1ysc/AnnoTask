using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnnoTaskClient.UIController
{
    class ThesaurusUI
    {
        private AddThesaurusWindow addThesaurusWindow;

        public ThesaurusUI(AddThesaurusWindow addThesaurusWindow)
		{
            this.addThesaurusWindow = addThesaurusWindow;
		}

        public String conceptFrom
        {
            get { return (String)addThesaurusWindow.comboBox1.SelectedValue; }
        }

        public String conceptTo
        {
            get { return (String)addThesaurusWindow.comboBox2.SelectedValue; }
        }

        public String metaOntology
        {
            get { return (String)addThesaurusWindow.comboBox3.SelectedValue; }
        }

    }
}
