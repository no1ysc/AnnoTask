package com.kdars.AnnoTask.DB;

import java.util.ArrayList;

public class ContentDBManager {
	private static ContentDBManager contentDBManager = new ContentDBManager();
	private ContentDBConnector contentDB;
	
	public ContentDBManager(){
		contentDB = new ContentDBConnector();
	}
	
	public static ContentDBManager getInstance(){
		return	contentDBManager;
	}
	
	public Document getContent(int docID){
		// TODO : 예제.
		return contentDB.query("doc_id", String.valueOf(docID));
	}
	
	public ArrayList<Integer> getJobCandidates(){
		ArrayList <Integer> jobCandidates = new ArrayList<Integer>();
		jobCandidates = contentDB.checkUpdates("job_table", "working_status", 0);
		
		return jobCandidates;
	}
}
