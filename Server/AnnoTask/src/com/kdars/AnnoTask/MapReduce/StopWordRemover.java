package com.kdars.AnnoTask.MapReduce;

import com.kdars.AnnoTask.DB.DeleteListDBManager;
import com.kdars.AnnoTask.DB.DocTermFreqByTerm;

public class StopWordRemover {

	public boolean isStopWord(String deleteTerm) {
		if (DeleteListDBManager.getInstance().CheckForDeleteTerm(deleteTerm)){
			return true;
		}
		
		return false;
	}
}
