using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;
using System;
using System.Text.RegularExpressions;
using System.Linq;
using System.Reflection;


public class Firebase : MonoBehaviour {

	[Header ("DATABASE URL")]
	public string databaseRoot = "https://surfandroidgame.firebaseio.com/";

	private static Firebase instance;
	public static Firebase Instance { get {return instance; } }

	void Awake() { instance = this; DontDestroyOnLoad(this); }

	public delegate void OnPost();
	public delegate void OnGet<T> (T result);
	public delegate void OnGetArray<T> (List<T> data);

	public void GetData<T>(string path, OnGet<T> callback){
		string url = String.Format ("{0}{1}.json", databaseRoot,path);
		RestClient.Get(url).Then( response => {
			Debug.Log(String.Format("Response code:{0}",response.StatusCode));
			callback(JsonUtility.FromJson<T>(response.Text));
		});
	}

	public void ListData<T>(string path, OnGetArray<T> callback){
		string url = String.Format ("{0}{1}.json", databaseRoot,path);
		RestClient.Get(url).Then( response => {
			try{
				Debug.Log(String.Format("Response code:{0}",response.StatusCode));
				string jsonArray = ParseData(response.Text);
				jsonArray.Replace("-","");
				Firebase.JsonArray<T> farray = JsonUtility.FromJson<Firebase.JsonArray<T>>(jsonArray);
				List<T> list = farray.content.ToList();
				callback(list);
			} catch(Exception e){ Debug.LogError(e.Message); }
		});
	}

	public void UpdateData<T>(string path, T data, OnPost callback){
		string url = String.Format ("{0}{1}.json", databaseRoot,path);
		RestClient.Put<T>(url,data).Then( response => { callback(); });
	}

	public string ParseData(string json){
		string pattern = "{\"[a-zA-Z0-9]*\":{", pattern2 = "},\"[a-zA-Z0-9]*\":{";
		string parsedJson = Regex.Replace (json.Trim(), pattern,"{\"content\":[{",RegexOptions.IgnoreCase);
		parsedJson = Regex.Replace (parsedJson, pattern2,"},{",RegexOptions.IgnoreCase);
		parsedJson= parsedJson.Substring(0,parsedJson.LastIndexOf ("}"));
		return parsedJson+"]}";
	}

	[System.Serializable] public class JsonArray<T>{ public T[] content; }

}
