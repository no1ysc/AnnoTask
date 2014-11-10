package com.kdars.AnnoTask.MapReduce;

import java.util.ArrayList;

import com.kdars.AnnoTask.DB.DocByTerm;
import com.kdars.AnnoTask.DB.TermFreqDBManager;

public class Master extends Thread{
	private ArrayList<Job>	jobList;
	
	public Master(){
		jobList = new ArrayList<Job>();
	}
	
	public void run(){
		while(true){
			Job job = jobAllocate();
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

	private Job jobAllocate(){
		// 리스트 변화
		;
		return new Job(1000, 0);
	}
}
