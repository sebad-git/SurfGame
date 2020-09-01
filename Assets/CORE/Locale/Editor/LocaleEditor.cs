using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor (typeof(Locale))]
public class LocaleEditor : Editor{
	
	private Locale locale;
	private Vector2 scrollPos;
	private static string subGroupStyle = "ObjectFieldThumb";
	private static string rootGroupStyle = "GroupBox";

	public void OnEnable(){ locale = (Locale)target; }

	public override void OnInspectorGUI(){
		scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
		EditorGUILayout.BeginVertical(subGroupStyle);
		if (locale != null && locale.dictionary != null) {
				//HEADER.
				EditorGUILayout.BeginHorizontal(rootGroupStyle);
					GUILayout.FlexibleSpace(); EditorGUILayout.LabelField("LANGUAGE EDITOR",EditorStyles.boldLabel); GUILayout.FlexibleSpace(); 
				EditorGUILayout.EndHorizontal();
				EditorGUILayout.Space();
				EditorGUILayout.BeginHorizontal(subGroupStyle);
					locale.emulateLanguage = EditorGUILayout.Toggle("Test Language?",locale.emulateLanguage);
					if(locale.emulateLanguage){ locale.emulatedLanguage = (Languages)EditorGUILayout.EnumPopup("Language:",locale.emulatedLanguage); }
				EditorGUILayout.EndHorizontal();
				EditorGUILayout.Space();
				//END HEADER.
				//WORDS HEADER.
				EditorGUILayout.BeginHorizontal(subGroupStyle);
				GUILayout.FlexibleSpace(); EditorGUILayout.LabelField("WORDS");
				GUILayout.FlexibleSpace(); if(StyledButton("ADD WORD")){ addWord(); } GUILayout.FlexibleSpace();
				EditorGUILayout.EndHorizontal();
				//EMPTY.
				if (locale.dictionary.Count == 0) {
					EditorGUILayout.BeginVertical(rootGroupStyle);
					EditorGUILayout.BeginHorizontal();
					GUILayout.FlexibleSpace(); GUILayout.Label ("FILE EMPTY"); GUILayout.FlexibleSpace(); 
					EditorGUILayout.EndHorizontal();
					GUILayout.FlexibleSpace();
					EditorGUILayout.EndVertical();
					//LIST.
				}else{
					
					for(int k=0; k<locale.dictionary.Count; k++){
						EditorGUILayout.BeginVertical(rootGroupStyle);
						Keyword key = locale.dictionary[k];
						//FOLD WORD;
						key.showWord = EditorGUILayout.Foldout(key.showWord,(System.String.IsNullOrEmpty(key.keywordId)? "New Word":key.keywordId), EditorStyles.foldout);
						if(key.showWord){
							EditorGUILayout.BeginHorizontal(subGroupStyle);
							//FOLD TRANSLATIONS;
							key.keywordId = EditorGUILayout.TextField(key.keywordId.Trim()); 
							if(StyledButton("Remove Word")){ removeWord(k); } if(StyledButton("Duplicate Word")){ duplicateWord(k); }
							EditorGUILayout.EndHorizontal();
							key.showTranslations = EditorGUILayout.Foldout(key.showTranslations, "Translations", EditorStyles.foldout);
							if (key.showTranslations) {
								for(int t=0; t<key.translations.Count; t++) {
									Translation trans = key.translations[t];
									EditorGUILayout.BeginHorizontal(subGroupStyle);
									trans.text = EditorGUILayout.TextField (trans.text);
									trans.language = (Languages)EditorGUILayout.EnumPopup (trans.language);
									if(StyledButton("Remove Translation")){ removeTranslation(k,t); }
									EditorGUILayout.EndHorizontal();
								}
								if(StyledButton("Add Translation")){ addTranslation(k); }
							}
						}
						EditorGUILayout.EndVertical();
					}
				}
			}
			else { GUILayout.Label("No locale selected."); }
		GUILayout.FlexibleSpace();
		EditorGUILayout.EndVertical();
		EditorGUILayout.EndScrollView();
		EditorUtility.SetDirty(locale);
	}

	private void addWord(){
		Keyword kw = new Keyword(); kw.keywordId=""; kw.showWord=true;
		kw.translations = new System.Collections.Generic.List<Translation>();
		locale.dictionary.Add(kw);
	}

	private void removeWord(int index){ locale.dictionary.RemoveAt(index); }

	private void duplicateWord(int index){
		Keyword kw = locale.dictionary[index];
		Keyword nkw = new Keyword(); nkw.keywordId=kw.keywordId; nkw.showWord=true;
		nkw.translations = new System.Collections.Generic.List<Translation>();
		foreach(Translation tr in kw.translations){
			Translation ntr = new Translation(); ntr.text=tr.text; ntr.language=tr.language; 
			nkw.translations.Add(ntr);
		}
		locale.dictionary.Add(nkw);
	}

	private void addTranslation(int k){
		if (locale.dictionary[k].translations == null) { locale.dictionary[k].translations = new System.Collections.Generic.List<Translation>(); }
		Translation tr = new Translation(); tr.text=""; locale.dictionary[k].translations.Add(tr);
	}

	private void removeTranslation(int index, int trIndex){ locale.dictionary[index].translations.RemoveAt (trIndex); }

	public bool StyledButton (string label) {
		EditorGUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		bool clickResult = GUILayout.Button(label, "CN CountBadge");
		GUILayout.FlexibleSpace();
		EditorGUILayout.EndHorizontal();
		return clickResult;
	}
}
