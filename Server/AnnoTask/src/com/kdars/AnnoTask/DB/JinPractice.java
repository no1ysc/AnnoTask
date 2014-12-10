package com.kdars.AnnoTask.DB;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

import com.kdars.AnnoTask.ContextConfig;
import com.kdars.AnnoTask.Server.*;


public class JinPractice {

	public static void main(String[] args) {
		// TODO Auto-generated method stub
		int docId = 0;
		ArrayList<Integer> test = new ArrayList<Integer>();
		docId = 104550;
		test.add(docId);
		ArrayList<TermFreqByDoc> yoyo = nGramFilter(test);
		System.out.println(yoyo.size());
		
	}
	
	private static ArrayList<TermFreqByDoc> nGramFilter(ArrayList<Integer> docIdList){
		
		ArrayList<TermFreqByDoc> filtering = TermFreqDBManager.getInstance().getTermConditional(docIdList);
		System.out.println(filtering.size());
		for (int i = 2; i >= 0; i--){
			TermFreqByDoc termFreqByDocFilter = filtering.get(i);
			TermFreqDBManager.getInstance().termLock(termFreqByDocFilter.getTerm(), termFreqByDocFilter.getTermHolder());
			int termFreqSum = 0;
			for (int termFreq : termFreqByDocFilter.values()){
				termFreqSum = termFreqSum + termFreq;
			}
			if (termFreqSum < 2){
				filtering.remove(termFreqByDocFilter);
				continue;
			}
			termFreqByDocFilter.setTermFreq4RequestedCorpus(termFreqSum);
		}

		return filtering;
	}
	
}
