using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

public class DataTest : MonoBehaviour {

	void Start(){
		parse ();
	}
	
	void Update () {
		if(Input.GetKeyDown(KeyCode.A)){ 
			Debug.LogWarning("ADDING USER.");
			GameData data = new GameData(); data.playerName=string.Format("TEST:{0}",System.DateTime.Now);
			data.bestScore=UnityEngine.Random.Range(100,2000); data.bestDistance=UnityEngine.Random.Range(1,100);
			data.id="msmtst"+UnityEngine.Random.Range(120,15000);
			Firebase.Instance.UpdateData<GameData>(string.Format("Game/splash/BestScores/{0}",data.id), data, () => { Debug.Log("Data Uploaded."); });
		}
		if(Input.GetKeyDown(KeyCode.R)){
			Debug.LogWarning("READING USERS.");
			Firebase.Instance.ListData<GameData>("Game/splash/BestScores",null, gamedata => { 
				Debug.Log(gamedata);
			});
		}

		if(Input.GetKeyDown(KeyCode.S)){
			Debug.LogWarning("READING USER.");
			Firebase.Instance.GetData<GameData>(string.Format("Game/splash/BestScores/{0}","msmtst3176"), gamedata => { 
				Debug.Log(gamedata.playerName);
			});
		}


		if(Input.GetKeyDown(KeyCode.T)){
			parse ();
		}
			
	}

	private void parse(){
		string json = "{\"msmjtw92da86cec0c2c4f28b9b20bbae51370a7a1e4dcf\":{\"date\":\"2020/09/01 19:33:58\",\"distance\":12.390000343322754,\"gamesPlayed\":2,\"playerName\":\"myname\",\"region\":\"Spanish\",\"score\":225,\"totalScore\":250},\"msmtst3176\":{\"bestDistance\":80.0,\"bestScore\":1986,\"coins\":0,\"currentBoard\":\"defaultBoard\",\"currentSuit\":\"defaultSuit\",\"date\":\"2020/09/01 18:51:15\",\"gamesPlayed\":0,\"id\":\"msmtst3176\",\"playerName\":\"TEST:1/9/2020 18:51:15\",\"rated\":false,\"sensitivity\":3.0,\"sound\":1.0,\"totalScore\":0,\"vibration\":true},\"msmtst8946\":{\"bestDistance\":20.0,\"bestScore\":1246,\"coins\":0,\"currentBoard\":\"defaultBoard\",\"currentSuit\":\"defaultSuit\",\"date\":\"2020/09/01 18:51:13\",\"gamesPlayed\":0,\"id\":\"msmtst8946\",\"playerName\":\"TEST:1/9/2020 18:51:13\",\"rated\":false,\"sensitivity\":3.0,\"sound\":1.0,\"totalScore\":0,\"vibration\":true}}";
		Debug.LogWarning(Firebase.Instance.ParseData(json));
	}
}
