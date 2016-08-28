using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


//TODO the inventory should probably be frozen
//when a trade is in progress...

public struct Trade {
	public int slotA;
	public int slotB;
	public Inventory A;
	public Inventory B;
	public int value;

	public Trade(Inventory A_, int slotA_, Inventory B_, int slotB_) {
		A = A_;
		B = B_;
		slotA = slotA_;
		slotB = slotB_;
		int aVal = 0; int bVal = 0;
		//TODO fix this shitcode
		if (A.peek (slotA) != null) {
			aVal = A.peek (slotA).getValue();
		} if (B.peek (slotB) != null) {
			bVal = B.peek (slotB).getValue();
		}
		value = aVal - bVal;
	}

	public void doTrade() {
		Item tmp = A.take (slotA);
		A.put (B.take (slotB), slotA);
		B.put (tmp, slotB);
	}
}


public class Inventory : MonoBehaviour {

	public int capacity = 15;

	List<Item> items;

	//this gets set to true when an item gets picked up
	//or rearranged. Then the UI knows to update
	bool updated = false;


	public int gold;

	// Use this for initialization
	void Start () {
		items = new List<Item> (capacity);
		for (int i = 0; i < capacity; i++) {
			items.Add (null);
		}
	}

	public bool
	needsUpdate() {
		bool needsUpdate = updated;
		updated = false;
		return needsUpdate;
	}

	public int getCapacity() {
		return capacity;
	}


	public Item peek(int slot) {
		return items [slot];
	}


	public void put(Item it, int index) {
		items [index] = it;
	}

	public Item take(int index) {
		Item tmp = items [index];
		items [index] = null;
		return tmp;
	}


	public static Trade Trade(Inventory A, int slotA, Inventory B, int slotB) {
		Trade T = new Trade();
		if (slotA >= 0 && slotB >= 0 && slotA < A.getCapacity() && slotB < B.getCapacity()) {
			T = new Trade (A, slotA, B, slotB);
			T.doTrade ();
		}
		return T;
	}

	public static void Trade(Trade T) {
		T.doTrade ();
	}
		
	void OnTriggerEnter(Collider other) {
		ItemObject io;
		io = other.gameObject.GetComponent<ItemObject> ();
		if (io != null) {
			updated = true;
			if (addItem (io.getItem ())) {
				Destroy (io.gameObject);
			}
		}
	}

	public void swap(int index1, int index2) {
		if (index1 >= 0 && index2 >= 0 && index1 < capacity && index2 < capacity) {
			Item tmp = items [index1];
			items [index1] = items [index2];
			items [index2] = tmp;
		}
	}

	bool addItem(Item item) {
		bool success = false;
		for (int i = 0; i < items.Count; i++) {
			if (items [i] == null) {
				items [i] = item;
				success = true;
				break;
			}
		}
		return success;
	}

	// Update is called once per frame
	void Update () {

	}

	public List<Item> getItems() {
		return items;
	}
}
