package com.kdars.AnnoTask.MapReduce;

import java.sql.Timestamp;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;

import com.kdars.AnnoTask.ContextConfig;
import com.kdars.AnnoTask.DB.ContentDBManager;
import com.kdars.AnnoTask.DB.DocTermFreqByTerm;
import com.kdars.AnnoTask.DB.TermFreqDBManager;
import com.kdars.AnnoTask.MapReduce.ContentProcessor.ProcessState;

public class Monitor extends Thread{
	private ArrayList<ContentProcessor>	processQueue;
	private ArrayList <Integer> jobCandidates = new ArrayList<Integer>();
	
	public long processStartTime;
	
	
	public Monitor(){
		processQueue = new ArrayList<ContentProcessor>();
	}
	
	public void run(){
		while(true){
			if(processQueue.size() < ContextConfig.getInstance().getMaxContentProcessor()){
				// create ContentProcessor and allocate job by giving doc_id for each thread and start running.
				if(checkUpdates()){
					jobAllocate().start();
				}else{ 
					delayMonitor(1000);
				}
			}
			
			// 3. Check ContentProcessor's status, if complete then delete from ArrayList and set complete_status to 1 in job_table
			for(int i = processQueue.size()-1; i >= 0 ; i--){
				if(processQueue.get(i).getProcessState() == ProcessState.Completed){
					ContentDBManager.getInstance().updateJobCompletion(processQueue.get(i).getDocument().getDocumentID());
					processQueue.remove(i);
				}
				
				if(((System.currentTimeMillis() - processStartTime) / (60*1000)) > 1){
					processQueue.get(i).rollbackWorkingStatus(processQueue.get(i).getDocument().getDocumentID());
					processQueue.remove(i);
					System.out.println("process removed!");
				}
			}
			
		}
	}
	
	// 	1. Check whether ContentProcessors are free or not 
	//	2. Check job_table where working_status is 0, false; get doc_id 
	private boolean checkUpdates(){
		ArrayList<Integer> temp = ContentDBManager.getInstance().getJobCandidates();
		if(temp.size() != 0){
			jobCandidates.add(temp.get(0));
//			System.out.println(jobCandidates.size());
		}
			
			return !jobCandidates.isEmpty();
	}
	
	// TODO: maximum job number per thread need to be defined
	private ContentProcessor jobAllocate(){  
		ContentProcessor cp = new ContentProcessor(jobCandidates.get(0));
		processQueue.add(cp);
		jobCandidates.remove(0);
		processStartTime = System.currentTimeMillis();
//		System.out.println(processStartTime);
		return cp;
	}
	
	private void delayMonitor(int mSecond){
		try {
			sleep(mSecond);
		} catch (InterruptedException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}
	
}
