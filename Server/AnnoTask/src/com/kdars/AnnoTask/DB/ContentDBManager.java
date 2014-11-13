package com.kdars.AnnoTask.DB;

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
}
