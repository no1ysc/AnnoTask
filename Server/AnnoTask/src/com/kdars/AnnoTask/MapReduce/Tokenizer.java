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

		/* Tokenizing according to delimiter configuration */
		termList = (ArrayList<String>) ((ArrayList) Collections.list(new StringTokenizer(doc, delim)));

		/* Setting Meta Data */
		int DocID = document.getDocumentID();
		String DocCategory = document.getCategory();
		DocByTerm[] docByTermList = new DocByTerm[ngram];
		for (int i = 0; i < ngram; i++){
			docByTermList[i] = new DocByTerm(DocID, i+1, DocCategory);
		}
		
		/* Getting n_gram list */
		getNgramTerms(docByTermList);
		return docByTermList;
	}

	private void getNgramTerms(DocByTerm[] docByTermList) {

		/* Making n_grams with tokens */
		for (int i = 0; i <  termList.size(); i++) {
			StringBuilder sb = new StringBuilder();
			//sb.append(termList.get(i));
			for (int j = 0; j < docByTermList.length; j++) {
				if (termList.size() > i + j) {
					sb.append(" " + termList.get(i + j));
					String processedString = specialCharRemove(sb.toString().trim());
					if (processedString != null) {
						String FinalString = DeletePostFix(processedString);
						docByTermList[j].increaseFreq(FinalString);
					}
				}else{
					break;
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
	
	private String specialCharRemove(String rawstring) {
		/*
		 * Starts process ONLY when the string contains the special characters
		 * defined in globalContext
		 */
		Pattern p = Pattern.compile(specialCharsPattern);
		Matcher m = p.matcher(rawstring);
		boolean b = m.find();
		if (b == false) {
			return rawstring;
		} else {
			String processingStr = removePrePostSpecialChar(rawstring);
			for(int n = 0; n < processingStr.length(); n++){
				if (specialChars.contains(String.valueOf(processingStr.charAt(n)))){
					if (processingStr.charAt(n-1)== ' ' || processingStr.charAt(n+1)== ' '){
						return null;
					}
				}
			}
			return processingStr;
		}
	}

	private String removePrePostSpecialChar(String processingStr) {
		int firstIndex = 0;
		int lastIndex = processingStr.length();
		
		for (int index = 0; index < processingStr.length(); index++){
			String temp = String.valueOf(processingStr.charAt(index));
			if (!specialChars.contains(temp)){
				firstIndex = index;
				break;
			}
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

	
}
