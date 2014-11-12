package com.kdars.AnnoTask.MapReduce;

import com.kdars.AnnoTask.DB.DeleteListDBManager;
import com.kdars.AnnoTask.DB.DocByTerm;

public class StopWordRemover {

	public boolean isStopWord(String deleteTerm) {
		if (DeleteListDBManager.getInstance().queryDeleteTerm(deleteTerm) == null){
			return false;
		}
		
		return true;
	}
}
