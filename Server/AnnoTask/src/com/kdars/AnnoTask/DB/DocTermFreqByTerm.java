package com.kdars.AnnoTask.DB;

import java.util.HashMap;
import java.util.Map;

public class DocTermFreqByTerm extends HashMap<String, Integer>{
	//Term, Freq
	private int docID;
	private String docTitle;
	private String docCategory;
	private int ngram;
	
	public DocTermFreqByTerm(int docID, int ngram, String docCategory){
		this.docCategory = docCategory;
		this.docID = docID;
		this.ngram = ngram;
	}

	public String getTitle(){
		return this.docTitle;
	}
	public void setTitle(String title){
		this.docTitle = title;
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
