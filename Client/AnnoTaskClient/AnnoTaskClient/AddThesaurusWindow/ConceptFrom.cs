using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnnoTaskClient
{
	class ConceptFrom
	{
		public ConceptFrom(string conceptFrom)
		{
			this.conceptFrom = conceptFrom;
		}

		private string conceptFrom;
		public string ConceptFromTerm
		{
			get { return conceptFrom; }
			set { conceptFrom = value; }
		}

		public override string ToString()
		{
			return conceptFrom;
		}
	}
}
