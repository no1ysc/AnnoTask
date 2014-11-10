package com.kdars.AnnoTask.DB;

import java.util.HashMap;

public class DocByTerm extends HashMap<String, Integer>{
	//Term, Freq
	private int docID;
	private String docCategory;
	private int ngram;
	
	public DocByTerm(int docID, int ngram, String docCategory){
		this.docCategory = docCategory;
		this.docID = docID;
		this.ngram = ngram;
	}

	public int getDocID() {
		return docID;
	}

	public String getDocCategory() {
		return docCategory;
	}
	
	public int getNGram(){
		return	this.ngram;
	}
}
