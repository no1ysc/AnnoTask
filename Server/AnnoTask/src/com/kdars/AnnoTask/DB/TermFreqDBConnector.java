package com.kdars.AnnoTask.DB;

import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.HashMap;

import com.kdars.AnnoTask.GlobalContext;

public class TermFreqDBConnector {
	private java.sql.Connection sqlConnection;
	private String termFreqTable = GlobalContext.getInstance().TermFreq_DB_TABLE_NAME;
	private String colName1 = "DocID_Term";
	private String colName2 = "DocID";
	private String colName3 = "DocCategory";
	private String colName4 = "Term";
	private String colName5 = "N_gram";
	private String colName6 = "TermFrequency";
	private String colName7 = "TermStatus";

	public TermFreqDBConnector(){
		connect();
	}
	
	public boolean createTable(String tableName){
		return false;
	}
	
	public boolean addDoc(DocByTerm docByTerm){
		String docCategory = docByTerm.getDocCategory();
		int docID = docByTerm.getDocID();
		int nGram = docByTerm.getNGram();

		try {
			for (String addTermCheck : docByTerm.keySet()){
				java.sql.Statement stmt = sqlConnection.createStatement();
				stmt.executeUpdate("insert into "+ termFreqTable + " (" + colName1 + ", " + colName2 + ", " + colName3 + ", " + colName4 + ", " + colName5 + ", " + colName6 + ", " + colName7 + ") values ('" + String.valueOf(docID) + "_" + addTermCheck + "', '" + String.valueOf(docID) + "', '" + docCategory + "', '" + addTermCheck + "', '" + String.valueOf(nGram) + "', '" + String.valueOf(docByTerm.get(addTermCheck)) + "', '0');");
			}

		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
			return false;
		}
		
		return true;
	}
	
	public boolean deleteDoc(DocByTerm docByTerm){
		String docCategory = docByTerm.getDocCategory();
		int docID = docByTerm.getDocID();
		int nGram = docByTerm.getNGram();

		try {
			for (String deleteTermCheck : docByTerm.keySet()){
				java.sql.Statement stmt = sqlConnection.createStatement();
				stmt.execute("delete from "+ termFreqTable + " (" + colName4 + ") values ('" + deleteTermCheck + "');");
			}

		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
			return false;
		}
		
		return true;
	}
	
	/**
	 * 홀더 변경...... 아무도 안잡고 있으면 0
	 * @param term
	 * @param termHolder
	 * @return 락에 성공하면 true, 실패하면 False
	 */
	public boolean updateTermLockState(String term, int termHolder){
		return	true;
	}
	
	/**
	 * 해당 Term 모두 삭제. 
	 * @param term
	 * @return
	 */
	public boolean 	deleteTerm(String term){
		return	false;
	}
	
	/**
	 * Column 한줄 가져옴
	 * @param term
	 * @return
	 */
	public TermByDoc queryTerm(String term){
		return null;
	}
	
	/**
	 * Row 한줄 가져옴.
	 * @param docID
	 * @return
	 */
	public DocByTerm queryDoc(int docID){
		DocByTerm docIDCheck = null;
		ResultSet resultSet = null;
		try {
			java.sql.Statement stmt = sqlConnection.createStatement();
			resultSet = stmt.executeQuery("select * from " + termFreqTable + " where " + colName2 + " = '" + docID + "';");
			/* exist check */
			if(!resultSet.next()){
				return null;
			}
			
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		return docIDCheck;
	}
	
	
	public boolean queryTermFreqByDocIDandNGram(DocByTerm docByTerm){
		int docID = docByTerm.getDocID();
		int nGram = docByTerm.getNGram();
		
		ResultSet resultSet = null;
		
		try {
			java.sql.Statement stmt = sqlConnection.createStatement();
			resultSet = stmt.executeQuery("select * from " + termFreqTable + " where " + colName2 + " = " + docID + " AND " + colName5 + " = " + nGram + ";");
			
			while(resultSet.next()){
				docByTerm.put(resultSet.getString(colName4), resultSet.getInt(colName6));
			}
			
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
			return false;
		}
		
		return true;
	}
	
	
	private boolean connect(){
//		sqlConnection;
		String jdbcUrl = GlobalContext.getInstance().TermFreq_DB_JDBC_URL;
		String DBName = GlobalContext.getInstance().TermFreq_DB_NAME;
		String userID = GlobalContext.getInstance().TermFreq_DB_USER_ID;
		String userPass = GlobalContext.getInstance().TermFreq_DB_USER_PASS;
		
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
 