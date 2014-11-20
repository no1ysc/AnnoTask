package com.kdars.AnnoTask.Server;

import java.io.BufferedReader;
import java.io.DataOutputStream;
import java.io.IOException;
import java.io.InputStreamReader;
import java.net.Socket;

import com.kdars.AnnoTask.Server.Command.Client2Server.DocumentRequest;
import com.kdars.AnnoTask.Server.Command.Client2Server.RequestByDate;
import com.kdars.AnnoTask.Server.Command.Client2Server.RequestTermTransfer;
import com.kdars.AnnoTask.Server.Command.Server2Client.SendDocumentCount;

import flexjson.JSONDeserializer;
import flexjson.JSONSerializer;

public class UserControl extends Thread{
	private Socket	socket;
	private BufferedReader input;
//	private BufferedWriter output;
	private DataOutputStream output;
	
	public UserControl(Socket socket){
		this.socket = socket;
		try {
			input = new BufferedReader(new InputStreamReader(socket.getInputStream()));
//			output = new BufferedWriter(new OutputStreamWriter(socket.getOutputStream()));
			output = new DataOutputStream(socket.getOutputStream());
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}
	
	@Override
	protected void finalize() throws Throwable {
		input.close();
		output.close();
		socket.close();
		super.finalize();
	}
	
	public void run(){
		while(true){
			String commandFromUser = commandFromUser();
			commandParser(commandFromUser);
//			Command commandToUser = new Command();
//			commandToUser(commandToUser);
		}
	}

	private void commandToUser(Object commandToUser) {
		
	}

	private void commandParser(String commandFromUser) {
		// 1-1 처리.
		if (commandFromUser.substring(0, 20).contains("startDate")){
			System.out.println(commandFromUser);
			RequestByDate requestByDate = new JSONDeserializer<RequestByDate>().deserialize(commandFromUser, RequestByDate.class);
			requestByDateHandler(requestByDate);
		}
		//1-3 처리.
		if (commandFromUser.substring(0, 20).contains("bTransfer")){
			RequestTermTransfer requestTermTransfer = new JSONDeserializer<RequestTermTransfer>().deserialize(commandFromUser);
			requestTermTransferHandler(requestTermTransfer);
		}
		//2-1 처리.
		if (commandFromUser.substring(0, 20).contains("documentID")){
			DocumentRequest documentRequest = new JSONDeserializer<DocumentRequest>().deserialize(commandFromUser);
			documentRequestHandler(documentRequest);
		}
	}

	private void documentRequestHandler(DocumentRequest documentRequest) {
		// TODO Doc ID가 오면 문서 원문 던져줌.
	}

	private void requestTermTransferHandler(
			RequestTermTransfer requestTermTransfer) {
		// TODO 문서전송 시작.!
		
		/*
		 * for `~~~
		 * 1. DOC ID set을 탐색하여 Term Freq DB 뒤짐.
		 * 2. 텀 락 걸어야됨.
		 * 3. DocByTerm 형태로 변환하여 전송.
		 * for........
		 */
		
	}

	private void requestByDateHandler(RequestByDate requestByDate) {
		// TODO 날짜날라오면 할꺼.
		/*
		 * 1. Contents DB 날짜로 쿼리.(싸이트별)
		 * 2. 1의 결과를 DOC ID Set으로 받아옴
		 * 3. DOC 카운트 전송.
		 */
				
		
		SendDocumentCount sendDocumentCount = new SendDocumentCount();
		sendDocumentCount.doucumentCount = 100;
		
		String json = new JSONSerializer().exclude("*.class").serialize(sendDocumentCount);
		System.out.println(json);
		try {
			output.writeBytes((json+"\r\n"));
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}

	private String commandFromUser() {
		String command = "";
		try {
			command = input.readLine();
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		return command;
	}
	
	
}
