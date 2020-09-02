using UnityEngine;
using System.Collections;

public class DataTest : MonoBehaviour {
	
	void Update () {
		if(Input.GetKeyDown(KeyCode.A)){ 
			Debug.LogWarning("ADDING USER.");
			GameData data = new GameData(); data.playerName=string.Format("TEST:{0}",System.DateTime.Now);
			data.bestScore=UnityEngine.Random.Range(100,2000); data.bestDistance=UnityEngine.Random.Range(1,100);
			data.id="msmtst"+UnityEngine.Random.Range(120,15000);
			Firebase.Instance.setData<GameData>(string.Format("Game/splash/BestScores/{0}",data.id), data, () => { Debug.Log("Data Uploaded."); });
		}
		if(Input.GetKeyDown(KeyCode.R)){
			Debug.LogWarning("READING USERS.");
			Firebase.Instance.getData<GameData>("Game/splash/BestScores", gamedata => { 
				Debug.Log(gamedata.bestDistance);
			});
		}

		if(Input.GetKeyDown(KeyCode.S)){
			Debug.LogWarning("READING USER.");
			Firebase.Instance.getData<GameData>(string.Format("Game/splash/BestScores/{0}","msmtst3176"), gamedata => { 
				Debug.Log(gamedata.playerName);
			});
		}
	}
}
