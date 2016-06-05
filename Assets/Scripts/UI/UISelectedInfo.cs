using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UISelectedInfo : MonoBehaviour {

	public Text unitName;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetUnitName(string name) {
		unitName.text = "Selected: " + name;
	}
}
