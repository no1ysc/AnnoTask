using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnnoTaskClient
{
	class LastDocListNode : TreeNode
	{
		public LastDocListNode(string title) : base(title)
		{
			this.Title = title;
		}

		public string Title { get; set; }
		public int DocID { get; set; }
	}
}
