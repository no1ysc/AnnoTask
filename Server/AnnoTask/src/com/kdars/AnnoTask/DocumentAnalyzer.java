package com.kdars.AnnoTask;

import com.kdars.AnnoTask.MapReduce.Monitor;

public class DocumentAnalyzer {
	private Monitor contentMonitor = new Monitor();
	private String analysisStatus;
	
	public void run(){
		contentMonitor.start();
	}
	
	public String getAnalysisStatus(){
		return	this.analysisStatus;
	}
	
	public synchronized void setAnalysisStatus(String status){
		this.analysisStatus = status;
	}
}
