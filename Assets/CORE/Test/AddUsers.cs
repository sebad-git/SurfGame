
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class AddUsers : MonoBehaviour {

	private void Start(){
		this.createRandomData();
	}

	private void createRandomData(){
		this.create("GamerGod");
		this.create("Mario");
		this.create("Kratos");
		this.create("Troll Master");
		this.create("Martin Dune");
		this.create("Rob");
		this.create("Anonymus");
		this.create("Ryu");
		this.create("Christopher");
		this.create("Negan");
		this.create("geek1");
		this.create("Gordon");
		Debug.Log("DONE.");
	}

	private void create(string name){
		GameData data = new GameData(); data.playerName=name;
		data.bestScore=UnityEngine.Random.Range(100,2000); data.bestDistance=UnityEngine.Random.Range(1,100);
		data.id="msmtst"+UnityEngine.Random.Range(120,15000);
		this.upload(data);
	}

	private void upload(GameData data){
		ScoreData score=new ScoreData(data);
		Firebase.Instance.UpdateData<GameData>(string.Format("Game/splash/BestScores/{0}",data.id), data, () => 
		{ Debug.Log(string.Format("User {0} uploaded.",data.id)); });
	}
}
