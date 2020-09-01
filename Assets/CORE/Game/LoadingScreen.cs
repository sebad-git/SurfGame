using UnityEngine;
using System.Collections;

public class LoadingScreen : MonoBehaviour {

	void Start () {
		AdMobManager.showBanner(true);
		int next=(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex+1);
		UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(next);
	}

}
