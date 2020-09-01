using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Facebook.Unity;
using System.Linq;
using System;

public abstract class FBManagerInvite : FBManagerBase {

	public void facebookInvite(){
		Debug.Log("FBU: inviting");
		this.facebookLoginPublish();
	}

	protected override void publishAction(){
//		FB.AppRequest(
//			config.FACEBOOK.invite.message,null, null, null, config.FACEBOOK.invite.friends,
//			config.FACEBOOK.invite.title,string.Empty,inviteCallback );
		FB.Mobile.AppInvite(new Uri(config.FACEBOOK.share.linkUrl),
			new Uri(config.FACEBOOK.share.imageUrl), this.inviteCallback);
	}

	protected void inviteCallback (IResult result){
		Debug.Log("FBU: Invite ");
		postError = null;
		if (result != null) {
			IDictionary<string, object> responseObject = result.ResultDictionary;
			object obj = 0;
			if (responseObject.TryGetValue ("cancelled", out obj)) {
				Debug.Log ("FBU: ERROR: Request cancelled");
				this.OnPublishCancel();
			} else if (responseObject.TryGetValue ("request", out obj)) {
				Debug.Log ("FBU: Invite SUCCESS");
				this.OnPublishSuccess();
			}
		} else {
			Debug.Log ("FBU: Invite SUCCESS");
			this.OnPublishSuccess();
		}
	}

}
