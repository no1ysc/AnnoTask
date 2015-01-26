package com.kdars.AnnoTask.DB;

import java.math.BigInteger;
import java.security.KeyFactory;
import java.security.PrivateKey;
import java.security.PublicKey;
import java.security.spec.RSAPrivateKeySpec;
import java.security.spec.RSAPublicKeySpec;
import java.util.Base64;

import javax.crypto.Cipher;

import com.kdars.AnnoTask.Server.UserIDPasswordSet;
import com.kdars.AnnoTask.Server.Command.Server2Client.UserInfo;

/**
 * 
 * @author kihpark
 * @date 1/21/2015
 * 
 **/
public class UserDBManager {
	private static UserDBManager userDBManager = new UserDBManager();
	private UserDBConnector userDB;

	public UserDBManager(){
		userDB = new UserDBConnector();
	}

	public static UserDBManager getInstance(){
		return	userDBManager;
	}

	public boolean registerNewUser(String email, String password, String userName){
		String encryptedPassword = encryptPasswordForDB(password);
		if(userDB.registerNewUser(email, encryptedPassword, userName)){
			return true;
		}else{
			System.out.println("Failed to register new user in AnnoTask!");
			return false;			
		}
	}

	public UserInfo getUserInformation(String emailAddress) {
		return userDB.getUserInfo(emailAddress);
	}

	public UserInfo loginCheck(String userID, String password) {
		UserInfo userInfor = null;
		UserIDPasswordSet userIDPasswordSetFromDB = userDB.getUserIDPass(userID);
		
		// 암복호화 이승철 추가.
		String userPasswordFromRequest = decryptFromRequest(password);
		String userPasswordFromDB = decryptFromDB(userIDPasswordSetFromDB.getPassword());
		
		// 요청들어온 Password와 DB에 있는 Password 매칭
		if (userPasswordFromRequest.equals(userPasswordFromDB)){
			// 매칭 성공.
			return userIDPasswordSetFromDB.getUserInfo();
		} else {
			// 매칭 실패.
			return null;
		}
				
//		return userDB.loginCheck(userID, password);
	}

	/**
	 * 요청이 들어온 암호화된 Password를 평문으로 바꾸어줌.
	 * 요청을 위한 private Key사용.
	 * @author JS
	 * @param encryptedPassword
	 * @return
	 */
	private String decryptFromRequest(String encryptedPassword) {
		// 원본 키
		//String privateKeyString = "<RSAKeyValue><Modulus>qaFx1UhSkXdh2BvbiMMAh5YMh/txhdKPqipyPKAP5X8LgfCKlDRmq8XGYoJnWxfB7qB9c7/eB46pwDXBh0Z9fFT11BB4lAyFLOihKMouSc10I4wdhuMAvHXw6F2QFYoBPAzqs/Okisd71kv52iDkCwHpJsXBGmK97J40oKVXcNM=</Modulus><Exponent>AQAB</Exponent><P>2ZLI0gO3tCrtUIH+VKsPKzv87C604z+PyKb2U9qCJyRX9s7iUEQgzW1QdyaiplYBy6jb9pC8qtkMM1gMhCSmZQ==</P><Q>x5cChqO7gEO4lspJEvPt4mL6YO7nq/XQlsv3soS3Fppfs4NIdt7Ek7aPYtLsyP+w11TUq57Tvw4F6YFPm/TK1w==</Q><DP>AyP9RhrLogwklM7rjulRNLyO8BHUhps6Rhky4Q78Zfg+VM+zsJjrKEv1p2KlYmVHbVsooayBLK4pYOxaceXdiQ==</DP><DQ>DBTQ+1Nn4yC2SLJf7/zB9oUlQL7VWSxc/vPDv5OW/ZBEoLoepctgPMy9Ky83VAdeLfqdkPHhQVxY8UR5jCgqKw==</DQ><InverseQ>a9Z3xFlEm5KVUxFWDgCKIhuVGyXpNuLjmv5wjsRXtuQGljqw0T1rScc+ss3/5peukQJmr7FOUsu0c+Wm0VqeCw==</InverseQ><D>fS5zPx58MHWVc1I7lJWzkludK8zXXhahhsaEP1Ev6gDzkRTeb7ir/B+b456wf3zs1RkC+6SgSZtykjIZe6b9esusytI8NqzBEyhcZ2fAsLbwrhJf8aGc9Xxzw4cMu8ZbYC8Sukhp9JGuYGSI6pZVocmKokhlYxiMFfxYShy6QCE=</D></RSAKeyValue>";
		
		String plainPassword = "";
		
		String modulusElem ="qaFx1UhSkXdh2BvbiMMAh5YMh/txhdKPqipyPKAP5X8LgfCKlDRmq8XGYoJnWxfB7qB9c7/eB46pwDXBh0Z9fFT11BB4lAyFLOihKMouSc10I4wdhuMAvHXw6F2QFYoBPAzqs/Okisd71kv52iDkCwHpJsXBGmK97J40oKVXcNM=";
		String dElem = "fS5zPx58MHWVc1I7lJWzkludK8zXXhahhsaEP1Ev6gDzkRTeb7ir/B+b456wf3zs1RkC+6SgSZtykjIZe6b9esusytI8NqzBEyhcZ2fAsLbwrhJf8aGc9Xxzw4cMu8ZbYC8Sukhp9JGuYGSI6pZVocmKokhlYxiMFfxYShy6QCE=";
		
		try{
			byte[] modBytes = Base64.getDecoder().decode(modulusElem.trim());
			byte[] dBytes = Base64.getDecoder().decode(dElem.trim());
			
			BigInteger modules = new BigInteger(1, modBytes);
			BigInteger d = new BigInteger(1, dBytes);
			
			KeyFactory factory = KeyFactory.getInstance("RSA");
			Cipher cipher = Cipher.getInstance("RSA");

		    byte[] encData = Base64.getDecoder().decode(encryptedPassword);
				
			RSAPrivateKeySpec privSpec = new RSAPrivateKeySpec(modules, d);
			PrivateKey privKey = factory.generatePrivate(privSpec);
			cipher.init(Cipher.DECRYPT_MODE, privKey);
			byte[] decrypted = cipher.doFinal(encData);
			plainPassword = new String(decrypted);
		} catch (Exception e){
			e.printStackTrace();
		}
		
		return plainPassword;
	}
	
	/**
	 * DB에 암호화되어 저장된 Password를 평문으로 바꾸어줌. 
	 * DB를 위한 private Key사용.
	 * @author JS
	 * @param encryptedPassword
	 * @return
	 */
	private String decryptFromDB(String encryptedPassword) {

		// 원본 키
		//String privateKeyString = "<RSAKeyValue><Modulus>wHrf8EDATLz6APhjHUmBBEGZG7vKqAyIgSVH4cBl7djGRTkCk+wToZ4CTi3kK89wJ/NULSCPJnY4VXfAO3UdLOSqYnjj0+H6UsoYN0CLOczg6TgtCmEGAJrsyOyv0fBrh2UZ+FeJngxGEs7/O3PjkTTOSe6W2nm/2h+6iD83akE=</Modulus><Exponent>AQAB</Exponent><P>4knkjAH94FOLqJlTzK9C+xBjp/cbjuquQD5sFuxZSFAdbntBowjuTMq/2nzDuLkx+BIfly1HiBb+DQhv3IpeXQ==</P><Q>2cCXWyi1RnVqJ3ay68rjLDaVfbYpr8R0pkBg+EVENlp2IFX99LQyLIMMb3u8Xj0PDAi2/aiMibhGRk7xNGFVNQ==</Q><DP>UIYM7wlyZxYzt15AZLDlO+QcIlQbmWLHeRL9cbbPGp1vq7XuqG5wJiFr3frRxvUX4/fHCAvTzYipBMhAzhDq/Q==</DP><DQ>p12D+Cj9y74LbXGqa9lxalY56HnO6K4TCoWJAsoad7xn9sqheyfVOKkxMa7lRXmgyxsXqzeVbXUZbojWk/AL0Q==</DQ><InverseQ>2nKPEWbZeRIMcAAn/WlvZ/pfAJdBzw69qi7mbkcUrBlyAjcUnmVYk8rXAAfosYROMaqunaAXTMb5M9RIm7h+aA==</InverseQ><D>SlfrYN+RptIi+fb2SVyXoW441fZtqwTUQJWGsxJeDET7J8eCUGIRnw3ptAqTo7xGhJe+foOh5ugokBJlxFFSx9W0FRlZ2OOUiGXmfeBAQEdpMsniYeEGjjFffbWqRlVdgpDUm88xC5uVm59L31hgLOXyme4j2PaSfTlpXrvJ8LE=</D></RSAKeyValue>";
		
		String plainPassword = "";
		
		String modulusElem ="wHrf8EDATLz6APhjHUmBBEGZG7vKqAyIgSVH4cBl7djGRTkCk+wToZ4CTi3kK89wJ/NULSCPJnY4VXfAO3UdLOSqYnjj0+H6UsoYN0CLOczg6TgtCmEGAJrsyOyv0fBrh2UZ+FeJngxGEs7/O3PjkTTOSe6W2nm/2h+6iD83akE=";
		String dElem = "SlfrYN+RptIi+fb2SVyXoW441fZtqwTUQJWGsxJeDET7J8eCUGIRnw3ptAqTo7xGhJe+foOh5ugokBJlxFFSx9W0FRlZ2OOUiGXmfeBAQEdpMsniYeEGjjFffbWqRlVdgpDUm88xC5uVm59L31hgLOXyme4j2PaSfTlpXrvJ8LE=";
		
		try{
			byte[] modBytes = Base64.getDecoder().decode(modulusElem.trim());
			byte[] dBytes = Base64.getDecoder().decode(dElem.trim());
			
			BigInteger modules = new BigInteger(1, modBytes);
			BigInteger d = new BigInteger(1, dBytes);
			
			KeyFactory factory = KeyFactory.getInstance("RSA");
			Cipher cipher = Cipher.getInstance("RSA");

		    byte[] encData = Base64.getDecoder().decode(encryptedPassword);
				
			RSAPrivateKeySpec privSpec = new RSAPrivateKeySpec(modules, d);
			PrivateKey privKey = factory.generatePrivate(privSpec);
			cipher.init(Cipher.DECRYPT_MODE, privKey);
			byte[] decrypted = cipher.doFinal(encData);
			plainPassword = new String(decrypted);
		} catch (Exception e){
			e.printStackTrace();
		}
		
		return plainPassword;
	}

	/**
	 * Password를 DB에 암호화하여 저장하기 위해 암호문으로 바꾸어줌.
	 * DB를 위한 public Key 사용. 
	 * @param plainPassword
	 * @return
	 */
	private String encryptPasswordForDB(String plainPassword){
		String encryptedPassword = "";
//		String publicKeyString = "<RSAKeyValue><Modulus>wHrf8EDATLz6APhjHUmBBEGZG7vKqAyIgSVH4cBl7djGRTkCk+wToZ4CTi3kK89wJ/NULSCPJnY4VXfAO3UdLOSqYnjj0+H6UsoYN0CLOczg6TgtCmEGAJrsyOyv0fBrh2UZ+FeJngxGEs7/O3PjkTTOSe6W2nm/2h+6iD83akE=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
		
		String modulusElem ="wHrf8EDATLz6APhjHUmBBEGZG7vKqAyIgSVH4cBl7djGRTkCk+wToZ4CTi3kK89wJ/NULSCPJnY4VXfAO3UdLOSqYnjj0+H6UsoYN0CLOczg6TgtCmEGAJrsyOyv0fBrh2UZ+FeJngxGEs7/O3PjkTTOSe6W2nm/2h+6iD83akE=";
		String expElem = "AQAB";
		
		byte[] modBytes = Base64.getDecoder().decode(modulusElem.trim());
		byte[] expBytes = Base64.getDecoder().decode(expElem.trim());
				
		BigInteger modules = new BigInteger(1, modBytes);
		BigInteger exp = new BigInteger(1, expBytes);
		
		try {
			KeyFactory factory = KeyFactory.getInstance("RSA");
			Cipher cipher = Cipher.getInstance("RSA");
	
		    byte[] encData = plainPassword.getBytes("UTF-8");
						
		    RSAPublicKeySpec pubSpec = new RSAPublicKeySpec(modules, exp);
			PublicKey pubKey = factory.generatePublic(pubSpec);
			cipher.init(Cipher.ENCRYPT_MODE, pubKey);
			byte[] encrypted = cipher.doFinal(encData);
			String encode = Base64.getEncoder().encodeToString(encrypted);
			System.out.println("encrypted: " + encode);
		} catch (Exception e){
			e.printStackTrace();
		}
		
		return encryptedPassword;
	}

	
	
	public void userActivation(String userID) {
		userDB.activateUser(userID);
	}

	public void userDeactivation(String userID) {
		userDB.deactivateUser(userID);		
	}

	public void increaseLoginCount(String userID) {
		userDB.increaseLoginCount(userID);
	}

	public void increaseDeleteListAddedCount(String userID) {
		userDB.increaseDeleteListAddedCount(userID);		
	}

	public void increaseThesaurusAddedCount(String userID) {
		userDB.increaseThesaurusAddedCount(userID);		
	}

	public boolean checkUserID(String checkUserID) {
		return userDB.checkUserID(checkUserID);
	}

	public void updateLatestLoginTime(String userID) {
		userDB.updateLatestLogin(userID);
	}
}
