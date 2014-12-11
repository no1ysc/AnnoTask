using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnnoTaskClient.Logic
{
	public class Frequency
	{
        public Frequency(string term, int nGram, int totalTermFreq, int freqInDocument)
		{
			this.term = term;
			this.nGram = nGram;
            this.totalTermFreq = totalTermFreq;
            this.freqInDocument = freqInDocument;
		}

		private string term;
		public string Term
		{
			get { return term; }
			set { term = value; }
		}

		private int nGram;
		public int NGram
		{
			get { return nGram; }
			set { nGram = value; }
		}

		private int totalTermFreq;
		public int TotalTermFreq
		{
			get { return totalTermFreq; }
			set { totalTermFreq = value; }
		}
		private int freqInDocument;
		public int FreqInDocument
		{
			get { return freqInDocument; }
			set { freqInDocument = value; }
		}

		private Dictionary<string, Dictionary<int, string>> category = new Dictionary<string, Dictionary<int, string>>();
		public Dictionary<string, Dictionary<int, string>> Category // id, title
		{
			get { return category; }
		}
		public void appendCategory(string strCategory, int docID, string title)
		{
			// TODO : 제목은 어떻게 하지,,,,,,,,,?
			if (!category.ContainsKey(strCategory))
			{
				category[strCategory] = new Dictionary<int, string>();
			}

			if (!category[strCategory].ContainsKey(docID))
			{
				category[strCategory].Add(docID, title);
			}
		}
	}
}
