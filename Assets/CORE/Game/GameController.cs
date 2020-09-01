using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	
	public Text counter;
	public Text scoreLabel;
	public Text distanceLabel;
	public Image hit;
	private int score;
	[HideInInspector]public static int gameCounter;
	public static GameController instance;
	[HideInInspector]public GameData data;
	[HideInInspector]public Player player;
	private float playerOrigin;
	private AudioSource sound;
	private bool gameStarted;
	private GameConfig config;

	void Awake () {
		instance = this; this.score=0; this.scoreLabel.text="0"; this.data = GameData.load();
		AudioListener.volume=data.sound;
		this.player = ((GameObject)GameObject.FindGameObjectWithTag("Player")).GetComponent<Player>();
		this.playerOrigin = this.player.transform.position.z;
		this.config=(GameConfig)ConfigLoader.loadConfig(ConfigLoader.GAME_CONFIG);
	}

	void Start(){
		GameController.gameCounter++;
		Application.targetFrameRate=30;
		this.sound=gameObject.GetComponent<AudioSource>(); 
		AdMobManager.showBanner(true);
		Camera.main.GetComponent<Animator>().enabled=false;
		Screen.sleepTimeout=SleepTimeout.NeverSleep;
		this.startCountDown();
	}

	private IEnumerator countDown(){
		this.gameStarted=false; Time.timeScale=0; this.counter.enabled=true;
		this.sound.Stop();
		this.sound.loop=false; this.sound.clip=this.config.MUSIC.countDownSound; 
		this.counter.text="3";
		this.sound.Play();
		yield return new WaitForSecondsRealtime(1);
		this.counter.text="2";
		this.sound.Play();
		yield return new WaitForSecondsRealtime(1);
		this.counter.text="1";
		this.sound.Play();
		yield return new WaitForSecondsRealtime(1);
		this.sound.clip=this.config.MUSIC.gameMusic;  this.sound.loop=true; this.sound.Play();
		this.counter.enabled=false; Time.timeScale=1; this.gameStarted=true;
	}

	public void startCountDown(){ this.StartCoroutine(this.countDown()); }

	public void addScore(int p_score){
		this.score+=p_score; this.scoreLabel.text=this.score.ToString();
	}

	public void gameOver(){
		this.gameStarted=false;
		this.hit.sprite=this.config.hitEffects[Random.Range(0,this.config.hitEffects.Length)]; this.hit.gameObject.SetActive(true);
		Camera.main.GetComponent<Animator>().enabled=true;
		this.sound.Stop(); this.sound.loop=false; this.sound.clip=this.config.MUSIC.looseMusic; this.sound.Play();
		this.StartCoroutine(this.showGameOver());
	}

	public void pause(){
		if(this.gameStarted){
			Time.timeScale=0; this.sound.Stop();
			GameObject.Instantiate(this.config.GUI.pauseMenu);
		}
	}

	private IEnumerator showGameOver(){
		yield return new WaitForSeconds(this.config.GUI.gameoverDelay);
		Time.timeScale=0;
		GameObject.Instantiate(this.config.GUI.gameoverMenu);
	}

	public float getDistance(){ 
		float distance = (this.player.transform.position.z-this.playerOrigin);
		if(distance<=0){ return 0; }
		distance = (float)System.Math.Round((distance/100),2);
		return distance;
	}
	public int getScore(){ return this.score; }

	void FixedUpdate(){ if(this.gameStarted){ this.distanceLabel.text=System.String.Format("{0}m",this.getDistance()); }}
}
