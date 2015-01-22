package com.kdars.AnnoTask.DB;

import java.sql.DriverManager;
import java.sql.SQLException;
import java.util.Calendar;

import com.kdars.AnnoTask.ContextConfig;

/**
 * 
 * @author kihpark
 * @date 1/21/2015
 * 
 **/
public class ActivityLogConnector {
	private java.sql.Connection sqlConnection;
	private String activityLogTable = ContextConfig.getInstance().ActivityLog_DB_TABLE_NAME;
	
	private Calendar calendar = Calendar.getInstance();

	public ActivityLogConnector(){
		while (!connect());
	}
	
	private boolean connect(){
		String jdbcUrl = ContextConfig.getInstance().ActivityLog_DB_JDBC_URL;
		String DBName = ContextConfig.getInstance().ActivityLog_DB_NAME;
		String userID = ContextConfig.getInstance().ActivityLog_DB_USER_ID;
		String userPass = ContextConfig.getInstance().ActivityLog_DB_USER_PASS;
		
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
			System.err.println("ActivityLog DB Disconnection Error.");
			return	false;
		}
		
		return true;
	}

	public void add_deletelist(String userID, String deleteTerm) {
		try {
			java.sql.Statement stmt = sqlConnection.createStatement();
			stmt.executeUpdate("insert into " + activityLogTable  + "(email, category, deletelist, timestamp) "
					+ "values (\""+userID+"\", \"add_deletelist\", \"" +deleteTerm+ "\", " + calendar.getTime().getTime() +");");
			stmt.close();
		} catch (SQLException e) {
			e.printStackTrace();
		}
	}
	
	public void changeThes2DeleteList(String userID, String deleteTerm) {
		try {
			java.sql.Statement stmt = sqlConnection.createStatement();
			stmt.executeUpdate("insert into " + activityLogTable  + "(email, category, deletelist, conceptTo, timestamp) "
					+ "values (\""+userID+"\", \"chage_thesaurus_to_deletelist\", \"" +deleteTerm+ "\", \"" +deleteTerm+ "\", " + calendar.getTime().getTime() +");");
			stmt.close();
		} catch (SQLException e) {
			e.printStackTrace();
		}

	}

	public void change_DeleteList2Thes(String userID, String conceptFrom, String conceptTo, String metaOntology) {
		try {
			java.sql.Statement stmt = sqlConnection.createStatement();
			stmt.executeUpdate("insert into " + activityLogTable  + "(email, category, deletelist, conceptFrom, conceptTo, metaOntology, timestamp) "
					+ "values (\""+userID+"\", \"chage_deletelist_to_thesaurus\", \"" +conceptFrom+ "\", \"" +conceptFrom+ "\", \"" +conceptTo+ "\", \"" +metaOntology+ "\", "+ calendar.getTime().getTime() +");");
			stmt.close();
		} catch (SQLException e) {
			e.printStackTrace();
		}
	}

	public void add_thesaurus(String userID, String conceptFrom, String conceptTo, String metaOntology) {
		try {
			java.sql.Statement stmt = sqlConnection.createStatement();
			stmt.executeUpdate("insert into " + activityLogTable  + "(email, category, conceptFrom, conceptTo, metaOntology, timestamp) "
					+ "values (\""+userID+"\", \"add_thesaurus\", \"" +conceptFrom+ "\", \"" +conceptTo+ "\", \"" +metaOntology+ "\", "+ calendar.getTime().getTime() +");");
			stmt.close();
		} catch (SQLException e) {
			e.printStackTrace();
		}
	}

	public void update_thesaurus(String userID, String conceptFrom, String conceptTo, String metaOntology) {
		try {
			java.sql.Statement stmt = sqlConnection.createStatement();
			stmt.executeUpdate("insert into " + activityLogTable  + "(email, category, conceptFrom, conceptTo, metaOntology, timestamp) "
					+ "values (\""+userID+"\", \"update_thesaurus\", \"" +conceptFrom+ "\", \"" +conceptTo+ "\", \"" +metaOntology+ "\", "+ calendar.getTime().getTime() +");");
			stmt.close();
		} catch (SQLException e) {
			e.printStackTrace();
		}
	}

}
