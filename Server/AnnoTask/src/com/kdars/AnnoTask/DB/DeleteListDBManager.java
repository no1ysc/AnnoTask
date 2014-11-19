package com.kdars.AnnoTask.DB;

public class DeleteListDBManager {
	private static DeleteListDBManager thisClass = new DeleteListDBManager();
	private DeleteListDBConnector deleteListDB;
	
	public DeleteListDBManager(){
		deleteListDB = new DeleteListDBConnector();
	}
	
	public static DeleteListDBManager getInstance(){
		return	thisClass;
	}
	
	/**
	 * 
	 * @param from 
	 * @return to
	 */
	public String queryDeleteTerm(String deleteTerm){
		// TODO : from을 날려서 to가 올라옴.
		return deleteListDB.query(deleteTerm);
	}
	
	public boolean setDeleteTermEntry(String DeleteCandidate,)
	
}
