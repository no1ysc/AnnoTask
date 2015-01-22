package com.kdars.AnnoTask.Server;

import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.OutputStreamWriter;
import java.net.Socket;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;

import com.kdars.AnnoTask.ContextConfig;
import com.kdars.AnnoTask.DB.UserDBManager;
import com.kdars.AnnoTask.Server.Command.Client2Server.RequestAddUserAccount;

import flexjson.JSONDeserializer;
import flexjson.JSONSerializer;

/**
 * 
 * @author kihpark
 * @category user register and login procedure
 * @date 1/21/2015
 * 
 **/
public class UserAuthentication extends Thread{
	private UserListener userListener;
	private Socket	socket;
	private BufferedReader input;
	private BufferedWriter output;
	private String userID;

	private boolean bValidConnection = true;
	public boolean getbValidConnection(){
		return this.bValidConnection;
	}
	public UserAuthentication(Socket socket, UserListener userListener){
		this.userListener = userListener;
		this.socket = socket;
		try {
			input = new BufferedReader(new InputStreamReader(socket.getInputStream()));
			output = new BufferedWriter(new OutputStreamWriter(socket.getOutputStream(), "UTF8"));
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}
	
	public void run(){
		String commandFromUser = commandFromUser();
		if (commandFromUser != null) {
			commandParser(commandFromUser);
		}
		
		// 인스턴스가 사라질 때 reference 다 끊어주기
		socket = null;
		input = null;
		output = null;
		userListener = null;
	}
	
	private void commandParser(String commandFromUser) {

		// 유저 회원가입 요청 시
		if(commandFromUser.contains("addUserAccount")){
			RequestAddUserAccount requestAddUserAccount = new JSONDeserializer<RequestAddUserAccount>().deserialize(commandFromUser, RequestAddUserAccount.class);
			requestAddUserAccount(requestAddUserAccount);
		}
		
		// TODO 로그인 시도

	}
	
	private String commandFromUser() {
		String command = " ";
		try {
			command = input.readLine();
		} catch (NullPointerException | IOException e) {
			bValidConnection = false;
		}
		return command;
	}
	
	// (기흥) 유저 회원가입 시 동작
	private void requestAddUserAccount(	RequestAddUserAccount requestAddUserAccount) {
		// 회원가입 성공 시
		if(addNewUser(requestAddUserAccount)){
			// 자동 로그인
			automaticLogin(requestAddUserAccount);
		}
	}

	private void automaticLogin(	RequestAddUserAccount requestAddUserAccount) {
		userID = UserDBManager.getInstance().getUserInformation(requestAddUserAccount.emailAddress);
		userListener.createActiveUser(socket, userID);
		System.out.println(userID + "님이 IP주소 " + socket.getInetAddress().getAddress().toString() + "로 접속하였습니다.");
	}
	
	private boolean addNewUser(RequestAddUserAccount requestAddUserAccount) {
		return UserDBManager.getInstance().registerNewUser(requestAddUserAccount.emailAddress, requestAddUserAccount.Password, requestAddUserAccount.userName);
	}
	
	private void transferObject(Object obj) {
		String json = new JSONSerializer().exclude("*.class").serialize(obj);
		
		try {
			output.write(json + "\r\n");
			output.flush();
		} catch (IOException e) {
			e.printStackTrace();
		}
	}

}
