using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class UnityAds : MonoBehaviour, UnityAds.AdManager {

	public string gameId = "project";
	public bool testMode = true;
	public string bannerId = "banner";
	public BannerPosition bannerPosition = BannerPosition.BOTTOM_CENTER;
	public string videoId = "video";
	public string rewardVideoId = "rvideo";
	public string intersitialId = "intersitial";

	private static UnityAds.AdManager instance;
	public static UnityAds.AdManager Instance { get {return instance; } }

	void Awake() { 
		instance = this; DontDestroyOnLoad(this); 
		Advertisement.Initialize (gameId, testMode);
	}

	public void ShowBanner(bool show){
		if (!show) { Advertisement.Banner.Hide(false); return; }
		StartCoroutine(ShowBannerWhenInitialized());
	}

	private IEnumerator ShowBannerWhenInitialized () {
		while (!Advertisement.isInitialized) { yield return new WaitForSeconds(0.5f); }
		Advertisement.Banner.SetPosition (bannerPosition);
		Advertisement.Banner.Show(bannerId);
	}

	public void ShowInterstitialAd() {
		if (Advertisement.IsReady()) { Advertisement.Show(); } 
		else {
			Debug.Log("Interstitial ad not ready at the moment! Please try again later!");
		}
	}

	public void ShowRewardedVideo() {
		if (Advertisement.IsReady(rewardVideoId)) {
			Advertisement.Show(rewardVideoId);
		} 
		else {
			Debug.Log("Rewarded video is not ready at the moment! Please try again later!");
		}
	}

	public void ShowVideo() {
		if (Advertisement.IsReady(videoId)) {
			Advertisement.Show(videoId);
		}
		else {
			Debug.Log("Video is not ready at the moment! Please try again later!");
		}
	}

	public interface AdManager{
		void ShowBanner (bool show);
		void ShowInterstitialAd();
		void ShowVideo();
		void ShowRewardedVideo();

	}
}
