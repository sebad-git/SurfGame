using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class Preloader : MonoBehaviour {
	
	void Start () {
		int next=(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex+1);
		UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(next);
	}

}
