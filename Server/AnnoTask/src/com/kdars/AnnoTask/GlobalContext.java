package com.kdars.AnnoTask;

import java.util.ArrayList;

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
	private String SpecialChars = "'()/,.\"?";
	public String getSpecialChars(){
		return this.SpecialChars;
	}
	
	private String[] PostFix = {"은 는 이 가 을 를 에 의 도 만 로 와 과",
								"에서 에게 한테 로서 로써 께서 까지 조차 부터 마저"};
	public String[] getPostFix(){
		return this.PostFix;
	}
	
	/* Content DB Connect Info(Running time is not changed.)*/   
//	public final String CONTENT_DB_JDBC_URL = "jdbc:mysql://192.168.1.7:3306/webcrawler_v01";
//	public final String CONTENT_DB_NAME = "webcrawler_v01"; 
//	public final String CONTENT_DB_USER_ID = "root";
//	public final String CONTENT_DB_USER_PASS = "1q2w3e4r";
//	public final String CONTENT_DB_TABLE_NAME = "content_v01";

	
	
	public final String CONTENT_DB_JDBC_URL = "jdbc:mysql://127.0.0.1:3306/job_database";
	public final String CONTENT_DB_NAME = "contentdb"; 
	public final String CONTENT_DB_USER_ID = "root";
	public final String CONTENT_DB_USER_PASS = "1qaz@WSX";
	public final String CONTENT_DB_contentTABLE_NAME = "content_v02";
	public final String CONTENT_DB_jobTABLE_NAME = "job_table";
	
	/* ContentProcessor Attributes */
	private int maxContentProcessor = 10;
	public int getMaxContentProcessor(){
		return this.maxContentProcessor;
	}
	
	
	/* Content DB Connect Info */
	
	/* DeleteList DB Connect Info*/
	public final String DeleteList_DB_JDBC_URL = "jdbc:mysql://192.168.1.12:3306/deletelistdb";
	public final String DeleteList_DB_NAME = "deletelistdb"; 
	public final String DeleteList_DB_USER_ID = "root";
	public final String DeleteList_DB_USER_PASS = "jinqkim69";
	public final String DeleteList_DB_TABLE_NAME = "deletelist";
	/* DeleteList DB Connect Info*/

	/* TermFreq DB Connect Info*/
	public final String TermFreq_DB_JDBC_URL = "jdbc:mysql://192.168.1.12:3306/termfreqdb";
	public final String TermFreq_DB_NAME = "termfreqdb"; 
	public final String TermFreq_DB_USER_ID = "root";
	public final String TermFreq_DB_USER_PASS = "jinqkim69";
	public final String TermFreq_DB_TABLE_NAME = "tftable";
	/* TermFreq DB Connect Info*/
	
	/* Thesaurus DB Connect Info*/
	public final String Thesaurus_DB_JDBC_URL = "jdbc:mysql://192.168.1.12:3306/thesaurusdb";
	public final String Thesaurus_DB_NAME = "thesaurusdb"; 
	public final String Thesaurus_DB_USER_ID = "root";
	public final String Thesaurus_DB_USER_PASS = "jinqkim69";
	public final String Thesaurus_DB_TABLE_NAME1 = "conceptfromtable";
	public final String Thesaurus_DB_TABLE_NAME2 = "concepttotable";
	/* Thesaurus DB Connect Info*/
	
}
