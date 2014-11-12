package com.kdars.AnnoTask;

import com.kdars.AnnoTask.MapReduce.Monitor;

public class DocumentAnalyzer {
	private Monitor master = new Monitor();
	private String analysisStatus;
	
	public void run(){
		master.start(); // TODO:시퀸스 다이어그램 임시.
//		master.run();
	}
	
	public String getAnalysisStatus(){
		return	this.analysisStatus;
	}
	
	public synchronized void setAnalysisStatus(String status){
		this.analysisStatus = status;
	}
}
