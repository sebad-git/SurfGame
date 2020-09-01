using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StoreConfig {
	public List<Item> suits;
	public List<Item> boards;

	public Item getItem(string itemId){
		List<Item> allItems = new List<Item>();
		allItems.AddRange(suits); allItems.AddRange(boards);
		System.Predicate<Item> itemFinder = (Item item) => { return item.itemId.ToString().ToLower() == itemId.ToLower(); };
		return allItems.Find(itemFinder);
	}
}


