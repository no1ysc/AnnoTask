package com.kdars.AnnoTask;

public class GlobalContext {
	private static  GlobalContext globalContext = new GlobalContext();
	public static GlobalContext getInstance(){
		return	globalContext;
	}
	
	private int N_Gram = 4;	// 최대 N-gram
	public int getN_Gram(){
		return this.N_Gram;
	}
}
