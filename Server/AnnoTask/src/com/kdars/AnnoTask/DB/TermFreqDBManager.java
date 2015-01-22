package com.kdars.AnnoTask.DB;

import java.util.ArrayList;

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
	
	public ArrayList<TermFreqByDoc> getTermConditional(ArrayList<Integer> docIdList){
		return termFreqDB.queryTermConditional(docIdList);
	}
	
	public boolean flagDeleteTerm(String term){
		return termFreqDB.flagDeleteTerm(term);
	}
	
	public boolean deleteTerm(String term){
		return	termFreqDB.deleteTerm(term);
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
	
	public boolean termLock(ArrayList<Integer> doc_id, int termHolder){
		return termFreqDB.updateTermLockState(doc_id, termHolder);
	}

	public int sumTermFrequency(String term, Integer doc_id) {
		return termFreqDB.sumTermFrequency(term, doc_id);
	}
}
