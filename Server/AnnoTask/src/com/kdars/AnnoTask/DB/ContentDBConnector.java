package com.kdars.AnnoTask.DB;

import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;

import javax.print.attribute.standard.PresentationDirection;

import org.apache.commons.lang3.StringEscapeUtils;

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
			stmt.execute("DROP TRIGGER IF EXISTS update_trigger;");
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
	
	public Document query(String colName, String value){
		ResultSet resultSet = null;
		Document doc = null;
		try {
			java.sql.Statement stmt = sqlConnection.createStatement();
			resultSet = stmt.executeQuery("select * from " + contentTable + " where " + colName + " = " + String.valueOf(value));
			
			doc = new Document(resultSet.getInt(ContentDBSchema.doc_id), resultSet.getString(ContentDBSchema.url), resultSet.getString(ContentDBSchema.collect_date), 
										resultSet.getString(ContentDBSchema.news_date), resultSet.getString(ContentDBSchema.site_name), resultSet.getString(ContentDBSchema.press_name), 
										resultSet.getString(ContentDBSchema.category), unescape(resultSet.getString(ContentDBSchema.title)), unescape(resultSet.getString(ContentDBSchema.body)), 
										resultSet.getString(ContentDBSchema.comments), resultSet.getString(ContentDBSchema.crawler_version));

		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		return doc;
	}

	public boolean update(String columnName, int value){ //for job completion updates
		try {
			java.sql.Statement stmt = sqlConnection.createStatement();
			stmt.execute("update " + jobTable + " set " + columnName + " = 1 where doc_id = " + value);
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		return false;
	}
	
	public ArrayList<Integer> checkUpdates(String columnName, int value){ 
		ResultSet resultSet = null;
		ArrayList<Integer> docID_List = new ArrayList<Integer>();
		try {
			java.sql.Statement stmt = sqlConnection.createStatement();
			// add docID_List where working_status is equal to 0
			resultSet = stmt.executeQuery("select * from " + jobTable + " where " + columnName + " = " + String.valueOf(value));
			while(resultSet.next()){
				docID_List.add(resultSet.getInt("doc_id"));
				System.out.println(docID_List.get(docID_List.size()-1));
			}
			
			// update working_status
			for(int i = 0; i< docID_List.size(); i++){
				stmt.execute("update " + jobTable + " set working_status = 1 where doc_id = " + docID_List.get(i));
			}
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		
		return docID_List;
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
	
	private String escape(String text) {
		String result = StringEscapeUtils.escapeHtml4(text);
		return result;
	}
	
	private String unescape(String text) {
		String result = StringEscapeUtils.unescapeHtml4(text);
		return result;
	}
}
