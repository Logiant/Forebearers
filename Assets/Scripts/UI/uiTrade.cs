using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class uiTrade : MonoBehaviour {

	Inventory[] traders;

	public Image[][] icons;

	public GameObject button;

	public Text costText;

	public Text ATrader, BTrader;

	int moveIndex = -1;
	int moveInv = -1;

	int tradeValue = 0;

	Stack<Trade> trades;

	// Use this for initialization
	void Start () {
		traders = new Inventory [2]; //only 2 way trades allowed
		icons = new Image[2][];
		icons [0] = new Image[0];
		icons [1] = new Image[0];

		trades = new Stack<Trade> ();
	}

	void updateCost() {
		costText.text = "Trade Cost: " + tradeValue + " gold";
		ATrader.text = traders [0].name + ": " + traders [0].gold + " gold";
		BTrader.text = traders [1].name + ": " + traders [1].gold + " gold";

	}


	public void moveItem(int callerInv, int callerIndex) {
		//dont bother moving "from" an empty space
		if (icons [callerInv][callerIndex].sprite != null && moveIndex == -1) {
			moveIndex = callerIndex;
			moveInv = callerInv;
		} else if (moveIndex != -1) {
			trades.Push (Inventory.Trade (traders [callerInv], callerIndex, traders [moveInv], moveIndex));
			int direction = 1; //trading from A to B
			if (callerInv == 0) { //trading from B to A
				direction = -1;
			}
			if (callerInv == moveInv) { //trading A-A or B-B
				direction = 0;
			}
			tradeValue += trades.Peek ().value * direction;
			updateCost();
			updateIcons ();
			moveIndex = -1;
			moveInv = -1;
		}
	}

	public void Confirm() {
		//TODO it may be useful to introduce a "family wealth" variable
		//individual units/buildings may have their own wealth, but it's tedious
		//to make sure each individual unit has enough money to buy what it needs
		//each family should have a shared "bank" composed of everyone's personal wealth
		//the challenge is deciding where to pull it from
		//For example, with building A, and units B, C, and D
		//let's say they each have 10 gold for a total of 40.
		//if D wants to buy an item for 26 gold, where does he take the other
		//16 from? Is it mostly-even from everyone? Does it depend on who is closest?
		//does it come from buildings first? Does all wealth pass through the "patriarch"?
		//maybe if we have "player" B, "spouce" C, and child D the money will come from the order
		// B, A, C, D -- or would the "player" have its money drained last, A, C, D, B?
		// it should probably be split evenly, with the "odd amount" of wealth coming from the purchaser
		// so in this case, it's 6 gold each + 2 from D. Then it just goes through a quick list to get
		//whatever remaining gold - A,D,C,B. It makes sense that the "player" would control a majority of the wealth
		//so take from them last. It doesn't really matter unless they get robbed. you can probably manually transfer gold as well


		if (traders [0].gold >= tradeValue && traders [1].gold >= -1 * tradeValue) {
			trades.Clear ();
			traders [0].gold -= tradeValue;
			traders [1].gold += tradeValue;
			closeWindow ();
		} else {
			costText.text = "Someone doesn't have enough gold!";
		}
	}

	public void closeWindow() {
		//undo all trades if they were not confirmed
		while (trades.Count > 0) {
			Inventory.Trade (trades.Pop ());
		}
		tradeValue = 0;
		gameObject.SetActive (false);
	}

	// Update is called once per frame
	void Update () {
		if (traders [0] != null) {
			bool dirty = false;
			for (int n = 0; n < traders.Length; n++) {
				dirty = dirty & traders [n].needsUpdate ();
			}
			if (dirty) {
				updateIcons ();
			}
		}
	}

	public void setTraders(Inventory A, Inventory B) {
		trades.Clear ();
		traders [0] = A;
		traders [1] = B;
		updateCost ();
		//delete all existing icons
		clear();
		//create new icons
		icons [0] = new Image [A.getCapacity ()];
		icons [1] = new Image [B.getCapacity ()];
		newIcons ();
		updateIcons ();
	}

	void clear() {
		for (int n = 0; n < icons.Length; n++) {
			for (int i = 0; i < icons [n].Length; i++) {
				GameObject.Destroy (icons [n][i].gameObject);
			}
		}
	}

	void newIcons() {
		for (int n = 0; n < icons.Length; n++) {
			for (int i = 0; i < icons [n].Length; i++) {
				icons [n] [i] = Instantiate (button).GetComponent<Image> ();
				icons [n] [i] = setupButton (icons [n] [i], i, n*250);
				int inv = n; int slot = i;
				icons [n][i].GetComponent<Button> ().onClick.AddListener (() => { moveItem (inv, slot); });
			}
		}
	}


	void updateIcons() {
		for (int n = 0; n < icons.Length; n++) {
			List<Item> items = traders [n].getItems ();
			for (int i = 0; i < icons [n].Length; i++) {
				if (items [i] != null) {
					icons [n] [i].sprite = items [i].getIcon();
				} else {
					icons [n] [i].sprite = null;
				}
			}
		}
	}

	Image setupButton(Image img, int i, float offset) {
		img.gameObject.transform.SetParent (gameObject.transform);
		img.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(offset + 30 + 55*(i%4), -60 - 55*(Mathf.Floor(i/4)));
		return img;
	}
}

