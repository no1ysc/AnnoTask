package com.kdars.AnnoTask.DB;

import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;

import org.apache.commons.lang3.StringEscapeUtils;

import com.kdars.AnnoTask.ContextConfig;
import com.mysql.jdbc.Connection;

public class ThesaurusDBConnector {
	private String conceptFromTable = ContextConfig.getInstance().Thesaurus_DB_TABLE_NAME1;
	private String conceptToTable = ContextConfig.getInstance().Thesaurus_DB_TABLE_NAME2;
	private String fromColName1 = "ConceptFrom_ID";
	private String fromColName2 = "ConceptFrom";
	private String foreignKeyColName = "ConceptTo_ID";
	private String toColName1 = "ConceptTo";
	private String toColName2 = "MetaOntology";
	private String toColName3 = "MetaName";
	private String toColName4 = "POS";
	private String toColName5 = "Definition";
	
	private java.sql.Connection sqlConnection;
	// TODO : 향후 한번에 조절하기 위해 모아야할 정보 : SQL 커넥션 정보,
	
	public ThesaurusDBConnector(){
		//TODO: Connector 생성되면 connect 시도해서 성공하면 ok, 실패하면 표시.
//		java.sql.Connection sqlConnection;
		if ((sqlConnection = connect()) == null){
			System.exit(2);
		}
//		disconnect(sqlConnection);
//		while(connect());
	}
	
	public boolean add(Thesaurus thes){

		try {
			java.sql.Statement stmt = sqlConnection.createStatement();
			String conceptToEscape = escape(thes.getConceptTo());
			String conceptFromEscape = escape(thes.getConceptFrom());
			String conceptToInsertQuery = "insert into " + conceptToTable + " (" + toColName1 + ", " + toColName2 + ", " + toColName3 + ") values (\"" + conceptToEscape + "\", '" + thes.getMetaOntology() + "', '" + thes.getMetaOntology() + "');";
			String conceptToIDgetQuery = "select " + foreignKeyColName + " from " + conceptToTable + " where " + toColName1 + " = \"" + conceptToEscape + "\";";
			
			ResultSet checkConceptToID = stmt.executeQuery(conceptToIDgetQuery);
			if (!checkConceptToID.next()){		/// 새로 ConceptTo를 적는경우
				stmt.executeUpdate(conceptToInsertQuery);
				ResultSet getConceptToID = stmt.executeQuery(conceptToIDgetQuery);
				getConceptToID.next();
				thes.setConceptToID(getConceptToID.getInt(1));
				String conceptFromInsertQuery = "insert into " + conceptFromTable + " (" + fromColName2 + ", " + foreignKeyColName + ") values (\"" + conceptFromEscape + "\", '" + String.valueOf(thes.getConceptToID()) + "');";
				stmt.executeUpdate(conceptFromInsertQuery);
				stmt.close();
				return true;
			} 
			// 기존에 있는 ConceptTo 를 반영해야할 경우.
			// 윗 로직에서 검증거쳐왔으므로 여기선 무조껀 바꾸어주는걸로,
				
			thes.setConceptToID(checkConceptToID.getInt(1));
			java.sql.Statement stmt2 = sqlConnection.createStatement();
			stmt2.executeUpdate("update " + conceptToTable + " set " + toColName2 + "=\"" + thes.getMetaOntology() + "\", " + toColName3 + "=\"" + thes.getMetaOntology() + "\" where " + foreignKeyColName + "=\"" + thes.getConceptToID() + "\";");
			
			String conceptFromInsertQuery = "insert into " + conceptFromTable + " (" + fromColName2 + ", " + foreignKeyColName + ") values (\"" + conceptFromEscape + "\", '" + String.valueOf(thes.getConceptToID()) + "');";
			stmt.executeUpdate(conceptFromInsertQuery);
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
	
	public boolean delete(String deleteTerm){  
		ResultSet resultset = null;
		try {
			java.sql.Statement stmt = sqlConnection.createStatement();
			String escapedConceptFrom = escape(deleteTerm);
			resultset = stmt.executeQuery("select * from " + conceptToTable + " where " + toColName1 + " = \"" + escapedConceptFrom + "\";");
			if (resultset.next()){
				int conceptToID = resultset.getInt(1);
				java.sql.Statement stmt2 = sqlConnection.createStatement();
				stmt2.execute("delete from " + conceptFromTable + " where " + foreignKeyColName + " = " + conceptToID + ";");
				stmt2.execute("delete from " + conceptToTable + " where " + foreignKeyColName + " = " + conceptToID + ";");
				stmt2.close();
			}else{
				java.sql.Statement stmt3 = sqlConnection.createStatement();
				stmt3.execute("delete from " + conceptFromTable + " where " + fromColName2 + " = \"" + escapedConceptFrom + "\";");
				stmt3.close();
			}
			stmt.close();
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		
		return false;
	}
	
	public Thesaurus query(String colName, String value){
		// TODO checks for duplicates in ThesaurusDB.  Null if there is no duplicate. Thesaurus containing a row in ThesaurusDB if there is a duplicate.
//		java.sql.Connection sqlConnectionLocal = connect();
		Thesaurus conceptFromTermCheck = new Thesaurus();
		ResultSet resultSet = null;
		try {
			java.sql.Statement stmt = sqlConnection.createStatement();
			String valueEscape = escape(value);
			resultSet = stmt.executeQuery("select * from " + conceptFromTable + ", " + conceptToTable + " where " + colName + " = \"" + valueEscape + "\" and " + conceptFromTable + "." + foreignKeyColName + " = " + conceptToTable + "." + foreignKeyColName + ";");
			/* exist check */
			if(!resultSet.next()){
				stmt.close();
//				disconnect(sqlConnectionLocal);
				return null;
			}

			conceptFromTermCheck.setConceptFromID(resultSet.getInt(1));
			conceptFromTermCheck.setConceptFrom(resultSet.getString(2));
			conceptFromTermCheck.setConceptToID(resultSet.getInt(3));
			conceptFromTermCheck.setConceptTo(resultSet.getString(5));
			conceptFromTermCheck.setMetaOntology(resultSet.getString(6));
			conceptFromTermCheck.setMetaName(resultSet.getString(7));
			conceptFromTermCheck.setPOS(resultSet.getString(8));
			conceptFromTermCheck.setDefinition(resultSet.getString(9));
			
			stmt.close();
//			disconnect(sqlConnectionLocal);
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
//			disconnect(sqlConnectionLocal);
		}
		return conceptFromTermCheck;
	}
	
	private java.sql.Connection connect(){
//		sqlConnection;
		java.sql.Connection sqlConnection = null;
		
		String jdbcUrl = ContextConfig.getInstance().Thesaurus_DB_JDBC_URL;
		String DBName = ContextConfig.getInstance().Thesaurus_DB_NAME;
		String userID = ContextConfig.getInstance().Thesaurus_DB_USER_ID;
		String userPass = ContextConfig.getInstance().Thesaurus_DB_USER_PASS;
		
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
			System.err.println("ThesaurusDB Connection Error.");
			disconnect(sqlConnection);
			return null;
		}
		
		return sqlConnection;
	}
	
	private boolean disconnect(java.sql.Connection sqlConnection){
//		sqlConnection;
		try {
			sqlConnection.close();
		} catch (SQLException e) {
			System.err.println("ThesaurusDB Disconnection Error.");
			return	false;
		} catch (NullPointerException e) {
			System.err.println("ThesaurusDB Disconnection Error.");
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
	
	/**
	 * @author ????
	 * @param queryTerm
	 * @return
	 * 이승철 수정, 20141223, 컨셉투 쿼리방식 수정
	 * 이승철 수정, 20150126, 클라이언트에서 쿼리하는 방식이 타자칠때마다가 아닌 특정키(엔터)를 누를때만 요청하는것으로 바뀌었으므로, 갯수제한은 위에서 채크한다.
	 */
	public ArrayList<ConceptToList> queryConceptList(String queryTerm) {
		ArrayList<ConceptToList> ret = new ArrayList<ConceptToList>();
		
		ResultSet resultSet = null;
		try {
			java.sql.Statement stmt = sqlConnection.createStatement();
			resultSet = stmt.executeQuery("select * from " + conceptToTable + " where " + toColName1 + " like \"%" + queryTerm +"%\";");
//			int queryCount = ContextConfig.getInstance().getLimitConceptToCount();
			while (resultSet.next()){
				ret.add(new ConceptToList(resultSet.getInt(foreignKeyColName), resultSet.getString(toColName1), resultSet.getString(toColName2)));
//				if (--queryCount == 0){
//					break;
//				}
			}
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		
		return ret;
	}
	
	public ArrayList<LinkedList> queryLinkedList(String id) {
		ArrayList<LinkedList> ret = new ArrayList<LinkedList>();
		
		ResultSet resultSet = null;
		try {
			java.sql.Statement stmt = sqlConnection.createStatement();
			resultSet = stmt.executeQuery("select * from " + conceptFromTable +" where "+ foreignKeyColName+" = "+id);
			while (resultSet.next()){
				ret.add(new LinkedList(resultSet.getString(fromColName2)));	
			}
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		
		return ret;
	}
	
	public String queryMetaInfo(String id) {
		String ret = "";
		
		ResultSet resultSet = null;
		try {
			java.sql.Statement stmt = sqlConnection.createStatement();
			resultSet = stmt.executeQuery("select * from " + conceptToTable +" where "+ foreignKeyColName+" = "+id);
			while (resultSet.next()){
				ret = resultSet.getString(toColName2);	
			}
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		
		return ret;
	}
}
