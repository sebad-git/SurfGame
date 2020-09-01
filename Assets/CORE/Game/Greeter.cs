using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Greeter : MonoBehaviour {

	public Text greeting;
	public Locale locale;
	private string greetingFormat="{0} {1}";

	public string localeGreeting;

	void Start () {
		if(!System.String.IsNullOrEmpty(GameData.load().playerName)){
			this.greeting.text=System.String.Format(this.greetingFormat,this.locale.getText(this.localeGreeting),GameData.load().playerName);
		}else{
			this.greeting.enabled=false;
		}
	}

}
