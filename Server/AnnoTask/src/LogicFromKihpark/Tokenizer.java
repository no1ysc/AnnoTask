package LogicFromKihpark;
import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.File;
import java.io.FileOutputStream;
import java.io.FileReader;
import java.io.IOException;
import java.io.OutputStreamWriter;
import java.io.Writer;
import java.util.ArrayList;
import java.util.Collections;
import java.util.Enumeration;
import java.util.StringTokenizer;


public class Tokenizer {
	private ArrayList <String> termList;
	
	public Tokenizer(String inputText){
		termList = (ArrayList<String>)((ArrayList)Collections.list(new StringTokenizer(inputText)));		
	}
	
	public void tokenizeInputText(String input) {
		//String delims = " :;//.,*#$@%&<>()[]!?_+-=~'\"0123456789\t";
	}
	
	/**
	 * n ���� �ݵ�� 1 �̻��̾�� ��
	 * @param n �� n-gram������ n�� �ǹ��Ѵ�.
	 * @return n-gram���� ����� �ܾ���� ArrayList ������ return�Ѵ�.
	 */
	public ArrayList <String> getNgramTerms(int n){
		ArrayList <String> ngramList = new ArrayList<String>();
		for(int i = 0; i < (termList.size()-n+1); i++){
			StringBuilder sb = new StringBuilder();
			sb.append(termList.get(i));
			for(int j = 1; j < n; j++){
				sb.append(" " + termList.get(i+j));
			}
			ngramList.add(sb.toString());
		}
		
		return	ngramList;
	}

}
