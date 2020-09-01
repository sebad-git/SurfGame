using UnityEngine;

public class MovingObjectConfig : ScriptableObject {

	public Vector3 direction;
	public float minSpeed=6f;
	public bool random=false;
	public float maxSpeed=6f;
	public bool randomJump=false;
	public float jumpSpeed=6f;
}
