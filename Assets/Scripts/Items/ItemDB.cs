using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ItemDB {

	static Dictionary<string, Item> resources;
	static Dictionary<string, Item> foods;
	static Dictionary<string, Item> weapons;

	//dictionary of dictionaries of various items

	static Dictionary<string, Dictionary<string, Item>> items;


	static string path = "icons/";
	static private bool instantiated = false;


	//sets up all the items
	public static void setup() {
		/** Initialize Dictionaries **/
		resources = new Dictionary<string, Item> ();
		foods = new Dictionary<string, Item> ();
		weapons = new Dictionary<string, Item> ();
		items = new Dictionary<string, Dictionary<string, Item>> ();
		/** Add Items to Each Dictionary **/
		weapons.Add("sword", new Melee("sword", load(path+"katana")));
		weapons.Add("shiruken", new Thrown("shuriken",load(path+"throwing_star")));


		/** Add Item Types to Main Dictionary **/
		items.Add ("resource", resources);
		resources.Add("wood", new Melee("wood",load(path+"wood")));

		items.Add ("food", foods);
		items.Add ("weapon", weapons);
		instantiated = true;
	}


	public static Item getItem(string type, string name) {
		if (!instantiated) {
			setup ();
		}
		Item newItem = null;
		Dictionary<string, Item> typeDict;
		if (items.TryGetValue(type, out typeDict)) {
			typeDict.TryGetValue(name, out newItem);
		}
		return newItem;

	}

	public static Sprite load(string file) {
		Sprite s = null;

		Texture2D loaded = Resources.Load (file) as Texture2D;
		if (loaded != null) {
			s = Sprite.Create (loaded, new Rect (0, 0, loaded.width, loaded.height), new Vector2 (0, 0));
		}
		Debug.Log (s);
		return s;
	}
}
