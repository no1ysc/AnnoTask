package com.kdars.AnnoTask.Server.Command.Server2Client;

import com.kdars.AnnoTask.Server.Command.Command;

public class TermTransfer extends Command
{
	/*
	 * (진규) phase 2.5에서부터 NgramFilter 구현 방식 바뀜.
	 * 구현 계획 1: Client로부터 요청된 docIDList를 받아 TermFreqByDoc list를 만들어 TermFreqByDoc를 하나씩 보냄.
	 */

	public String term;
	public int nGram;
	public int termFreq4RequestedCorpus;
	public String docIdByTermFreqJson; //추후 Map으로 다시 변환.<docID, termFreq>
	
//	// 텀 1개씩 전송	1-4(반복)
//	public int docID;
//	public String docCategory;
//	public String docTitle;
//	public int ngram;
//	public String termsJson;	// 추후 Map으로 다시 변환.<Term, Freq>
//	
//	//
}