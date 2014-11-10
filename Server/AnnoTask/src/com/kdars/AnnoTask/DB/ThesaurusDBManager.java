package com.kdars.AnnoTask.DB;

public class ThesaurusDBManager {
	private static ThesaurusDBManager thisClass = new ThesaurusDBManager();
	private ThesaurusDBConnector thesaurusDB;
	
	public ThesaurusDBManager(){
		thesaurusDB = new ThesaurusDBConnector();
	}
	
	public static ThesaurusDBManager getInstance(){
		return	thisClass;
	}
	
	/**
	 * 
	 * @param from 
	 * @return to
	 */
	public String queryTerm(String conceptFrom){
		// TODO : from을 날려서 to가 올라옴.
		Thesaurus	thes = thesaurusDB.query("ConceptFrom", conceptFrom);
		// ...............
		return null;
	}
	
	public boolean setContent(String from, String to){
		// TODO : for Phase2.
		return false;
	}
}
