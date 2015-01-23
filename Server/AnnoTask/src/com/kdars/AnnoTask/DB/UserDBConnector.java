package com.kdars.AnnoTask.DB;

import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.SQLException;

import com.kdars.AnnoTask.ContextConfig;
import com.kdars.AnnoTask.Server.Command.Server2Client.UserInfo;

/**
 * 
 * @author kihpark
 * @date 1/21/2015
 * 
 **/
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

	public UserInfo getUserInfo(String emailAddress) {
		UserInfo userInfo = new UserInfo();
		ResultSet resultSet = null;
		try {
			java.sql.Statement stmt = sqlConnection.createStatement();
			resultSet = stmt.executeQuery("select * from " + userAccountsTable + " where email = \"" + emailAddress + "\";");
			while(resultSet.next()){
				userInfo.userId = resultSet.getString(2); //email
				userInfo.userName = resultSet.getString(3); //user_name
			}
			stmt.close();

		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
			return null;
		}
		return userInfo;
	}

	public UserInfo loginCheck(String loginID, String password) {
		UserInfo userInfo = new UserInfo();
		ResultSet resultSet = null;
		try {
			java.sql.Statement stmt = sqlConnection.createStatement();
			resultSet = stmt.executeQuery("select * from " + userAccountsTable + " where email = \"" + loginID + "\";");
			while(resultSet.next()){
				if(password.equals(resultSet.getString(4))){
					userInfo = getUserInfo(loginID);
				}else{
					return null;
				}
			}
			stmt.close();
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
			return null;
		}
		return userInfo;
	}

	// 유저 접속중 상태 flag 활성화
	public void activateUser(String userID) {
		try {
			java.sql.Statement stmt = sqlConnection.createStatement();
			stmt.execute("update " + userAccountsTable + " set isWorking = 1 where email = \"" + userID + "\";");
			stmt.close();
		} catch (SQLException e) {
			e.printStackTrace();
		}
	}

	// 유저 접속중 상태 flag 비활성화
	public void deactivateUser(String userID) {
		try {
			java.sql.Statement stmt = sqlConnection.createStatement();
			stmt.execute("update " + userAccountsTable + " set isWorking = 0 where email = \"" + userID + "\";");
			stmt.close();
		} catch (SQLException e) {
			e.printStackTrace();
		}
	}

	public void increaseLoginCount(String userID) {
		try {
			java.sql.Statement stmt = sqlConnection.createStatement();
			stmt.execute("update " + userAccountsTable + " set login_count = login_count+1 where email = \"" + userID + "\";");
			stmt.close();
		} catch (SQLException e) {
			e.printStackTrace();
		}		
	}

	public void increaseDeleteListAddedCount(String userID) {
		try {
			java.sql.Statement stmt = sqlConnection.createStatement();
			stmt.execute("update " + userAccountsTable + " set deleteListAdded_count = deleteListAdded_count+1 where email = \"" + userID + "\";");
			stmt.close();
		} catch (SQLException e) {
			e.printStackTrace();
		}		
	}

	public void increaseThesaurusAddedCount(String userID) {
		try {
			java.sql.Statement stmt = sqlConnection.createStatement();
			stmt.execute("update " + userAccountsTable + " set thesaurusAdded_count = thesaurusAdded_count+1 where email = \"" + userID + "\";");
			stmt.close();
		} catch (SQLException e) {
			e.printStackTrace();
		}		
	}

	public boolean checkUserID(String checkUserID) {
		ResultSet resultSet = null;
		try {
			java.sql.Statement stmt = sqlConnection.createStatement();
			resultSet = stmt.executeQuery("select * from " + userAccountsTable + " where email = \"" + checkUserID + "\";");
			while(resultSet.next()){
				if(resultSet.getInt(1) == 1){
					return true;
				}
			}
			stmt.close();
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}

		return false;
	}
	
}
