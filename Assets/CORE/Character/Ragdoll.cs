using UnityEngine;
using System.Collections;

[System.Serializable]
public class Ragdoll{

	public Rigidbody root;
	public Vector3 pushForce=new Vector3(10,10,10);

	private System.Collections.Generic.List<CharacterJoint> members;

	public void activate(bool activate){
		if(this.members==null){
			this.members=new System.Collections.Generic.List<CharacterJoint>(root.GetComponentsInChildren<CharacterJoint>());
		}
		root.GetComponent<Collider>().enabled=activate; root.GetComponent<Rigidbody>().isKinematic=!activate;
		foreach(CharacterJoint joint in members){
			joint.GetComponent<Collider>().enabled=activate; joint.GetComponent<Rigidbody>().isKinematic=!activate;
		}
	}
}
