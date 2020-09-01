using UnityEngine;
using System.Collections;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System.Threading.Tasks;
using System.Collections.Generic;

public class FirebaseManager {

	public static Firebase.Auth.FirebaseUser user;
	private static bool started;

	public static void init(){
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://surfandroidgame.firebaseio.com/");
		FirebaseDatabase.DefaultInstance.GoOnline();
		if(!started){
			FirebaseApp.FixDependenciesAsync().ContinueWith(task => {
				if (task.IsCompleted && !task.IsFaulted) { Debug.Log("Ready."); } 
			});
			started=true;
		}
		Debug.Log("Firebase Ready.");
	}

	public static void loginToken(){
		string token = GameData.load().id;
		if(!System.String.IsNullOrEmpty(token)){
			Firebase.Auth.FirebaseAuth.DefaultInstance.SignInWithCustomTokenAsync(token).ContinueWith(task => {
				if (task.IsCompleted && !task.IsCanceled && !task.IsFaulted) {
					Debug.Log("Signed in.");
					user = task.Result;
				}
			});
		}else{
			Debug.LogError("Token is null");
			loginAnonymus();
		}
	}


	public static void login(){
		string token = System.String.Format("{0}@mountsix.com",GameData.load().id);
		Firebase.Auth.FirebaseAuth.DefaultInstance.CreateUserWithEmailAndPasswordAsync(token,"msm"+token).ContinueWith(task => {
			if (task.IsCompleted && !task.IsCanceled && !task.IsFaulted) {
				Firebase.Auth.FirebaseAuth.DefaultInstance.SignInWithEmailAndPasswordAsync(token,"msm"+token).ContinueWith(task1 => {
					if (task1.IsCompleted && !task1.IsCanceled && !task1.IsFaulted) {
						Debug.Log("Signed in.");
						user = task1.Result;
					}else{
						Debug.LogError("Login Error." + task1.Exception);
					}
				});
			}else{
				Debug.LogError("Login Error." + task.Exception);
			}
		});
	}

	public static void loginAnonymus(){
		Firebase.Auth.FirebaseAuth.DefaultInstance.SignInAnonymouslyAsync().ContinueWith(task => {
			if (task.IsCompleted && !task.IsCanceled && !task.IsFaulted) {
				Debug.Log("Signed in.");
				user = task.Result;
			}else{
				Debug.LogError("Login Error." + task.Exception);
			}
		});
	}

	public static void uploadData<T>(string location, string id, T data){
		DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference(location).Child(id);
		reference.SetRawJsonValueAsync(JsonUtility.ToJson(data)).ContinueWith(task2 => {
			if (task2.Exception != null) { Debug.Log(task2.Exception.ToString()); }
		});
	}

}
