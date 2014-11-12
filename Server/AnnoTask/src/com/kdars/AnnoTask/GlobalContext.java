package com.kdars.AnnoTask;

public class GlobalContext {
	private static  GlobalContext globalContext = new GlobalContext();
	public static GlobalContext getInstance(){
		return	globalContext;
	}
	
	
	/* N-Gram Setting */
	private int N_Gram = 4;	// 최대 N-gram
	public int getN_Gram(){
		return this.N_Gram;
	}
	/* N-Gram Setting */
	
	/* Special Character Remove */
	private boolean IS_REMOVE = false;	// 최대 N-gram
	public boolean isSpecialCharRemove(){
		return this.IS_REMOVE;
	}
	/* N-Gram Setting */
}
