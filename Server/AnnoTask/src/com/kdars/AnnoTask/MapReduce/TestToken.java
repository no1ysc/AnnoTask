package com.kdars.AnnoTask.MapReduce;

import java.util.Iterator;
import java.util.Map;

import com.kdars.AnnoTask.DB.DocByTerm;
import com.kdars.AnnoTask.DB.Document;


public class TestToken {

	public static void main(String[] args) {
		// TODO Auto-generated method stub
		String body = "정보조직법이 공포되면서 출범합니다. ?? ??.? ?? 정원만 1만 명이 넘는 거대 조직입니다.";
		Tokenizer tokenize = new Tokenizer();
		Document doc = new Document(0, null, null, null, null, null, null, null, body, null, null);
		DocByTerm[] doctermList = tokenize.termGenerate(doc, 2);
		for (int i = 0; i<2; i++){
			System.out.println(i+1);
			System.out.println(doctermList[i].keySet().size());
			for (Iterator<Map.Entry<String, Integer>> iter = doctermList[i].entrySet().iterator(); iter.hasNext();){
				Map.Entry<String, Integer> entry = iter.next();
				if (entry.getKey().contains("다")){
					iter.remove();
					System.out.println("delete : ( " + entry + " )");
					continue;
				}
				
			}
			System.out.println(doctermList[i].keySet().size());
		}
	}

}
