using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreMenu : MonoBehaviour {
	
	public Text coins;
	public Image itemIcon; public Text cost; public Text itemName;
	public Button buttonNext;
	public Button buttonPrevious;
	public Button buyButton;
	public Button useButton;
	//Private
	private GameConfig config;
	private GameData data;
	private int index;
	private List<Item> items;

	void Start(){ 
		data=GameData.load(); 
		this.config=(GameConfig)ConfigLoader.loadConfig(ConfigLoader.GAME_CONFIG);
		this.coins.text=data.coins.ToString();
		this.items=this.config.STORE_ITEMS.suits; this.index=0;
		if(System.String.IsNullOrEmpty(data.currentSuit)){
			data.currentSuit=this.config.STORE_ITEMS.suits[0].itemId;
		}
		if(System.String.IsNullOrEmpty(data.currentBoard)){
			data.currentBoard=this.config.STORE_ITEMS.boards[0].itemId;
		}
		data.save();
		this.loadItem();
	}

	public void showSuits(){ this.items=this.config.STORE_ITEMS.suits;  this.index=0; this.loadItem(); }
	public void showBoards(){ this.items=this.config.STORE_ITEMS.boards; this.index=0; this.loadItem(); }

	private void loadItem(){
		try{
			Item selected=this.items[this.index];
			bool isBought = data.items.Contains(selected.itemId);
			this.buttonNext.interactable=((this.index+1)<this.items.Count);
			this.buttonPrevious.interactable=(this.index>0); 
			bool canUse = false;
			if(selected.type == Item.ItemType.SUIT){ canUse = (data.currentSuit!=selected.itemId); }
			if(selected.type == Item.ItemType.BOARD){ canUse = (data.currentBoard!=selected.itemId); }
			this.useButton.interactable = ( ( (isBought || this.index==0) && canUse) );
			this.buyButton.interactable = (!isBought && (data.coins>=selected.cost) && (this.index!=0) );
			this.itemIcon.sprite = selected.icon;
			if(isBought || this.index==0){this.cost.text=""; }else{ this.cost.text=selected.cost.ToString(); }
			this.itemName.text = selected.name;
		}catch(System.Exception){
			this.buyButton.interactable = false; this.buttonNext.interactable=false;
			this.useButton.interactable = false; this.buttonPrevious.interactable=false;
		}
	}
		
	public void nextItem(){
		int nextItem = (this.index+1); bool canMove=false;
		canMove=(nextItem < this.items.Count);
		if(canMove){ this.index+=1; this.loadItem(); }
	}

	public void previousItem(){
		int nextItem = (this.index-1); bool canMove=(nextItem>=0);
		if(canMove){ this.index-=1; this.loadItem(); }
	}

	public void buy(){
		Item selected = this.items[this.index];
		data.addItem(selected.itemId);
		data.coins = (data.coins-selected.cost); data.save();
		this.coins.text=data.coins.ToString(); this.loadItem();
	}

	public void use(){
		Item selected = this.items[this.index];
		if(selected.type == Item.ItemType.SUIT){ data.currentSuit = selected.itemId; data.save(); }
		if(selected.type == Item.ItemType.BOARD){ data.currentBoard = selected.itemId; data.save(); }
		this.loadItem();
	}

	public void close(){ data.save(); Destroy(gameObject); }
		
}
