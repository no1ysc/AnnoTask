package com.kdars.AnnoTask.MapReduce;

import com.kdars.AnnoTask.GlobalContext;
import com.kdars.AnnoTask.DB.DocByTerm;
import com.kdars.AnnoTask.DB.Document;

public class Tokenizer {
	public Tokenizer(){
		
	}
	
	public DocByTerm	termGenerate(Document document, int ngram){
		/* Getting Target String */
		String doc = document.getBody();
				
		/* Setting Meta Data */
		DocByTerm ret = new DocByTerm(document.getDocumentID(), ngram, document.getCategory());
				
		/* Working Tokenize */
		String delim = GlobalContext.getInstance().getDelim();
		
		//TODO : 토큰하자.
		return null;
	}
	
	private void specialCharRemove(){
		
	}
}
