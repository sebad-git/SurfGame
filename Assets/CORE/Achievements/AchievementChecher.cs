using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AchievementChecher : MonoBehaviour {

	public AchievmentsConfig achievements;

	public void checkAchievements(Dictionary<AchievementsCategory,object> values){
		GameData data = GameData.load();
		foreach(Achievment ach in achievements.achievmentsList){
			if(values.ContainsKey(ach.category)){ 
				object value = values[ach.category];
				if(ach.unlockValueType==UnlockValueType.INTEGER){
					if(((int)value)>=ach.unlockValueInt){data.addAchievement(ach.aid); }
				}
				if(ach.unlockValueType==UnlockValueType.FLOAT){
					if(((float)value)>=ach.unlockValueFloat){data.addAchievement(ach.aid); }
				}
				if(ach.unlockValueType==UnlockValueType.BOOLEAN){
					if(((bool)value)==ach.unlockValueBoolean){data.addAchievement(ach.aid); }
				}
				if(ach.unlockValueType==UnlockValueType.STRING){
					if(((string)value)==ach.unlockValueString){data.addAchievement(ach.aid); }
				}
			}
		}
		data.save();
	}

}
