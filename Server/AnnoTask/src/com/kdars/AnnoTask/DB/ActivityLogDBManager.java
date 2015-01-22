package com.kdars.AnnoTask.DB;

/**
 * 
 * @author kihpark
 * @date 1/21/2015
 * 
 **/
public class ActivityLogDBManager {
	private static ActivityLogDBManager activityLogDBManager = new ActivityLogDBManager();
	private ActivityLogConnector activityLogDB;

	public ActivityLogDBManager(){
		activityLogDB = new ActivityLogConnector();
	}

	public static ActivityLogDBManager getInstance(){
		return	activityLogDBManager;
	}

	public void add_deletelist(String userID, String deleteTerm) {
		activityLogDB.add_deletelist(userID, deleteTerm);
	}

	public void changeThes2DeleteList(String userID, String deleteTerm) {
		activityLogDB.changeThes2DeleteList(userID, deleteTerm);
	}

	public void change_DeleteList2Thes(String userID, String conceptFrom, String conceptTo, String metaOntology) {
		activityLogDB.change_DeleteList2Thes(userID, conceptFrom, conceptTo, metaOntology);
	}

	public void add_thesaurus(String userID, String conceptFrom, String conceptTo, String metaOntology) {
		activityLogDB.add_thesaurus(userID, conceptFrom, conceptTo, metaOntology);
	}

	public void update_thesaurus(String userID, String conceptFrom, String conceptTo, String metaOntology) {
		activityLogDB.update_thesaurus(userID, conceptFrom, conceptTo, metaOntology);
		
	}

	
}
