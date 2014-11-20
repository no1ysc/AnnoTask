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
	
	public String getConceptFrom() {
		return this.conceptFrom;
	}
	
	public String justTestConceptFrom() {
		this.conceptFrom = "test";
		return this.conceptFrom;
	}
	
}
