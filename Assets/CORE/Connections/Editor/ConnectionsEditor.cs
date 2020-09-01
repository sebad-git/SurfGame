using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor (typeof(ConnectionsConfig))]
public class ConnectionsEditor : Editor {

	private ConnectionsConfig instance;
	private Vector2 scrollPos;
	private static string rootGroupStyle = "GroupBox";

	public void OnEnable(){ instance = (ConnectionsConfig)target; }

	[MenuItem ("Assets/Create/Connections/Connections Configuration")]
	public static void createConfig() {
		ConnectionsConfig config = ScriptableObject.CreateInstance<ConnectionsConfig>();
		ProjectWindowUtil.CreateAsset(config, "CONNECTIONS.asset");
	}

	public override void OnInspectorGUI(){
		//MAIN LAYOUT
		scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
		EditorGUILayout.BeginVertical(rootGroupStyle);
		EditorGUILayout.BeginHorizontal(rootGroupStyle);
		GUILayout.FlexibleSpace(); EditorGUILayout.LabelField("CONNECTIONS CONFIGURATION",EditorStyles.boldLabel); GUILayout.FlexibleSpace(); 
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.Space();

		//ADMOB.
		EditorGUILayout.BeginVertical(rootGroupStyle);
		instance.showAdmob = EditorGUILayout.Foldout(instance.showAdmob,("ADMOB"), EditorStyles.foldout);
		if(instance.showAdmob){
			//bannerId
			EditorGUILayout.BeginVertical(rootGroupStyle);
			EditorGUILayout.LabelField("Banner ID:",EditorStyles.helpBox); 
			instance.ADMOB.bannerID = EditorGUILayout.TextField(instance.ADMOB.bannerID);
			EditorGUILayout.EndVertical();
			//intersitial id
			EditorGUILayout.BeginVertical(rootGroupStyle);
			EditorGUILayout.LabelField("Intersitial ID:",EditorStyles.helpBox); 
			instance.ADMOB.intersitialID = EditorGUILayout.TextField(instance.ADMOB.intersitialID);
			EditorGUILayout.EndVertical();
		}
		EditorGUILayout.EndVertical();
		EditorGUILayout.Space();
		//END ADMOB.

		//FIREBASE.
		EditorGUILayout.BeginVertical(rootGroupStyle);
		instance.showFirebase = EditorGUILayout.Foldout(instance.showFirebase,("FIREBASE"), EditorStyles.foldout);
		if(instance.showFirebase){
			EditorGUILayout.BeginVertical(rootGroupStyle);
			EditorGUILayout.LabelField("Firebase ID:",EditorStyles.helpBox); 
			instance.FIREBASE.FIREBASE_ID = EditorGUILayout.TextField(instance.FIREBASE.FIREBASE_ID);
			EditorGUILayout.EndVertical();
		}
		EditorGUILayout.EndVertical();
		EditorGUILayout.Space();
		//END FIREBASE.

		//FACEBOOK.
		EditorGUILayout.BeginVertical(rootGroupStyle);
		instance.showFacebook = EditorGUILayout.Foldout(instance.showFacebook,("FACEBOOK"), EditorStyles.foldout);
		if(instance.showFacebook){
			EditorGUILayout.BeginVertical(rootGroupStyle);
			instance.FACEBOOK.loginTimeout = EditorGUILayout.IntField("Login Timeout:",instance.FACEBOOK.loginTimeout);
			EditorGUILayout.Space();
			//Share
			EditorGUILayout.BeginVertical(rootGroupStyle);
			instance.FACEBOOK.showShare = EditorGUILayout.Foldout(instance.FACEBOOK.showShare,("SHARE"), EditorStyles.foldout);
			if(instance.FACEBOOK.showShare){
				EditorGUILayout.Space();
				instance.FACEBOOK.share.linkUrl = EditorGUILayout.TextField("Link:",instance.FACEBOOK.share.linkUrl);
				instance.FACEBOOK.share.title = EditorGUILayout.TextField("Title:",instance.FACEBOOK.share.title);
				instance.FACEBOOK.share.description = EditorGUILayout.TextField("Description:",instance.FACEBOOK.share.description);
				instance.FACEBOOK.share.imageUrl = EditorGUILayout.TextField("Image URL:",instance.FACEBOOK.share.imageUrl);
			}
			EditorGUILayout.EndVertical();
			//Invite
			EditorGUILayout.BeginVertical(rootGroupStyle);
			instance.FACEBOOK.showInvite = EditorGUILayout.Foldout(instance.FACEBOOK.showInvite,("INVITE"), EditorStyles.foldout);
			if(instance.FACEBOOK.showInvite){
				EditorGUILayout.Space();
				instance.FACEBOOK.invite.title = EditorGUILayout.TextField("Title:",instance.FACEBOOK.invite.title);
				instance.FACEBOOK.invite.message = EditorGUILayout.TextField("Message:",instance.FACEBOOK.invite.message);
				instance.FACEBOOK.invite.friends = EditorGUILayout.IntField("Max Friends:",instance.FACEBOOK.invite.friends);
			}
			EditorGUILayout.EndVertical();
			EditorGUILayout.EndVertical();
		}
		EditorGUILayout.EndVertical();
		EditorGUILayout.Space();
		//END FACEBOOK.

		//END MAIN LAYOUT
		EditorUtility.SetDirty(instance);
		GUILayout.FlexibleSpace();
		EditorGUILayout.EndVertical();
		EditorGUILayout.EndScrollView();
	}

	private bool StyledButton(string label) {
		EditorGUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		bool clickResult = GUILayout.Button(label, "CN CountBadge");
		GUILayout.FlexibleSpace();
		EditorGUILayout.EndHorizontal();
		return clickResult;
	}

}
