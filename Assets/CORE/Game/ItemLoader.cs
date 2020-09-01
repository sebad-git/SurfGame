using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLoader : MonoBehaviour {

	public Renderer playerRenderer;
	public Renderer boardRenderer;
	private GameConfig config;

	void Awake() {
		this.config=(GameConfig)ConfigLoader.loadConfig(ConfigLoader.GAME_CONFIG);
		GameData data = GameData.load();
		if(!System.String.IsNullOrEmpty(data.currentSuit)){
			playerRenderer.material.mainTexture=config.STORE_ITEMS.getItem(data.currentSuit).texture;
		}
		if(!System.String.IsNullOrEmpty(data.currentBoard)){
			boardRenderer.material.mainTexture=config.STORE_ITEMS.getItem(data.currentBoard).texture;
		}
	}
}
