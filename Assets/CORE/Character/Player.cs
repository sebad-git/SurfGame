using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]

public class Player : MonoBehaviour {

	public float forwardSpeed=5;
	public float sidesSpeed=5;
	public float jumpSpeed=6;
	public float customGravity=-9;
	public float mobileSensitivity=1.2f;
	public GameObject waterEffect;
	public Ragdoll joints;
	public GameObject board;
	public bool forceMobile=false;
	public AudioClip landSound;
	public AudioClip jumpSound;
	public AudioClip dieSound;

	private float AXIS_X;
	private Rigidbody body;
	private AudioSource sounds;
	private bool DEAD;
	private float gravity;
	private bool GROUNDED;
	private bool MOBILE;
	protected PointerEventData eventDataCurrentPosition;

	void Start () {
		this.body = gameObject.GetComponent<Rigidbody>();
		this.sounds = gameObject.GetComponent<AudioSource>();
		this.gravity=0; this.joints.activate(false); this.GROUNDED=true;
		this.MOBILE=SystemInfo.deviceType==DeviceType.Handheld;
		this.MOBILE=this.forceMobile?true:this.MOBILE;
		this.sounds.playOnAwake=false; this.sounds.loop=false;
		this.mobileSensitivity=GameController.instance.data.sensitivity;
		this.eventDataCurrentPosition = new PointerEventData(EventSystem.current);
	}

	void Update () {
		if(!DEAD){
			//MOVE.
			if(this.MOBILE){ this.AXIS_X = (Input.acceleration.x * mobileSensitivity);  }
			else{ this.AXIS_X=Input.GetAxis("Horizontal"); }
			//JUMP.
			if( (Input.GetMouseButtonDown(0) || (Input.GetAxis("Vertical")>0)) && !this.IsPointerOverUIObject() ){
				if(this.GROUNDED){
					this.sounds.Stop(); this.sounds.clip=jumpSound; this.sounds.Play();
					this.body.AddForce(Vector3.up * (jumpSpeed*1000) ); 
					if(this.waterEffect!=null){ this.waterEffect.gameObject.SetActive(false); }
					this.gravity=-Mathf.Abs(customGravity);
					this.GROUNDED=false;
				}
			}
		}
	}

	void FixedUpdate(){
		if(!DEAD){
			float moveSides=(this.AXIS_X * sidesSpeed);
			this.body.velocity = new Vector3(moveSides,body.velocity.y+gravity,forwardSpeed);
		}else{
			this.body.velocity = Vector3.zero;
		}
	}

	void OnCollisionEnter(Collision col){
		if(!DEAD){
			if(col.gameObject.tag=="Hazzard"){ this.die(); }
			if(col.gameObject.tag=="Water"){ 
				if(!this.GROUNDED){
					if(!this.sounds.isPlaying){ this.sounds.clip=landSound; this.sounds.Play(); }
					if(this.waterEffect!=null){ this.waterEffect.gameObject.SetActive(true); } 	
				}
				this.gravity=0; this.GROUNDED=true;
			}
		}
	}

	private void die(){
		this.DEAD=true;
		//CHARACER.
		this.sounds.Stop(); this.sounds.clip=dieSound; this.sounds.Play();
		Transform character = this.joints.root.transform.parent.parent;
		character.gameObject.GetComponent<Animator>().enabled=false;
		character.parent=null;
		this.joints.activate(true);
		this.joints.root.velocity=this.joints.pushForce;
		//BOARD
		Rigidbody boardBody = this.board.AddComponent<Rigidbody>(); SphereCollider col = this.board.AddComponent<SphereCollider>();
		col.radius=0.005f; boardBody.mass=30f;
		boardBody.transform.parent=null; 
		this.joints.root.velocity=this.joints.pushForce;
		if(this.waterEffect!=null){ this.waterEffect.SetActive(false); }
		if(this.MOBILE && GameController.instance.data.vibration){ Handheld.Vibrate(); }
		GameController.instance.gameOver();
	}

	protected bool IsPointerOverUIObject() {
		eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
		System.Collections.Generic.List<RaycastResult> results = new System.Collections.Generic.List<RaycastResult>();
		EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
		return results.Count > 0;
	}
}
