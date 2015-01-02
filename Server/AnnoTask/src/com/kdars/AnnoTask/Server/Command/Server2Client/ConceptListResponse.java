package com.kdars.AnnoTask.Server.Command.Server2Client;

public class ConceptListResponse {

	public String conceptToId;
	public String ConceptToTerm;
	public String MetaOntology;
	
	public ConceptListResponse(String conceptToId, String ConceptToTerm, String MetaOntology){
		this.conceptToId = conceptToId;
		this.ConceptToTerm = ConceptToTerm;
		this.MetaOntology = MetaOntology;
	
	}
}
