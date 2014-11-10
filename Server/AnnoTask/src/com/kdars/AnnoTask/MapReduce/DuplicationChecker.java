package com.kdars.AnnoTask.MapReduce;

import com.kdars.AnnoTask.DB.DocByTerm;
import com.kdars.AnnoTask.DB.ThesaurusDBManager;

public class DuplicationChecker {

	public boolean checkDuplication(DocByTerm docByTerm) {
		// TODO 반드시 다시구현~! 예제임.
		return getConceptFrom("1111");
	}
	
	private boolean getConceptFrom(String term){
		if (ThesaurusDBManager.getInstance().queryTerm(term) == null){
			return false;
		}
		
		return true;
	}
	
}
