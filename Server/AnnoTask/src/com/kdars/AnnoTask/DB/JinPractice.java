package com.kdars.AnnoTask.DB;

public class JinPractice {

	public static void main(String[] args) {
		// TODO Auto-generated method stub
		ThesaurusDBConnector dbcon = new ThesaurusDBConnector();
		Thesaurus thes = new Thesaurus();
		//thes = dbcon.query("ConceptFrom", "0-157대장균");
		//System.out.println(thes.getMetaOntology());
		
		thes.setConceptFrom("박기봉");
		thes.setConceptTo("기봉");
		thes.setMetaOntology("agent");
		dbcon.add(thes);
	}

}
