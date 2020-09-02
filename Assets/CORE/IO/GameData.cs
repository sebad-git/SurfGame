using UnityEngine;
using System.Collections;

[System.Serializable]
public class GameData {
	
	public const string CURRENT_GAME="CURRENT_GAME";
	public const string ID_FORMAT="msmjtw{0}";
	public const string DATE_FORMAT="yyyy/MM/dd HH:mm:ss";
	public const string USERS = "Game/splash/BestScores";

	public string playerName;
	public string id;
	public int bestScore;
	public float bestDistance;
	public string date;
	public int coins;
	public int totalScore;
	public bool rated=false;
	public float sound;
	public bool vibration=true;
	public float sensitivity;
	public string currentSuit;
	public string currentBoard;
	public int gamesPlayed;
	public System.Collections.Generic.List<string> achievements;
	public System.Collections.Generic.List<string> items;

	public GameData(){
		this.date=getDate();
		this.sound=1f; this.sensitivity=3f;
		this.achievements = new System.Collections.Generic.List<string>();
		this.items = new System.Collections.Generic.List<string>();
		this.currentSuit="defaultSuit";
		this.currentBoard="defaultBoard";
	}

	public void save(){
		this.date=getDate();
		string json = JsonUtility.ToJson(this);
		PlayerPrefs.SetString(CURRENT_GAME,json); PlayerPrefs.Save();
	}
		
	public static GameData load(){
		try{
			if(PlayerPrefs.HasKey(CURRENT_GAME)){
				string json = PlayerPrefs.GetString(CURRENT_GAME);
				return JsonUtility.FromJson<GameData>(json);
			}else{
				Debug.Log("No data in the slot."); return createGame();
			}
		}catch(System.Exception e){
			Debug.LogError("[DATA] Error reading data:"+e.ToString()); 
			return createGame();
		}
	}

	public static GameData createGame(){
		GameData data = new GameData();
		data.id=System.String.Format(ID_FORMAT,SystemInfo.deviceUniqueIdentifier);
		data.save(); return data;
	}

	public bool addAchievement(string code){
		if(this.achievements == null ){this.achievements = new System.Collections.Generic.List<string>();}
		if( !this.achievements.Contains(code) ){ this.achievements.Add(code); return true; }
		return false;
	}

	public bool addItem(string code){
		if(this.items == null ){this.items = new System.Collections.Generic.List<string>();}
		if( !this.items.Contains(code) ){ this.items.Add(code); return true; }
		return false;
	}

	public void addScore(int score){
		this.coins=this.coins+score; this.totalScore=this.totalScore+score;
	}

	public static string getDate(){ return System.DateTime.Now.ToString(DATE_FORMAT); }
	public void setId(){ 
		if(System.String.IsNullOrEmpty(this.id)){
			this.id=System.String.Format(ID_FORMAT,SystemInfo.deviceUniqueIdentifier); this.save();
		}
	}
}

