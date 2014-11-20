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
	public boolean CheckForDeleteTerm(String deleteTerm){
		// TODO : term 날려서 term이 deletelistDB에 있으면 true, 없으면 false
		if (deleteListDB.query(deleteTerm) == null){
			return false;
		}
		
		return true;
	}
	
	public boolean AddTermToDelete(String deleteTerm){
		// TODO : term을 성공적으로 deletelistDB에 add했다면 true, 실패했다면 false
		return deleteListDB.add(deleteTerm);
	}
	
	public boolean RemoveTermFromDeleteList(String deleteTerm){
		// TODO : term을 성공적으로 deletelistDB에서부터 remove했다면 true, 실패했다면 false
		return deleteListDB.delete(deleteTerm);
	}
	
}
