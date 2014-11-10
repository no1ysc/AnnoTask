package com.kdars.AnnoTask.Server;

import java.util.concurrent.LinkedTransferQueue;

public class UserControl extends Thread{
	private String ip;
	private String port;
	
	public void run(){
		while(true){
			Command commandFormUser = commandFormUser();
			commandParser(commandFormUser);
			Command commandToUser = new Command();
			commandToUser(commandToUser);
		}
	}

	private void commandToUser(Command commandToUser) {
		// TODO Auto-generated method stub
		
	}

	private void commandParser(Command commandFormUser) {
		// TODO Auto-generated method stub
		
	}

	private Command commandFormUser() {
		// TODO Auto-generated method stub
		//wait..........
		return null;
	}
	
	
}
