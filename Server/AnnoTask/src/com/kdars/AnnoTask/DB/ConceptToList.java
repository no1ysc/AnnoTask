package com.kdars.AnnoTask.DB;

public class ConceptToList {
	public String conceptToId; 
	public String conceptToTerm;
	public String metaontology;
	
	public ConceptToList(int id, String conceptToTerm, String meta){
		this.conceptToId = Integer.toString(id);
		this.conceptToTerm = conceptToTerm;
		this.metaontology = meta;
	}

}
