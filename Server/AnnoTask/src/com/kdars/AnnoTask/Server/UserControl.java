package com.kdars.AnnoTask.Server;

import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.OutputStreamWriter;
import java.net.Socket;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.Iterator;
import java.util.Map;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

import com.kdars.AnnoTask.ContextConfig;
import com.kdars.AnnoTask.DB.ContentDBManager;
import com.kdars.AnnoTask.DB.DeleteListDBManager;
import com.kdars.AnnoTask.DB.DocTermFreqByTerm;
import com.kdars.AnnoTask.DB.Document;
import com.kdars.AnnoTask.DB.TermFreqDBManager;
import com.kdars.AnnoTask.DB.ThesaurusDBManager;
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
		System.out.println(socket.getInetAddress().getAddress() + "은 User ID " + userID + "로 접속하였습니다.");
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
			if(commandFromUser == null){
				break;
			}
			commandParser(commandFromUser);
		}
		termUnlock(userID);
		System.out.println(userID + "(" + socket.getInetAddress().toString() + ")" + " 님이  접속 종료하였습니다.");
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
		
		// 불용어 추가 요청시
		if(commandFromUser.contains("addDeleteList")){
			ArrayList<String> requestedDeleteList = new JSONDeserializer<ArrayList<String>>().deserialize(commandFromUser, String.class); //TODO: String.class가 맞는지 확인해야함...
			deleteListRequestHandler(requestedDeleteList);
		}
		
		// 사전 추가 요청시
		if(commandFromUser.contains("addThesaurus")){
			ArrayList<String> requestedThesaurus = new JSONDeserializer<ArrayList<String>>().deserialize(commandFromUser, String.class); //TODO: String.class가 맞는지 확인해야함...
			thesaurusRequestHandler(requestedThesaurus);
		}
		
	}

	private void deleteListRequestHandler(ArrayList<String> requestedDeleteList) {
		for(int index = 0; index < requestedDeleteList.size(); index++){
			DeleteListDBManager.getInstance().AddTermToDelete(requestedDeleteList.get(index));
		}
		
	}
	
	private void thesaurusRequestHandler(ArrayList<String> entryComponents) {
		String conceptFrom = entryComponents.get(0);
		String conceptTo = entryComponents.get(1);
		String metaOntology = entryComponents.get(2);
		ThesaurusDBManager.getInstance().setEntry(conceptFrom, conceptTo, metaOntology);
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

	private void requestTermTransferHandler(RequestTermTransfer requestTermTransfer) {
		// TODO 문서전송 시작.!
		
		/*
		 * for `~~~
		 * 1. DOC ID set을 탐색하여 Term Freq DB 뒤짐.
		 * 2. 텀 락 걸어야됨.
		 * 3. DocByTerm 형태로 변환하여 전송.
		 * for........
		 */
		
		ArrayList<DocTermFreqByTerm[]> filteredDocByTermList = nGramFilter(requestDocs);
		
		for (DocTermFreqByTerm[] docByTerm : filteredDocByTermList){
			TermTransfer termTransfer = new TermTransfer();
			termTransfer.docID = docByTerm[0].getDocID();
			termTransfer.docCategory = docByTerm[0].getDocCategory();
			termTransfer.docTitle = docByTerm[0].getTitle();
			
			termLockInDoc(docByTerm);
			
			transferDocbyTerm(docByTerm);
		}
		
//		for (Document doc : requestDocs){
//			TermTransfer termTransfer = new TermTransfer();
//			termTransfer.docID = doc.getDocumentID();
//			termTransfer.docCategory = doc.getCategory();
//			termTransfer.docTitle = doc.getTitle();
//			
//			DocTermFreqByTerm[] docByTerm = TermFreqDBManager.getInstance().getDocByTerm(doc.getDocumentID(), doc.getCategory(), doc.getTitle());
//			
//			termLockInDoc(docByTerm);
//			
//			// 전송
//			transferDocbyTerm(docByTerm);
//		}
		
		// 전송완료 알림.
		NotifyTransferEnd notifyTransferEnd = new NotifyTransferEnd();
		notifyTransferEnd.flagEnd = true;
		
		transferObject(notifyTransferEnd);
	}
	
	private ArrayList<DocTermFreqByTerm[]> nGramFilter(ArrayList<Document> requestDocs){
		int nGramNumber = ContextConfig.getInstance().getN_Gram();
		HashMap<String, Integer> termHash = new HashMap<String, Integer>();
		ArrayList<DocTermFreqByTerm[]> docByTermList = new ArrayList<DocTermFreqByTerm[]>();
		ArrayList<String> filterTermList = new ArrayList<String>();
		for (Document doc : requestDocs){
			DocTermFreqByTerm[] docByTerm = TermFreqDBManager.getInstance().getDocByTerm(doc.getDocumentID(), doc.getCategory(), doc.getTitle());
			docByTermList.add(docByTerm);
			for (int nGramIndex = 0; nGramIndex < nGramNumber; nGramIndex++){
				for (String term : docByTerm[nGramIndex].keySet()){
					
					if (termHash.containsKey(term)){
						termHash.put(term, termHash.get(term) + docByTerm[nGramIndex].get(term));
					} else {
						termHash.put(term, docByTerm[nGramIndex].get(term));
					}

					if (termHash.containsKey(term) && termHash.get(term) < 2){
						termHash.remove(term);
						filterTermList.add(term);
					}
					
				}
			}
		}
		
		for (String filterTerm : filterTermList){
			int whiteSpaceCount = 0;
			Pattern p = Pattern.compile(" ");
			Matcher m = p.matcher(filterTerm);
			while(m.find()){
				m.start();
				whiteSpaceCount++;
			}
			for (DocTermFreqByTerm[] docTermFreqByTerm : docByTermList){
				docTermFreqByTerm[whiteSpaceCount].remove(filterTerm);
			}
		}
		
	return docByTermList;	
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
			sendDocumentCount.doucumentCount = requestDocs.size() * ContextConfig.getInstance().getN_Gram();
			
			transferObject(sendDocumentCount);
	}

	private void transferDocbyTerm(DocTermFreqByTerm[] docByTerm) {
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

	private void termLockInDoc(DocTermFreqByTerm[] docByTerm) {
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
	
	private void termUnlock(int userID) {
		// TODO Auto-generated method stub
		TermFreqDBManager.getInstance().termUnlock(userID);
	}

	private String commandFromUser() {
		String command = " ";
		try {
//			command = input.readUTF();
			command = input.readLine();
//			if(command.isEmpty()){
//				bValidConnection = false;
//			}
		} catch (NullPointerException | IOException e) {
			// TODO Auto-generated catch block
//			e.printStackTrace();
			bValidConnection = false;
		}
		return command;
	}
	
	
}
