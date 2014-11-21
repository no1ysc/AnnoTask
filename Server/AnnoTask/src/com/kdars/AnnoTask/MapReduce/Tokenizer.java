package com.kdars.AnnoTask.MapReduce;

import com.kdars.AnnoTask.GlobalContext;
import com.kdars.AnnoTask.DB.DocByTerm;
import com.kdars.AnnoTask.DB.Document;

import java.util.*;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

public class Tokenizer {
	private int CharLimit = GlobalContext.getInstance().getCharLimit();
	private int DocLimit = GlobalContext.getInstance().getDocLimit();
	private String specialCharsPattern= GlobalContext.getInstance().getSpecialCharPattern();
	private String[] PostFix = GlobalContext.getInstance().getPostFix();
	private ArrayList<String> termList;
	private String specialCharProcessingString;
	
	public Tokenizer() {
	}
	
	
	public DocByTerm[] termGenerate(Document document, int ngram) {
		/* Getting Target String */
		String doc = document.getBody();
		
		/* Working Tokenize */
		String delim = GlobalContext.getInstance().getDelim();

		/* Setting Meta Data */
		int DocID = document.getDocumentID();
		String DocCategory = document.getCategory();
		DocByTerm[] docByTermList = new DocByTerm[ngram];
		for (int i = 0; i < ngram; i++){
			docByTermList[i] = new DocByTerm(DocID, i+1, DocCategory);
		}

		/* checking document length */
		ArrayList<String> checkedDoc = docAndTermLengthCheck(doc, DocLimit);	 
		
		for (int i = 0; i < checkedDoc.size(); i++){
			/* Tokenizing according to delimiter configuration */
			termList = (ArrayList<String>) ((ArrayList) Collections.list(new StringTokenizer(checkedDoc.get(i), delim)));
			
			/* Getting n_gram list */
			getNgramTerms(docByTermList);
		}
		
		return docByTermList;
	}

	private void getNgramTerms(DocByTerm[] docByTermList) {

		/* Making n_grams with tokens */
		for (int ngramPointer = 0; ngramPointer <  termList.size(); ngramPointer++) {
			StringBuilder sb = new StringBuilder();
			for (int ngramMaker = 0; ngramMaker < docByTermList.length; ngramMaker++) {
				if (termList.size() <= ngramPointer + ngramMaker){
					break;
				}
				
				String possibleNgram = termList.get(ngramPointer + ngramMaker);
				if (possibleNgram.length() < CharLimit) {
					
					sb.append(possibleNgram + " ");
					
					String processedString = specialCharTrimAndTermRemove(sb.toString().trim());
					if (processedString != null) {
						String FinalString = DeletePostFix(processedString);
						docByTermList[ngramMaker].increaseFreq(FinalString);
					}
				}
			}

		}
	}
	
	private String DeletePostFix(String processedString){
		
		for (int postFixLength = PostFix.length; postFixLength > 0; postFixLength--){
			if (processedString.length() <= postFixLength){
				continue;
			}
			if (PostFix[postFixLength - 1].contains(processedString.substring(processedString.length()-postFixLength, processedString.length()))){
				return processedString.substring(0,processedString.length() - postFixLength);
			}			
		}
		
		return processedString;
	}
	
	private String specialCharTrimAndTermRemove(String rawstring) {
		/*
		 * Starts process ONLY when the string contains the special characters
		 * defined in globalContext
		 */
		ArrayList<Integer> specialCharCheck = specialCharPatternMatch(rawstring);
		if (specialCharCheck.isEmpty()) {
			return rawstring;
		}
		
		if (specialCharCheck.size() == rawstring.length()){
			return null;
		}
		
		ArrayList<Integer> reducedSpecialCharCheck;
		reducedSpecialCharCheck = removePrePostSpecialChar(specialCharCheck,rawstring);
		
		for (int i : reducedSpecialCharCheck) {
			if (rawstring.charAt(i - 1) == ' ' || rawstring.charAt(i + 1) == ' '){
				return null;
			}
		}
		return specialCharProcessingString;
	} 

	private ArrayList<Integer> specialCharPatternMatch(String specialCharCheck){
		ArrayList<Integer> intArray = new ArrayList<Integer>();
		Pattern p = Pattern.compile(specialCharsPattern);
		Matcher m = p.matcher(specialCharCheck);
		while(m.find()){
			intArray.add(m.start());
		}
		return intArray;
	}
	
	private ArrayList<Integer> removePrePostSpecialChar(ArrayList<Integer> specialCharCheck, String rawstring) {

		int beginIndex = 0;
		while (specialCharCheck.contains(beginIndex)){
			beginIndex++;
		}
		
		int tempEndIndex = rawstring.length()-1;
		while (specialCharCheck.contains(tempEndIndex)){
			tempEndIndex--;
		}
		
		
		int endIndex = specialCharCheck.size()-(rawstring.length()-tempEndIndex)+1;
		
		if(beginIndex >= endIndex){
			specialCharCheck.clear();
			return specialCharCheck;
		}

		specialCharProcessingString = rawstring.substring(beginIndex, tempEndIndex+1);
		
	ArrayList<Integer> listToArrayList = new ArrayList<Integer>(specialCharCheck.subList(beginIndex, endIndex));
		return listToArrayList;
	}

	
	private ArrayList<String> docAndTermLengthCheck(String doc, int LengthLimit){
		ArrayList<String> splitdoc = new ArrayList<String>();
		int startIndex = 0;
		int endIndex = LengthLimit;
		
		while (endIndex < doc.length()){
			int adaptiveEndIndex = doc.indexOf(". ", endIndex);
			splitdoc.add(doc.substring(startIndex, adaptiveEndIndex));
			startIndex = adaptiveEndIndex;
			endIndex = adaptiveEndIndex + LengthLimit;
		}
		splitdoc.add(doc.substring(startIndex, doc.length()));
		
		return splitdoc;
	}
	
}
