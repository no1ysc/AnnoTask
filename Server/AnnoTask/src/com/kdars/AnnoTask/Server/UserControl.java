package com.kdars.AnnoTask.Server;

import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.OutputStreamWriter;
import java.net.Socket;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;
import java.util.HashMap;
import java.util.Iterator;
import java.util.Map;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

import com.kdars.AnnoTask.ContextConfig;
import com.kdars.AnnoTask.DB.ConceptToList;
import com.kdars.AnnoTask.DB.ContentDBManager;
import com.kdars.AnnoTask.DB.DeleteListDBManager;
import com.kdars.AnnoTask.DB.DocTermFreqByTerm;
import com.kdars.AnnoTask.DB.Document;
import com.kdars.AnnoTask.DB.LinkedList;
import com.kdars.AnnoTask.DB.TermFreqByDoc;
import com.kdars.AnnoTask.DB.TermFreqDBManager;
import com.kdars.AnnoTask.DB.ThesaurusDBManager;
import com.kdars.AnnoTask.Server.Command.Client2Server.DocumentRequest;
import com.kdars.AnnoTask.Server.Command.Client2Server.RequestAddDeleteList;
import com.kdars.AnnoTask.Server.Command.Client2Server.RequestAddThesaurus;
import com.kdars.AnnoTask.Server.Command.Client2Server.RequestAnnoTaskWork;
import com.kdars.AnnoTask.Server.Command.Client2Server.RequestByDate;
import com.kdars.AnnoTask.Server.Command.Client2Server.RequestDocMeta;
import com.kdars.AnnoTask.Server.Command.Client2Server.RequestGetLinkedList;
import com.kdars.AnnoTask.Server.Command.Client2Server.RequestTermTransfer;
import com.kdars.AnnoTask.Server.Command.Server2Client.ConceptListResponse;
import com.kdars.AnnoTask.Server.Command.Server2Client.DocMetaTransfer;
import com.kdars.AnnoTask.Server.Command.Server2Client.DocumentResponse;
import com.kdars.AnnoTask.Server.Command.Server2Client.MetaResponse;
import com.kdars.AnnoTask.Server.Command.Server2Client.NotifyTransferEnd;
import com.kdars.AnnoTask.Server.Command.Server2Client.SendConceptToCount;
import com.kdars.AnnoTask.Server.Command.Server2Client.SendDocumentCount;
import com.kdars.AnnoTask.Server.Command.Server2Client.TermTransfer;
import com.kdars.AnnoTask.Server.Command.Server2Client.SendLinkedListCount;
import com.kdars.AnnoTask.Server.Command.Server2Client.LinkedListResponse;
import com.kdars.AnnoTask.Server.Command.Server2Client.TotalTermCount;

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
	
	private ArrayList<Document> 		requestDocs;
	private ArrayList<ConceptToList>	conceptLists;
	private ArrayList<LinkedList> 		linkedLists;
	private ArrayList<Integer>			workingDocIds;
	
	public UserControl(Socket socket, int userID){
		this.socket = socket;
		this.userID = userID;
//		this.userID = socket.getInetAddress().getAddress();
		System.out.println(socket.getInetAddress().getAddress().toString() + "은 User ID " + userID + "로 접속하였습니다.");
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
		System.out.println("유저 " + userID + "(" + socket.getInetAddress().toString() + ")이  접속을 종료하였습니다.");
	}

	private void commandToUser(Object commandToUser) {
		
	}

	private void commandParser(String commandFromUser) {
		
		/*
		 * (기흥) phase2.5 새로 정책을 바꿔서 짤 코드
		 * 구현 계획 1: Client로 부터 작업 요청시 서버는 contentdb.job_table에서 client_jobstatus가 0인 녀석 중 10개의 doc_id를 가져오도록 한다. 가져오면 해당 status를 1로 업데이트.
		 * 구현 계획 2: 가져온 doc_id를 가지고 termfreqdb.tftable에 쿼리를 하여 doc_id에 해당하는 term들을 가져와서 DocByTerm[]를 만들도록 한다. 가져온 term들은 모두 lock하는 것으로...
		 * 구현 계획 3: DocByTerm[] 만들 때, doc 내 term freq.를 모두 계산해야 하며 ngram filter 로직을 태워야 함. 그리고 최종적으로 보낼 DocByTerm[]를 준비한다. 이 때 10개 문서에서의 cumulative한 term freq.를 보낼 자료구조를 만들어야함. 
		 * 구현 계획 4: 최종적으로 보낼 DocByTerm[]가 나오면 Client쪽으로 보내도록 한다.
		 */
		
		// 구현 계획 1
		if (commandFromUser.contains("bRequestAnnoTaskWork")){
			RequestAnnoTaskWork requestAnnoTaskWork = new JSONDeserializer<RequestAnnoTaskWork>().deserialize(commandFromUser, RequestAnnoTaskWork.class);
			termUnlock(userID); // 기흥: Client에서 작업 요청 시 이전 Lock되었던 Term들을 모두 Unlock 하도록 한다.
			requestAnnoTaskWork(requestAnnoTaskWork);			
		}
		
		
		// 문서 제목 및 카테고리 요청시
		if (commandFromUser.contains("termLinkedDocIds")){
//			System.out.println(commandFromUser);
			RequestDocMeta requestDocMeta = new JSONDeserializer<RequestDocMeta>().deserialize(commandFromUser, RequestDocMeta.class);
			requestDocMetaHandler(requestDocMeta);
		}
		//1-3 처리. --> phase2.5 구현 계획 2
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
			RequestAddDeleteList requestedDeleteList = new JSONDeserializer<RequestAddDeleteList>().deserialize(commandFromUser, RequestAddDeleteList.class);
			deleteListRequestHandler(requestedDeleteList);
		}
		
		// 사전 추가 요청시
		if(commandFromUser.contains("conceptFrom")){
			RequestAddThesaurus requestedThesaurus = new JSONDeserializer<RequestAddThesaurus>().deserialize(commandFromUser, RequestAddThesaurus.class); //TODO: String.class가 맞는지 확인해야함...
			thesaurusRequestHandler(requestedThesaurus);
		}
		
		// concept to list 요청시
		if(commandFromUser.contains("RequestConceptToList")){
			requestConceptToList();
		}
		
		// linked list 요청시
		if(commandFromUser.contains("RequestLinkedList")){
			RequestGetLinkedList requestedLinkedList = new JSONDeserializer<RequestGetLinkedList>().deserialize(commandFromUser, RequestGetLinkedList.class); //TODO: String.class가 맞는지 확인해야함...
			requestLinkedList(requestedLinkedList);
		}
		
	}
	
	// 트리뷰 요청 시
	private void requestDocMetaHandler(RequestDocMeta requestDocMeta) {
		DocMetaTransfer documentMeta = new DocMetaTransfer();
		documentMeta = ContentDBManager.getInstance().getDocMeta(requestDocMeta.termLinkedDocIds);
		String transferJSON = new JSONSerializer().exclude("*.class").serialize(documentMeta);
		transferObject(transferJSON);
	}

	// (기흥) 구현 계획 1
	private void requestAnnoTaskWork(RequestAnnoTaskWork requestAnnoTaskWork) {		
		this.workingDocIds = ContentDBManager.getInstance().getClientJobCandidates();		
		SendDocumentCount sendDocumentCount = new SendDocumentCount();
		sendDocumentCount.doucumentCount = workingDocIds.size();		
		transferObject(sendDocumentCount);
	}

	private void deleteListRequestHandler(RequestAddDeleteList requestedDeleteList) {
		for (String deleteTerm : requestedDeleteList.addDeleteList) {
			DeleteListDBManager.getInstance().AddTermToDelete(deleteTerm);
			TermFreqDBManager.getInstance().deleteTerm(deleteTerm, userID);
		}
		
	}
	
	private void thesaurusRequestHandler(RequestAddThesaurus entryComponents) {
		String conceptFrom = entryComponents.conceptFrom;
		String conceptTo = entryComponents.conceptTo;
		String metaOntology = entryComponents.metaOntology;
		ThesaurusDBManager.getInstance().setEntry(conceptFrom, conceptTo, metaOntology);
		TermFreqDBManager.getInstance().deleteTerm(conceptFrom, userID);
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

	// (기흥) 구현 계획 2
	private void requestTermTransferHandler(RequestTermTransfer requestTermTransfer) {
	
		// (기흥) nGramFilter argument를 this.workingDocIds만 넘겨주는 걸로 수정해야함... 여기서부터는 진규가 개발 예정.
		long start_time = System.currentTimeMillis();
		ArrayList<TermFreqByDoc> filteredTermByDocList = nGramFilter(workingDocIds);
		System.out.println("ngram filter 시간: " + ((System.currentTimeMillis() - start_time) / (1000))); // ngram 로직이 몇 분 걸리나 체크.
		// (진규) filteredTermByDocList.size() == 총 보낼 term의 갯수 보내기.
		TotalTermCount totalTermCount = new TotalTermCount();
		totalTermCount.totalTermCount = filteredTermByDocList.size();
		transferObject(totalTermCount);
		
		start_time = System.currentTimeMillis();
		for (TermFreqByDoc TermByDoc : filteredTermByDocList){
			TermTransfer termTransfer = new TermTransfer();
			termTransfer.term = TermByDoc.getTerm();
			termTransfer.termFreq4RequestedCorpus = TermByDoc.getTermFreq4RequestedCorpus();
			termTransfer.nGram = TermByDoc.getNgram();
			termTransfer.termsJson = new JSONSerializer().exclude("*.class").serialize(TermByDoc);
		
			// (진규) 단어 하나씩 TermFreqByDoc 구조에 담아서 보내기.
			transferObject(termTransfer);
		}
		System.out.println("for문 시간: " + ((System.currentTimeMillis() - start_time) / (1000)));
		
		// 전송완료 알림.
		NotifyTransferEnd notifyTransferEnd = new NotifyTransferEnd();
		notifyTransferEnd.flagEnd = true;
		
		transferObject(notifyTransferEnd);
	}
	
	/*
	 * (진규) phase 2.5에서부터 NgramFilter 구현 방식 바뀜.
	 * 구현 계획 1: DB단에서는 docID List를 받아서 TermFreqByDoc을 만들어서 리스트로 묶은 다음 보내준다.
	 * 구현 계획 2: Ngramfilter 진행 시, DB단에서 준 TermFreqByDoc list를 받아 각 TermFreqByDoc의 hashMap value들을 더해 2보다 작은 경우 TermFreqByDoc에서 remove하고, Filter된 list를 결과로 보내준다. 
	 * 구현 계획 3: filter되는 term들에 대해서도 termLock을 하기 위해 ngramfilter에서 요청된 DocID들에 물린 모든 term들을 termHolder로 lock한다.
	 */
	private ArrayList<TermFreqByDoc> nGramFilter(ArrayList<Integer> docIdList){
		
		ArrayList<TermFreqByDoc> filtering = TermFreqDBManager.getInstance().getTermConditional(docIdList.get(0));
		TermFreqDBManager.getInstance().termLock(docIdList, userID);

		for (int i = filtering.size()-1; i >= 0; i--){
			TermFreqByDoc termFreqByDocFilter = filtering.get(i);
	
			//termFreqByDocFilter.setTermHolder(userID);
			//int termFreqSum = TermFreqDBManager.getInstance().sumTermFrequency(termFreqByDocFilter.getTerm(),docIdList.get(0));;
			
			int termFreqSum = 0;
			for (int termFreq : termFreqByDocFilter.values()){
				termFreqSum = termFreqSum + termFreq;
			}
			
			if (termFreqByDocFilter.getNgram() == 1 || termFreqSum > 1){
				termFreqByDocFilter.setTermFreq4RequestedCorpus(termFreqSum);
				continue;
			}
			
			filtering.remove(termFreqByDocFilter);
			
		}

		return filtering;
	}
		
	//(진규) phase 2.5에서부터 NgramFilter 구현 방식 바뀜.	
//		int nGramNumber = ContextConfig.getInstance().getN_Gram();
//		HashMap<String, Integer> termHash = new HashMap<String, Integer>();
//		ArrayList<DocTermFreqByTerm[]> docByTermList = new ArrayList<DocTermFreqByTerm[]>();
//		for (Document doc : requestDocs){
//			DocTermFreqByTerm[] docByTerm = TermFreqDBManager.getInstance().getDocByTerm(doc.getDocumentID(), doc.getCategory(), doc.getTitle());
//			docByTermList.add(docByTerm);
//			for (int nGramIndex = 1; nGramIndex < nGramNumber; nGramIndex++){
//				for (String term : docByTerm[nGramIndex].keySet()){
//					
//					if (termHash.containsKey(term)){
//						int priorValue = termHash.get(term);
//						termHash.replace(term, priorValue, priorValue + docByTerm[nGramIndex].get(term));
//					} else {
//						termHash.put(term, docByTerm[nGramIndex].get(term));
//					}
//					
//				}
//			}
//		}
//		
//		for (String appendTerm : termHash.keySet()){
//			if (termHash.get(appendTerm) < 2){
//				int whiteSpaceCount = 0;
//				Pattern p = Pattern.compile(" ");
//				Matcher m = p.matcher(appendTerm);
//				while(m.find()){
//					m.start();
//					whiteSpaceCount++;
//				}
//				for (DocTermFreqByTerm[] docTermFreqByTerm : docByTermList){
//					docTermFreqByTerm[whiteSpaceCount].remove(appendTerm);
//				}
//			} else {
//			
//				
//			}
//		}
//		
//	return docByTermList;	
//	}
	
	
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
	
	private void requestConceptToList() {

		this.conceptLists = ThesaurusDBManager.getInstance().getConceptToList();

		SendConceptToCount sendConceptToCount = new SendConceptToCount();
		sendConceptToCount.conceptToCount = conceptLists.size();
		transferObject(sendConceptToCount);
		
		
		for(int idx = 0; idx <sendConceptToCount.conceptToCount; ++idx){
			ConceptToList conceptTo = conceptLists.get(idx);		
			ConceptListResponse temp = new ConceptListResponse(conceptTo.conceptToId, conceptTo.conceptToTerm);
			transferObject(temp);
		}
	}
	
	private void requestLinkedList(RequestGetLinkedList requestedLinkedList) {

		String id = requestedLinkedList.conceptToId;
		this.linkedLists = ThesaurusDBManager.getInstance().getLinkedList(id);
		
		SendLinkedListCount sendLinkedListCount = new SendLinkedListCount();
		sendLinkedListCount.linkedListCount = linkedLists.size();
		transferObject(sendLinkedListCount);
		
		
		for(int idx = 0; idx <linkedLists.size(); ++idx){
			LinkedList linkedList = linkedLists.get(idx);		
			LinkedListResponse temp = new LinkedListResponse(linkedList.linkedTerm);
			transferObject(temp);
		}	
		
		String metaInfo = ThesaurusDBManager.getInstance().getMetaInfo(id);
		
		MetaResponse meta = new MetaResponse(metaInfo);
		transferObject(meta);
	}
	
	//(진규) phase 2.5에서부터 NgramFilter 구현 방식 바뀜.
//	private void transferDocbyTerm(DocTermFreqByTerm[] docByTerm) {
//		for (int index = 0; index < docByTerm.length; index++){
//			TermTransfer termTransfer = new TermTransfer();
//			termTransfer.docCategory = docByTerm[index].getDocCategory();
//			termTransfer.docID = docByTerm[index].getDocID();
//			termTransfer.ngram = docByTerm[index].getNGram();
//			termTransfer.docTitle = docByTerm[index].getTitle();
//			termTransfer.termsJson = new JSONSerializer().exclude("*.class").serialize(docByTerm[index]);
//			
////			System.out.println(termTransfer.termsJson);
//			
//			transferObject(termTransfer);
//		}		
//	}

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

	//(진규) phase 2.5에서부터 NgramFilter 구현 방식 바뀜.
//	private void termLockInDoc(TermFreqByDoc TermByDoc) {	
//		for (int index = 0; index < docByTerm.length; index++){
//			
//			for (Iterator<Map.Entry<String, Integer>> iter = docByTerm[index].entrySet().iterator(); iter.hasNext();){
//				Map.Entry<String, Integer> entry = iter.next();
//				String term = entry.getKey();
//				if (!TermFreqDBManager.getInstance().termLock(term, userID)){
//					iter.remove();
//				}
//			}
//		}
//	}
	
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
