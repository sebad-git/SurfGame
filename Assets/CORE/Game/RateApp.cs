using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RateApp : MonoBehaviour {

	public void yes(){ GameData data = GameData.load(); data.rated=true; data.save(); Web.rate(); Destroy(gameObject); }

	public void no(){ GameData data = GameData.load(); data.rated=true; Destroy(gameObject); }
}
