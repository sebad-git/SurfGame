using UnityEngine;
using System.Collections;

public class Preloader : MonoBehaviour {

	// Use this for initialization
	void Start () {
		FirebaseManager.init();
		AdMobManager.showBanner(true);
		int next=(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex+1);
		UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(next);
	}

}
