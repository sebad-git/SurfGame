using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FBShareButton : FBManagerShare {

	public void Share(){
		GameData data = GameData.load();
		string shareCaption = 
			System.String.Format("{0} has a score of {1} in Jump the Whale. Can you beat him/her?",data.playerName,data.bestScore);
		this.facebookShare(shareCaption);
	}

	protected override void OnLoginError(){}
	protected override void OnLoginCancel(){}
	protected override void OnLogin(){}
	protected override void OnPublishSuccess(){}
	protected override void OnPublishError(){}
	protected override void OnPublishCancel(){}
	protected override void OnTryPublish(){ }

}
