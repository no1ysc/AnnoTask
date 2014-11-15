package com.kdars.AnnoTask.MapReduce;

import com.kdars.AnnoTask.GlobalContext;
import com.kdars.AnnoTask.DB.DocByTerm;
import com.kdars.AnnoTask.DB.Document;

import java.util.*;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

public class Tokenizer {
	private ArrayList<String> termList;
	private String specialCharsPattern;
	private String specialChars = GlobalContext.getInstance().getSpecialChars();
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

	public DocByTerm termGenerate(Document document, int ngram) {
		/* Getting Target String */
		String doc = document.getBody();

		/* Setting Meta Data */
		DocByTerm ret = new DocByTerm(document.getDocumentID(), ngram, document.getCategory());

		/* Working Tokenize */
		String delim = GlobalContext.getInstance().getDelim();

		/* Tokenizing according to delimiter configuration */
		termList = (ArrayList<String>) ((ArrayList) Collections.list(new StringTokenizer(doc, delim)));

		/* Getting n_gram list */
		//ArrayList<String> listOfNgrams = getNgramTerms(ngram, ret);
		getNgramTerms(ngram,ret);
		return ret;
	}

	//private ArrayList<String> getNgramTerms(int n, DocByTerm ret) {
	private void getNgramTerms(int n, DocByTerm docTerm) {
			
		/* Making n_grams with tokens */
		//ArrayList<String> ngramList = new ArrayList<String>();
		for (int i = 0; i < (termList.size() - n + 1); i++) {
			StringBuilder sb = new StringBuilder();
			sb.append(termList.get(i));
			for (int j = 1; j < n; j++) {
				sb.append(" " + termList.get(i + j));
			}
			String processedString = specialCharRemove(sb.toString());
			if (processedString != null){
				docTerm.increaseFreq(processedString);
			//ngramList.add(processedString);
			}
		}

		//return ngramList;
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
