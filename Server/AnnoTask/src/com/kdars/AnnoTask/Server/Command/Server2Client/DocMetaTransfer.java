package com.kdars.AnnoTask.Server.Command.Server2Client;

import java.util.HashMap;
import com.kdars.AnnoTask.Server.Command.Command;

public class DocMetaTransfer extends Command{
	public HashMap<String, HashMap<Integer, String>> docMeta; // cat, id, title
	public String transferJSON;
}
