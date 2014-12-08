package com.kdars.AnnoTask.DB;

import com.kdars.AnnoTask.ContextConfig;

public class TermFreqDBManager {
	private static TermFreqDBManager thisClass = new TermFreqDBManager();
	private TermFreqDBConnector termFreqDB;
	
	public TermFreqDBManager(){
		termFreqDB = new TermFreqDBConnector();
	}
	
	public static TermFreqDBManager getInstance(){
		return	thisClass;
	}
	
	public TermFreqByDoc getTerm(String term, int termHolder){
		TermFreqByDoc	termByDoc = new TermFreqByDoc(term, termHolder);
		// termByDoc 체우는 로직 필요,,,,,
		termLock(term, termHolder);
		// 체우면서 DB에 락걸어주어야함,
		
		return termByDoc;
	}
	
	public boolean deleteTerm(String term, int termHolder){
		// 소유자 확인해서 일치할때만 삭제.
		
		return	termFreqDB.deleteTerm(term, termHolder);
	}
	
	public DocTermFreqByTerm[] getDocByTerm(int docID, String category, String title){
		int nGramNumber = ContextConfig.getInstance().getN_Gram();
		DocTermFreqByTerm[] docByTerm = new DocTermFreqByTerm[nGramNumber];
		
		// 임시로 카테고리 지정안함....
		for (int nGramIndex = 0; nGramIndex < nGramNumber; nGramIndex++){
			docByTerm[nGramIndex] = new DocTermFreqByTerm(docID, nGramIndex + 1, category);
			docByTerm[nGramIndex].setTitle(title);
			termFreqDB.queryTermFreqByDocIDandNGram(docByTerm[nGramIndex]);
		}
		
		return	docByTerm;
	}
	
	public boolean addDocByTerm(DocTermFreqByTerm docByTerm){
		// DB에 체우기, 채우기 실패하면 false, 성공하면 true.
		return termFreqDB.addDoc(docByTerm);
	}
	
	
	public boolean termUnlock(int termHolder){
		//if (현재 쓰레드랑 똑같은 지 확인하고 확인되면,)
		termFreqDB.resetTermState(termHolder);
		return true;
	}
	
	public boolean termLock(String term, int termHolder){
		return termFreqDB.updateTermLockState(term, termHolder);
	}
}
