package com.kdars.AnnoTask;

import com.kdars.AnnoTask.Server.ListenUser;

public class Driver {
	private AnnoTask annoTask = new AnnoTask();
	
	public Driver(){
		;
	}
	
	public void run(){
		DocumentAnalyzer	documentAnalyzer = new DocumentAnalyzer();
		ListenUser			listenUser = new ListenUser(documentAnalyzer);
		
		listenUser.start();
		documentAnalyzer.run();
	}
}
