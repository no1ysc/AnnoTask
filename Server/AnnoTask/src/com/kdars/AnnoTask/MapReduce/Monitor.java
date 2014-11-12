package com.kdars.AnnoTask.MapReduce;

import java.util.ArrayList;

import com.kdars.AnnoTask.DB.DocByTerm;
import com.kdars.AnnoTask.DB.TermFreqDBManager;

public class Monitor extends Thread{
	private ArrayList<ContentProcessor>	processContext;
	
	public Monitor(){
		processContext = new ArrayList<ContentProcessor>();
	}
	
	public void run(){
		while(true){
			ContentProcessor job = jobAllocate();
			job.start();
			
			if (!job.isAlive()){
				reduce();
			}
		}
	}
	
	private void reduce() {
		// TODO 돌면서 저장
		TermFreqDBManager.getInstance().addDocByTerm(new DocByTerm(1,1,"a"));
	}

	private ContentProcessor jobAllocate(){
		// 리스트 변화
		;
		return new ContentProcessor(1000, 0);
	}
}
