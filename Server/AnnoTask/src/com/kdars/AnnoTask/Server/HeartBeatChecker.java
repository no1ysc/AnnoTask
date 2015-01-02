package com.kdars.AnnoTask.Server;

import java.sql.Timestamp;
import java.util.ArrayList;

import com.kdars.AnnoTask.ContextConfig;

public class HeartBeatChecker extends Thread {
	private ArrayList<UserControl> userList;
	
	public HeartBeatChecker(ArrayList<UserControl> connectUserList) {
		this.userList = connectUserList;
	}

	public void run(){
		UserControl target = null;
		while(true){
			for(UserControl user : userList){
				long timeSpan = System.currentTimeMillis() - user.getLastHeartBeat();
				if(timeSpan > ContextConfig.getInstance().getTimeoutLimitation()){
					target = user;
					break;
				}
			}
			
			if (target != null) {
				disconnectUser(target);
			}
			
			try {
				sleep(10);
			} catch (InterruptedException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
		}
		
		
	}

	private void disconnectUser(UserControl user) {
		if (user.isAlive()){
			// 비정상 접속종료
			// 쓰레드 종료안됨.
			// 쓰레드 죽이기 readLine 타임아웃걸기.
			user.forceDown();
		} else {
			// 정상 접속종료.
		}
		
		// 인스턴스 삭제.
        synchronized(userList){
        	userList.remove(user);
        }
	}
}
