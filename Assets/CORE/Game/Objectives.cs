using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Objectives : MonoBehaviour {

	public RectTransform achievementsHolder;
	public AchievmentUI achievmentItem;
	public AchievmentsConfig achievements;
	public Sprite unlockedIcon;

	void Start () {
		GameData data = GameData.load();
		//Chech Achievements.
		System.Collections.Generic.Dictionary<AchievementsCategory,object> values = new System.Collections.Generic.Dictionary<AchievementsCategory,object>();
		values.Add(AchievementsCategory.CATEGORY_1, data.bestScore);
		values.Add(AchievementsCategory.CATEGORY_2, data.bestDistance);
		gameObject.GetComponent<AchievementChecher>().checkAchievements(values);
		//Load Achievements.
		foreach(Achievment ach in achievements.achievmentsList){
			AchievmentUI item = (AchievmentUI)Instantiate(achievmentItem);
			item.descriptionLabel.text=achievements.getText(ach.description);
			item.nameLabel.text=achievements.getText(ach.title);
			if(data.achievements.Contains(ach.aid)){ item.icon.sprite=unlockedIcon; }
			item.transform.SetParent(achievementsHolder.transform);
		}
	}

	public void close(){
		Destroy(gameObject);
	}
}
