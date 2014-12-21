package com.kdars.AnnoTask.Server.Command.Server2Client;

public class ReturnAddThesaurus {
	public String term;
	public String returnValue;
	public String message;
	
	public ReturnAddThesaurus(String term, String returnValue, String message){
		this.term = term;
		this.returnValue = returnValue;
		this.message = message;
	}
}
