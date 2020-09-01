using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using System.Linq;
using System;

public abstract class FBManagerBase : MonoBehaviour {
	//PRIVATE
	protected string loginMessage;
	protected string postError;
	protected ConnectionsConfig config;

	private void Awake () {
		this.config=ConnectionsLoader.loadConfig();
		FB.Init(OnInitComplete, OnHideUnity,null);
	}

	protected void OnInitComplete() { Debug.Log("FB.Init completed: Is user logged in? " + FB.IsLoggedIn); }
	protected void OnHideUnity(bool isGameShown) { Debug.Log("Is game showing? " + isGameShown); }

	protected void facebookLoginPublish() {
		this.OnTryPublish();
		FB.LogInWithPublishPermissions(new List<string>(){"publish_actions"}, PublishCallback);
	}

	private void PublishCallback(IResult result){
		if (result.Error != null) { this.OnLoginError(); } 
		else if (!FB.IsLoggedIn) { this.OnLoginCancel(); }
		else { this.publishAction(); }
		Debug.Log("FBU: "+loginMessage);
	}
		
	protected void baseCallback(IResult result){
		postError = null;
		if (!string.IsNullOrEmpty(result.Error)){
			postError = "Error Response:" + result.Error;
			Debug.Log("FBU: "+postError);
			this.OnPublishError();
		}
		else { 
			Debug.Log("FBU: SUCCESS ="+result.RawResult); 
			this.OnPublishSuccess();
		}
	}

	//ABSTRACT
	protected abstract void OnTryPublish();
	protected abstract void OnLoginError();
	protected abstract void OnLoginCancel();
	protected abstract void OnLogin();
	protected abstract void publishAction();
	protected abstract void OnPublishSuccess();
	protected abstract void OnPublishError();
	protected abstract void OnPublishCancel();
}
