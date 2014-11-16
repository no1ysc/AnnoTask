package com.kdars.AnnoTask.Server.Command.Server2Client;

import com.kdars.AnnoTask.Server.Command.Command;

public class DocumentResponse extends Command
{
	// 문서원본 전송, Document 그대로 전송해도 됨.	2-2
	public int documentID;
	public String collectDate;
	public String newsDate;
	public String siteName;
	public String pressName;
	public String url;
	public String category;
	public String title;
	public String body;
	public String comment;
	public String crawlerVersion;
}