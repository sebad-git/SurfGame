using UnityEngine;

public class ConnectionsLoader {

	private const string CONNECTIONS="CONNECTIONS";

	public static ConnectionsConfig loadConfig(){
		ConnectionsConfig config = (ConnectionsConfig)Resources.Load(CONNECTIONS);
		if(config==null){ Debug.LogError("Error loading ["+CONNECTIONS+" File]"); }
		return config;
	}

}
