package com.kdars.AnnoTask.MapReduce;

import com.kdars.AnnoTask.GlobalContext;
import com.kdars.AnnoTask.DB.DocByTerm;
import com.kdars.AnnoTask.DB.Document;

import java.util.*;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

public class Tokenizer {
	private ArrayList<String> termList;
	private String specialChars = GlobalContext.getInstance().getSpecialChars();
	private String specialCharsPattern;
	private String[] PostFix = GlobalContext.getInstance().getPostFix();
	private int CharLimit = 500;
	private int DocLimit = 5000;
	
	public Tokenizer() {
		/* For processing special characters faster */
		StringTokenizer str = new StringTokenizer(GlobalContext.getInstance().getSpecialChars(), "");
		StringBuilder patternString = new StringBuilder();
		patternString.append("[");
		while (str.hasMoreTokens()) {
			patternString.append(str.nextToken() + "||");
		}
		patternString.append("]");
		specialCharsPattern = patternString.toString();
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

	private boolean specialCharFirstLastChecker(String ngram){
		if (specialChars.contains(String.valueOf(ngram.charAt(0))) || specialChars.contains(String.valueOf(ngram.charAt(ngram.length()-1)))){
			return true;
		}
		return false;
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
		if (specialCharPatternMatch(rawstring) == false) {
			return rawstring;
		}
		
		String processingStr = removePrePostSpecialChar(rawstring);
		
		if (processingStr == null){
			return null;
		}
		
		if(specialCharPatternMatch(processingStr)){
			for(int n = 0; n < processingStr.length(); n++){
				if (specialChars.contains(String.valueOf(processingStr.charAt(n)))){
					if (processingStr.charAt(n-1)== ' ' || processingStr.charAt(n+1)== ' '){
						return null;
					}
				}
			}
		}
		return processingStr;
	} 

	private boolean specialCharPatternMatch(String specialCharCheck){
		Pattern p = Pattern.compile(specialCharsPattern);
		Matcher m = p.matcher(specialCharCheck);
		return m.find();
	}
	
	private String removePrePostSpecialChar(String processingStr) {
		int firstIndex = 0;
		int lastIndex = processingStr.length();
		
		for (firstIndex = 0; firstIndex < processingStr.length(); firstIndex++){
			String temp = String.valueOf(processingStr.charAt(firstIndex));
			if (!specialChars.contains(temp)){
				break;
			}
		}
		
		if (firstIndex == lastIndex){
			return null;
		}
		
		for (int index = processingStr.length() - 1; index >= 0; index--){
			String temp = String.valueOf(processingStr.charAt(index));
			if (!specialChars.contains(temp)){
				lastIndex = index + 1;
				break;
			}
		}
		
		return processingStr.substring(firstIndex, lastIndex);
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
