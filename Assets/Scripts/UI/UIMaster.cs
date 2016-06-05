using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class UIMaster : MonoBehaviour {

	public EntityScript selected;

	UISelectedInfo selectedUi;

	// Use this for initialization
	void Start () {
		selectedUi = GetComponentInChildren<UISelectedInfo> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (!EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButtonDown (0)) { //left click
			selectUnit();
		}

		if (!EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButtonDown (1)) { //left click
			rightClick();
		}

		string name = "None";

		if (selected != null) {
			name = selected.unitName;
		}

		selectedUi.SetUnitName (name);

	}


	void selectUnit() {
		if (selected != null) {
			selected.setSelected (false);
			selected = null; //who even likes sticky selection?
		}
		RaycastHit hitInfo;

		if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hitInfo, Mathf.Infinity)) {
			EntityScript us = hitInfo.collider.gameObject.GetComponent<EntityScript> ();
			if (us != null) {
				selected = us;
				us.setSelected (true);
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
}
