package com.kdars.AnnoTask.MapReduce;

import com.kdars.AnnoTask.DB.ThesaurusDBManager;

public class DuplicationChecker {

	public boolean duplicationCheck(String term) {
		if (ThesaurusDBManager.getInstance().queryTerm(term) == null){
			return false;
		}
		
		return true;
	}	
}
