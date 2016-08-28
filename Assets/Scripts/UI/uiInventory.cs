using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class uiInventory : MonoBehaviour {

	public GameObject button;
	Image[] icons;
	Inventory current;

	int moveIndex = -1;

	// Use this for initialization
	void Start () {
		icons = new Image[0];
	}


	public void moveItem(int callerIndex) {
		//dont bother moving "from" an empty space
		if (icons [callerIndex].sprite != null && moveIndex == -1) {
			moveIndex = callerIndex;
		} else if (moveIndex != -1) {
			current.swap (callerIndex, moveIndex);
			updateInventory ();
			moveIndex = -1;
		}
	}


	// Update is called once per frame
	void Update () {
		if (current != null) {
			if (current.needsUpdate ()) {
				updateInventory ();
			}
			Debug.Log (ItemDB.getItem ("weapon", "sword").getIcon());
		}
	}


	void updateInventory() {
		List<Item> items = current.getItems ();
		for (int i = 0; i < items.Count; i++) {
			if (items [i] != null) {
				icons [i].sprite = items [i].getIcon();
			} else {
				icons [i].sprite = null;
			}
		}
	}
	public void setInventory(Inventory newInv) {
		for (int i = 0; i < icons.Length; i++) {
			if (icons [i] != null) {
				Destroy (icons [i].gameObject);
			}
		}
		if (newInv != null) {
			current = newInv;
			int cap = newInv.getCapacity ();
			icons = new Image[cap];
			for (int i = 0; i <cap; i++) {
				GameObject go = Instantiate (button);
				icons.SetValue(go.GetComponent<Image>(), i);
				icons [i].sprite = null;
				icons [i].gameObject.transform.SetParent(transform);
				icons [i].gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(30 + 55*(i%4), -30 - 55*(Mathf.Floor(i/4)));
				int capIndex = i;
				icons [i].GetComponent<Button> ().onClick.AddListener (() => { moveItem (capIndex); });
			}
			updateInventory ();
		}
	}

}
