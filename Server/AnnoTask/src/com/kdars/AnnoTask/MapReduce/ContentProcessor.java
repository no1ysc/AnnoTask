package com.kdars.AnnoTask.MapReduce;

import com.kdars.AnnoTask.GlobalContext;
import com.kdars.AnnoTask.DB.ContentDBManager;
import com.kdars.AnnoTask.DB.DocByTerm;
import com.kdars.AnnoTask.DB.Document;
import com.kdars.AnnoTask.DB.TermFreqDBManager;

public class ContentProcessor extends Thread{
//	private NgramFilter ngramFilter;	// TODO: 저쪽으로 가야할듯., 유저가 가저가기 전,,,,,,에서 하는걸로,.
	private int nGram;
	private Document document = null;
	
	public enum ProcessState{
		Ready, Running, Completed 
	}
	private ProcessState state;
		
	private JobResultTable	jobResultTable;
	
	public ContentProcessor(int docID){
		document = ContentDBManager.getInstance().getContent(docID);
				
		this.state = ProcessState.Ready;
	}

	public void run(){
		this.state = ProcessState.Running;

		Tokenizer tokenizer = new Tokenizer();
		DuplicationChecker dupChecker = new DuplicationChecker();
		StopWordRemover stopWordRemover = new StopWordRemover();
	
//		for (int docID = this.startDocIDIndex; docID <= this.endDocIDIndex; docID++){
//			Document document = ContentDBManager.getInstance().getContent(docID);
//			
//			extractTermStructure(tokenizer, dupChecker, stopWordRemover, document);
//		}
		
		this.state = ProcessState.Completed;
	}

	private void extractTermStructure(	Tokenizer tokenizer, 
										DuplicationChecker dupChecker, 
										StopWordRemover stopWordRemover,
										Document document) {
		for (int ngram = 0; ngram < this.nGram; ngram++){
			DocByTerm docByTerm = tokenizer.termGenerate(document, ngram);
			
			for (String term : docByTerm.keySet()){
//				if (stopWordRemover.isStopWord(term) || dupChecker.checkDuplication(term)){
//					continue;
//				}
				
				TermFreqDBManager.getInstance().addDocByTerm(docByTerm);
			}
		}
	}
	
	public ProcessState getProcessState(){
		return state;
	}
	
	public Document getDocument(){
		return document;
	}
}
