package com.kdars.AnnoTask.DB;

import com.mysql.jdbc.Connection;

public class ThesaurusDBConnector {
	private Connection sqlConnection;
	// TODO : 향후 한번에 조절하기 위해 모아야할 정보 : SQL 커넥션 정보,
	
	public ThesaurusDBConnector(){
		connect(); 
	}
	
	public boolean add(Thesaurus thes){
		return false;
	}
	
	public boolean delete(Thesaurus thes){  
		return false;
	}
	
	public Thesaurus query(String colName, String value){
		return null;
	}
	
	private boolean connect(){
//		sqlConnection;
		return false;
	}
	
	private boolean disconnect(){
//		sqlConnection;
		return false;
	}
}
