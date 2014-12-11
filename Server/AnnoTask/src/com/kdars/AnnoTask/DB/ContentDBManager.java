package com.kdars.AnnoTask.DB;

import java.util.ArrayList;
import java.util.List;

import com.kdars.AnnoTask.ContextConfig;
import com.kdars.AnnoTask.Server.Command.Client2Server.RequestDocMeta;
import com.kdars.AnnoTask.Server.Command.Server2Client.DocMetaTransfer;

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
		return contentDB.query("doc_id", String.valueOf(docID));
	}
	
	public ArrayList<Integer> getJobCandidates(){
		ArrayList <Integer> jobCandidates = new ArrayList<Integer>();
		jobCandidates = contentDB.checkUpdates("working_status", 0); // colName, value, status
		return jobCandidates;
	}

	public void updateJobCompletion(int docID) {
		contentDB.update("complete_status", docID, 1);
	}
	
	public void updateWorkingStatus(int docID, int value){
		contentDB.update("working_status", docID, value);
	}

	public ArrayList<Document> getDocIDsFromDate(String startDate,
			String endDate, boolean bNaver, boolean bDaum, boolean bNate) {
		return contentDB.queryFromDate(startDate, endDate, bNaver, bDaum, bNate);
	}

	// (기흥) phase2.5 - 구현 계획1
	public ArrayList<Integer> getClientJobCandidates() {
		return contentDB.queryClientJobCandidates(ContextConfig.getInstance().getClientJobUnit());
	}

	public DocMetaTransfer getDocMeta(List<Integer> docIDList) {
		return contentDB.queryDocMeta(docIDList);
	}
}
