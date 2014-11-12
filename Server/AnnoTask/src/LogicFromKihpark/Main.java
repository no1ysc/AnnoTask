package LogicFromKihpark;
import java.io.IOException;
import java.util.ArrayList;


public class Main {

	public static void main(String[] args) {
		// TODO Auto-generated method stub
		Tokenizer tk = new Tokenizer(Configure.text);
		ArrayList <String> ngramTerms = tk.getNgramTerms(Configure.n);
		
		// Test#1
		for (String a : ngramTerms){
			System.out.println(a);
		}
		
		GeneralizationThesaurus genThes = new GeneralizationThesaurus();
		genThes.read(Configure.filename);
				
		// Test#2
		System.out.println(genThes.thesaurus.get("�����Ƿ����"));
		
		
		Checker ck = new Checker();
		for(int i = 0; i < ngramTerms.size(); i++){
			if(ck.check(ngramTerms.get(i), genThes)){
				ngramTerms.set(i, genThes.thesaurus.get(ngramTerms.get(i))); // conceptTo �ܾ� ����
			}else{
				if(ck.checkAfterStemming(ngramTerms.get(i), genThes)){
					ngramTerms.set(i, genThes.thesaurus.get("���縦 ��� �ܾ�"));
				}else{
					//������ ����Ʈ�� �߰�
				}
			}
		}
		System.out.println(ck.check(ngramTerms, genThes));
	}
}
