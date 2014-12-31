using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnnoTaskClient
{
	class LinkedListItem
	{
		public LinkedListItem(string term)
		{
			this.linkedTerm = term;
		}
		public LinkedListItem(string term, string meta)
		{
			this.linkedTerm = term;
			this.metaInfo = meta;
		}

		private string linkedTerm;
		public string LinkedTerm
		{
			get { return linkedTerm; }
			set { linkedTerm = value; }
		}

		private string metaInfo;	// 얜 뭐지.
		public string MetaInfo
		{
			get { return metaInfo; }
			set { metaInfo = value; }
		}

		public override string ToString()
		{
			return linkedTerm;
		}
	}
}
