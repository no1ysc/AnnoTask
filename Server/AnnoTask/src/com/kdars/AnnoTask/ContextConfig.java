package com.kdars.AnnoTask;

import java.util.ArrayList;

public class ContextConfig {
	private static  ContextConfig globalContext = new ContextConfig();
	public static ContextConfig getInstance(){
		return	globalContext;
	}
	
	//TODO: 처음에 프로그램 시작할때, DB 전부 연결될때까지 대기하는 부분,
	// TODO : TermDB Lock 걸린거 전부 풀어주고 초기화 하는 부분.,
	
	/* 서버 병렬 코어로 처리할지 여부 */
	// 현재 병렬 처리 구현된 부분 : HeartBeat, 각 UserControl
	private boolean MULTI_CORE = true;
	public boolean isMultiCore(){
		return MULTI_CORE;
	}
	/* 서버 병렬 코어로 처리할지 여부 */
	
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
	
	/* 커넥션 HeartBeat 체크를 위한 기준시간 */
	private int TIMEOUT_LIMITATION = 20000;
	public long getTimeoutLimitation() {
		return TIMEOUT_LIMITATION;
	}
	/* 커넥션 HeartBeat 체크를 위한 기준시간 */
	
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
	private String specialCharsPattern= "[\\x{0021}-\\x{002f}_\\x{003a}-\\x{0040}_\\x{005b}-\\x{0060}_\\x{007b}-\\x{007e}_\\x{2018}-\\x{201f}]";
	private String specialCharsPattern_delete = "[\\x{2010}-\\x{2017}_\\x{2020}-\\x{206f}_\\x{2460}-\\x{24ff}_\\x{25a0}-\\x{26ff}_\\x{1f330}-\\x{1f5ff}]";
	public String getSpecialCharPattern(){
		return this.specialCharsPattern;
	}
	public String getSpecialCharPattern_delete(){
		return this.specialCharsPattern_delete;
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
	public final String CONTENT_DB_JDBC_URL = "jdbc:mysql://128.2.213.162/ewok_v01";
	public final String CONTENT_DB_NAME = "ewok_v01"; 
	public final String CONTENT_DB_USER_ID = "kdars";
	public final String CONTENT_DB_USER_PASS = "kdarscom";
	public final String CONTENT_DB_contentTABLE_NAME = "contents";
	public final String CONTENT_DB_jobTABLE_NAME = "job_table";
	
	/* ContentProcessor Attributes */
	private int maxContentProcessor = 1;
	public int getMaxContentProcessor(){
		return this.maxContentProcessor;
	}
	
	/**
	 * @author kihpark
	 * @date 1/21/2015
	 */
	/*User DB UserAccount Info */
	public final String UserAccounts_DB_JDBC_URL = "jdbc:mysql://128.2.213.162/annotask";
	public final String UserAccounts_DB_NAME = "annotask"; 
	public final String UserAccounts_DB_USER_ID = "kdars";
	public final String UserAccounts_DB_USER_PASS = "kdarscom";
	public final String UserAccounts_DB_TABLE_NAME = "useraccounts";
	/*User DB UserAccount Info */
	
	/**
	 * @author kihpark
	 * @date 1/21/2015
	 */
	/*ActivityLog DB Connect Info */
	public final String ActivityLog_DB_JDBC_URL = "jdbc:mysql://128.2.213.162/annotask";
	public final String ActivityLog_DB_NAME = "annotask"; 
	public final String ActivityLog_DB_USER_ID = "kdars";
	public final String ActivityLog_DB_USER_PASS = "kdarscom";
	public final String ActivityLog_DB_TABLE_NAME = "activitylog";
	/*ActivityLog DB Connect Info */
	
	/* DeleteList DB Connect Info*/
	public final String DeleteList_DB_JDBC_URL = "jdbc:mysql://128.2.213.162/annotask";
	public final String DeleteList_DB_NAME = "annotask"; 
	public final String DeleteList_DB_USER_ID = "kdars";
	public final String DeleteList_DB_USER_PASS = "kdarscom";
	public final String DeleteList_DB_TABLE_NAME = "deletelist";
	/* DeleteList DB Connect Info*/

	/* TermFreq DB Connect Info*/
	public final String TermFreq_DB_JDBC_URL = "jdbc:mysql://128.2.213.162/annotask";
	public final String TermFreq_DB_NAME = "annotask"; 
	public final String TermFreq_DB_USER_ID = "kdars";
	public final String TermFreq_DB_USER_PASS = "kdarscom";
	public final String TermFreq_DB_TABLE_NAME = "tftable";
	/* TermFreq DB Connect Info*/
	
	/* Thesaurus DB Connect Info*/
	public final String Thesaurus_DB_JDBC_URL = "jdbc:mysql://128.2.213.162/annotask";
	public final String Thesaurus_DB_NAME = "annotask"; 
	public final String Thesaurus_DB_USER_ID = "kdars";
	public final String Thesaurus_DB_USER_PASS = "kdarscom";
	public final String Thesaurus_DB_TABLE_NAME1 = "conceptfromtable";
	public final String Thesaurus_DB_TABLE_NAME2 = "concepttotable";
	/* Thesaurus DB Connect Info*/

		
}
