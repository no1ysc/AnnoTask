package com.kdars.AnnoTask.DB;

import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.SQLException;

import org.apache.commons.lang3.StringEscapeUtils;

import com.kdars.AnnoTask.GlobalContext;
import com.mysql.jdbc.Connection;

public class ThesaurusDBConnector {
	private String conceptFromTable = GlobalContext.getInstance().Thesaurus_DB_TABLE_NAME1;
	private String conceptToTable = GlobalContext.getInstance().Thesaurus_DB_TABLE_NAME2;
	// TODO : 향후 한번에 조절하기 위해 모아야할 정보 : SQL 커넥션 정보,
	
	public ThesaurusDBConnector(){
		//TODO: Connector 생성되면 connect 시도해서 성공하면 ok, 실패하면 표시.
		java.sql.Connection sqlConnection;
		if ((sqlConnection = connect()) == null){
			System.exit(2);
		}
		disconnect(sqlConnection);
//		while(connect());
	}
	
	public boolean add(Thesaurus thes){
		return false;
	}
	
	public boolean delete(Thesaurus thes){  
		return false;
	}
	
	public Thesaurus query(String colName, String value){
		java.sql.Connection sqlConnectionLocal = connect();
		Thesaurus conceptFromTermCheck = new Thesaurus();
		ResultSet resultSet = null;
		try {
			java.sql.Statement stmt = sqlConnectionLocal.createStatement();
			String valueEscape = escape(value);
			resultSet = stmt.executeQuery("select * from " + conceptFromTable + " where " + colName + " = \"" + valueEscape + "\";");
			/* exist check */
			if(!resultSet.next()){
				stmt.close();
				disconnect(sqlConnectionLocal);
				return null;
			}
			conceptFromTermCheck.getConceptFrom();
			stmt.close();
			disconnect(sqlConnectionLocal);
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
			disconnect(sqlConnectionLocal);
		}
		return conceptFromTermCheck;
	}
	
	private java.sql.Connection connect(){
//		sqlConnection;
		java.sql.Connection sqlConnection = null;
		
		String jdbcUrl = GlobalContext.getInstance().Thesaurus_DB_JDBC_URL;
		String DBName = GlobalContext.getInstance().Thesaurus_DB_NAME;
		String userID = GlobalContext.getInstance().Thesaurus_DB_USER_ID;
		String userPass = GlobalContext.getInstance().Thesaurus_DB_USER_PASS;
		
		try{
			Class.forName("com.mysql.jdbc.Driver");
		}catch(ClassNotFoundException e){
//			e.printStackTrace();
			System.err.println("JDBC is not found.");
			return null;
		}
		
		try{
			sqlConnection = DriverManager.getConnection(jdbcUrl, userID, userPass);
			
			java.sql.Statement stmt = sqlConnection.createStatement();
			stmt.execute("use "+DBName);
			stmt.close();
		}catch(SQLException e){
			e.printStackTrace();
			System.err.println("ThesaurusDB Connection Error.");
			disconnect(sqlConnection);
			return null;
		}
		
		return sqlConnection;
	}
	
	private boolean disconnect(java.sql.Connection sqlConnection){
//		sqlConnection;
		try {
			sqlConnection.close();
		} catch (SQLException e) {
			System.err.println("ThesaurusDB Disconnection Error.");
			return	false;
		} catch (NullPointerException e) {
			System.err.println("ThesaurusDB Disconnection Error.");
			return	false;
		}
		
		return true;
	}
	
	private String escape(String text) {
		String result = StringEscapeUtils.escapeHtml4(text);
		return result;
	}
	
	private String unescape(String text) {
		String result = StringEscapeUtils.unescapeHtml4(text);
		return result;
	}
	
}
