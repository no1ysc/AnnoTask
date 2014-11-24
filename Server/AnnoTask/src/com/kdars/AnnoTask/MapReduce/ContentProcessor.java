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
	
	public enum ProcessState{
		Ready, Running, Completed 
	}
	private ProcessState state;
	
	public ContentProcessor(int docID){
		document = ContentDBManager.getInstance().getContent(docID);
				
		this.state = ProcessState.Ready;
	}

	public void run(){
		this.state = ProcessState.Running;

		Tokenizer tokenizer = new Tokenizer();
		DuplicationChecker dupChecker = new DuplicationChecker();
		StopWordRemover stopWordRemover = new StopWordRemover();
	
		extractTermStructure(tokenizer, dupChecker, stopWordRemover, document);
		
		this.state = ProcessState.Completed;
		System.out.println("completed");
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
