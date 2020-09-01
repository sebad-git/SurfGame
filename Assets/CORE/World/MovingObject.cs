using UnityEngine;
using System.Collections;

public class MovingObject : MonoBehaviour {
	
	public MovingObjectConfig config;
	private float averageSpeed;

	void Start(){ 
		if(this.config.random){ this.averageSpeed = Random.Range(this.config.minSpeed,this.config.maxSpeed); } 
		else{ this.averageSpeed = this.config.minSpeed; } 
		if(this.config.randomJump){ this.StartCoroutine(this.jump()); }
	}

	void FixedUpdate() { 
		transform.Translate(this.config.direction * this.averageSpeed * Time.deltaTime); 
	}

	private IEnumerator jump(){
		float jumpTime=Random.Range(1f,10f);
		yield return new WaitForSeconds(jumpTime);
		gameObject.GetComponent<Rigidbody>().AddForce(0,this.config.jumpSpeed*1000,0);
	}

}
