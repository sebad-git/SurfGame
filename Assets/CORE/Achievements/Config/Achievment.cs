using UnityEngine;
using System.Collections;

[System.Serializable]
public class Achievment {
	public string name;
	public string aid;
	public int title;
	public int description;
	public string gpid;
	public AchievementsCategory category;
	public Sprite icon;
	public UnlockValueType unlockValueType=UnlockValueType.INTEGER;
	public int unlockValueInt;
	public string unlockValueString;
	public float unlockValueFloat;
	public bool unlockValueBoolean;
	public bool active;

}
