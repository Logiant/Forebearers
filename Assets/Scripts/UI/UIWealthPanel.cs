using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class UIWealthPanel : MonoBehaviour {

	public Text wealthText;

	public void setWealth(int gold) {
		wealthText.text = "Gold: " + gold;
	}
}
