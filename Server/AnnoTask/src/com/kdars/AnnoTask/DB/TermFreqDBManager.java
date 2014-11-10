package com.kdars.AnnoTask.DB;

public class TermFreqDBManager {
	private static TermFreqDBManager thisClass = new TermFreqDBManager();
	private TermFreqDBConnector termFreqDB;
	
	public TermFreqDBManager(){
		termFreqDB = new TermFreqDBConnector();
	}
	
	public static TermFreqDBManager getInstance(){
		return	thisClass;
	}
	
	public TermByDoc getTerm(String term, int termHolder){
		TermByDoc	termByDoc = new TermByDoc(term, termHolder);
		// termByDoc 체우는 로직 필요,,,,,
		termLock(term, termHolder);
		// 체우면서 DB에 락걸어주어야함,
		
		return termByDoc;
	}
	
	public boolean deleteTerm(String term, int termHolder){
		// 소유자 확인해서 일치할때만 삭제.
		
		return	false;
	}
	
	public DocByTerm getDocByTerm(int docID){
		DocByTerm docByTerm = new DocByTerm(docID, 1,"temp");
		// DocByTerm 체우는 로직 필요,,,,, 
		
		return	docByTerm;
	}
	
	public boolean addDocByTerm(DocByTerm docByTerm){
		// DB에 체우기.
		termFreqDB.addDoc(docByTerm);
		return false;
	}
	
	
	public boolean termUnlock(String term, int termHolder){
		//if (현재 쓰레드랑 똑같은 지 확인하고 확인되면,)
		termFreqDB.updateTermLockState(term, 0);
		return false;
	}
	
	private boolean termLock(String term, int termHolder){
		termFreqDB.updateTermLockState(term, termHolder);
		return	false;
	}
}
