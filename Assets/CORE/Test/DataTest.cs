using UnityEngine;
using System.Collections;

public class DataTest : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.D)){ PlayerPrefs.DeleteAll(); PlayerPrefs.Save(); Debug.Log("DATA ERASED."); }
		if(Input.GetKeyDown(KeyCode.M)){GameData data = GameData.load(); data.coins+=500; data.save(); Debug.Log("MONEY."); }
	}
}
