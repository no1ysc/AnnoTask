package com.kdars.AnnoTask.DB;

import com.kdars.AnnoTask.Server.Command.Server2Client.UserInfo;

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

	public UserInfo loginCheck(String loginID, String password) {
		return userDB.loginCheck(loginID, password);
	}

	public void userActivation(String userID) {
		userDB.activateUser(userID);
	}

	public void userDeactivation(String userID) {
		userDB.deactivateUser(userID);		
	}
}
