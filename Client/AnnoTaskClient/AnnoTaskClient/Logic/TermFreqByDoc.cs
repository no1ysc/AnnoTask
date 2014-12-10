using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnnoTaskClient.Logic
{
	class TermFreqByDoc
	{
		private string term;
		private int ngram;

        private Dictionary<int, int> terms;

		public string Term
		{
			get { return term; }
			set { term = value; }
		}
		public int Ngram
		{
			get { return ngram; }
			set { ngram = value; }
		}
		public Dictionary<int, int> Terms
		{
			get { return terms; }
			set { terms = value; }
		}
	}
}
