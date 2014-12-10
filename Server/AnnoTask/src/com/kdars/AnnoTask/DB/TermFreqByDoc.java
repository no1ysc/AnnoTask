package com.kdars.AnnoTask.DB;

import java.util.HashMap;

public class TermFreqByDoc extends HashMap<Integer, Integer>{
	// docID, frequency
	private int termHolder;
	private String term;
	private int nGram;
	
	public TermFreqByDoc(String term, int nGram, int termHolder){
		this.term = term;
		this.nGram = nGram;
		this.termHolder = termHolder;
	}
	
	public String getTerm(){
		return this.term;
	}
}
