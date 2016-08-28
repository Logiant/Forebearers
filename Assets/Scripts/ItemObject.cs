using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//Unity wrapper for ingame items - this allows them to be picked up and interacted with!
public class ItemObject : MonoBehaviour {

	//should this maybe be an uint/byte ID w/ a lookup table?
	public string type = "melee";
	public string itemName = "sword";

	public Sprite sprite;

	Item item;


	public Item getItem() {
		return item;
	}


	void Start() {

		item = ItemDB.getItem (type, itemName);
		if (item == null) {
			Debug.Log ("Item " + type + "/" + itemName + " does not exist in ItemDB!");
		} else {
			Debug.Log (item.getIcon());
		}
	}


	//(class) ItemData [parent] ?? 

}
