using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Scores : MonoBehaviour {

	public Image loader;
	public HorizontalLayoutGroup scoreData;
	private Text[] slots;
	public enum ScoreType{BEST_SCORE=0, BEST_DISTANCE=1}
	[HideInInspector]public ScoreType scoreType;

	public const string SCORE_FORMAT = "{0}. {1} ({2})";
	private const string DISTANCE_FORMAT="{0}. {1} ({2} m.)";

	void Start() {
		this.slots = scoreData.GetComponentsInChildren<Text>(); 
		this.loadScores();
	}

	private void loadScores(){
		try{
			List<ScoreData> scores = new List<ScoreData>();
			string criteria="score";
			if(scoreType==ScoreType.BEST_SCORE){ criteria = "score";}
			if(scoreType==ScoreType.BEST_DISTANCE){ criteria = "distance"; }
			Firebase.Instance.ListData<ScoreData>(GameData.USERS,"totalScore", data => {
				List<ScoreData> users = data;
				users.ForEach( us => scores.Add(us));
				//Display.
				this.display(scores);
			});
		}catch(System.Exception e){
			loader.GetComponentInChildren<Text>().text="Error loading data.";
			Debug.LogError("Error loading data."+e);
		}
	}

	private void display(List<ScoreData> scores){
		try{
			if(scoreType==ScoreType.BEST_SCORE){
				List<ScoreData> orderedScores = scores.OrderByDescending(o=>o.score).ToList(); 
				for (int i = 0; i < this.slots.Length; i++) {
					if(i < orderedScores.Count){
						ScoreData scoreData = orderedScores[i];
						this.slots[i].text = System.String.Format(SCORE_FORMAT,(i+1),scoreData.playerName,scoreData.score);
					}else{
						this.slots[i].text = "";
					}
				}
			}
			if(scoreType==ScoreType.BEST_DISTANCE){ 
				List<ScoreData> orderedScores = scores.OrderByDescending(o=>o.distance).ToList(); 
				for (int i = 0; i < this.slots.Length; i++) {
					if(i < orderedScores.Count){
						ScoreData scoreData = orderedScores[i];
						this.slots[i].text = System.String.Format(DISTANCE_FORMAT,(i+1),scoreData.playerName,scoreData.distance);
					}else{
						this.slots[i].text = "";
					}
				}
			}

			loader.gameObject.SetActive(false);
		}catch(System.Exception e){
			loader.GetComponentInChildren<Text>().text="Error loading data.";
			Debug.LogError("Error loading data."+e);
		}

	}

	public void close(){ Destroy(gameObject); }

}

