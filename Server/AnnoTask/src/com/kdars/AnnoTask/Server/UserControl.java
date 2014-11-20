package com.kdars.AnnoTask.Server;

import java.io.BufferedReader;
import java.io.DataOutputStream;
import java.io.IOException;
import java.io.InputStreamReader;
import java.net.Socket;
import java.sql.ResultSet;
import java.sql.Statement;
import java.util.ArrayList;
import java.util.Iterator;
import java.util.Map;

import com.kdars.AnnoTask.GlobalContext;
import com.kdars.AnnoTask.DB.ContentDBConnector;
import com.kdars.AnnoTask.DB.ContentDBManager;
import com.kdars.AnnoTask.DB.DocByTerm;
import com.kdars.AnnoTask.DB.Document;
import com.kdars.AnnoTask.DB.TermFreqDBManager;
import com.kdars.AnnoTask.Server.Command.Client2Server.DocumentRequest;
import com.kdars.AnnoTask.Server.Command.Client2Server.RequestByDate;
import com.kdars.AnnoTask.Server.Command.Client2Server.RequestTermTransfer;
import com.kdars.AnnoTask.Server.Command.Server2Client.NotifyTransferEnd;
import com.kdars.AnnoTask.Server.Command.Server2Client.SendDocumentCount;
import com.kdars.AnnoTask.Server.Command.Server2Client.TermTransfer;

import flexjson.JSONDeserializer;
import flexjson.JSONSerializer;

public class UserControl extends Thread{
	private Socket	socket;
	private BufferedReader input;
//	private BufferedWriter output;
	private DataOutputStream output;
	private int userID;
	
	private ArrayList<Document> 	requestDocs;
	public UserControl(Socket socket, int userID){
		this.socket = socket;
		this.userID = userID;
//		this.userID = socket.getInetAddress().getAddress();
				
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
			RequestTermTransfer requestTermTransfer = new JSONDeserializer<RequestTermTransfer>().deserialize(commandFromUser, RequestTermTransfer.class);
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
		
		for (Document doc : requestDocs){
			TermTransfer termTransfer = new TermTransfer();
			termTransfer.docID = doc.getDocumentID();
			termTransfer.docCategory = doc.getCategory();
			
			DocByTerm[] docByTerm = TermFreqDBManager.getInstance().getDocByTerm(doc.getDocumentID());
						
			termLockInDoc(docByTerm);
			
			// 전송
			transferDocbyTerm(docByTerm);
		}
		
		// 전송완료 알림.
		NotifyTransferEnd notifyTransferEnd = new NotifyTransferEnd();
		notifyTransferEnd.flagEnd = true;
		
		transferObject(notifyTransferEnd);
	}

	private void requestByDateHandler(RequestByDate requestByDate) {
			// TODO 날짜날라오면 할꺼.
			/*
			 * 1. Contents DB 날짜로 쿼리.(싸이트별)- 싸이트별로는 어떻게?
			 * 2. 1의 결과를 DOC ID Set으로 받아옴
			 * 3. DOC 카운트 전송.
			 */
			
			this.requestDocs = ContentDBManager.getInstance().getDocIDsFromDate(requestByDate.startDate, requestByDate.endDate, requestByDate.bNaver, requestByDate.bDaum, requestByDate.bNate);
			
			SendDocumentCount sendDocumentCount = new SendDocumentCount();
			sendDocumentCount.doucumentCount = requestDocs.size() * GlobalContext.getInstance().getN_Gram();
			
			transferObject(sendDocumentCount);
	}

	private void transferDocbyTerm(DocByTerm[] docByTerm) {
		for (int index = 0; index < docByTerm.length; index++){
			TermTransfer termTransfer = new TermTransfer();
			termTransfer.docCategory = docByTerm[index].getDocCategory();
			termTransfer.docID = docByTerm[index].getDocID();
			termTransfer.ngram = docByTerm[index].getNGram();
			termTransfer.termsJson = new JSONSerializer().exclude("*.class").serialize(docByTerm);
			
			transferObject(termTransfer);
		}		
	}

	private void transferObject(Object obj) {
		String json = new JSONSerializer().exclude("*.class").serialize(obj);
		
		try {
			output.writeBytes((json+"\r\n"));
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}

	private void termLockInDoc(DocByTerm[] docByTerm) {
		for (int index = 0; index < docByTerm.length; index++){
			
			for (Iterator<Map.Entry<String, Integer>> iter = docByTerm[index].entrySet().iterator(); iter.hasNext();){
				Map.Entry<String, Integer> entry = iter.next();
				String term = entry.getKey();
				if (!TermFreqDBManager.getInstance().termLock(term, userID)){
					iter.remove();
				}
			}
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
