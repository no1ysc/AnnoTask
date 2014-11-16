package com.kdars.AnnoTask.Server.Command.Server2Client;

import com.kdars.AnnoTask.Server.Command.Command;

public class TermTransfer extends Command
{
	// 텀 1개씩 전송	1-4(반복)
	public int docID;
	public String docCategory;
	public int ngram;
	public String termsJson;	// 추후 Map으로 다시 변환.<Term, Freq>
}