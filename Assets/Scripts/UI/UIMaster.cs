using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

/*
 * The UIMaster class acts as the interface between game data and the user interface
 * It stores a reference to the currently selected object and sends left/right click inputs to it
 * Game objects are required to send any updates to UIMaster. It doesn't ask anything for updates
 * Any objects that need to send information to UImaster can find it on the canvas
 * 
 */
public class UIMaster : MonoBehaviour {

	//currently selected model
	public MainEntityScript selected;

	//Hooks to lower level UI
	UISelectedInfo selectedUI;
	uiInventory inventoryUI;
	UIWealthPanel wealthUI;
	UIaction actionUI;
	uiTrade tradeUI;

	//flags for UI state
	bool showInventory = false;

	//Hooks to game data
	PlayerDataScript playerData;

	// Use this to grab references
	void Awake () {
		//grab the child UI objects
		selectedUI = GetComponentInChildren<UISelectedInfo> ();
		inventoryUI = GetComponentInChildren<uiInventory> ();
		wealthUI = GetComponentInChildren<UIWealthPanel> ();
		actionUI = GetComponentInChildren<UIaction> ();
		tradeUI = GetComponentInChildren<uiTrade> ();
		//grab game data objects
		playerData = GetComponentInChildren<PlayerDataScript> ();

	}

	void Start() {
		//initialization
		wealthUI.setWealth (playerData.getGold ());
		tradeUI.closeWindow();
		inventoryUI.gameObject.SetActive(false);
		selectedUI.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {

		if (!EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButtonDown (0)) { //left click
			Constants.ACTION action = actionUI.getCurrentAction();

			if (selected == null || action == Constants.ACTION.NONE) {
				selectUnit();
			} else {
				//do a raycast
				RaycastHit hitInfo = new RaycastHit();
				if (selected != null && doRaycast(ref hitInfo)) { //TODO check for action validity and show UI warning?!
					MainEntityScript target = hitInfo.collider.gameObject.GetComponent<MainEntityScript> ();
					if (target != null) {
						selected.setAction (action, hitInfo.collider.gameObject.GetComponent<MainEntityScript> ());
						bool showTrade = (action == Constants.ACTION.TRADE);
						if (showTrade) { //TODO this may need to be cleaned up - can it be done independently of the player?
							Inventory IA, IB;
							IA = selected.GetComponent<Inventory> ();
							IB = target.GetComponent<Inventory> ();
							if (IA != null && IB != null) {
								tradeUI.setTraders (IA, IB);
								inventoryUI.gameObject.SetActive (false);
								tradeUI.gameObject.SetActive (true);
							} else {
								tradeUI.closeWindow ();
								inventoryUI.gameObject.SetActive (showInventory);
							}
						} else {
							tradeUI.closeWindow ();
						}
					}
				}
			}
		}

		if (!EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButtonDown (1)) { //left click
			rightClick();
		}

		if (Input.GetButtonUp("inventory")) {
			showInventory = !inventoryUI.gameObject.activeSelf;
			//never show the trade UI and the inventory UI at the same time!
			inventoryUI.gameObject.SetActive(showInventory && !tradeUI.gameObject.activeSelf);
		}

	}


	bool doRaycast(ref RaycastHit hitInfo) {
		return Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hitInfo, Mathf.Infinity);
	}

	void selectUnit() {
		if (selected != null) { //something was selected
			deselect();
		}
		RaycastHit hitInfo = new RaycastHit();

		if (doRaycast(ref hitInfo)) {
			MainEntityScript us = hitInfo.collider.gameObject.GetComponent<MainEntityScript> ();

			if (us != null) { //new selection!
				selected = us;
				us.setSelected (true);
				inventoryUI.gameObject.SetActive(showInventory);
				inventoryUI.setInventory(us.getInventory());
				selectedUI.gameObject.SetActive (true);
				selectedUI.SetUnitName (selected.getName ());
			}
		}

	}

	void rightClick() {
		if (selected != null) {
			
			RaycastHit hitInfo;

			if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hitInfo, Mathf.Infinity)) {
				selected.rightClick (hitInfo);
			}
		}
	}

	void deselect() {
		selected.setSelected (false);
		selected = null; //who even likes sticky selection?
		tradeUI.closeWindow();
		inventoryUI.gameObject.SetActive(false);
		selectedUI.gameObject.SetActive (false);
	}
}
