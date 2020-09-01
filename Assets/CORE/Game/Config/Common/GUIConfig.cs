using UnityEngine;

[System.Serializable]
public class GUIConfig {
	public float gameoverDelay=2f;
	public int gamesPerAds=5;
	public int gamesPerRate=30;
	public GameOverMenu gameoverMenu;
	public PauseMenu pauseMenu;
	public RateApp rateMenu;
	public Options optionsMenu;
	public Objectives objectivesMenu;
	public ChoosScore scoresMenu;
	public StoreMenu storeMenu;
}
