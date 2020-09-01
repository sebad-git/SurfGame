using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System;
using System.IO;

public class FBDatabase : MonoBehaviour {

	private const string databaseUrl="https://surfandroidgame.firebaseio.com";
	private const string API_KEY = "AIzaSyD6vSHH7HTe80VkmRUwh3TuU0eExymG21Q";

	void Start () {
		getData ("Game/jtw/BestScores");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void getData(string path){
		HttpWebRequest request = (HttpWebRequest)WebRequest.Create(String.Format("{0}/{1}&api_key={2}",databaseUrl,path,API_KEY));
		HttpWebResponse response = (HttpWebResponse)request.GetResponse();
		StreamReader reader = new StreamReader(response.GetResponseStream());
		string jsonResponse = reader.ReadToEnd();

		//StartCoroutine(GetUser());

	}

	IEnumerator GetUser(String path) {
		String url = String.Format ("{0}/{1}", databaseUrl, path);
		using (WWW www = new WWW(url)) {
			yield return www;

			Renderer renderer = GetComponent<Renderer>();
			renderer.material.mainTexture = www.texture;
		}
	}

}
