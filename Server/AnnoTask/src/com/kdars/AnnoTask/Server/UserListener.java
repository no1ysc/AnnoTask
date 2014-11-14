package com.kdars.AnnoTask.Server;

import java.util.ArrayList;

import com.kdars.AnnoTask.DocumentAnalyzer;

public class UserListener extends Thread{
	private DocumentAnalyzer documentAnalyzer;
	private ArrayList<UserControl>	connectUserList = new  ArrayList<UserControl>();	// TODO : 이거 쓰레드로 변형해서 주기적 커넥션 체크.
	
	public UserListener(DocumentAnalyzer documentAnalyzer) {
		// TODO Auto-generated constructor stub
		this.documentAnalyzer = documentAnalyzer;
	}

	public void run(){
//		while(true){
			accept();
			UserControl	user = createUser();
			user.start();
//		}
	}

	private UserControl createUser() {
		// TODO Auto-generated method stub
		connectUserList.add(new UserControl());
		return	new UserControl();
	}

	private void accept() {
		// TODO Auto-generated method stub
		
	}
	
	private void disConnect(UserControl user){
		connectUserList.remove(user);
	}
}
