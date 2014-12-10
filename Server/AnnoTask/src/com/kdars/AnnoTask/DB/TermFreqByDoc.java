package com.kdars.AnnoTask.DB;

import java.util.HashMap;

public class TermFreqByDoc extends HashMap<Integer, Integer>{
	// docID, frequency
	private int termHolder;
	private String term;
	private int nGram;
	private int termFreq4RequestedCorpus;
	/*
	 * (진규) phase 2.5에서부터 NgramFilter 구현 방식 바뀜.
	 * 구현 계획 1: partialSum은 client로부터 요청된 doc들안에 있는 특정 term의 빈도수를 더한 값으로, nGramFilter진행할 때 set할 예정.
	 * 구현 계획 2: getTerm은 DB단에서 TermFreqByDoc을 만들어 올려보내기 위해 term확인이 필요하여 생성.
	 */
	public TermFreqByDoc(String term, int nGram, int termHolder){
		this.term = term;
		this.nGram = nGram;
		this.termHolder = termHolder;
	}
	
	public void setTermFreq4RequestedCorpus(int sum){
		this.termFreq4RequestedCorpus = sum;
	}
	
	public void setTermHolder(int termHolder){
		this.termHolder = termHolder;
	}
	
	public int getNgram(){
		return this.nGram;
	}
	
	public int getTermFreq4RequestedCorpus(){
		return this.termFreq4RequestedCorpus;
	}
	
	public String getTerm(){
		return this.term;
	}
	
	public int getTermHolder(){
		return this.termHolder;
	}
}
