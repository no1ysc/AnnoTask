package com.kdars.AnnoTask;

import com.kdars.AnnoTask.Server.UserListener;

public class Driver {
	private AnnoTask annoTask = new AnnoTask();
	
	public Driver(){
		;
	}
	
	public void run(){
		DocumentAnalyzer	documentAnalyzer = new DocumentAnalyzer();
		UserListener			userListener = new UserListener(documentAnalyzer);
		
		userListener.start();
		documentAnalyzer.run();
	}
}
