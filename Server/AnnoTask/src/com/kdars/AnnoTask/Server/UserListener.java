package com.kdars.AnnoTask.Server;

import java.io.IOException;
import java.net.ServerSocket;
import java.net.Socket;
import java.net.SocketException;
import java.util.ArrayList;

import com.kdars.AnnoTask.DocumentAnalyzer;

public class UserListener extends Thread{
	private DocumentAnalyzer documentAnalyzer;
	private ArrayList<UserControl>	connectUserList = new  ArrayList<UserControl>();	// TODO : 이거 쓰레드로 변형해서 주기적 커넥션 체크. 현재 쓰래드는 죽으나, 인스턴스는 종료하지 않음.
	private ServerSocket serverSocket = null;
	private int userIdGenerator = 0;
	private AddLocker addLocker;	// 이승철 추가 20141231, Bug25, 유저들이 동시에 delete, 시소러스 테이블에 접근할 수 없도록 막을 공유자원.
	private HeartBeatChecker heartbeatChecker;
	
	public UserListener(DocumentAnalyzer documentAnalyzer) {
		// TODO Auto-generated constructor stub
		this.documentAnalyzer = documentAnalyzer;
	}

	public void run(){
		try {
			serverSocket = new ServerSocket(50000);
			heartbeatChecker =  new HeartBeatChecker(connectUserList);
			heartbeatChecker.start();
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		
		try {
			// 요청대기시간 설정 (Exception : SocketTimeoutException)
            serverSocket.setSoTimeout(0);
		} catch (SocketException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		        
		while(true){
			// 서버소켓은 클라이언트의 연결요청이 올 때까지 실행을 멈추고 대기.
	        // 클라이언트의 연결요청이 오면 클라이언트 소켓과 통신할 새로운 소켓을 생성한다.
	        Socket socket;
			try {
				socket = serverSocket.accept();
				System.out.println(socket.getInetAddress() + "로부터 연결요청이 들어왔습니다.");
			} catch (IOException e) {
				e.printStackTrace();
				continue;
			}
	        
	        UserControl user = new UserControl(socket, ++userIdGenerator, addLocker);
	        synchronized(connectUserList){
		        connectUserList.add(user);	        	
	        }
			user.start();
		}
	}
	
//	public void disConnect(UserControl user){
//		connectUserList.remove(user);
//	}
}
