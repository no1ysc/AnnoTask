package com.kdars.AnnoTask.Server.Command.Server2Client;

public class ReturnAddDeleteList {
	public String term;
	public String returnValue;
	public String message;
	
	public ReturnAddDeleteList(String term, String returnValue, String message){
		this.term = term;
		this.returnValue = returnValue;
		this.message = message;
	}
}
