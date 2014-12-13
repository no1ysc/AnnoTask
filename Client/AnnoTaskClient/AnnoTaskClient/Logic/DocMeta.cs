using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnnoTaskClient.Logic
{
    public class DocMeta
    {
        public DocMeta(List<string> category, List<int> doc_id, List<string> title)
		{
			this.category = category;
            this.docIDList = doc_id;
            this.title = title;
		}
        
        private List<string> category;
        public List<string> Category
		{
			get { return category; }
			set { category = value; }
		}

        private List<int> docIDList;
        public List<int> DocIDList
		{
			get { return docIDList; }
			set { docIDList = value; }
		}
        
        private List<string> title;
        public List<string> Title
		{
			get { return title; }
			set { title = value; }
		}
    }
}
