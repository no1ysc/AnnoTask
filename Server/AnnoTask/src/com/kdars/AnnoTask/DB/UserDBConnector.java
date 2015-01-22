package com.kdars.AnnoTask.DB;

import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.SQLException;

import com.kdars.AnnoTask.ContextConfig;

public class UserDBConnector {
	private java.sql.Connection sqlConnection;
	private String userAccountsTable = ContextConfig.getInstance().UserAccounts_DB_TABLE_NAME;

	public UserDBConnector(){
		while (!connect());
	}
	
	private boolean connect(){
		String jdbcUrl = ContextConfig.getInstance().UserAccounts_DB_JDBC_URL;
		String DBName = ContextConfig.getInstance().UserAccounts_DB_NAME;
		String userID = ContextConfig.getInstance().UserAccounts_DB_USER_ID;
		String userPass = ContextConfig.getInstance().UserAccounts_DB_USER_PASS;
		
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
			System.err.println("UserAccountsDB Connection Error.");
			return false;
		}
		
		return true;
	}
	
	private boolean disconnect(){
		try {
			sqlConnection.close();
		} catch (SQLException e) {
			System.err.println("UserAccountsDB Disconnection Error.");
			return	false;
		}
		
		return true;
	}
	
	public boolean registerNewUser(String email, String password, String userName){
		try {
			java.sql.Statement stmt = sqlConnection.createStatement();
			stmt.executeUpdate("insert into " + userAccountsTable  + "(email, user_name, password) values (\""+email+"\", \"" +userName+ "\", \"" +password+ "\");");
			stmt.close();
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
			return false;
		}
		return true;
	}

	public String getUserId(String emailAddress) {
		ResultSet resultSet = null;
		String userID = "";
		try {
			java.sql.Statement stmt = sqlConnection.createStatement();
			resultSet = stmt.executeQuery("select * from " + userAccountsTable + " where email = \"" + emailAddress + "\";");
			while(resultSet.next()){
				userID = resultSet.getString(2); //email
			}
			stmt.close();

		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
			return null;
		}
		return userID;
	}
	
}
