using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

	public Slider sensitivity;

	void Start(){
		sensitivity.minValue=1; sensitivity.maxValue=10; sensitivity.value=GameController.instance.data.sensitivity;
		Screen.sleepTimeout=SleepTimeout.SystemSetting;
	}

	public void restart(){
		Time.timeScale=1;
		UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
	}

	public void menu(){
		Time.timeScale=1;
		UnityEngine.SceneManagement.SceneManager.LoadScene(0);
	}

	public void resume(){
		Time.timeScale=1; 
		//TODO
		GameController.instance.data.sensitivity = this.sensitivity.value;
		GameController.instance.data.save();
		GameController.instance.player.mobileSensitivity=GameController.instance.data.sensitivity;
		Screen.sleepTimeout=SleepTimeout.NeverSleep;
		GameController.instance.startCountDown();
		Destroy(gameObject);
	}



}
