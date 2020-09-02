using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class UnityAds : MonoBehaviour, UnityAds.AdManager {

	public enum Platform { ANDROID, IOS }
	public Platform platform;
	public string androidGameID = "Play Store ID";
	public string iosGameID = "App Store ID";

	public string bannerId = "banner";
	public BannerPosition bannerPosition = BannerPosition.BOTTOM_CENTER;
	public string videoId = "video";
	public string rewardVideoId = "rvideo";
	public string intersitialId = "intersitial";

	private static UnityAds.AdManager instance;
	public static UnityAds.AdManager Instance { get {return instance; } }

	void Awake() {
		try{ 
			if(platform==Platform.ANDROID){Advertisement.Initialize(androidGameID, false); }
			if(platform==Platform.IOS){Advertisement.Initialize(iosGameID, false); }
		}
		catch(System.Exception e){ Debug.LogWarning(e.Message); }
		instance = this; DontDestroyOnLoad(this); 
	}

	public void ShowBanner(bool show){
		try{ 
			if (!show) { Advertisement.Banner.Hide(false); return; }
			StartCoroutine(ShowBannerWhenInitialized());
		}catch(System.Exception e){ Debug.LogWarning(e.Message); }
	}

	private IEnumerator ShowBannerWhenInitialized () {
		while (!Advertisement.isInitialized) { yield return new WaitForSeconds(0.5f); }
		Advertisement.Banner.SetPosition (bannerPosition);
		Advertisement.Banner.Show(bannerId);
	}

	public void ShowInterstitialAd() {
		try{ 
			if (Advertisement.IsReady()) { Advertisement.Show(); }
			else { Debug.Log("Interstitial ad not ready at the moment! Please try again later!"); }
		} catch(System.Exception e){ Debug.LogWarning(e.Message); }
	}

	public void ShowRewardedVideo() {
		try{ 
			if (Advertisement.IsReady(rewardVideoId)) { Advertisement.Show(rewardVideoId); } 
			else { Debug.Log("Rewarded video is not ready at the moment! Please try again later!"); }
		} catch(System.Exception e){ Debug.LogWarning(e.Message); }
	}

	public void ShowVideo() {
		try{ 
			if (Advertisement.IsReady(videoId)) { Advertisement.Show(videoId); }
			else { Debug.Log("Video is not ready at the moment! Please try again later!"); }
		} catch(System.Exception e){ Debug.LogWarning(e.Message); }


	}

	public interface AdManager {
		void ShowBanner (bool show);
		void ShowInterstitialAd();
		void ShowVideo();
		void ShowRewardedVideo();

	}
}
