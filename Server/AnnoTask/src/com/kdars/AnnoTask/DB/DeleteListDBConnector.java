package com.kdars.AnnoTask.DB;

import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.SQLException;

import org.apache.commons.lang3.StringEscapeUtils;

import com.kdars.AnnoTask.GlobalContext;

public class DeleteListDBConnector {
	private java.sql.Connection sqlConnection;
	private String deleteListTable = GlobalContext.getInstance().DeleteList_DB_TABLE_NAME;
	private String colName = "Stopwords";
	
	// TODO : 향후 한번에 조절하기 위해 모아야할 정보 : SQL 커넥션 정보,
	
	public DeleteListDBConnector(){
		connect();
	}
	
	public boolean add(String deleteTerm){
		
		try {
			java.sql.Statement stmt = sqlConnection.createStatement();
			String deleteTermEscape = escape(deleteTerm);
			stmt.executeUpdate("insert into "+ deleteListTable + " (" + colName + ") values (\"" + deleteTermEscape + "\");");
			stmt.close();
			disconnect();
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			disconnect();
			e.printStackTrace();
			return false;
		}
		
		return true;
	}
	
	public boolean delete(String deleteTerm){

		try {
			java.sql.Statement stmt = sqlConnection.createStatement();
			String deleteTermEscape= escape(deleteTerm);
			stmt.execute("delete from "+ deleteListTable + " where " + colName + " = \"" + deleteTermEscape +"\";");
			stmt.close();
			disconnect();
			
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			disconnect();
			e.printStackTrace();
			return false;
		}
		
		return true;
	}
	
	public String query(String deleteTerm){
		String deleteTermCheck = null;
		ResultSet resultSet = null;
		try {
			java.sql.Statement stmt = sqlConnection.createStatement();
			String deleteTermEscape = escape(deleteTerm);
			resultSet = stmt.executeQuery("select * from " + deleteListTable + " where " + colName + " = \"" + deleteTermEscape + "\";");
			/* exist check */
			System.out.println(deleteTermEscape + "      " + resultSet);
			if(!resultSet.next()){
				return null;
			}
			deleteTermCheck = resultSet.getString(1);
			stmt.close();
			disconnect();
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			disconnect();
			e.printStackTrace();
		}
		return deleteTermCheck;
	}
	
	private boolean connect(){
//		sqlConnection;
		String jdbcUrl = GlobalContext.getInstance().DeleteList_DB_JDBC_URL;
		String DBName = GlobalContext.getInstance().DeleteList_DB_NAME;
		String userID = GlobalContext.getInstance().DeleteList_DB_USER_ID;
		String userPass = GlobalContext.getInstance().DeleteList_DB_USER_PASS;
		
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
			System.err.println("DeleteListDB Connection Error.");
			return false;
		}
		
		return true;
	}
	
	private boolean disconnect(){
//		sqlConnection;
		try {
			sqlConnection.close();
		} catch (SQLException e) {
			System.err.println("DeleteListDB Disconnection Error.");
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