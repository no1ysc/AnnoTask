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

	public Tokenizer() {
		/* For processing special characters faster */
		StringTokenizer str = new StringTokenizer(GlobalContext.getInstance()
				.getSpecialChars(), "");
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
		DocByTerm ret = new DocByTerm(document.getDocumentID(), ngram,
				document.getCategory());

		/* Working Tokenize */
		String delim = GlobalContext.getInstance().getDelim();

		/* Tokenizing according to delimiter configuration */
		termList = (ArrayList<String>) ((ArrayList) Collections
				.list(new StringTokenizer(doc, delim)));

		/* Getting n_gram list */
		ArrayList<String> listOfNgrams = getNgramTerms(ngram);

		return null;
	}

	public ArrayList<String> getNgramTerms(int n) {
		/* Making n_grams with tokens */
		ArrayList<String> ngramList = new ArrayList<String>();
		for (int i = 0; i < (termList.size() - n + 1); i++) {
			StringBuilder sb = new StringBuilder();
			sb.append(termList.get(i));
			for (int j = 1; j < n; j++) {
				sb.append(" " + termList.get(i + j));
			}
			String processedString = specialCharRemove(sb.toString());
			if (processedString != null){
			ngramList.add(processedString);
			}
		}

		return ngramList;
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
			String specialChars = GlobalContext.getInstance().getSpecialChars();
			String processingStr = rawstring;
			for (int k = 0; k < specialChars.length(); k++) {
				// Removing special characters at the beginning and the end of the string
				while (processingStr.startsWith(Character.toString(specialChars.charAt(k)))
							|| processingStr.endsWith(Character.toString(specialChars.charAt(k)))) {

					if (processingStr.startsWith(Character.toString(specialChars.charAt(k)))
							&& processingStr.endsWith(Character.toString(specialChars.charAt(k)))) {
						processingStr = processingStr.substring(1,processingStr.length() - 1);
					} else if (rawstring.startsWith(Character.toString(specialChars.charAt(k)))) {
						processingStr = processingStr.substring(1,processingStr.length());
					} else if (rawstring.endsWith(Character.toString(specialChars.charAt(k)))) {
						processingStr = processingStr.substring(0,processingStr.length() - 1);
					} else {
						System.out.println("not prepared for this case");
					}
				}
				//Returning null for strings that contain special characters with white spaces attached to them
				for(int n = 0; n < processingStr.length(); n++){
					if (processingStr.charAt(n) == specialChars.charAt(k)){
						if (processingStr.charAt(n-1)== ' ' || processingStr.charAt(n+1)== ' '){
							return null;
						}
					}
				}

			}

			return processingStr;
		}
	}
}
