package com.kdars.AnnoTask.MapReduce;

import com.kdars.AnnoTask.DB.DocByTerm;
import com.kdars.AnnoTask.DB.ThesaurusDBManager;

public class DuplicationChecker {

	public boolean checkDuplication(String term) {
		return getConceptFrom(term);
	}
	
	private boolean getConceptFrom(String term){
		if (ThesaurusDBManager.getInstance().queryTerm(term) == null){
			return false;
		}
		
		return true;
	}
	
}
