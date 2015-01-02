package com.kdars.AnnoTask.Server;

import java.util.ArrayList;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;

import com.kdars.AnnoTask.ContextConfig;

public class HeartBeatChecker extends Thread {
	private ArrayList<UserControl> userList;
	private ExecutorService eservice;
	
	public HeartBeatChecker(ArrayList<UserControl> connectUserList) {
		this.userList = connectUserList;
	}
	
	@Override
	protected void finalize() throws Throwable{
		this.eservice.shutdownNow();
		this.eservice = null;
	}

	/**
	 * @author JS
	 * 병렬로 처리할지 말지를 결정하여 현재 쓰레드 시작. 20150101
	 */
	public void runForThreadScheme(){
		if (ContextConfig.getInstance().isMultiCore()){
			eservice = Executors.newSingleThreadExecutor();
			this.eservice.submit(this);
		} else {
			this.start();
		}
	}

	public void run(){
		
		while(true){
			UserControl target = null;
			
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
		
		// 쓰레드 해제
		user.destroyThread();
		
		// 인스턴스 삭제.
        synchronized(userList){
        	userList.remove(user);
        }
	}
}
