using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Inventory))]
public class Processes : MonoBehaviour {

	Inventory I;

	bool loop = false;

	//cooldown/production time info?

	// Use this for initialization
	void Start () {
		I = GetComponent<Inventory> ();
	}
	
	// Update is called once per frame
	void Update () {
		//if loop && cooldown == 0
			//production
		//else if cooldown ??? 0
			//disable/enable?
	}


	void Produce() {
		//if I has the materials
		//remove items from I
		//do cooldown/production time

	}
}
