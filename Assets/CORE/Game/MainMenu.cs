using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
	public CanvasRenderer namePanel;
	public InputField nameLabel;
	private GameConfig config;

	void Awake(){
		this.config=(GameConfig)ConfigLoader.loadConfig(ConfigLoader.GAME_CONFIG);
		GameData data = GameData.load(); data.setId();
		Time.timeScale=1; 
		AudioListener.volume=data.sound;
	}

	void Start () { UnityAds.Instance.ShowBanner(false); namePanel.gameObject.SetActive(false); }

	public void play() {
		GameData data = GameData.load();
		if( System.String.IsNullOrEmpty(data.playerName) ){ namePanel.gameObject.SetActive(true); }
		else{ this.loadGame(data); }
	}

	public void createGame(){
		GameData data = GameData.load();
		if(!System.String.IsNullOrEmpty(this.nameLabel.text)){
			data.playerName=this.nameLabel.text; data.save(); this.loadGame(data);
		}
	}

	private void loadGame(GameData data){
		int next=(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex+1);
		UnityEngine.SceneManagement.SceneManager.LoadScene(next);
	}

	public void exit(){ 
		Application.Quit();
	}

	public void showOptions(){ Instantiate(this.config.GUI.optionsMenu); }
	public void showObjectives(){ Instantiate(this.config.GUI.objectivesMenu); }
	public void showScores(){ Instantiate(this.config.GUI.scoresMenu); }
	public void showStore(){ Instantiate(this.config.GUI.storeMenu); }

}
