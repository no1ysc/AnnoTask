using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnnoTaskClient
{
	class ConceptTo
	{
		// 이승철 Meta 관련 추가, 20150101
		public ConceptTo(string conceptTo, string conceptToId, string meta)
		{
			conceptToTerm = conceptTo;
			this.conceptToId = conceptToId;
			this.meta = meta;
		}

		private string conceptToId;
		private string conceptToTerm;
		private string meta;

		public string ConceptToID
		{
			get { return conceptToId; }
			set { conceptToId = value; }
		}
		public string ConceptToTerm
		{
			get { return conceptToTerm; }
			set { conceptToTerm = value; }
		}
		public string MetaOntology
		{
			get
			{
				return meta;
			}
		}



		public override string ToString()
		{
			return conceptToTerm;
		}
	}
}
