using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;
using System;


public class Firebase : MonoBehaviour {

	public string databaseUrl="https://surfandroidgame.firebaseio.com/";

	private static Firebase instance;
	public static Firebase Instance { get {return instance; } }

	void Awake() { instance = this; DontDestroyOnLoad(this); }

	public delegate void OnPost();
	public delegate void OnGet<T> (T result);
	public delegate void OnGetArray<T> (Firebase.FArray<T> farray);

	public void getData<T>(string path, OnGet<T> callback){
		string url = String.Format ("{0}{1}.json", databaseUrl,path);
		Debug.Log("CALLING URL:"+url);
		RestClient.Get(url).Then( response => {
			Debug.Log(String.Format("Response code:{0}",response.StatusCode));
			callback(JsonUtility.FromJson<T>(response.Text));
		});
	}

	public void listData<T>(string path, OnGetArray<T> callback){
		string url = String.Format ("{0}{1}.json", databaseUrl,path);
		Debug.Log("CALLING URL:"+url);
		RestClient.Get(url).Then( response => {
			Debug.Log(String.Format("Response code:{0}",response.StatusCode));
			string jsonArray = String.Format("{{\"content\": [{0}]}}",response.Text);
			Debug.Log(jsonArray);
			Firebase.FArray<T> farray = JsonUtility.FromJson<Firebase.FArray<T>>(jsonArray);
			Debug.Log(farray.content.Length);
			callback(farray);
		});
	}

	public void setData<T>(string path, T data, OnPost callback){
		string url = String.Format ("{0}{1}.json", databaseUrl,path);
		RestClient.Put<T>(url,data).Then( onResolved: response => { callback(); });
	}


	[System.Serializable] public class FArray<T>{ public T[] content; }

}
