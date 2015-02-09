package com.kdars.AnnoTask.Server.WordProc;

import java.util.ArrayList;
import java.util.Collections;
import java.util.Comparator;
import java.util.HashSet;

/**
 * 입력된 문자들에서 매치되는 단어를 정책에 따라 정리해줌.
 * 입력되는 Generic T는 반드시 toString 구현해야함.
 * 1. 전방일치, 중간일치, 순으로 정렬함.
 * 2. 각 일치 되는 문자열을 정렬 시, 문자열의 크기로 sorting
 * @author JS
 *
 */
public class WordMatchInWordSet <T>{
	private String inputWord;
	private ArrayList<T> inputWords;
	private ArrayList<T> result = new ArrayList<T>();
	
	/**
	 * 
	 * @param inputWord : 추출 및 정렬의 기준단어
	 * @param inputWords : 정렬대상 오브젝트 셋
	 */
	public WordMatchInWordSet(String inputWord, ArrayList<T> inputWords){
		this.inputWord = inputWord;
		this.inputWords = inputWords;
	}
	
	/**
	 * 결과 추출
	 * @return
	 */
	public ArrayList<T> getResult(){
		if (result.size() == 0){
			result.addAll(removeDuplicationSort(fowardMatch()));	// 앞부분 일치
			result.addAll(removeDuplicationSort(includeMatch()));	// 중간 일치
//			result.addAll(removeDuplicationSort(backwardMatch()));	// 뒷부분 일치
		}
		
		return result;
	}
	
	private boolean containItem(T item){
		return result.contains(item);
	}
	
	private ArrayList<T> fowardMatch(){
		String regex = inputWord + ".*";
		return _Match(regex);
	}
	
	private ArrayList<T> backwardMatch(){
		String regex = ".*" + inputWord;
		return _Match(regex);
	}
	
	private ArrayList<T> includeMatch(){
		String regex = ".*" + inputWord + ".*";
		return _Match(regex);
	}
	
	private ArrayList<T> removeDuplicationSort(ArrayList<T> target){
		ArrayList<T> proc = new ArrayList<T>(new HashSet<T>(target));
		ArrayList<T> subResult = new ArrayList<T>();
		// 걸러내기
		for (T item : proc){
			if (!containItem(item)){
				subResult.add(item);
			}
		}
		
		// 스트링의 크기로 정렬
		Collections.sort(subResult, new Comparator(){
			@Override
			public int compare(Object arg0, Object arg1) {
				return arg0.toString().length() - arg1.toString().length();  
			}
			
		});
		
		return subResult;
	}
	
	private ArrayList<T> _Match(String regex){
		ArrayList<T> result = new ArrayList<T>();
		
		for (T word : inputWords){		//문자열 돌면서 검사
			if (word.toString().matches(regex)){	//현재조건의 문자열 검사
				result.add(word);
			}
		}
		
		return result;
	}
}

