package LogicFromKihpark;
import java.util.ArrayList;


public class Checker {
	public String[] endingWord = { "��", "��", "��", "��", "����", "����", "��", "��", "��", "��", "����", "����", "����", "����", "��", "����", "��", "��", "��", "��" };
	
	public boolean check(ArrayList<String> ngramTerms, GeneralizationThesaurus genThes){
			
		for(int ngramIndex = 0; ngramIndex < ngramTerms.size(); ngramIndex++){
			if(genThes.thesaurus.containsKey(ngramTerms.get(ngramIndex))){ // ngram �ܾ ������ ������� 
				ngramTerms.set(ngramIndex, genThes.thesaurus.get(ngramTerms.get(ngramIndex))); // conceptTo �ܾ� ����
			}else{ // ���簡 �پ �ν��� ���߰ų� �ƴϸ� �������� ���ɼ�...
				if(ngramTerms.get(ngramIndex).length() != 1){ // tokenized�� �ܾ 1�����̸� �н�~
					for(int ewIndex = 0; ewIndex < endingWord.length; ewIndex++){
						if(endingWord[ewIndex].equals(ngramTerms.get(ngramIndex).substring(ngramTerms.get(ngramIndex).length()-endingWord[ewIndex].length(), ngramTerms.get(ngramIndex).length()))){
							
							return true;
						}
					}				
				}				
			}
		}
		return false;
	}
	
	// ���� �ܾ ������ �ֳ� ���� üũ
	public boolean check(String ngramTerm, GeneralizationThesaurus genThes) {
		if(genThes.thesaurus.containsKey(ngramTerm)){ // ngram �ܾ ������ ������� 
			return true; 
		}
		return false;
	}

	public boolean checkAfterStemming(String ngramTerm, GeneralizationThesaurus genThes) {
		if(ngramTerm.length() != 1){ // �Է� �ܾ 1�����̸� �н�~
			for(int ewIndex = 0; ewIndex < endingWord.length; ewIndex++){ // ��� ���縦 ���ϰڴٴ�
				if(endingWord[ewIndex].equals(ngramTerm.substring(ngramTerm.length()-endingWord[ewIndex].length(), ngramTerm.length()))){ // ���簡 �ڿ� �پ����� üũ
					String wordAfterStem = ngramTerm.substring(0, ngramTerm.length()-endingWord[ewIndex].length()); // ���� �����
					return check(wordAfterStem, genThes); // ���� ��� �ܾ ������ �ִ��� ������ üũ
				}
			}
		}else{
			return check(ngramTerm, genThes);
		}
		return false;						
	}
}
