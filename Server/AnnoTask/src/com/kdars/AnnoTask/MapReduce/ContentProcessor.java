package com.kdars.AnnoTask.MapReduce;

import com.kdars.AnnoTask.GlobalContext;
import com.kdars.AnnoTask.DB.ContentDBManager;
import com.kdars.AnnoTask.DB.DocByTerm;
import com.kdars.AnnoTask.DB.Document;

public class ContentProcessor extends Thread{
	private DuplicationChecker dupChecker;
//	private NgramFilter ngramFilter;	// TODO: 저쪽으로 가야할듯., 유저가 가저가기 전,,,,,,에서 하는걸로,.
	private int startDocIDIndex;
	private int endDocIDIndex;
	
	private JobResultTable	jobResultTable;
	
	public ContentProcessor(int startDocID, int count){
		this.startDocIDIndex = startDocID;
		this.endDocIDIndex = startDocID + count;
		
		jobResultTable = new JobResultTable();
	}
	
	public JobResultTable getJobResultTable(){
		return	this.jobResultTable;
	}
	
	public void run(){
		Tokenizer tokenizer = new Tokenizer();
		DuplicationChecker dupChecker = new DuplicationChecker();
		StopWordRemover stopWordRemover = new StopWordRemover();
		for (int docID = this.startDocIDIndex; docID <= this.endDocIDIndex; docID++){
			Document document = ContentDBManager.getInstance().getContent();
			
			DocByTerm[]	docByTerm = new DocByTerm[GlobalContext.getInstance().getN_Gram()];
			for (int ngram = 0; ngram < GlobalContext.getInstance().getN_Gram(); ngram++){
				//TODO: 반드시 고쳐야함!
				docByTerm[ngram] = tokenizer.termGenerate(document, ngram);
				stopWordRemover.remove(docByTerm[ngram]);
				dupChecker.checkDuplication(docByTerm[ngram]);
				jobResultTable.add();
			}
		}
	}
}
