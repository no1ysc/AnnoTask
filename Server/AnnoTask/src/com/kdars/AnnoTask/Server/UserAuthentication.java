package com.kdars.AnnoTask.Server;

import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.OutputStreamWriter;
import java.net.Socket;

import com.kdars.AnnoTask.ContextConfig;
import com.kdars.AnnoTask.DB.UserDBManager;
import com.kdars.AnnoTask.Server.Command.Client2Server.RequestAddUserAccount;
import com.kdars.AnnoTask.Server.Command.Client2Server.RequestCheckUserID;
import com.kdars.AnnoTask.Server.Command.Client2Server.RequestLogin;
import com.kdars.AnnoTask.Server.Command.Server2Client.SendUserIdCheckResult;
import com.kdars.AnnoTask.Server.Command.Server2Client.UserInfo;

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
	private String userID; //email
	private String userName;

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
			e.printStackTrace();
		}
	}
	
	public void run(){
		while(bValidConnection){
			String commandFromUser = commandFromUser();
			if (commandFromUser != null) {
				commandParser(commandFromUser);
			}
		}
		// 인스턴스가 사라질 때 reference 다 끊어주기
		socket = null;
		input = null;
		output = null;
		userListener = null;
	}
	
	private void commandParser(String commandFromUser) {
		
		// 계정 생성 전 중복 검사
		if(commandFromUser.contains("checkUserID")){
			RequestCheckUserID requestCheckUserID = new JSONDeserializer<RequestCheckUserID>().deserialize(commandFromUser, RequestCheckUserID.class);
			requestCheckUserID(requestCheckUserID);
		}
		// 유저 계정 생성 요청
		if(commandFromUser.contains("userName")){
			RequestAddUserAccount requestAddUserAccount = new JSONDeserializer<RequestAddUserAccount>().deserialize(commandFromUser, RequestAddUserAccount.class);
			requestAddUserAccount(requestAddUserAccount);
			
			return;
		}
		
		// 유저 로그인 시도
		if(commandFromUser.contains("userID")){
			RequestLogin requestLogin = new JSONDeserializer<RequestLogin>().deserialize(commandFromUser, RequestLogin.class);
			requestLogin(requestLogin);
		}
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
	
	/** Command 처리 함수들 (시작) **/
	private void requestCheckUserID(RequestCheckUserID requestCheckUserID) {
		String checkUserID = requestCheckUserID.checkUserID;
		SendUserIdCheckResult sendUserIdCheckResult = new SendUserIdCheckResult();
		sendUserIdCheckResult.isDuplicate = checkUserID(checkUserID);
		transferObject(sendUserIdCheckResult);
	}

	private void requestAddUserAccount(	RequestAddUserAccount requestAddUserAccount) {
		// 회원가입 성공 시
		if(addNewUser(requestAddUserAccount)){
			// 자동 로그인
			automaticLogin(requestAddUserAccount);
			bValidConnection = false;
		}
	}

	private void requestLogin(RequestLogin requestLogin) {
		// 로그인 인증
		if(authentication(requestLogin)){
			bValidConnection = false;
		}else{
			System.out.println(socket.getInetAddress() + " 로그인 인증 실패!");
		}
	}

	private boolean authentication(RequestLogin requestLogin) {
		UserInfo userInfo = new UserInfo();
		boolean ret = false;
		userInfo = UserDBManager.getInstance().loginCheck(requestLogin.userID, requestLogin.password);
		// LOGIN SUCCESS
		if(userInfo != null){
			login(userInfo);
			ret = true;
		} else {
			//LogInFail
			ret = false;
			userInfo = new UserInfo();
		}
		userInfo.isLoginSuccess = ret;
		transferObject(userInfo);
		return ret;
	}
	/** Command 처리 함수들 (끝) **/

	private boolean checkUserID(String checkUserID) {
		return UserDBManager.getInstance().checkUserID(checkUserID);
	}
	
	private void login(UserInfo userInfo) {
		userID = userInfo.userId;
		userName = userInfo.userName;
		userListener.createActiveUser(socket, userID, userName);
		System.out.println(userName + "(" + userID + ")" + "님이 IP주소 " + socket.getInetAddress() + "로 접속하였습니다.");
		
		//유저 접속 상태 정보 업데이트 (활성화)
		UserDBManager.getInstance().userActivation(userID);
		//유저 로그인 횟수 업데이트
		UserDBManager.getInstance().increaseLoginCount(userID);
		//유저 마지막 로그인 시간 업데이트
		UserDBManager.getInstance().updateLatestLoginTime(userID);
	}

	private void automaticLogin(	RequestAddUserAccount requestAddUserAccount) {
		UserInfo userInfo = new UserInfo();
		userID = UserDBManager.getInstance().getUserInformation(requestAddUserAccount.userID).userId;
		userName = UserDBManager.getInstance().getUserInformation(requestAddUserAccount.userID).userName;
		
		userInfo.userId = userID;
		userInfo.userName = userName;
		userInfo.isLoginSuccess = true;
		userListener.createActiveUser(socket, userID, userName);
		System.out.println(userName + "(" + userID + ")" + "님이 IP주소 " + socket.getInetAddress() + "로 접속하였습니다.");
		//로그인 성공 packet 보내기
		transferObject(userInfo);
		
		//유저 접속 상태 정보 업데이트 (활성화)
		UserDBManager.getInstance().userActivation(userID);
		//유저 로그인 횟수 업데이트
		UserDBManager.getInstance().increaseLoginCount(userID);
		//유저 마지막 로그인 시간 업데이트
		UserDBManager.getInstance().updateLatestLoginTime(userID);
	}
	
	private boolean addNewUser(RequestAddUserAccount requestAddUserAccount) {
		return UserDBManager.getInstance().registerNewUser(requestAddUserAccount.userID, requestAddUserAccount.password, requestAddUserAccount.userName);
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
