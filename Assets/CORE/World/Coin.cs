using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {

	public int value=5;
	public GameObject effect;

	void OnTriggerEnter(Collider col){
		if(col.gameObject.tag=="Player"){
			GameController.instance.addScore(value);
			if(effect!=null){ Instantiate(effect,transform.position,Quaternion.identity); }
			Destroy(gameObject);
		}
	}
}
