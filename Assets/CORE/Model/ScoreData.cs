using UnityEngine;
using System.Collections;

[System.Serializable]
public class ScoreData {
	public string playerName;
	public int score;
	public float distance;
	public string date;
	public string region;
	public int gamesPlayed;
	public int totalScore;

	public ScoreData(GameData data){
		this.playerName=data.playerName; this.score=data.bestScore; this.distance=data.bestDistance;
		this.date=data.date; this.region = Application.systemLanguage.ToString();
		this.gamesPlayed=data.gamesPlayed; this.totalScore=data.totalScore;
	}
}
