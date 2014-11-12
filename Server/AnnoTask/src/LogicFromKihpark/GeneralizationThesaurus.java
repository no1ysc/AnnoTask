package LogicFromKihpark;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.util.HashMap;
import java.util.StringTokenizer;


public class GeneralizationThesaurus {
	ReadFile thesaurusFile = null;
	HashMap<String, String> thesaurus = new HashMap<String, String>();
	
	public void read(String file) {
		try{
			thesaurusFile = new ReadFile(file);
		}catch(FileNotFoundException e){
			e.printStackTrace();
		}
		
		String line;
		try {
			thesaurusFile.readLine();
			while((line = thesaurusFile.readLine()) != null){
				StringTokenizer st = new StringTokenizer(line, " ,");
						thesaurus.put(st.nextToken(), st.nextToken());
			}
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}
}

