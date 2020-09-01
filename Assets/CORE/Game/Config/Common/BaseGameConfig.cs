using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGameConfig : ScriptableObject {

	public MusicConfig MUSIC;
	public GUIConfig GUI;
	public ConnectionsConfig CONNECTIONS;

	[HideInInspector]public bool showMusic = false;
	[HideInInspector]public bool showGui = false;

}
