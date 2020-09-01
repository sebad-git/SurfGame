using UnityEngine;
using System.Collections;

public class ChoosScore : MonoBehaviour {

	public Scores scores;

	public void showScores () {
		Scores nscores= Instantiate(scores);
		nscores.scoreType=Scores.ScoreType.BEST_SCORE;
	}
	
	public void showDistances() {
		Scores nscores= Instantiate(scores);
		nscores.scoreType=Scores.ScoreType.BEST_DISTANCE;
	}

	public void close(){
		Destroy(gameObject);
	}
}
