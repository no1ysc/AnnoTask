package com.kdars.AnnoTask.DB;

import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;

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
	private String colName8 = "Delete_Pending";
	private Integer flag = 1;
	
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
				//System.out.println("select * from " + termFreqTable + " where " + colName1 + "= \"" + String.valueOf(docID) + "_" +addTermCheck +"\";");
			//	resultSet = stmt.executeQuery("select * from " + termFreqTable + " where " + colName1 + "= \"" + String.valueOf(docID) + "_" +addTermCheck +"\";");
			//	if(!resultSet.next()){
					stmt.executeUpdate("insert into "+ termFreqTable + " (" + colName1 + ", " + colName2 + ", " + colName3 + ", " + colName4 + ", " + colName5 + ", " + colName6 + ", " + colName7 + ") values (\"" + String.valueOf(docID) + "_" + addTerm + "\", '" + String.valueOf(docID) + "', '" + docCategory + "', \"" + addTerm + "\", '" + String.valueOf(nGram) + "', '" + String.valueOf(docByTerm.get(addTermCheck)) + "', '0');");
				//}
				stmt.close();
			}
//			disconnect(sqlConnectionLocal);
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			// 이승철 박기흥 20141218
//			System.out.println("insert into "+ termFreqTable + " (" + colName1 + ", " + colName2 + ", " + colName3 + ", " + colName4 + ", " + colName5 + ", " + colName6 + ", " + colName7 + ") values (\"" + String.valueOf(docID) + "_" + addTerm + "\", '" + String.valueOf(docID) + "', '" + docCategory + "', \"" + addTerm + "\", '" + String.valueOf(nGram) + "', '" + "1', '0');");
//			e.printStackTrace();
//			disconnect(sqlConnectionLocal);
			return false;
		}
		
		return true;
	}
	public int sumTermFrequency(String term, Integer doc_id) {
		ResultSet resultSet = null;
		int sumTermFreq = 0;
		try {
			java.sql.Statement stmt = sqlConnection.createStatement();
			String targetedTerm = escape(term);
			int lastDocId = doc_id+ContextConfig.getInstance().getClientJobUnit()-1;
			
			String query = "select sum(" + colName6 + ") from " + termFreqTable + " where term = \"" + targetedTerm + "\" and docid between " + doc_id + "  and " + lastDocId + ";";
			resultSet = stmt.executeQuery(query);
			while(resultSet.next()){
				sumTermFreq = resultSet.getInt(1);				
			}
			stmt.close();
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		return sumTermFreq;
		
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
	 * @author 기무진규
	 * @param term
	 * @param termHolder
	 * @return 락에 성공하면 true, 실패하면 False
	 */
	public boolean updateTermLockState(ArrayList<Integer> doc_id, int termHolder){
		try {
			java.sql.Statement stmt = sqlConnection.createStatement();
			StringBuilder queryMaker = new StringBuilder();
			queryMaker.append("update " + termFreqTable + " set " + colName7 + " = " + termHolder + " where ");
			for (int docID : doc_id){
				queryMaker.append(colName2 + " = " + Integer.toString(docID) + " OR ");
			}
			queryMaker.replace(queryMaker.length()-4, queryMaker.length(), ";");

			stmt.execute(queryMaker.toString());
			stmt.close();
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		return	true;
	}
	
	public boolean resetTermState(int termHolder){
		try {
			java.sql.Statement stmt = sqlConnection.createStatement();
			String sql = "update " + termFreqTable + " set " + colName7 + " = 0 where " + colName7 + " = " + termHolder + ";";
			stmt.execute(sql);
			stmt.close();
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
			return false;
		}
		
		return true;
	}
	
	/**
	 * 해당 Term 모두 flag. 
	 * @param term
	 * @return
	 */
	public boolean flagDeleteTerm(String term){
		try {
			java.sql.Statement stmt = sqlConnection.createStatement();
			String deleteTerm = escape(term);
			stmt.execute("update " + termFreqTable + " set " + colName8 + " = 1 where " + colName4 + " = \"" + deleteTerm + "\";"); /* AND " + colName7 + " = '" + String.valueOf(termHolder) + "';");*/
			stmt.close();
//		disconnect(sqlConnectionLocal);
	} catch (SQLException e) {
		// TODO Auto-generated catch block
		e.printStackTrace();
//		disconnect(sqlConnectionLocal);
		return false;
	}
	
	return true;
	}
	
	/**
	 * 해당 Term 모두 삭제. 
	 * @param term
	 * @return
	 */
	public boolean deleteTerm(String term){
//		java.sql.Connection sqlConnectionLocal = connect();

		try {
				java.sql.Statement stmt = sqlConnection.createStatement();
				String deleteTerm = escape(term);
				stmt.execute("delete from "+ termFreqTable + " where " + colName4 + " = \"" + deleteTerm + "\";"); /* AND " + colName7 + " = '" + String.valueOf(termHolder) + "';");*/
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
	
	/*
	 * (진규) phase 2.5에서부터 NgramFilter 구현 방식 바뀜.
	 * 구현 계획 1: client로부터 요청된 docID list를 받아서, 해당 docID들에 포함된 term들을 query하고 termFreqByDoc 채운 후, 이들을 묶어서 list로 return.
	 * 구현 계획 2: connector레벨에서 termFreqByDoc을 만들 경우, 나중에 DB단이 바뀔 때 코드가 복잡해지므로 connector레벨에서는 docID, term, ngram, termFrequency, termHolder를 manager로 보내주고, manager에서 termFreqByDoc list를 만드는 구조로 바꿀 예정.
	 */
	public ArrayList<TermFreqByDoc> queryTermConditional(ArrayList<Integer> docIdList){
		ArrayList<TermFreqByDoc> termFreqByDocList = new ArrayList<TermFreqByDoc>();
		ResultSet resultSet = null;
		try {
			StringBuilder queryMaker = new StringBuilder();
			queryMaker.append("select count(*) from " + termFreqTable + " where " + colName8 + " != " + flag + " AND (");
			for (int docID : docIdList){
				queryMaker.append(colName2 + " = " + String.valueOf(docID) + " OR ");
			}
			queryMaker.replace(queryMaker.length()-4, queryMaker.length(), ");");
			
			java.sql.Statement stmt = sqlConnection.createStatement();
			resultSet = stmt.executeQuery(queryMaker.toString());
			resultSet.next();
			int rowCnt = resultSet.getInt(1);
			
			
			queryMaker = new StringBuilder();
			queryMaker.append("select " + colName2 + ", " + colName4 + ", " + colName5 + ", " + colName6 + " from " + termFreqTable + " where ");
			for (int docID : docIdList){
				queryMaker.append(colName2 + " = " + String.valueOf(docID) + " OR ");
			}
			queryMaker.replace(queryMaker.length()-4, queryMaker.length(), ";");
			stmt = sqlConnection.createStatement();
			
			
			
			resultSet = stmt.executeQuery(queryMaker.toString());
			
			int a=0;
			boolean forLoopBreaker;

			
			
			
			resultSet.setFetchSize(rowCnt);
			while (resultSet.next()){
	
				a++;
				
				
				forLoopBreaker = false;
				int docID = resultSet.getInt(1);				
				String term = unescape(resultSet.getString(2));
				int nGram = resultSet.getInt(3);
				int termFreq = resultSet.getInt(4);
				
			
				
				for (TermFreqByDoc appendTermFreqByDoc : termFreqByDocList){
					if (appendTermFreqByDoc.getTerm().equals(term)){
						appendTermFreqByDoc.put(docID, termFreq);
						forLoopBreaker = true;
						continue;
					}
				}
				
				
				if (forLoopBreaker){
					continue;
				}
				
				TermFreqByDoc newTermFreqByDoc = new TermFreqByDoc(term, nGram);
				newTermFreqByDoc.put(docID, termFreq);
				termFreqByDocList.add(newTermFreqByDoc);
	
			}
			
		} catch (SQLException e) {
			// TODO Auto-generated catch block
//			disconnect(sqlConnection);
			e.printStackTrace();
		}
				
		return termFreqByDocList;
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
				docByTerm.put(unescape(resultSet.getString(colName4)), resultSet.getInt(colName6)); //colName4는 term, colName6는 term freq.
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
 
