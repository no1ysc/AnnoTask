package com.kdars.AnnoTask.MapReduce;

import com.kdars.AnnoTask.ContextConfig;
import com.kdars.AnnoTask.DB.DocTermFreqByTerm;
import com.kdars.AnnoTask.DB.Document;

import java.util.*;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

public class Tokenizer {
	private int CharLimit = ContextConfig.getInstance().getCharLimit();
	private int DocLimit = ContextConfig.getInstance().getDocLimit();
	private String specialCharsPattern= ContextConfig.getInstance().getSpecialCharPattern();
	private String[] PostFix = ContextConfig.getInstance().getPostFix();
//	private String specialCharProcessingString;
	
	public Tokenizer() {
	}
	
	
	public DocTermFreqByTerm[] termGenerate(Document document, int ngram) {
		/* Getting Target String */
		String doc = document.getBody();
		
		/* Working Tokenize */
		String delim = ContextConfig.getInstance().getDelim();

		/* Setting Meta Data */
		int DocID = document.getDocumentID();
		String DocCategory = document.getCategory();
		DocTermFreqByTerm[] docByTermList = new DocTermFreqByTerm[ngram];
		for (int i = 0; i < ngram; i++){
			docByTermList[i] = new DocTermFreqByTerm(DocID, i+1, DocCategory);
		}

		/* checking document length */
		ArrayList<String> checkedDoc = docAndTermLengthCheck(doc, DocLimit);	 
		
		for (int i = 0; i < checkedDoc.size(); i++){
			/* Tokenizing according to delimiter configuration */
			ArrayList<String> termList = (ArrayList<String>) ((ArrayList) Collections.list(new StringTokenizer(checkedDoc.get(i), delim)));
			
			/* Getting n_gram list */
			getNgramTerms(docByTermList, termList);
		}
		
		return docByTermList;
	}

	private void getNgramTerms(DocTermFreqByTerm[] docByTermList, ArrayList<String> termList) {
		int genPoint = -1;
		int gramPoint = -1;
		int limitNGram = docByTermList.length;
				
		while ((++genPoint) < termList.size()){
			StringBuilder sb = new StringBuilder();
			while ((++gramPoint) < limitNGram){
				// 일단 만들어.
				String checkTerm = termList.get(genPoint + gramPoint);
				if(checkTerm.length() >= CharLimit){
					break;
				}
				
				sb.append(checkTerm);
				String procStr = removeSpecialChar(sb.toString()); 

				if (procStr == null){
					break;
				}
				
				// 은는이가 뜯음.
				procStr = DeletePostFix(procStr);
				
				//저장
				String finalStr = procStr.trim();
				int spaceChecker = 0;
				Matcher spaceDetector = Pattern.compile(" ").matcher(finalStr);
				while (spaceDetector.find()){
					spaceDetector.start();
					spaceChecker ++;
				}
				docByTermList[spaceChecker].increaseFreq(finalStr.toLowerCase());


				// 지금 만든게 특문으로 끝나는지 하는지.
				if (isEndWithSpecialChar(sb.toString())){
					// 특문이면 여기까지만 만들고 나가자.
					break;
				}
				
				// 다음놈 인덱스 Check
				if (genPoint + gramPoint + 1 >= termList.size()){
					break;
				}
				// 만들고 나서 다음놈 불러와봄. 불러와서 스페셜 캐릭터로 시작하면 나가자.
				if (isStartWithSpecialChar(termList.get(genPoint + gramPoint + 1))){
					// 다음놈 시작이 특문이면 가차없이 나가자.
					break;
				}
								
				// 점검 다 끝났으니, 공백 붙이고 계속하자.
//				sb.setLength(0);
				sb.append(" ");
			}
			
			gramPoint = -1;
		}
	}
	
	private boolean isStartWithSpecialChar(String string) {
		return isSpecialChar(string.charAt(0));
	}
	private boolean isEndWithSpecialChar(String string) {
		return isSpecialChar(string.charAt(string.length()-1));
	}
	private boolean isSpecialChar(char c){
		if (Pattern.compile(specialCharsPattern).matcher(String.valueOf(c)).find()){
			return	true;
		}
		return false;
	}
	private String removeSpecialChar(String str){
		ArrayList<Integer> specialCharCheck = specialCharPatternMatch(str);
		if (specialCharCheck.isEmpty()) {
			return str;
		}
		
		if (specialCharCheck.size() == str.length()){
			return null;
		}
		
		int beginIndex = 0;
		while (specialCharCheck.contains(beginIndex)){
			beginIndex++;
		}
		
		int tempEndIndex = str.length()-1;
		while (specialCharCheck.contains(tempEndIndex)){
			tempEndIndex--;
		}
		
		return str.substring(beginIndex, tempEndIndex+1);
	}
	
	
	private ArrayList<String> docAndTermLengthCheck(String doc, int LengthLimit){
		ArrayList<String> splitdoc = new ArrayList<String>();
		int startIndex = 0;
		int endIndex = LengthLimit;
		
		while (endIndex < doc.length()){
			int adaptiveEndIndex = doc.indexOf(". ", endIndex);
			if (adaptiveEndIndex <= 0){
				break;
			}
			splitdoc.add(doc.substring(startIndex, adaptiveEndIndex));
			startIndex = adaptiveEndIndex;
			endIndex = adaptiveEndIndex + LengthLimit;
		}
		splitdoc.add(doc.substring(startIndex, doc.length()));
		
		return splitdoc;
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
	
	
	
	
	
	
//	
//	private String specialCharTrimAndTermRemove(String rawstring) {
//		/*
//		 * Starts process ONLY when the string contains the special characters
//		 * defined in globalContext
//		 */
//		ArrayList<Integer> specialCharCheck = specialCharPatternMatch(rawstring);
//		if (specialCharCheck.isEmpty()) {
//			return rawstring;
//		}
//		
//		if (specialCharCheck.size() == rawstring.length()){
//			return null;
//		}
//		
//		ArrayList<Integer> reducedSpecialCharCheck;
//		reducedSpecialCharCheck = removePrePostSpecialChar(specialCharCheck,rawstring);
//		
//		for (int i : reducedSpecialCharCheck) {
//			if (rawstring.charAt(i - 1) == ' ' || rawstring.charAt(i + 1) == ' '){
//				return null;
//			}
//		}
//		return specialCharProcessingString;
//	} 

	//	private ArrayList<Integer> removePrePostSpecialChar(ArrayList<Integer> specialCharCheck, String rawstring) {
	//
	//		int beginIndex = 0;
	//		while (specialCharCheck.contains(beginIndex)){
	//			beginIndex++;
	//		}
	//		
	//		int tempEndIndex = rawstring.length()-1;
	//		while (specialCharCheck.contains(tempEndIndex)){
	//			tempEndIndex--;
	//		}
	//		
	//		
	//		int endIndex = specialCharCheck.size()-(rawstring.length()-tempEndIndex)+1;
	//		
	//		if(beginIndex >= endIndex){
	//			specialCharCheck.clear();
	//			return specialCharCheck;
	//		}
	//
	//		specialCharProcessingString = rawstring.substring(beginIndex, tempEndIndex+1);
	//		
	//	ArrayList<Integer> listToArrayList = new ArrayList<Integer>(specialCharCheck.subList(beginIndex, endIndex));
	//		return listToArrayList;
	//	}
	
		

	
//	private ArrayList<Integer> removePrePostSpecialChar(ArrayList<Integer> specialCharCheck, String rawstring) {
//
//		int beginIndex = 0;
//		while (specialCharCheck.contains(beginIndex)){
//			beginIndex++;
//		}
//		
//		int tempEndIndex = rawstring.length()-1;
//		while (specialCharCheck.contains(tempEndIndex)){
//			tempEndIndex--;
//		}
//		
//		
//		int endIndex = specialCharCheck.size()-(rawstring.length()-tempEndIndex)+1;
//		
//		if(beginIndex >= endIndex){
//			specialCharCheck.clear();
//			return specialCharCheck;
//		}
//
//		specialCharProcessingString = rawstring.substring(beginIndex, tempEndIndex+1);
//		
//	ArrayList<Integer> listToArrayList = new ArrayList<Integer>(specialCharCheck.subList(beginIndex, endIndex));
//		return listToArrayList;
//	}

	
}
