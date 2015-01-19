package com.kdars.AnnoTask;

import com.kdars.AnnoTask.Server.UserListener;

public class AnnoTaskUserControl {
	public static void main(String[] args) {
		UserListener			userListener = new UserListener();
		userListener.run();
	}
}
