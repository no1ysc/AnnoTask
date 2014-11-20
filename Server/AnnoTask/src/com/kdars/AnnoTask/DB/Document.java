package com.kdars.AnnoTask.DB;

public class Document {
	private int documentID;
	private String collectDate; 
	private String newsDate;
	private String siteName;
	private String pressName;
	private String url;
	private String category;
	private String title;
	private String body;
	private String comment;
	private String crawlerVersion;
	
	public Document(int docId, String category){
		this.documentID = docId;
		this.category = category;
	}
	
	public Document(int documentID, String collectDate, String newsDate, String siteName,
							String pressName, String url, String category, String title, String body,
							String comment, String crawlerVersion){ //TODO: DB 스키마 바뀌면 수정해야함!
		this.documentID = documentID;
		this.collectDate = collectDate; 
		this.newsDate = newsDate;
		this.siteName = siteName;
		this.pressName = pressName;
		this.url = url;
		this.category = category;
		this.title = title;
		this.body = body;
		this.comment = comment;
		this.crawlerVersion = crawlerVersion;
	}
	
	public int getDocumentID() {
		return documentID;
	}
	public String getCollectDate() {
		return collectDate;
	}
	public String getNewsDate() {
		return newsDate;
	}
	public String getSiteName() {
		return siteName;
	}
	public String getPressName() {
		return pressName;
	}
	public String getUrl() {
		return url;
	}
	public String getCategory() {
		return category;
	}
	public String getTitle() {
		return title;
	}
	public String getBody() {
		return body;
	}
	public String getComment() {
		return comment;
	}
	public String getCrawlerVersion() {
		return crawlerVersion;
	}

	
	
}
