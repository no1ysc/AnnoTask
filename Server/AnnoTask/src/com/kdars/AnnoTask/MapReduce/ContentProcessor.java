package com.kdars.AnnoTask.MapReduce;

import java.sql.Timestamp;
import java.util.Iterator;
import java.util.Map;

import com.kdars.AnnoTask.ContextConfig;
import com.kdars.AnnoTask.DB.ContentDBManager;
import com.kdars.AnnoTask.DB.DocTermFreqByTerm;
import com.kdars.AnnoTask.DB.Document;
import com.kdars.AnnoTask.DB.TermFreqDBManager;

public class ContentProcessor extends Thread{
//	private NgramFilter ngramFilter;	// TODO: 저쪽으로 가야할듯., 유저가 가저가기 전,,,,,,에서 하는걸로,.
	private int nGram = ContextConfig.getInstance().getN_Gram();
	private Document document = null;
	private long processStartTime;
	
	private int docID;
	public int getDocID(){
		return this.docID;
	}
	
	public enum ProcessState{
		Ready, Running, Completed, EmptyDoc
	}
	private ProcessState state;
	
	public ContentProcessor(int docID){
		this.docID = docID;
		document = ContentDBManager.getInstance().getContent(docID);
		
		this.state = ProcessState.Ready;
		processStartTime = System.currentTimeMillis();
	}
	
	public void run(){
		this.state = ProcessState.Running;
		System.out.println("System now parsing document id : "+String.valueOf(docID));
		if(document != null){
			Tokenizer tokenizer = new Tokenizer();
			DuplicationChecker dupChecker = new DuplicationChecker();
			StopWordRemover stopWordRemover = new StopWordRemover();
			extractTermStructure(tokenizer, dupChecker, stopWordRemover, document);
		}else{
			this.state = ProcessState.EmptyDoc;
		}
		
		this.state = ProcessState.Completed;
		System.out.println(document.getDocumentID() + " is completed now");
	}
	
	/**
	 * 프로세스가 정해진 시간동안 끝나지 않으면 종료하기 위한 Checker 리턴.
	 * @author JS
	 * @return true : 종료대상, false :  
	 */
	public boolean checkOvertime(){
		if ( (System.currentTimeMillis() - processStartTime) > ContextConfig.getInstance().getLimitTimeSec() * 1000){
			return true;
		}
		
		return false;
	}

	private void extractTermStructure(	Tokenizer tokenizer, 
														DuplicationChecker dupChecker, 
														StopWordRemover stopWordRemover,
														Document document) {
			
			DocTermFreqByTerm[] docByTermList = tokenizer.termGenerate(document, this.nGram);
			for (int i = 0; i < this.nGram; i++ ){
				
				for (Iterator<Map.Entry<String, Integer>> iter = docByTermList[i].entrySet().iterator(); iter.hasNext();){
					Map.Entry<String, Integer> entry = iter.next();
					String keyCheck = entry.getKey();
					
					if (stopWordRemover.isStopWord(keyCheck) || dupChecker.duplicationCheck(keyCheck)){
						iter.remove();
						continue;
						
					}
					
				}
				
			
				
			TermFreqDBManager.getInstance().addDocByTerm(docByTermList[i]);
			}
			
	}
	
	public ProcessState getProcessState(){
		return state;
	}
	
	public Document getDocument(){
		return document;
	}
	
	public boolean rollbackWorkingStatus(int docID){
		ContentDBManager.getInstance().updateWorkingStatus(docID, 0);
		return false;
	}
}
