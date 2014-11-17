package com.kdars.AnnoTask.DB;

import java.util.ArrayList;

public class ContentDBManager {
	private static ContentDBManager thisClass = new ContentDBManager();
	private ContentDBConnector contentDB;
	
	public ContentDBManager(){
		contentDB = new ContentDBConnector();
	}
	
	public static ContentDBManager getInstance(){
		return	thisClass;
	}
	
	public Document getContent(int docID){
		// TODO : 예제.
		return contentDB.query("docid", String.valueOf(docID));
	}

	public ArrayList<Integer> getDocIDsFromDate(String startDate,
			String endDate, boolean bNaver, boolean bDaum, boolean bNate) {
		return contentDB.queryFromDate(startDate, endDate, bNaver, bDaum, bNate);
	}
}
