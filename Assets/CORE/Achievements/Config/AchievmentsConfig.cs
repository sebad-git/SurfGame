using UnityEngine;
using System.Collections.Generic;

public class AchievmentsConfig : ScriptableObject {
	public bool emulateLanguage=false;
	public Languages emulatedLanguage;
	public Locale language;
	public List<Achievment> achievmentsList;

	public string getText(int textID){
		string keyWord=language.dictionary[textID].keywordId;
		return this.language.getText(keyWord);
	}

}
