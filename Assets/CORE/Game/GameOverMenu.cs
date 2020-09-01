﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour {
	public Text distance;
	public Text score;
	public Text bestScore;
	public Text bestDistance;
	public Text nameLabel;
	private GameConfig config;

	private const string scoreFormat="{0}: {1}";
	//private const string timeFormat="{0:0}:{1:00}";
	private const string distanceFormat="{0} m.";

	void Start(){
		AdMobManager.showBanner(false);
		this.config=(GameConfig)ConfigLoader.loadConfig(ConfigLoader.GAME_CONFIG);
		int score = int.Parse( GameController.instance.scoreLabel.text);
		float distance = GameController.instance.getDistance();
		GameData data = GameController.instance.data;
		if(score > GameController.instance.data.bestScore){ data.bestScore=score; }
		data.addScore(score); data.gamesPlayed++;
		if(distance > GameController.instance.data.bestDistance){ data.bestDistance = distance; }
		//SAVE DATA.
		data.save();
		//UPLOAD TO FIREBASE.
		if(System.String.IsNullOrEmpty(data.playerName)){data.playerName="Anonymus"; }
		data.date=GameData.getDate();
		ScoreData scoreData=new ScoreData(data);
		FirebaseManager.uploadData<ScoreData>(GameData.USERS,data.id,scoreData);
		//fill score.
		this.score.text=GameController.instance.scoreLabel.text;
		this.bestScore.text = GameController.instance.data.bestScore.ToString();
		//fill distance.
		this.distance.text=System.String.Format(distanceFormat,distance);
		this.bestDistance.text=System.String.Format(distanceFormat,GameController.instance.data.bestDistance);
		//fill name.
		this.nameLabel.text=GameController.instance.data.playerName.ToUpper();
		//Check Achievements.
		System.Collections.Generic.Dictionary<AchievementsCategory,object> values = new System.Collections.Generic.Dictionary<AchievementsCategory,object>();
		values.Add(AchievementsCategory.CATEGORY_1, data.bestScore);
		values.Add(AchievementsCategory.CATEGORY_2, data.bestDistance);
		gameObject.GetComponent<AchievementChecher>().checkAchievements(values);
		//Screen Normal.
		Screen.sleepTimeout=SleepTimeout.SystemSetting;
		//Interisial.
		if(GameController.gameCounter>=0){ AdMobManager.createInterstitial(); }
		if(GameController.gameCounter>this.config.GUI.gamesPerAds){
			bool shown = AdMobManager.showInterstitial(); if(shown){ GameController.gameCounter=0; }
		}
		if(data.rated==false && GameController.gameCounter>this.config.GUI.gamesPerRate){ GameObject.Instantiate(this.config.GUI.rateMenu); }
	}

	public void restart(){
		Time.timeScale=1;
		UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
	}

	public void menu(){
		Time.timeScale=1;
		UnityEngine.SceneManagement.SceneManager.LoadScene(0);
	}
}
