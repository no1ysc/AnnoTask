using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnnoTaskClient.Logic
{
	class DocByTerm
	{
		private int docID;
		private string docCategory;
		private int ngram;
		private string title;
		private Dictionary<string, int> terms;	// 추후 Map으로 다시 변환.<Term, Freq>

		//// 임시코드임..................
		//public DocByTerm()
		//{
		//	terms = new Dictionary<string, int>();
		//}
		//// 지울꺼임, 생성할 필요가 없음..........

		public int DocID
		{
			get { return docID; }
			set { docID = value; }
		}
		public string DocCategory
		{
			get { return docCategory; }
			set { docCategory = value; }
		}
		public int Ngram
		{
			get { return ngram; }
			set { ngram = value; }
		}
		public string Title
		{
			get { return title; }
			set { title = value; }
		}
		public Dictionary<string, int> Terms
		{
			get { return terms; }
			set { terms = value; }
		}
	}
}
