package com.kdars.AnnoTask.DB;

public class TermFreqDBConnector {
	private java.sql.Connection sqlConnection;
	
	public boolean createTable(String tableName){
		return false;
	}
	
	public boolean addDoc(DocByTerm docByTerm){
		return false;
	}
	
	public boolean deleteDoc(DocByTerm docByTerm){
		return false;
	}
	
	/**
	 * 홀더 변경...... 아무도 안잡고 있으면 0
	 * @param term
	 * @param termHolder
	 * @return
	 */
	public boolean updateTermLockState(String term, int termHolder){
		return	false;
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
		return null;
	}
	
	
	private boolean connect(){
		return false;
	}
	
	private boolean disconnect(){
		return false;
	}
}
 