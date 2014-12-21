package com.kdars.AnnoTask.DB;

import java.util.ArrayList;

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
	 * 텀의 정보를 가져옴.
	 * @author JS 
	 * @param term
	 * @return
	 */
	public String getTermInformation(String term){
		return thesaurusDB.query(queryColumnName, term).getInformation();
	}
	
	public void deleteTerm(String deleteTerm){
		thesaurusDB.delete(deleteTerm);
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
	
	public boolean setEntry(String from, String to, String metaontology){
		// TODO : for Phase2.
		// thesaurusDB에 잘 더해졌으면 true, exception이면 false.
		Thesaurus thes = new Thesaurus();
		thes.setConceptFrom(from);
		thes.setConceptTo(to);
		thes.setMetaOntology(metaontology);
		return thesaurusDB.add(thes);
	}
	
	public ArrayList<ConceptToList> getConceptToList(){
		return thesaurusDB.queryConceptList();
	}	
	
	public ArrayList<LinkedList> getLinkedList(String id){
		return thesaurusDB.queryLinkedList(id);
	}	
	
	public String getMetaInfo(String id){
		return thesaurusDB.queryMetaInfo(id);
	}		
}
