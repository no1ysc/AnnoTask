package com.kdars.AnnoTask.DB;

import java.util.HashMap;
import java.util.Map;

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
	
	public void increaseFreq(String key){
		Integer value = this.get(key);
		if (value != null){
			this.put(key, value + 1);
		}else{
			this.put(key, 1);
		}
	}
}
