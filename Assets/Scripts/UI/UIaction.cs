using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class UIaction : MonoBehaviour {

	Constants.ACTION CURRENT_ACTION = Constants.ACTION.NONE;
	public GameObject button;

	GameObject[] buttons = new GameObject[Constants.ACTIONSTR.Length];

	// Use this for initialization
	void Start () {
		for (int i = 1; i < buttons.Length; i++) {
			GameObject b = Instantiate (button);
			b.GetComponent<RectTransform>().SetParent (transform);
			Vector3 newPos = new Vector3(25 + (50*i), 0, 0);
			b.GetComponent<RectTransform>().localPosition = newPos;
			b.GetComponentInChildren<Text> ().text = Constants.ACTIONSTR [i];

			b.GetComponent<Button>().onClick.AddListener(delegate {Action(b); });

			buttons [i] = button;
		}

	}

	public void Action(GameObject caller) {
		string txt = (caller.GetComponentInChildren<Text> ().text);
		uint index = 100000;
		for (uint i = 0; i < Constants.ACTIONSTR.Length; i++) {
			if (Constants.ACTIONSTR [i] == txt) {
				index = i;
			}
		}
		CURRENT_ACTION = (Constants.ACTION)index; //explicit casting
	}


	public Constants.ACTION getCurrentAction() {
		Constants.ACTION retVal = CURRENT_ACTION;
		CURRENT_ACTION = Constants.ACTION.NONE; //Reset
		return retVal;
	}
}
