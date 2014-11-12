package com.kdars.AnnoTask.MapReduce;

import com.kdars.AnnoTask.GlobalContext;
import com.kdars.AnnoTask.DB.DocByTerm;
import com.kdars.AnnoTask.DB.Document;

public class Tokenizer {
	private boolean isSpecialCharRemove = GlobalContext.getInstance().isSpecialCharRemove();
	
	public Tokenizer(){
		
	}
	
	public DocByTerm	termGenerate(Document document, int ngram){
		/* Getting Target String */
		String doc = document.getBody();
				
		/* Setting Meta Data */
		DocByTerm ret = new DocByTerm(document.getDocumentID(), ngram, document.getCategory());
				
		/* Working Tokenize */
		if (this.isSpecialCharRemove){
			
		} else {
			
		}
		specialCharRemove();
		return null;
	}
	
	private void specialCharRemove(){
		
	}
}
