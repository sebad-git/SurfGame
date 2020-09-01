using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionsConfig : ScriptableObject {

	public FacebookConfig FACEBOOK;
	public AdmobConfig ADMOB;
	public FirebaseConfig FIREBASE;

	[HideInInspector]public bool showFacebook = false;
	[HideInInspector]public bool showAdmob = false;
	[HideInInspector]public bool showFirebase = false;
}
