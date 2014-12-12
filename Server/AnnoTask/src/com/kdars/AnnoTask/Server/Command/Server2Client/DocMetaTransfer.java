package com.kdars.AnnoTask.Server.Command.Server2Client;

import com.kdars.AnnoTask.Server.Command.Command;

public class DocMetaTransfer extends Command{
	
	public String category;
	public String docMetaJson; //  HashMap<docID, title>
}
