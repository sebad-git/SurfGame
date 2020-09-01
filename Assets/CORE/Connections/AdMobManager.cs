using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;

public class AdMobManager {

	public static BannerView banner;
	private static InterstitialAd interAd;
	private static AdRequest request;

	public static void createBanner(){
		if(banner==null){
			banner = new BannerView(ConnectionsLoader.loadConfig().ADMOB.bannerID, AdSize.SmartBanner, AdPosition.Top);
			if(request ==null){ request = new AdRequest.Builder().Build(); } 
			banner.LoadAd(request);
			banner.Hide();
		}
	}

	public static void showBanner(bool show){
		if(banner==null){ createBanner(); }
		if (show==true) { banner.Show(); } else{ banner.Hide(); }
	}

	//Intersitial
	public static void createInterstitial() {
		if(request ==null){ request = new AdRequest.Builder().Build(); }
		if(interAd == null){ interAd = new InterstitialAd(ConnectionsLoader.loadConfig().ADMOB.intersitialID); }
		interAd.LoadAd(request);
	}

	public static bool showInterstitial(){
		if(interAd!=null && interAd.IsLoaded()){ interAd.Show(); return true;} return false;
	}
}
