package com.kdars.AnnoTask.DB;

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

	public String getUserInformation(String emailAddress) {
		return userDB.getUserId(emailAddress);
	}
}
