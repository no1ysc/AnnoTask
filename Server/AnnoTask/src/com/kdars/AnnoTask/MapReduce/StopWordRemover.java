package com.kdars.AnnoTask.MapReduce;

import com.kdars.AnnoTask.DB.DeleteListDBManager;
import com.kdars.AnnoTask.DB.DocByTerm;

public class StopWordRemover {

	public void remove(DocByTerm docByTerm) {
		// TODO Auto-generated method stub
		if (isDeleting("111111")){
			// TODO: 지우기.
		}
	}
	
	private boolean isDeleting(String deleteTerm){
		if (DeleteListDBManager.getInstance().queryDeleteTerm(deleteTerm) == null){
			return false;
		}
		
		return true;
	}

}
