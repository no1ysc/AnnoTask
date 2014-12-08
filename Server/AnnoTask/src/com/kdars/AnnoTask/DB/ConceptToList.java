package com.kdars.AnnoTask.DB;

public class ConceptToList {
	public String conceptToId; 
	public String conceptToTerm;
	
	public ConceptToList(int id, String conceptToTerm){
		this.conceptToId = Integer.toString(id);
		this.conceptToTerm = conceptToTerm;
	}

}
