package com.kdars.AnnoTask.DB;

import java.util.Map;
import java.util.List;
import java.util.HashMap;

public class Thesaurus {
	private int conceptFromID;
	private String conceptFrom;
	private int conceptToID;
	private String conceptTo;
	private String metaOntology;
	private String metaName;
	private String partOfSpeech;
	private String definition;
	
	public Thesaurus(){
	}
	
	public void setConceptFromID(int conceptfromid) {
		conceptFromID = conceptfromid;
	}

	public void setConceptFrom(String conceptfrom) {
		conceptFrom = conceptfrom;
	}
	
	public void setConceptToID(int concepttoid) {
		conceptToID = concepttoid;
	}
	
	public void setConceptTo(String conceptto) {
		conceptTo = conceptto;
	}
	
	public void setMetaOntology(String metaontology) {
		metaOntology = metaontology;
	}
	
	public void setMetaName(String metaname) {
		metaName = metaname;
	}
	
	public void setPOS(String pos) {
		partOfSpeech = pos;
	}
	
	public void setDefinition( String definition) {
		definition = definition;
	}
	
	public int getConceptFromID() {
		return this.conceptFromID;
	}

	public String getConceptFrom() {
		return this.conceptFrom;
	}
	
	public int getConceptToID() {
		return this.conceptToID;
	}
	
	public String getConceptTo() {
		return this.conceptTo;
	}
	
	public String getMetaOntology() {
		return this.metaOntology;
	}
	
	public String getMetaName() {
		return this.metaName;
	}
	
	public String getPOS() {
		return this.partOfSpeech;
	}
	
	public String getDefinition() {
		return this.definition;
	}
//	public String getConceptFrom() {
//		return this.conceptFrom;
//	}
	
	
}
