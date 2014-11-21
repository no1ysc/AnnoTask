package com.kdars.AnnoTask.MapReduce;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.Iterator;
import java.util.Map;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

import com.kdars.AnnoTask.DB.DeleteListDBManager;
import com.kdars.AnnoTask.DB.DocByTerm;
import com.kdars.AnnoTask.DB.Document;
import com.kdars.AnnoTask.DB.TermFreqDBManager;


public class TestToken {

	public static void main(String[] args) {
		// TODO Auto-generated method stub
		String body = "내가....?? 원했던,. 것은[ 바로] 이런! 것@ 난# 스페셜$ 캐릭터가% 너무^ 너무& 너무* 너무( 너무) 너무_ 너무- 너무+ 너무= 너무{ 너무} 너무| 너무~~ 좋아";
		Tokenizer tokenize = new Tokenizer();
		Document doc = new Document(0, null, null, null, null, null, null, null, body, null, null);
		DocByTerm[] doctermList = tokenize.termGenerate(doc, 4);
		for (int i = 0; i<4; i++){
			System.out.println(i+1);
			System.out.println(doctermList[i].keySet().size());
			for (Iterator<Map.Entry<String, Integer>> iter = doctermList[i].entrySet().iterator(); iter.hasNext();){
				Map.Entry<String, Integer> entry = iter.next();
//				if (entry.getKey().contains("다")){
//					iter.remove();
					System.out.println("delete : ( " + entry + " )");
//					continue;
//				}
				
			}
//			ContentProcessor test = new ContentProcessor(0);
//			test.run();
			System.out.println(doctermList[i].keySet().size());
			
		}
		
//	
//		System.out.println(TermFreqDBManager.getInstance().addDocByTerm(doctermList[0]));
		
		System.out.println(DeleteListDBManager.getInstance().AddTermToDelete("승철"));
//		DeleteListDBManager.getInstance().AddTermToDelete("기흥");
		System.out.println(DeleteListDBManager.getInstance().RemoveTermFromDeleteList("승철"));
		System.out.println(DeleteListDBManager.getInstance().CheckForDeleteTerm("승철"));
		
		
	}

}
