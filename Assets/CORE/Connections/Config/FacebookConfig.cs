using UnityEngine;

[System.Serializable]
public class FacebookConfig {

public int loginTimeout = 300;

	[System.Serializable]
	public class MsShare {
		public string linkUrl="https://play.google.com/store/apps/details?id=com.mountsix.jtw";
		public string title="Jump The Whale";
		public string description="";
		public string imageUrl="https://lh3.googleusercontent.com/NBERb2m6YYOZQ6I1UiASuObhP61Ic96Pqlw0yOnp9kprAltvQClIJwXPzLP_r6FKlrNZ=w300-rw";
	}
	public MsShare share;

	[System.Serializable]
	public class MsInvite{
		public string message="";
		public int friends = 500;
		public string title="";
	}
	public MsInvite invite;

	[HideInInspector]public bool showInvite = false;
	[HideInInspector]public bool showShare = false;
	
}
