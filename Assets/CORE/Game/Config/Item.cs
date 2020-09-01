using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item {
	public string name;
	public enum ItemType{SUIT=0,BOARD=1};
	public ItemType type;
	public string itemId;
	public int cost;
	public Sprite icon;
	public Texture texture;
	[HideInInspector]public bool showItem = false;
}
