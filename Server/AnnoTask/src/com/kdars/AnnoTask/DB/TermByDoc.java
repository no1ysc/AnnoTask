package com.kdars.AnnoTask.DB;

import java.util.HashMap;

public class TermByDoc extends HashMap<Integer, Integer>{
	// docID, frequency
	private int termHolder;
	private String term;
	
	public TermByDoc(String term, int termHolder){
		this.term = term;
		this.termHolder = termHolder;
	}
}
