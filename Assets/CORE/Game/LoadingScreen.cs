using UnityEngine;
using System.Collections;

public class LoadingScreen : MonoBehaviour {

	void Start () {
		UnityAds.Instance.ShowInterstitialAd();
		int next=(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex+1);
		UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(next);
	}

}
