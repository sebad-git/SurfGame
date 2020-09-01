using UnityEngine;
using System.Collections;

public class Water : MonoBehaviour {

	private Transform player;

	void Start () {
		this.player = ((GameObject)GameObject.FindGameObjectWithTag("Player")).transform;
	}

	void FixedUpdate() {
		Vector3 pos=transform.position; pos.z=this.player.position.z;
		transform.position=pos;
	}
}
