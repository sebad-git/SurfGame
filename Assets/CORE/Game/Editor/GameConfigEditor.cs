using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor (typeof(GameConfig))]
public class GameConfigEditor : Editor {

	private GameConfig instance;
	private Vector2 scrollPos;
	private static string subGroupStyle = "ObjectFieldThumb";
	private static string rootGroupStyle = "GroupBox";

	public void OnEnable(){ instance = (GameConfig)target; }

	[MenuItem ("Assets/Create/Game/Game Configuration")]
	public static void createConfig() {
		GameConfig config = ScriptableObject.CreateInstance<GameConfig>();
		ProjectWindowUtil.CreateAsset(config, "gameConfig.asset");
	}

	public override void OnInspectorGUI(){
		//MAIN LAYOUT
		scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
		EditorGUILayout.BeginVertical(rootGroupStyle);
		EditorGUILayout.BeginHorizontal(rootGroupStyle);
		GUILayout.FlexibleSpace(); EditorGUILayout.LabelField("GAME CONFIGURATION",EditorStyles.boldLabel); GUILayout.FlexibleSpace(); 
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.Space();

		//MUSIC.
		EditorGUILayout.BeginVertical(rootGroupStyle);
		instance.showMusic = EditorGUILayout.Foldout(instance.showMusic,("MUSIC"), EditorStyles.foldout);
		if(instance.showMusic){
			EditorGUILayout.BeginVertical(rootGroupStyle);
			instance.MUSIC.gameMusic = (AudioClip)EditorGUILayout.ObjectField("Game Music:",instance.MUSIC.gameMusic,(typeof(AudioClip)),false);
			instance.MUSIC.looseMusic = (AudioClip)EditorGUILayout.ObjectField("Loose Music:",instance.MUSIC.looseMusic,(typeof(AudioClip)),false);
			instance.MUSIC.countDownSound = (AudioClip)EditorGUILayout.ObjectField("Countdown Sound:",instance.MUSIC.countDownSound,(typeof(AudioClip)),false);
			EditorGUILayout.EndVertical();
		}
		EditorGUILayout.EndVertical();
		EditorGUILayout.Space();
		//END MUSIC.
		//GUI.
		EditorGUILayout.BeginVertical(rootGroupStyle);
		instance.showGui = EditorGUILayout.Foldout(instance.showGui,("GUI"), EditorStyles.foldout);
		if(instance.showGui){
			EditorGUILayout.BeginVertical(rootGroupStyle);
			//Config
			instance.GUI.gameoverDelay = EditorGUILayout.FloatField("Game Over Delay:",instance.GUI.gameoverDelay);
			instance.GUI.gamesPerAds = EditorGUILayout.IntField("Show Ads every:",instance.GUI.gamesPerAds);
			instance.GUI.gamesPerRate = EditorGUILayout.IntField("Show Rate every:",instance.GUI.gamesPerRate);
			//Menus
			instance.GUI.gameoverMenu = (GameOverMenu)EditorGUILayout.ObjectField("Game Over Menu:",instance.GUI.gameoverMenu,(typeof(GameOverMenu)),false);
			instance.GUI.pauseMenu = (PauseMenu)EditorGUILayout.ObjectField("Pause Menu:",instance.GUI.pauseMenu,(typeof(PauseMenu)),false);
			instance.GUI.objectivesMenu = (Objectives)EditorGUILayout.ObjectField("Objectives Menu:",instance.GUI.objectivesMenu,(typeof(Objectives)),false);
			instance.GUI.storeMenu = (StoreMenu)EditorGUILayout.ObjectField("Store Menu:",instance.GUI.storeMenu,(typeof(StoreMenu)),false);
			instance.GUI.optionsMenu = (Options)EditorGUILayout.ObjectField("Options Menu:",instance.GUI.optionsMenu,(typeof(Options)),false);
			instance.GUI.scoresMenu = (ChoosScore)EditorGUILayout.ObjectField("Scores Menu:",instance.GUI.scoresMenu,(typeof(ChoosScore)),false);
			instance.GUI.rateMenu = (RateApp)EditorGUILayout.ObjectField("Scores Menu:",instance.GUI.rateMenu,(typeof(RateApp)),false);
			EditorGUILayout.EndVertical();
		}
		EditorGUILayout.EndVertical();
		EditorGUILayout.Space();
		//END GUI.

		//STORE.
		EditorGUILayout.BeginVertical(rootGroupStyle);
		instance.showStore = EditorGUILayout.Foldout(instance.showStore,("STORE ITEMS"), EditorStyles.foldout);
		if(instance.showStore){
			EditorGUILayout.Space();
			//SUITS
			EditorGUILayout.BeginHorizontal(subGroupStyle);
			GUILayout.FlexibleSpace(); EditorGUILayout.LabelField("SUITS");
			GUILayout.FlexibleSpace(); if(StyledButton("ADD SUIT")){ addSuit(); } GUILayout.FlexibleSpace();
			EditorGUILayout.EndHorizontal();

			if (instance.STORE_ITEMS.suits.Count == 0) {
				EditorGUILayout.BeginVertical(subGroupStyle);
				EditorGUILayout.BeginHorizontal();
				GUILayout.FlexibleSpace(); GUILayout.Label ("NO SUITS YET"); GUILayout.FlexibleSpace(); 
				EditorGUILayout.EndHorizontal();
				GUILayout.FlexibleSpace();
				EditorGUILayout.EndVertical();
			}else{
				EditorGUILayout.BeginVertical(rootGroupStyle);
				for(int s=0; s<instance.STORE_ITEMS.suits.Count; s++){
					EditorGUILayout.BeginVertical(rootGroupStyle);
					instance.STORE_ITEMS.suits[s].showItem = EditorGUILayout.Foldout(instance.STORE_ITEMS.suits[s].showItem,(System.String.IsNullOrEmpty(instance.STORE_ITEMS.suits[s].name)? "NEW SUIT":instance.STORE_ITEMS.suits[s].name.ToUpper()), EditorStyles.foldout);
					if (instance.STORE_ITEMS.suits[s].showItem) {
						instance.STORE_ITEMS.suits[s].name=EditorGUILayout.TextField("Name:",instance.STORE_ITEMS.suits[s].name);
						instance.STORE_ITEMS.suits[s].itemId=EditorGUILayout.TextField("Item ID:",instance.STORE_ITEMS.suits[s].itemId);
						instance.STORE_ITEMS.suits[s].cost=EditorGUILayout.IntField("Cost:",instance.STORE_ITEMS.suits[s].cost);
						instance.STORE_ITEMS.suits[s].type=(Item.ItemType)EditorGUILayout.EnumPopup("Type:",instance.STORE_ITEMS.suits[s].type);
						instance.STORE_ITEMS.suits[s].icon = (Sprite)EditorGUILayout.ObjectField("Icon:",instance.STORE_ITEMS.suits[s].icon,typeof(Sprite),false);
						instance.STORE_ITEMS.suits[s].texture = (Texture)EditorGUILayout.ObjectField("Texture:",instance.STORE_ITEMS.suits[s].texture,typeof(Texture),false);
						EditorGUILayout.BeginHorizontal();
						if(StyledButton("Remove")){ removeSuit(s); } 
						if(StyledButton("Move Up")){ moveUpSuit(s);} if(StyledButton("Move Down")){ moveDownSuit(s);} 
						EditorGUILayout.EndHorizontal();
					}
					EditorGUILayout.EndVertical();
				}
				EditorGUILayout.EndVertical();
				//END SUITS.
			}
			EditorGUILayout.Space();
			//BOARDS.
			EditorGUILayout.BeginHorizontal(subGroupStyle);
			GUILayout.FlexibleSpace(); EditorGUILayout.LabelField("BOARDS");
			GUILayout.FlexibleSpace(); if(StyledButton("ADD BOARD")){ addBoard(); } GUILayout.FlexibleSpace();
			EditorGUILayout.EndHorizontal();

			if (instance.STORE_ITEMS.boards.Count == 0) {
				EditorGUILayout.BeginVertical(subGroupStyle);
				EditorGUILayout.BeginHorizontal();
				GUILayout.FlexibleSpace(); GUILayout.Label ("NO BOARDS YET"); GUILayout.FlexibleSpace(); 
				EditorGUILayout.EndHorizontal();
				GUILayout.FlexibleSpace();
				EditorGUILayout.EndVertical();
			}else{
				EditorGUILayout.BeginVertical(rootGroupStyle);
				for(int s=0; s<instance.STORE_ITEMS.boards.Count; s++){
					EditorGUILayout.BeginVertical(rootGroupStyle);
					instance.STORE_ITEMS.boards[s].showItem = EditorGUILayout.Foldout(instance.STORE_ITEMS.boards[s].showItem,(System.String.IsNullOrEmpty(instance.STORE_ITEMS.boards[s].name)? "NEW BOARD":instance.STORE_ITEMS.boards[s].name.ToUpper()), EditorStyles.foldout);
					if (instance.STORE_ITEMS.boards[s].showItem) {
						instance.STORE_ITEMS.boards[s].name=EditorGUILayout.TextField("Name:",instance.STORE_ITEMS.boards[s].name);
						instance.STORE_ITEMS.boards[s].itemId=EditorGUILayout.TextField("Item ID:",instance.STORE_ITEMS.boards[s].itemId);
						instance.STORE_ITEMS.boards[s].cost=EditorGUILayout.IntField("Cost:",instance.STORE_ITEMS.boards[s].cost);
						instance.STORE_ITEMS.boards[s].type=(Item.ItemType)EditorGUILayout.EnumPopup("Type:",instance.STORE_ITEMS.boards[s].type);
						instance.STORE_ITEMS.boards[s].icon = (Sprite)EditorGUILayout.ObjectField("Icon:",instance.STORE_ITEMS.boards[s].icon,typeof(Sprite),false);
						instance.STORE_ITEMS.boards[s].texture = (Texture)EditorGUILayout.ObjectField("Texture:",instance.STORE_ITEMS.boards[s].texture,typeof(Texture),false);
						EditorGUILayout.BeginHorizontal();
						if(StyledButton("Remove")){ removeBoard(s); } 
						if(StyledButton("Move Up")){ moveUpBoard(s);} if(StyledButton("Move Down")){ moveDownBoard(s);} 
						EditorGUILayout.EndHorizontal();
					}
					EditorGUILayout.EndVertical();
				}
				EditorGUILayout.EndVertical();
				//END BOARDS.
			}

		}
		EditorGUILayout.EndVertical();
		EditorGUILayout.Space();
		//END STORE.


		//EFFECTS.
		EditorGUILayout.BeginVertical(rootGroupStyle);
		instance.showMisc = EditorGUILayout.Foldout(instance.showMisc,("EXTRA"), EditorStyles.foldout);
		if(instance.showMisc){
			EditorGUILayout.BeginVertical(rootGroupStyle);
			EditorGUILayout.Space();
			EditorGUILayout.BeginHorizontal(rootGroupStyle);
			GUILayout.FlexibleSpace(); EditorGUILayout.LabelField("HIT EFFECTS");
			GUILayout.FlexibleSpace(); if(StyledButton("ADD EFFECT")){ addEffect(); } GUILayout.FlexibleSpace();
			EditorGUILayout.EndHorizontal();

			if (instance.hitEffects.Length == 0) {
				EditorGUILayout.BeginVertical(rootGroupStyle);
				EditorGUILayout.BeginHorizontal();
				GUILayout.FlexibleSpace(); GUILayout.Label ("FILE EMPTY"); GUILayout.FlexibleSpace(); 
				EditorGUILayout.EndHorizontal();
				GUILayout.FlexibleSpace();
				EditorGUILayout.EndVertical();
			}else{
				for(int e=0; e<instance.hitEffects.Length; e++){
					EditorGUILayout.BeginVertical(rootGroupStyle);
					EditorGUILayout.BeginHorizontal();
					instance.hitEffects[e] = (Sprite)EditorGUILayout.ObjectField(instance.hitEffects[e],typeof(Sprite),false);
					if(StyledButton("Remove Effect")){ removeEffect(e); }
					EditorGUILayout.EndHorizontal();
					EditorGUILayout.EndVertical();
				}
			}
			EditorGUILayout.EndVertical();
		}
		EditorGUILayout.EndVertical();
		EditorGUILayout.Space();
		//END EFFECTS.

		EditorGUILayout.BeginHorizontal(rootGroupStyle);
		EditorGUILayout.LabelField("CONNECTIONS",EditorStyles.boldLabel); 
		instance.CONNECTIONS = (ConnectionsConfig)EditorGUILayout.ObjectField(instance.CONNECTIONS,(typeof(ConnectionsConfig)),false);
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.Space();

		//END MAIN LAYOUT
		EditorUtility.SetDirty(instance);
		GUILayout.FlexibleSpace();
		EditorGUILayout.EndVertical();
		EditorGUILayout.EndScrollView();
	}

	private void addEffect(){
		List<Sprite> effects = null;
		if(instance.hitEffects==null){effects=new List<Sprite>(); }
		else{ effects = new List<Sprite>(instance.hitEffects); }
		effects.Add(new Sprite());
		instance.hitEffects=effects.ToArray();
	}


	private void removeEffect(int index){
		List<Sprite> effects = null;
		if(instance.hitEffects==null){effects=new List<Sprite>(); }
		else{ effects = new List<Sprite>(instance.hitEffects); }
		effects.RemoveAt(index);
		instance.hitEffects=effects.ToArray();
	}
	//SUITS ACTIONS.
	private void addSuit(){
		if(instance.STORE_ITEMS.suits==null){instance.STORE_ITEMS.suits=new List<Item>(); }
		Item item = new Item(); item.type = Item.ItemType.SUIT;
		instance.STORE_ITEMS.suits.Add(new Item());
	}

	private void removeSuit(int index){
		if(instance.STORE_ITEMS.suits==null){instance.STORE_ITEMS.suits=new List<Item>(); }
		instance.STORE_ITEMS.suits.RemoveAt(index);
	}

	private void moveUpSuit(int index){
		if(instance.STORE_ITEMS.suits==null){instance.STORE_ITEMS.suits=new List<Item>(); }
		if (index < instance.STORE_ITEMS.suits.Count) {
			Item item = instance.STORE_ITEMS.suits[index];
			instance.STORE_ITEMS.suits.RemoveAt(index);
			instance.STORE_ITEMS.suits.Insert( (index-1), item);
		}
	}

	private void moveDownSuit(int index){
		if(instance.STORE_ITEMS.suits==null){instance.STORE_ITEMS.suits=new List<Item>(); }
		if(index>0){
			Item item = instance.STORE_ITEMS.suits[index];
			instance.STORE_ITEMS.suits.RemoveAt(index);
			instance.STORE_ITEMS.suits.Insert( (index+1), item);
		}
	}

	//BOARDS ACTIONS.
	private void addBoard(){
		if(instance.STORE_ITEMS.boards==null){instance.STORE_ITEMS.boards=new List<Item>(); }
		Item item = new Item(); item.type = Item.ItemType.BOARD;
		instance.STORE_ITEMS.boards.Add(new Item());
	}

	private void removeBoard(int index){
		if(instance.STORE_ITEMS.boards==null){instance.STORE_ITEMS.boards=new List<Item>(); }
		instance.STORE_ITEMS.boards.RemoveAt(index);
	}

	private void moveUpBoard(int index){
		if(instance.STORE_ITEMS.boards==null){instance.STORE_ITEMS.boards=new List<Item>(); }
		if(index>0){
			Item item = instance.STORE_ITEMS.boards[index];
			instance.STORE_ITEMS.boards.RemoveAt(index);
			instance.STORE_ITEMS.boards.Insert( (index-1), item);
		}
	}

	private void moveDownBoard(int index){
		if(instance.STORE_ITEMS.boards==null){instance.STORE_ITEMS.boards=new List<Item>(); }
		if (index < instance.STORE_ITEMS.boards.Count) {
			Item item = instance.STORE_ITEMS.boards[index];
			instance.STORE_ITEMS.boards.RemoveAt(index);
			instance.STORE_ITEMS.boards.Insert( (index+1), item);
		}
	}

	private bool StyledButton(string label) {
		EditorGUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		bool clickResult = GUILayout.Button(label, "CN CountBadge");
		GUILayout.FlexibleSpace();
		EditorGUILayout.EndHorizontal();
		return clickResult;
	}

}
