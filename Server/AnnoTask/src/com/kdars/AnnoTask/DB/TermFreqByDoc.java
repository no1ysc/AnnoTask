package com.kdars.AnnoTask.DB;

import java.util.HashMap;

public class TermFreqByDoc extends HashMap<Integer, Integer>{
	// docID, frequency
	private int termHolder;
	private String term;
	
	public TermFreqByDoc(String term, int termHolder){
		this.term = term;
		this.termHolder = termHolder;
	}
}
