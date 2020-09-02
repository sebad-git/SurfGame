using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

public class DataTest : MonoBehaviour {

	void Start(){
		Debug.LogWarning("READING USERS.");
		Firebase.Instance.ListData<GameData>("Game/splash/BestScores",gamedata => { 
			gamedata.ForEach(user => Debug.Log(user.playerName) );
		} );
	}
	
	void Update () {
		if(Input.GetKeyDown(KeyCode.A)){ 
			Debug.LogWarning("ADDING USER.");
			GameData data = new GameData(); data.playerName=string.Format("TEST:{0}",System.DateTime.Now);
			data.bestScore=UnityEngine.Random.Range(100,2000); data.bestDistance=UnityEngine.Random.Range(1,100);
			data.id="msmtst"+UnityEngine.Random.Range(120,15000);
			Firebase.Instance.UpdateData(string.Format("Game/splash/BestScores/{0}",data.id), JsonUtility.ToJson(data), () => { Debug.Log("Data Uploaded."); });
		}
		if(Input.GetKeyDown(KeyCode.R)){
			Debug.LogWarning("READING USERS.");
			Firebase.Instance.ListData<GameData>("Game/splash/BestScores",gamedata => { 
				Debug.Log(gamedata[0].playerName);
			} );
		}

		if(Input.GetKeyDown(KeyCode.S)){
			Debug.LogWarning("READING USER.");
			Firebase.Instance.GetData<GameData>("Game/splash/BestScores/msmtst3176", gamedata => { 
				Debug.Log(gamedata.playerName); 
			});
		}
			
	}

}
