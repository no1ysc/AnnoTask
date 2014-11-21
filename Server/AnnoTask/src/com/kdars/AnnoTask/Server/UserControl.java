package com.kdars.AnnoTask.Server;

import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.OutputStreamWriter;
import java.net.Socket;
import java.util.ArrayList;
import java.util.Iterator;
import java.util.Map;

import com.kdars.AnnoTask.GlobalContext;
import com.kdars.AnnoTask.DB.ContentDBManager;
import com.kdars.AnnoTask.DB.DocByTerm;
import com.kdars.AnnoTask.DB.Document;
import com.kdars.AnnoTask.DB.TermFreqDBManager;
import com.kdars.AnnoTask.Server.Command.Client2Server.DocumentRequest;
import com.kdars.AnnoTask.Server.Command.Client2Server.RequestByDate;
import com.kdars.AnnoTask.Server.Command.Client2Server.RequestTermTransfer;
import com.kdars.AnnoTask.Server.Command.Server2Client.DocumentResponse;
import com.kdars.AnnoTask.Server.Command.Server2Client.NotifyTransferEnd;
import com.kdars.AnnoTask.Server.Command.Server2Client.SendDocumentCount;
import com.kdars.AnnoTask.Server.Command.Server2Client.TermTransfer;

import flexjson.JSONDeserializer;
import flexjson.JSONSerializer;

public class UserControl extends Thread{
	private Socket	socket;
	private BufferedReader input;
	private BufferedWriter output;
//	private DataInputStream input;
//	private DataOutputStream output;
	private int userID;
	private boolean bValidConnection = true;
	
	private ArrayList<Document> 	requestDocs;
	public UserControl(Socket socket, int userID){
		this.socket = socket;
		this.userID = userID;
//		this.userID = socket.getInetAddress().getAddress();
				
		try {
			input = new BufferedReader(new InputStreamReader(socket.getInputStream()));
//			input = new BufferedReader(new InputStreamReader(socket.getInputStream(), "UTF8"));
//			input = new DataInputStream(socket.getInputStream());
//			output = new BufferedWriter(new OutputStreamWriter(socket.getOutputStream()));
			output = new BufferedWriter(new OutputStreamWriter(socket.getOutputStream(), "UTF8"));
//			output = new DataOutputStream(socket.getOutputStream());
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
		while(bValidConnection){
			String commandFromUser = commandFromUser();
			commandParser(commandFromUser);
		}
		System.out.println(socket.getInetAddress().toString() + " : 접속종료.");
	}

	private void commandToUser(Object commandToUser) {
		
	}

	private void commandParser(String commandFromUser) {
		// 1-1 처리.
		if (commandFromUser.contains("startDate")){
//			System.out.println(commandFromUser);
			RequestByDate requestByDate = new JSONDeserializer<RequestByDate>().deserialize(commandFromUser, RequestByDate.class);
//			RequestByDate requestByDate = new JSONDeserializer<RequestByDate>().use(null, RequestTermTransfer.class).deserialize(commandFromUser);
			requestByDateHandler(requestByDate);
		}
		//1-3 처리.
		if (commandFromUser.contains("bTransfer")){
			RequestTermTransfer requestTermTransfer = new JSONDeserializer<RequestTermTransfer>().deserialize(commandFromUser, RequestTermTransfer.class);
			requestTermTransferHandler(requestTermTransfer);
		}
		//2-1 처리.
		if (commandFromUser.contains("documentID")){
			DocumentRequest documentRequest = new JSONDeserializer<DocumentRequest>().deserialize(commandFromUser, DocumentRequest.class);
			documentRequestHandler(documentRequest);
		}
	}

	private void documentRequestHandler(DocumentRequest documentRequest) {
		Document document = ContentDBManager.getInstance().getContent(documentRequest.documentID);
		
		DocumentResponse documentRes = new DocumentResponse();
		documentRes.body = document.getBody();
		documentRes.category = document.getCategory();
		documentRes.collectDate = document.getCollectDate();
		documentRes.comment = document.getComment();
		documentRes.crawlerVersion = document.getCrawlerVersion();
		documentRes.documentID = document.getDocumentID();
		documentRes.newsDate = document.getNewsDate();
		documentRes.pressName = document.getPressName();
		documentRes.siteName = document.getSiteName();
		documentRes.title = document.getTitle();
		documentRes.url = document.getUrl();
		
		transferObject(documentRes);
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
			termTransfer.docTitle = doc.getTitle();
			
			DocByTerm[] docByTerm = TermFreqDBManager.getInstance().getDocByTerm(doc.getDocumentID(), doc.getCategory(), doc.getTitle());
			
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
			termTransfer.docTitle = docByTerm[index].getTitle();
			termTransfer.termsJson = new JSONSerializer().exclude("*.class").serialize(docByTerm[index]);
			
//			System.out.println(termTransfer.termsJson);
			
			transferObject(termTransfer);
		}		
	}

	private void transferObject(Object obj) {
		String json = new JSONSerializer().exclude("*.class").serialize(obj);
		
		try {
//			output.writeUTF(json);
			output.write(json + "\r\n");
			output.flush();
		} catch (IOException e) {
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
		String command = " ";
		try {
//			command = input.readUTF();
			command = input.readLine();
		} catch (IOException e) {
			// TODO Auto-generated catch block
//			e.printStackTrace();
			bValidConnection = false;
		}
		return command;
	}
	
	
}
