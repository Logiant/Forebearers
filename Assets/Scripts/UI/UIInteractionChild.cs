using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIInteractionChild : MonoBehaviour {

	public List<GameObject> children;
	public UIInteractionBase parent;

	public void Start() {
		Collapse ();
	}


	public void Collapse() {
		//collapse all of your children options
		foreach (GameObject go in children) {
			go.SetActive (false);
		}
	}

	public void Expand() {
		//expand all of your options!
		parent.CollapseExcept(this);
		//parent.closeAllButMe(this)
		foreach (GameObject go in children) {
			go.SetActive (true);
		}
	}
}
