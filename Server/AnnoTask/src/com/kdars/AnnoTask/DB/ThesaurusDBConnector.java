package com.kdars.AnnoTask.DB;

import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.SQLException;

import com.kdars.AnnoTask.GlobalContext;
import com.mysql.jdbc.Connection;

public class ThesaurusDBConnector {
	private java.sql.Connection sqlConnection;
	private String conceptFromTable = GlobalContext.getInstance().Thesaurus_DB_TABLE_NAME1;
	private String conceptToTable = GlobalContext.getInstance().Thesaurus_DB_TABLE_NAME2;
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
		Thesaurus conceptFromTermCheck = new Thesaurus();
		ResultSet resultSet = null;
		try {
			java.sql.Statement stmt = sqlConnection.createStatement();
			resultSet = stmt.executeQuery("select * from " + conceptFromTable + " where " + colName + " = '" + value + "';");
			/* exist check */
			if(!resultSet.next()){
				return null;
			}
			conceptFromTermCheck.justTestConceptFrom();
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		return conceptFromTermCheck;
	}
	
	private boolean connect(){
//		sqlConnection;
		String jdbcUrl = GlobalContext.getInstance().Thesaurus_DB_JDBC_URL;
		String DBName = GlobalContext.getInstance().Thesaurus_DB_NAME;
		String userID = GlobalContext.getInstance().Thesaurus_DB_USER_ID;
		String userPass = GlobalContext.getInstance().Thesaurus_DB_USER_PASS;
		
		try{
			Class.forName("com.mysql.jdbc.Driver");
		}catch(ClassNotFoundException e){
//			e.printStackTrace();
			System.err.println("JDBC is not found.");
			return false;
		}
		
		try{
			sqlConnection = DriverManager.getConnection(jdbcUrl, userID, userPass);
			
			java.sql.Statement stmt = sqlConnection.createStatement();
			stmt.execute("use "+DBName);
			stmt.close();
		}catch(SQLException e){
//			e.printStackTrace();
			System.err.println("ContentDB Connection Error.");
			return false;
		}
		
		return true;
	}
	
	private boolean disconnect(){
//		sqlConnection;
		try {
			sqlConnection.close();
		} catch (SQLException e) {
			System.err.println("ContentDB Disconnection Error.");
			return	false;
		}
		
		return true;
	}
}
