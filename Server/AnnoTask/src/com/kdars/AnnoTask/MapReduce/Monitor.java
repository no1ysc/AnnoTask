package com.kdars.AnnoTask.MapReduce;

import java.util.ArrayList;

import com.kdars.AnnoTask.GlobalContext;
import com.kdars.AnnoTask.DB.ContentDBManager;
import com.kdars.AnnoTask.DB.DocByTerm;
import com.kdars.AnnoTask.DB.TermFreqDBManager;
import com.kdars.AnnoTask.MapReduce.ContentProcessor.ProcessState;

public class Monitor extends Thread{
	private ArrayList<ContentProcessor>	processQueue;
	private ArrayList <Integer> jobCandidates = new ArrayList<Integer>();
	
	public Monitor(){
		processQueue = new ArrayList<ContentProcessor>();
	}
	
	public void run(){
		while(true){
			if(processQueue.size() < GlobalContext.getInstance().getMaxContentProcessor()){
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
			}
			
		}
	}
	
	// 	1. Check whether ContentProcessors are free or not 
	//	2. Check job_table where working_status is 0, false; get doc_id
	private boolean checkUpdates(){
			jobCandidates = ContentDBManager.getInstance().getJobCandidates();
			System.out.println(jobCandidates.size());
			
			return !jobCandidates.isEmpty();
	}
	
	// TODO: maximum job number per thread need to be defined
	private ContentProcessor jobAllocate(){  
		ContentProcessor cp = new ContentProcessor(jobCandidates.get(0));
		processQueue.add(cp);
		jobCandidates.remove(0);
		
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
