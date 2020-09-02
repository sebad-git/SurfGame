using UnityEngine;

public class ConfigLoader {

	public const string GAME_CONFIG="GAME";

	public static ScriptableObject loadConfig(string name){
		ScriptableObject config = (ScriptableObject)Resources.Load(name);
		if(config==null){ Debug.LogError("Error loading ["+name+" File]"); }
		return config;
	}

}
