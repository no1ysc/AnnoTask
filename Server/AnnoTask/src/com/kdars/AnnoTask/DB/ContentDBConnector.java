package com.kdars.AnnoTask.DB;

import com.mysql.jdbc.Connection;

public class ContentDBConnector {
	private Connection sqlConnection;
	// TODO : 향후 한번에 조절하기 위해 모아야할 정보 : SQL 커넥션 정보,
	
	public ContentDBConnector(){
		connect();
	}
	
	public boolean add(Document document){
		return false;
	}
	
	public boolean delete(Document document){
		return false;
	}
	
	public Document query(String colName, String value){
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
