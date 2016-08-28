using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerDataScript : MonoBehaviour {

	int gold;

	// Use this for initialization
	void Start () {
		gold = 1000;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void addGold(int amt) {
		gold += amt;
	}

	public int getGold() {
		return gold;
	}
}
