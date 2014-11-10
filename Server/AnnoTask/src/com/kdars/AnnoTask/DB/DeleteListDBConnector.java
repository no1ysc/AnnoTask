package com.kdars.AnnoTask.DB;

import com.mysql.jdbc.Connection;

public class DeleteListDBConnector {
	private Connection sqlConnection;
	// TODO : 향후 한번에 조절하기 위해 모아야할 정보 : SQL 커넥션 정보,
	
	public DeleteListDBConnector(){
		connect();
	}
	
	public boolean add(String deleteTerm){
		return false;
	}
	
	public boolean delete(String deleteTerm){
		return false;
	}
	
	public String query(String deleteTerm){
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
