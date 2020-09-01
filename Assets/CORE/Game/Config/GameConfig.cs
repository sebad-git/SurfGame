using UnityEngine;

public class GameConfig : BaseGameConfig {
	
	public Sprite[] hitEffects;
	public StoreConfig STORE_ITEMS;
	[HideInInspector]public bool showMisc = false;
	[HideInInspector]public bool showStore = false;
}
