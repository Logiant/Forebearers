using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class UIInteractionBase : MonoBehaviour {

	public List<UIInteractionChild> children;

	public void MouseEnter() {
		Debug.Log ("Entered! " + gameObject.name);
	}


	public void MouseExit() {
		Debug.Log ("Exited! " + gameObject.name);
	}

	public void Collapse() {
		//close all children
		foreach (UIInteractionChild go in children) {
			go.Collapse ();
		}
	}

	public void CollapseExcept(UIInteractionChild exception) {
		foreach (UIInteractionChild go in children) {
			if (go != exception) {
				go.Collapse ();
			}
		}
	}

}
