using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Facebook.Unity;
using System.Linq; 
using System;

public abstract class FBManagerShare : FBManagerBase {

	private string caption;

	protected void facebookShare(string p_caption){
		this.caption=p_caption;
		Debug.Log("FBU: sharing");
		this.facebookLoginPublish();
	}

	protected override void publishAction(){
		FB.ShareLink(
			new Uri(config.FACEBOOK.share.linkUrl),
			config.FACEBOOK.share.title,
			this.caption,
			new Uri(config.FACEBOOK.share.imageUrl),
			baseCallback);
	}

}
