package com.kdars.AnnoTask.DB;

import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.SQLException;

import org.apache.commons.lang3.StringEscapeUtils;

import com.kdars.AnnoTask.ContextConfig;

public class TermFreqDBConnector {
	private java.sql.Connection sqlConnection;
	private String termFreqTable = ContextConfig.getInstance().TermFreq_DB_TABLE_NAME;
	private String colName1 = "DocID_Term";
	private String colName2 = "DocID";
	private String colName3 = "DocCategory";
	private String colName4 = "Term";
	private String colName5 = "N_gram";
	private String colName6 = "TermFrequency";
	private String colName7 = "TermStatus";

	public TermFreqDBConnector(){
		//TODO: Connector 생성되면 connect 시도해서 성공하면 ok, 실패하면 표시.
//		java.sql.Connection sqlConnection;
		if ((sqlConnection = connect()) == null){
			System.exit(2);
		}
//		disconnect(sqlConnection);
//		while(connect());
	}
	
	public boolean createTable(String tableName){
		return false;
	}
	
	public boolean addDoc(DocTermFreqByTerm docByTerm) {
//		java.sql.Connection sqlConnectionLocal = connect();
		String docCategory = docByTerm.getDocCategory();
		int docID = docByTerm.getDocID();
		int nGram = docByTerm.getNGram();

		String addTerm = null;
		try {
			for (String addTermCheck : docByTerm.keySet()){
				java.sql.Statement stmt = sqlConnection.createStatement();
				addTerm = escape(addTermCheck);
				stmt.executeUpdate("insert into "+ termFreqTable + " (" + colName1 + ", " + colName2 + ", " + colName3 + ", " + colName4 + ", " + colName5 + ", " + colName6 + ", " + colName7 + ") values (\"" + String.valueOf(docID) + "_" + addTerm + "\", '" + String.valueOf(docID) + "', '" + docCategory + "', \"" + addTerm + "\", '" + String.valueOf(nGram) + "', '" + String.valueOf(docByTerm.get(addTermCheck)) + "', '0');");
				stmt.close();
			}
//			disconnect(sqlConnectionLocal);
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			System.out.println("insert into "+ termFreqTable + " (" + colName1 + ", " + colName2 + ", " + colName3 + ", " + colName4 + ", " + colName5 + ", " + colName6 + ", " + colName7 + ") values (\"" + String.valueOf(docID) + "_" + addTerm + "\", '" + String.valueOf(docID) + "', '" + docCategory + "', \"" + addTerm + "\", '" + String.valueOf(nGram) + "', '" + "1', '0');");
			e.printStackTrace();
//			disconnect(sqlConnectionLocal);
			return false;
		}
		
		return true;
	}
	
	public boolean deleteDoc(DocTermFreqByTerm docByTerm){
//		java.sql.Connection sqlConnectionLocal = connect();
		String docCategory = docByTerm.getDocCategory();
		int docID = docByTerm.getDocID();
		int nGram = docByTerm.getNGram();

		try {
			for (String deleteTermCheck : docByTerm.keySet()){
				java.sql.Statement stmt = sqlConnection.createStatement();
				String deleteTerm = escape(deleteTermCheck);
				stmt.execute("delete from "+ termFreqTable + " where " + colName4 + " = \"" + deleteTerm + "\";");
				stmt.close();
			}
//			disconnect(sqlConnectionLocal);
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
//			disconnect(sqlConnectionLocal);
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
		try {
			java.sql.Statement stmt = sqlConnection.createStatement();
			String escapedTerm = escape(term);
			stmt.execute("update TFtable set TermStatus = " + termHolder + " where Term = \"" + escapedTerm + "\";");
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		return	true;
	}
	
	public boolean resetTermState(int termHolder){
		try {
			java.sql.Statement stmt = sqlConnection.createStatement();
			stmt.execute("update TFtable set TermStatus = 0 where TermStatus = " + termHolder + ";");			
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
			return false;
		}
		
		return true;
	}
	
	/**
	 * 해당 Term 모두 삭제. 
	 * @param term
	 * @return
	 */
	public boolean deleteTerm(String term, int termHolder){
//		java.sql.Connection sqlConnectionLocal = connect();

		try {
				java.sql.Statement stmt = sqlConnection.createStatement();
				String deleteTerm = escape(term);
				stmt.execute("delete from "+ termFreqTable + " where " + colName4 + " = \"" + deleteTerm + "\" AND " + colName7 + " = '" + String.valueOf(termHolder) + "';");
				stmt.close();
//			disconnect(sqlConnectionLocal);
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
//			disconnect(sqlConnectionLocal);
			return false;
		}
		
		return true;
	}
	
	/**
	 * Column 한줄 가져옴
	 * @param term
	 * @return
	 */
	public TermFreqByDoc queryTerm(String term){
		return null;
	}
	
	/**
	 * Row 한줄 가져옴.
	 * @param docID
	 * @return
	 */
	public DocTermFreqByTerm queryDoc(int docID){
//		java.sql.Connection sqlConnectionLocal = connect();
		DocTermFreqByTerm docIDCheck = null;
		ResultSet resultSet = null;
		try {
			java.sql.Statement stmt = sqlConnection.createStatement();
			resultSet = stmt.executeQuery("select * from " + termFreqTable + " where " + colName2 + " = '" + docID + "';");
			/* exist check */
			if(!resultSet.next()){
				
			}
				
			stmt.close();
		} catch (SQLException e) {
			// TODO Auto-generated catch block
//			disconnect(sqlConnection);
			e.printStackTrace();
		}
//		disconnect(sqlConnection);
		
		return docIDCheck;
	}
	
	public boolean queryTermFreqByDocIDandNGram(DocTermFreqByTerm docByTerm){
//		java.sql.Connection sqlConnectionLocal = connect();
		int docID = docByTerm.getDocID();
		int nGram = docByTerm.getNGram();
		
		ResultSet resultSet = null;
		
		try {
			java.sql.Statement stmt = sqlConnection.createStatement();
			resultSet = stmt.executeQuery("select * from " + termFreqTable + " where " + colName2 + " = " + docID + " AND " + colName5 + " = " + nGram + " AND TermStatus = 0;");
			
			while(resultSet.next()){
				docByTerm.put(resultSet.getString(colName4), resultSet.getInt(colName6)); //colName4는 term, colName6는 term freq.
			}
			
			stmt.close();
			
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
//			disconnect(sqlConnectionLocal);
			return false;
		}
		
//		disconnect(sqlConnectionLocal);
		
		return true;
	}
	
	
	private java.sql.Connection connect(){
//		sqlConnection;
		java.sql.Connection sqlConnection = null;
		
		String jdbcUrl = ContextConfig.getInstance().TermFreq_DB_JDBC_URL;
		String DBName = ContextConfig.getInstance().TermFreq_DB_NAME;
		String userID = ContextConfig.getInstance().TermFreq_DB_USER_ID;
		String userPass = ContextConfig.getInstance().TermFreq_DB_USER_PASS;
		
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
			System.err.println("TermDB Connection Error.");
			disconnect(sqlConnection);
			return null;
		}
		
		return sqlConnection;
	}
	
	private String escape(String text) {
		String result = StringEscapeUtils.escapeHtml4(text);
		return result;
	}
	
	private String unescape(String text) {
		String result = StringEscapeUtils.unescapeHtml4(text);
		return result;
	}
	
	private boolean disconnect(java.sql.Connection sqlConnection){
//		sqlConnection;
		try {
			sqlConnection.close();
		} catch (SQLException e) {
			e.printStackTrace();
			System.err.println("TermDB Disconnection Error.");
			return	false;
		} catch (NullPointerException e) {
			e.printStackTrace();
			System.err.println("TermDB Disconnection Error.");
			return	false;
		}
		
		return true;
	}
}
 
