package com.kdars.AnnoTask;

import java.util.ArrayList;

public class ContextConfig {
	private static  ContextConfig globalContext = new ContextConfig();
	public static ContextConfig getInstance(){
		return	globalContext;
	}
	
	//TODO: 처음에 프로그램 시작할때, DB 전부 연결될때까지 대기하는 부분,
	// TODO : TermDB Lock 걸린거 전부 풀어주고 초기화 하는 부분.,
	
	/* 사용자에게 보여줄 관련 컨셉투 리스트 제한 개수 */
	private int LIMIT_CONCEPT_TO_COUNT = 20;
	public int getLimitConceptToCount(){
		return LIMIT_CONCEPT_TO_COUNT;
	}
	/* 사용자에게 보여줄 관련 컨셉투 리스트 제한 개수 */
	
	/* 각 프로세스 작업제한 시간 */
	private int LIMIT_TIME_SEC = 1000;
	public int getLimitTimeSec(){
		return	LIMIT_TIME_SEC;
	}
	/* 각 프로세스 작업제한 시간 */
	
	/* N-Gram Setting */
	private int N_Gram = 4;	// 최대 N-gram
	public int getN_Gram(){
		return this.N_Gram;
	}
	/* N-Gram Setting */
	
	/* Delimiter Setting */
	private String DELIM = " \r\n\t";
	public String getDelim(){
		return this.DELIM;
	}
	/* Delimiter Setting */
	
	/* Special Character Setting */
	private String specialCharsPattern= "[\\x{0021}-\\x{002f}_\\x{003a}-\\x{0040}_\\x{005b}-\\x{0060}_\\x{007b}-\\x{007e}]";
	public String getSpecialCharPattern(){
		return this.specialCharsPattern;
	}
	/* Special Character Setting */
	
	/* ClientJobUnit = number of rows */
	private int nRows = 10;
	public int getClientJobUnit() {
		return this.nRows;
	}

	/* ClientJobUnit = number of rows */
	
	/* Character length and document length limit for tokenizing */
	private int charlimit = 50;
	public int getCharLimit(){
		return this.charlimit;
	}
	
	private int doclimit = 100;
	public int getDocLimit(){
		return this.doclimit;	
	}
	/* Character length and document length limit for tokenizing */
	
	/* PostFix Setting */
	private String[] PostFix = {"은 는 이 가 을 를 에 의 도 만 로 와 과",
								"에서 에게 한테 로서 로써 께서 까지 조차 부터 마저"};
	public String[] getPostFix(){
		return this.PostFix;
	}
	/* PostFix Setting */
	
	/* Content DB Connect Info(Running time is not changed.)*/   
//	public final String CONTENT_DB_JDBC_URL = "jdbc:mysql://192.168.1.7:3306/webcrawler_v01";
//	public final String CONTENT_DB_NAME = "webcrawler_v01"; 
//	public final String CONTENT_DB_USER_ID = "root";
//	public final String CONTENT_DB_USER_PASS = "1q2w3e4r";
//	public final String CONTENT_DB_TABLE_NAME = "content_v01";

//	public final String CONTENT_DB_JDBC_URL = "jdbc:mysql://128.2.213.162/ewok_v01";
//	public final String CONTENT_DB_NAME = "ewok_v01"; 
//	public final String CONTENT_DB_USER_ID = "kdars";
//	public final String CONTENT_DB_USER_PASS = "kdarscom";
//	public final String CONTENT_DB_contentTABLE_NAME = "contents";
	public final String CONTENT_DB_JDBC_URL = "jdbc:mysql://192.168.1.9:3306/contentdb";
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
	public final String DeleteList_DB_JDBC_URL = "jdbc:mysql://192.168.1.4:3306/deletelistdb";
//	public final String DeleteList_DB_JDBC_URL = "jdbc:mysql://localhost:3306/deletelistdb";
	public final String DeleteList_DB_NAME = "deletelistdb"; 
	public final String DeleteList_DB_USER_ID = "root";
	public final String DeleteList_DB_USER_PASS = "jinqkim69";
	public final String DeleteList_DB_TABLE_NAME = "deletelist";
	/* DeleteList DB Connect Info*/

	/* TermFreq DB Connect Info*/
	public final String TermFreq_DB_JDBC_URL = "jdbc:mysql://192.168.1.4:3306/termfreqdb";
//	public final String TermFreq_DB_JDBC_URL = "jdbc:mysql://localhost:3306/termfreqdb";
	public final String TermFreq_DB_NAME = "termfreqdb"; 
	public final String TermFreq_DB_USER_ID = "root";
	public final String TermFreq_DB_USER_PASS = "jinqkim69";
	public final String TermFreq_DB_TABLE_NAME = "tftable";
	/* TermFreq DB Connect Info*/
	
	/* Thesaurus DB Connect Info*/
	public final String Thesaurus_DB_JDBC_URL = "jdbc:mysql://192.168.1.4:3306/thesaurusdb";
//	public final String Thesaurus_DB_JDBC_URL = "jdbc:mysql://localhost:3306/thesaurusdb";
	public final String Thesaurus_DB_NAME = "thesaurusdb"; 
	public final String Thesaurus_DB_USER_ID = "root";
	public final String Thesaurus_DB_USER_PASS = "jinqkim69";
	public final String Thesaurus_DB_TABLE_NAME1 = "conceptfromtable_test";
	public final String Thesaurus_DB_TABLE_NAME2 = "concepttotable_test";
	/* Thesaurus DB Connect Info*/
		
}
