package com.kdars.AnnoTask.DB;

import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;

import com.kdars.AnnoTask.GlobalContext;

public class ContentDBConnector {
	private java.sql.Connection sqlConnection;
	private String tableName = GlobalContext.getInstance().CONTENT_DB_TABLE_NAME;
	
	public ContentDBConnector(){
		while (!connect());
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

	public ArrayList<Integer> queryFromDate(String startDate, String endDate,
			boolean bNaver, boolean bDaum, boolean bNate) {
		ArrayList<Integer> ret = new ArrayList<Integer>();
		ArrayList<String> sites = new ArrayList<String>();
		if (bNaver){
			sites.add("Naver");
		}
		if (bDaum){
			sites.add("Daum");
		}
		if (bNate){
			sites.add("Nate");
		}
		
		
		ResultSet resultSet = null;
		try {
			java.sql.Statement stmt = sqlConnection.createStatement();
			//resultSet = stmt.executeQuery("select * from "+ "test_table" + " where " + colName + " = " + String.valueOf(value));
			resultSet = stmt.executeQuery("select DocID from ContentDB where newsDate >= ('startDate') AND newsDate <= ('endDate')");
			
			
			while (resultSet.next()){
				if(sites.contains(resultSet.getString(4))){
					ret.add(resultSet.getInt(1));
				}					
			
			}
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		
		return ret;
	}
}
