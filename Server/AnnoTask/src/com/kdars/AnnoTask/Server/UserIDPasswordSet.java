package com.kdars.AnnoTask.Server;

import com.kdars.AnnoTask.Server.Command.Server2Client.UserInfo;

public class UserIDPasswordSet {
	private String userID;
	private String password;
	private UserInfo userInfo;
	
	public UserIDPasswordSet(String userID, String password, UserInfo userInfo){
		this.userID = userID;
		this.password = password;
		this.userInfo = userInfo;
	}

	public String getUserID() {
		return userID;
	}

	public String getPassword() {
		return password;
	}
	
	public UserInfo getUserInfo(){
		return userInfo;
	}
}
