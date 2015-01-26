package com.kdars.AnnoTask.MapReduce;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.Iterator;
import java.util.Map;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

import com.kdars.AnnoTask.DB.DeleteListDBManager;
import com.kdars.AnnoTask.DB.DocTermFreqByTerm;
import com.kdars.AnnoTask.DB.Document;
import com.kdars.AnnoTask.DB.TermFreqDBManager;


public class TestToken {

	public static void main(String[] args) {
		// TODO Auto-generated method stub
		String body = "내가....?? \"원했던,. 것은[ 바로] '이런!' 것@ 난# 스페셜$ 캐릭터가% 너무^ 너무& 너무* 너무( 너무) 너무_ 너무- 너무+ 너무= 너무{ 너무} 너무| 너무~~ 좋아";
		Tokenizer tokenize = new Tokenizer();
		Document doc = new Document(0, null, null, null, null, null, null, null, body, null, null);
		DocTermFreqByTerm[] doctermList = tokenize.termGenerate(doc, 4);
		
		for ( DocTermFreqByTerm docs : doctermList){
			for ( String key : docs.keySet()){
				System.out.println(key + "  :  " + docs.get(key) + "개");
			}
		}
		
	}

}
