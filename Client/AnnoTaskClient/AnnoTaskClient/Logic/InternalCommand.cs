using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnnoTaskClient.Logic
{
	public class InternalCommand
	{
		protected string command;
		protected object data;

		public string Command { get { return command; } }
		public object Data { get { return data; } }
	}

	public class OpenThesaurusWindow : InternalCommand
	{
		public OpenThesaurusWindow(List<string> selectedTerms)
		{
			command = "OpenThesaurusWindow";
			this.selectedTerms = selectedTerms;
			data = this;
		}

		private List<string> selectedTerms;
		public List<string> SelectedTerms { get { return selectedTerms; } }
	}

	public class AddDeleteList : InternalCommand
	{
		public AddDeleteList(List<string> selectedTerms, int tabNum)
		{
			command = "AddDeleteList";
			this.selectedTerms = selectedTerms;
			this.tabNum = tabNum;
			data = this;
		}

		private List<string> selectedTerms;
		public List<string> SelectedTerms { get { return selectedTerms; } }
		private int tabNum;
		public int TabNum { get { return tabNum; } }
	}

	public class GetLinkedList : InternalCommand
	{
		public GetLinkedList(string term)
		{
			command = "GetLinkedList";
			this.term = term;
			data = this;
		}

		private string term;
		public string Term { get { return term; } }
	}
	
	public class AddThesaurus : InternalCommand
	{
		public AddThesaurus(string from, string to, string meta)
		{
			command = "AddThesaurus";
			this.conceptFrom = from;
			this.conceptTo = to;
			this.metaOntology = meta;
			data = this;
		}

		private string conceptFrom;
		public string ConceptFrom
		{
			get { return conceptFrom; }
		}
		private string conceptTo;
		public string ConceptTo
		{
			get { return conceptTo; }
		}
		private string metaOntology;
		public string MetaOntology
		{
			get { return metaOntology; }
		}
	}
	
	public class GetConceptToList : InternalCommand
	{
		public GetConceptToList(string term)
		{
			command = "GetConceptToList";
			this.inputConceptTo = term;
		}
		private string inputConceptTo;
		public string InputConceptTo
		{
			get { return inputConceptTo; }
		}
	}

	public class JobStart : InternalCommand
	{
		public JobStart()
		{
			command = "JobStart";
		}
	}

    public class RegisterUserAccount : InternalCommand
    {
        public RegisterUserAccount(string userName, string userID, string password)
        {
            command = "Register";
            this.userName = userName;
            this.userID = userID;
            this.password = password;
        }

        private string userName;
        public string UserName
        {
            get { return userName; }
        }
        private string userID;
        public string UserID
        {
            get { return userID; }
        }
        private string password;
        public string Password
        {
            get { return password; }
        }

    }

	

	
	
	
	
}
