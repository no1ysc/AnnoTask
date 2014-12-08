package com.kdars.AnnoTask.Server.Command.Server2Client;

public class ConceptListResponse {

	public String conceptToId;
	public String ConceptToTerm;
	
	public ConceptListResponse(String conceptToId, String ConceptToTerm){
		this.conceptToId = conceptToId;
		this.ConceptToTerm = ConceptToTerm;
	
	}
}
