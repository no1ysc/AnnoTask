package com.kdars.AnnoTask;

public class GlobalContext {
	private static  GlobalContext globalContext = new GlobalContext();
	public static GlobalContext getInstance(){
		return	globalContext;
	}
	
	
	/* N-Gram Setting */
	private int N_Gram = 4;	// 최대 N-gram
	public int getN_Gram(){
		return this.N_Gram;
	}
	/* N-Gram Setting */
	
	/* Special Character Remove */
	private String DELIM = " ";
	public String getDelim(){
		return this.DELIM;
	}
	/* Special Character Remove */
	
	
	
	/* Content DB Connect Info(Running time is not changed.)*/   
//	public final String CONTENT_DB_JDBC_URL = "jdbc:mysql://192.168.1.7:3306/webcrawler_v01";
//	public final String CONTENT_DB_NAME = "webcrawler_v01"; 
//	public final String CONTENT_DB_USER_ID = "root";
//	public final String CONTENT_DB_USER_PASS = "1q2w3e4r";
//	public final String CONTENT_DB_TABLE_NAME = "content_v01";

	
	
	public final String CONTENT_DB_JDBC_URL = "jdbc:mysql://127.0.0.1:3306/job_database";
	public final String CONTENT_DB_NAME = "job_database"; 
	public final String CONTENT_DB_USER_ID = "root";
	public final String CONTENT_DB_USER_PASS = "1qaz@WSX";
	public final String CONTENT_DB_contentTABLE_NAME = "test_table";
	public final String CONTENT_DB_jobTABLE_NAME = "job_table";
	
	/* ContentProcessor Attributes */
	private int maxContentProcessor = 10;
	public int getMaxContentProcessor(){
		return this.maxContentProcessor;
	}
	
	
	/* Content DB Connect Info */
}
