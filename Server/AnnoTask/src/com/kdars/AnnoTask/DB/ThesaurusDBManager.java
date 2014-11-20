package com.kdars.AnnoTask.DB;

public class ThesaurusDBManager {
	private static ThesaurusDBManager thisClass = new ThesaurusDBManager();
	private ThesaurusDBConnector thesaurusDB;
	private String queryColumnName = "ConceptFrom"; 
	
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
		// thesaurusDB에서 duplication check할 columnName와 term을 넣어서 duplicate이 아니면 thesarus = null, duplicate이 맞으면 thesaurus.getConceptFrom() = "test"
		Thesaurus	thes = thesaurusDB.query(queryColumnName, conceptFrom);
		if (thes == null){
			return null;
		}
		// ...............
		return thes.getConceptFrom();
	}
	
	public boolean setEntry(String from, String to){
		// TODO : for Phase2.
		return false;
	}
}
