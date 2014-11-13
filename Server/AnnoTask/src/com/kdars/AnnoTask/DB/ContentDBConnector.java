package com.kdars.AnnoTask.DB;

import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;

import com.kdars.AnnoTask.GlobalContext;

public class ContentDBConnector {
	private java.sql.Connection sqlConnection;
	private String contentTable = GlobalContext.getInstance().CONTENT_DB_contentTABLE_NAME;
	private String jobTable = GlobalContext.getInstance().CONTENT_DB_jobTABLE_NAME;
	
	// These queries for creating trigger in ContentDB
	String Query_FOR_CREATE_TRIGGER = "CREATE TRIGGER update_trigger "
														+ "AFTER INSERT ON "
														+ contentTable + " "
														+ "FOR EACH ROW "
														+ "BEGIN "
														+ "INSERT INTO " + jobTable 
														+ "(doc_id, working_status, complete_status) " 
														+ "VALUES " 
														+ "(NEW.doc_id, 0, 0);"
														+ "END;";
	
	public ContentDBConnector(){
		while (!connect());
		setTrigger();
	}
	
	private void setTrigger() {
		try {
			java.sql.Statement stmt = sqlConnection.createStatement();
			stmt.execute(Query_FOR_CREATE_TRIGGER);
			stmt.close();
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}

	public boolean add(Document document){
		return false;
	}
	
	public boolean delete(Document document){
		return false;
	}

	public Document query(String colName, String value){ // TODO : 다 고쳐야함
//		ArrayList<QueueEntry> result = new ArrayList<QueueEntry>();
//		String query = "\""+queryURL+"\"";
//		java.sql.Statement stmt = null;
//		ResultSet rs = null;
//		try {
//			String cmd = "select * from " + tableName + " where url=" + query + ";";
//			stmt = conn.createStatement();
//			rs = stmt.executeQuery(cmd);
//			
//			if(tableName.equals("url")){
//				while(rs.next()){e
//					QueueEntry QueueEntrytemp = new QueueEntry();
//					QueueEntrytemp.setSiteURL(rs.getString(1));
//					result.add(QueueEntrytemp);		
//				}
//			}else{
//				while(rs.next()){
//					QueueEntry QueueEntrytemp = new QueueEntry();
//					Article articleTemp = new Article();
//					QueueEntrytemp.setSiteURL(rs.getString(5));
//					articleTemp.date = rs.getString(2);
//					articleTemp.press = rs.getString(4);
//					articleTemp.title = unescape(rs.getString(5));
//					articleTemp.content = unescape(rs.getString(6));
//					
//					QueueEntrytemp.setArticle(articleTemp);
//					result.add(QueueEntrytemp);					
//				}
//			}
//			
//		} catch (SQLException e) {
//			// TODO Auto-generated catch block
//			e.printStackTrace();
//		}
		return null;
	}
	
	
	private boolean connect(){
		String jdbcUrl = GlobalContext.getInstance().CONTENT_DB_JDBC_URL;
		String DBName = GlobalContext.getInstance().CONTENT_DB_NAME;
		String userID = GlobalContext.getInstance().CONTENT_DB_USER_ID;
		String userPass = GlobalContext.getInstance().CONTENT_DB_USER_PASS;
		
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
		try {
			sqlConnection.close();
		} catch (SQLException e) {
			System.err.println("ContentDB Disconnection Error.");
			return	false;
		}
		
		return true;
	}
	
	private void reConnect(){
		disconnect();
		while(!connect());
	}
}
