using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(AchievmentsConfig))]
public class AchievmentsEditor : Editor {

	[MenuItem ("Assets/Create/Achievements/Achievements File")]
	public static void createConfig () {
		AchievmentsConfig config = ScriptableObject.CreateInstance<AchievmentsConfig>();
		ProjectWindowUtil.CreateAsset(config, "achievments.asset");
	}
	
	private AchievmentsConfig achievmentFile;
	private Vector2 scrollPos;
	private static string subGroupStyle = "ObjectFieldThumb";
	private static string titleStyle = "MeTransOffRight";
	private static string rootGroupStyle = "GroupBox";
	private static int achCounter;
	private string[] keyWords=null;

	public void OnEnable(){ achievmentFile = (AchievmentsConfig)target;
		if (achievmentFile != null && achievmentFile.achievmentsList != null) {
			achCounter = achievmentFile.achievmentsList.Count;
			if(achievmentFile.language!=null){
				keyWords = new string[achievmentFile.language.dictionary.Count];
				for (int i = 0; i < achievmentFile.language.dictionary.Count; i++) {
					keyWords[i]=achievmentFile.language.dictionary[i].keywordId;
				}
			}
		}
	}

	public override void OnInspectorGUI(){
		scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
		if (achievmentFile != null) {
			EditorGUILayout.BeginVertical(subGroupStyle);

			//HEADER.
			EditorGUILayout.Space();
			EditorGUILayout.BeginHorizontal(rootGroupStyle);
			GUILayout.FlexibleSpace();  EditorGUILayout.LabelField("ACHIEVEMENTS EDITOR",EditorStyles.boldLabel); GUILayout.FlexibleSpace(); 
			EditorGUILayout.EndHorizontal();
			//END HEADER.

			EditorGUILayout.BeginHorizontal(subGroupStyle);
			achievmentFile.language = (Locale)EditorGUILayout.ObjectField("Language File",achievmentFile.language,(typeof(Locale)),false);
			EditorGUILayout.EndHorizontal();
			EditorGUILayout.Space();

			if (achievmentFile.achievmentsList != null) {
				EditorGUILayout.BeginVertical(rootGroupStyle);

				//ADD ACHIEVEMENTS.
				EditorGUILayout.BeginHorizontal(subGroupStyle);
				GUILayout.FlexibleSpace(); EditorGUILayout.LabelField("Achievements");
				GUILayout.FlexibleSpace(); if(StyledButton("Add Achievement")){ Add(); } GUILayout.FlexibleSpace();
				EditorGUILayout.EndHorizontal();
				//END ADD ACHIEVEMENTS.

				if (achievmentFile.achievmentsList.Count == 0) {
					EditorGUILayout.BeginHorizontal();
					GUILayout.FlexibleSpace(); GUILayout.Label ("FILE EMPTY"); GUILayout.FlexibleSpace(); 
					EditorGUILayout.EndHorizontal();
				}else{
					for(int a=0; a<achievmentFile.achievmentsList.Count;a++) {
						Achievment ach = achievmentFile.achievmentsList[a];
						EditorGUILayout.Space();
						EditorGUILayout.BeginHorizontal(subGroupStyle);
						ach.active = EditorGUILayout.Foldout (ach.active, ach.name, EditorStyles.foldout); if (GUILayout.Button ("Remove")) { remove(a); }
						EditorGUILayout.EndHorizontal();
						if (ach.active) {
							EditorGUILayout.BeginHorizontal(subGroupStyle);
							ach.name = EditorGUILayout.TextField ("Achievement:", ach.name.Trim());
							EditorGUILayout.EndHorizontal();
							//----------------------------------------------------
							EditorGUILayout.BeginVertical(subGroupStyle);
							ach.aid = EditorGUILayout.TextField ("Achievement ID:", ach.aid.Trim());
							ach.gpid = EditorGUILayout.TextField ("Google Play ID:", ach.gpid.Trim());
							ach.category = (AchievementsCategory)EditorGUILayout.EnumPopup ("Category:", ach.category);
							EditorGUIUtility.labelWidth = 180;
							EditorGUILayout.LabelField ("Icon:");
							ach.icon = (Sprite)EditorGUILayout.ObjectField (ach.icon, typeof(Sprite), false);
							//UNLOCK VALUES.
							ach.unlockValueType = (UnlockValueType)EditorGUILayout.EnumPopup ("Unlock Value Type:", ach.unlockValueType);
							if (ach.unlockValueType == UnlockValueType.INTEGER) {
								ach.unlockValueInt = EditorGUILayout.IntField ("Unlock Value:", ach.unlockValueInt);
							}
							if (ach.unlockValueType == UnlockValueType.STRING) {
								ach.unlockValueString = EditorGUILayout.TextField ("Unlock Value:", ach.unlockValueString);
							}
							if (ach.unlockValueType == UnlockValueType.FLOAT) {
								ach.unlockValueFloat = EditorGUILayout.FloatField ("Unlock Value:", ach.unlockValueFloat);
							}
							if (ach.unlockValueType == UnlockValueType.BOOLEAN) {
								ach.unlockValueBoolean = EditorGUILayout.Toggle ("Unlock Value:", ach.unlockValueBoolean);
							}
							if(this.keyWords!=null){ ach.title=EditorGUILayout.Popup("Title:",ach.title,this.keyWords); }
							if(this.keyWords!=null){ ach.description=EditorGUILayout.Popup("Description:",ach.description,this.keyWords); }
							//----------------------------------------------------
							EditorGUILayout.BeginHorizontal(titleStyle);
							if (GUILayout.Button ("Duplicate")) { duplicate(a); } 
							if (GUILayout.Button ("Move Up")) { moveUp(a); } if (GUILayout.Button ("Move Down")) { moveDown(a); }
							EditorGUILayout.EndHorizontal();
							EditorGUILayout.EndVertical();
							//----------------------------------------------------
						}
					}
				}
				GUILayout.FlexibleSpace(); 	GUILayout.FlexibleSpace();
				EditorGUILayout.EndVertical(); 
			}else{ achievmentFile.achievmentsList = new List<Achievment>(); }
			GUILayout.FlexibleSpace();
			EditorGUILayout.EndVertical(); //EV
		}
		else { GUILayout.Label("No file selected."); }
		EditorGUILayout.EndScrollView();
		EditorUtility.SetDirty(achievmentFile);
	}

	public void duplicate(int index){
		Achievment ach = achievmentFile.achievmentsList[index];
		Achievment nach = new Achievment();
		nach.aid = "ACHV"+(achCounter+1);
		nach.name = ach.name;
		nach.title = ach.title; nach.description = ach.description; nach.gpid = ach.gpid;
		nach.category = ach.category; nach.icon = ach.icon;
		nach.unlockValueBoolean = ach.unlockValueBoolean; nach.unlockValueFloat = ach.unlockValueFloat; 
		nach.unlockValueInt = ach.unlockValueInt; nach.unlockValueString = ach.unlockValueString;
		nach.unlockValueType = ach.unlockValueType; nach.active= true;
		achievmentFile.achievmentsList.Add(nach);
		achCounter=achievmentFile.achievmentsList.Count;
	}

	public void remove(int index){
		achievmentFile.achievmentsList.RemoveAt(index);
		achCounter=achievmentFile.achievmentsList.Count;
	}

	public void Add(){
		Achievment ach = new Achievment();
		achievmentFile.achievmentsList.Add(ach);
		achCounter=achievmentFile.achievmentsList.Count;
	}

	public void moveUp(int index){
		if (index > 0) {
			Achievment ach = achievmentFile.achievmentsList [index];
			achievmentFile.achievmentsList.RemoveAt(index);
			achievmentFile.achievmentsList.Insert( (index-1), ach );
		}
		achCounter=achievmentFile.achievmentsList.Count;
	}

	public void moveDown(int index){
		if (index < achievmentFile.achievmentsList.Count) {
			Achievment ach = achievmentFile.achievmentsList [index];
			achievmentFile.achievmentsList.RemoveAt(index);
			achievmentFile.achievmentsList.Insert ((index + 1), ach);
		}
	}

	public bool StyledButton (string label) {
		EditorGUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		bool clickResult = GUILayout.Button(label, "CN CountBadge");
		GUILayout.FlexibleSpace();
		EditorGUILayout.EndHorizontal();
		return clickResult;
	}
}

