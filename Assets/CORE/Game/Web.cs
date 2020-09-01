using UnityEngine;
using System.Collections;

public class Web : MonoBehaviour {

	public const string FACEBOOK="https://www.facebook.com/mountsix/";
	public const string TWITTER="http://twitter.com/mountsixmedia";
	public const string YOUTUBE="https://www.youtube.com/channel/UCtrzaLH7rUXTSQuH0wSzPzQ";
	public const string BLOGGER="http://mountsix.blogspot.com.uy";
	public const string GOOGLE_STORE="https://play.google.com/store/apps/dev?id=4935644371733079637";
	public const string RATE="market://details?id=com.mountsix.jtw";
	public void facebook(){ Application.OpenURL(Web.FACEBOOK); }
	public void twitter(){ Application.OpenURL(Web.TWITTER); }
	public void youtube(){ Application.OpenURL(Web.YOUTUBE); }
	public void blogger(){ Application.OpenURL(Web.BLOGGER); }
	public void moreApps(){ Application.OpenURL(Web.GOOGLE_STORE); }
	public static void rate(){ Application.OpenURL(Web.RATE); }
}
