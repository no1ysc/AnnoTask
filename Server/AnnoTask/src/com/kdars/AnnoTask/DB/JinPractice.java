package com.kdars.AnnoTask.DB;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

import com.kdars.AnnoTask.ContextConfig;


public class JinPractice {

	public static void main(String[] args) {
		// TODO Auto-generated method stub
		Document doc1 = new Document(87075, "null", "the king is back");
		Document doc2 = new Document(87076, "null", "the queen is back");
		
		ArrayList<Document> requestDocs = new ArrayList<Document>();
		
		requestDocs.add(doc1);
		requestDocs.add(doc2);
		
		ArrayList<DocTermFreqByTerm[]> filteredDocByTermList = nGramFilter(requestDocs);
		
		DocTermFreqByTerm[] test = filteredDocByTermList.get(0);
		System.out.println(test[0].getTitle());
		System.out.println(test[0].containsKey("4관왕"));
		System.out.println(test[0].containsValue(1));
		System.out.println(test[0].containsValue(2));
	}

	public static ArrayList<DocTermFreqByTerm[]> nGramFilter(ArrayList<Document> requestDocs){
		int nGramNumber = ContextConfig.getInstance().getN_Gram();
		HashMap<String, Integer> termHash = new HashMap<String, Integer>();
		ArrayList<DocTermFreqByTerm[]> docByTermList = new ArrayList<DocTermFreqByTerm[]>();
		ArrayList<String> filterTermList = new ArrayList<String>();
		for (Document doc : requestDocs){
			DocTermFreqByTerm[] docByTerm = TermFreqDBManager.getInstance().getDocByTerm(doc.getDocumentID(), doc.getCategory(), doc.getTitle());
			docByTermList.add(docByTerm);
			for (int nGramIndex = 0; nGramIndex < nGramNumber; nGramIndex++){
				for (String term : docByTerm[nGramIndex].keySet()){
					
					if (termHash.containsKey(term)){
						termHash.put(term, termHash.get(term) + docByTerm[nGramIndex].get(term));
					} else {
						termHash.put(term, docByTerm[nGramIndex].get(term));
					}

					if (termHash.containsKey(term) && termHash.get(term) < 2){
						termHash.remove(term);
						filterTermList.add(term);
					}
					
				}
			}
		}
		
		for (String filterTerm : filterTermList){
			int whiteSpaceCount = 0;
			Pattern p = Pattern.compile(" ");
			Matcher m = p.matcher(filterTerm);
			while(m.find()){
				m.start();
				whiteSpaceCount++;
			}
			for (DocTermFreqByTerm[] docTermFreqByTerm : docByTermList){
				docTermFreqByTerm[whiteSpaceCount].remove(filterTerm);
			}
		}
		
	return docByTermList;	
	}
}
