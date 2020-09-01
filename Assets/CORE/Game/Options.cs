using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Options : MonoBehaviour {

	public Slider sensitivity;
	public InputField nameLabel;
	public Slider sound;
	public Toggle vibration;
	private GameData data;

	void Start () {
		this.data = GameData.load();
		sensitivity.minValue=1; sensitivity.maxValue=10; sensitivity.value=this.data.sensitivity;
		sound.minValue=0; sound.maxValue=1; sound.value=this.data.sound;
		this.nameLabel.text = data.playerName;
		this.vibration.isOn = data.vibration;
	}
	
	public void close(){
		this.data.sensitivity = this.sensitivity.value;
		this.data.sound = this.sound.value;
		this.data.playerName = this.nameLabel.text;
		this.data.vibration = this.vibration.isOn;
		this.data.save();
		AudioListener.volume=data.sound;
		Destroy(gameObject);
	}

	public void reset(){ sensitivity.value=3f; sound.value=1f; vibration.isOn=true; }

}
