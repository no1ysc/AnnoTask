package com.kdars.AnnoTask.MapReduce;

import com.kdars.AnnoTask.GlobalContext;
import com.kdars.AnnoTask.DB.DocByTerm;
import com.kdars.AnnoTask.DB.Document;

public class Tokenizer {
	private boolean isSpecialCharRemove = GlobalContext.getInstance().get;
	
	public Tokenizer(){
		
	}
	
	public DocByTerm	termGenerate(Document document, int ngram){
		specialCharRemove();
		return null;
	}
	
	private void specialCharRemove(){
		
	}
}
