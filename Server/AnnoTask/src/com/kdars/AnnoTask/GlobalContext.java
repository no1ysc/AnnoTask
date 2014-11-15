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
	private String SpecialChars = "/,.\"?";
	public String getSpecialChars(){
		return this.SpecialChars;
	}
	
	
	/* Content DB Connect Info(Running time is not changed.)*/   
	public final String CONTENT_DB_JDBC_URL = "jdbc:mysql://192.168.1.7:3306/webcrawler_v01";
	public final String CONTENT_DB_NAME = "webcrawler_v01"; 
	public final String CONTENT_DB_USER_ID = "root";
	public final String CONTENT_DB_USER_PASS = "1q2w3e4r";
	public final String CONTENT_DB_TABLE_NAME = "content_v01";
	/* Content DB Connect Info */
}
