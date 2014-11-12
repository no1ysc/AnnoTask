package LogicFromKihpark;
import java.io.BufferedReader;
import java.io.FileNotFoundException;
import java.io.FileReader;


public class ReadFile extends BufferedReader {
	public ReadFile(String filename) throws FileNotFoundException{
		super (new FileReader(filename));
	}
	
}
