package com.kdars.AnnoTask.DB;

import com.kdars.AnnoTask.Server.Command.Server2Client.UserInfo;

/**
 * 
 * @author kihpark
 * @date 1/21/2015
 * 
 **/
public class UserDBManager {
	private static UserDBManager userDBManager = new UserDBManager();
	private UserDBConnector userDB;

	public UserDBManager(){
		userDB = new UserDBConnector();
	}

	public static UserDBManager getInstance(){
		return	userDBManager;
	}

	public boolean registerNewUser(String email, String password, String userName){
		if(userDB.registerNewUser(email, password, userName)){
			return true;
		}else{
			System.out.println("Failed to register new user in AnnoTask!");
			return false;			
		}
	}

	public UserInfo getUserInformation(String emailAddress) {
		return userDB.getUserInfo(emailAddress);
	}

	public UserInfo loginCheck(String userID, String password) {
		return userDB.loginCheck(userID, password);
	}

	public void userActivation(String userID) {
		userDB.activateUser(userID);
	}

	public void userDeactivation(String userID) {
		userDB.deactivateUser(userID);		
	}

	public void increaseLoginCount(String userID) {
		userDB.increaseLoginCount(userID);
	}

	public void increaseDeleteListAddedCount(String userID) {
		userDB.increaseDeleteListAddedCount(userID);		
	}

	public void increaseThesaurusAddedCount(String userID) {
		userDB.increaseThesaurusAddedCount(userID);		
	}

	public boolean checkUserID(String checkUserID) {
		return userDB.checkUserID(checkUserID);
	}
}
