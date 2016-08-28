using UnityEngine;
using UnityEngine.UI;

public abstract class Item {
	
	protected Sprite icon;
	protected string name;
	protected int value = 1;
	//description
	//effects?!?! - modify health/damage/armor/whatever?

	public Item(){
		
	}
		
	public Item(string name, Sprite icon) {
		this.name = name;
		this.icon = icon;
	}
		
	public Sprite getIcon() {
		return icon;
	}

	public string getName() {
		return name;
	}

	public int getValue() {
		return value;
	}


}
