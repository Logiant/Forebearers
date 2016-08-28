using UnityEngine;
using System.Collections;

/*
 * The main entity script is the master script for each ingame object
 * Each component (movable, inventory, etc) is attached to the MainEntityScript
 * 
 * Any mouse action done by the user gets passed to this script by the UI
 * 
 */


public class MainEntityScript : MonoBehaviour {

	public string unitName;

	GameObject target;
	Constants.ACTION STATE = Constants.ACTION.NONE;

	//components the unit may or may not have
	Movable movable;
	Inventory inventory;
	Diplomacy diplomacy;

	// Use this to get references
	void Awake() {
		movable = GetComponent<Movable> ();
		inventory = GetComponent<Inventory> ();
		diplomacy = GetComponent<Diplomacy> ();
//		ui = GameObject.FindGameObjectWithTag ("UImaster").GetComponent<UIMaster>();
	}


	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		switch (STATE) {
		default:
		case Constants.ACTION.NONE:
		case Constants.ACTION.BECKON:
			break; //do nothing special
		case Constants.ACTION.FOLLOW:    /*TODO Modify NavMeshAgen StoppingDistance per action */
		case Constants.ACTION.ATTACK:
		case Constants.ACTION.BRIBE:
		case Constants.ACTION.TALK:
		case Constants.ACTION.TRADE:
			//moce toward the constant consistently
			moveTo (target.transform.position); //TODO this should happen every Nth frame
			break;


		}
	}

	public void setAction(Constants.ACTION action, MainEntityScript target) {
		if (target.validAction (action)) {
			Debug.Log (unitName + " do " + action + " to " + target.unitName);
			this.target = target.gameObject;
			moveTo (this.target.transform.position);
			STATE = action;
		} else {
			Debug.Log (unitName + " cannot " + action + " to " + target.unitName);
		}
	}


	public bool validAction(Constants.ACTION action) {
		//switch through the existing components to see if an action is possible
		switch (action) {
		default:
		case Constants.ACTION.NONE:
			//we should never get here
			Debug.Log("Action " + action + " IS NOT DEFINED!!");
			return false;
		case Constants.ACTION.FOLLOW:
		case Constants.ACTION.BECKON:
			return (movable != null); //can't follow buildings...
		case Constants.ACTION.BRIBE:
		case Constants.ACTION.TALK:
			return (diplomacy != null); //requires diplomatic information
		case Constants.ACTION.ATTACK:
			//nothing to do.. yet... requires HP?
			return true;
		case Constants.ACTION.TRADE:
			return (inventory != null);
		}
	}



		
	public void rightClick(RaycastHit hitInfo) { //called on right click by UIMaster
		STATE = Constants.ACTION.NONE;
//		if (owner != PLAYERS.PLAYER_1) {
//			return;
//		}
		GameObject go = hitInfo.collider.gameObject;

		if (go.CompareTag ("Terrain")) { //go to some point, set rally point
			moveTo (hitInfo.point);
		} else {
			//move toward the target
			Ray r = new Ray(transform.position, hitInfo.point - transform.position);
			RaycastHit info;
			if (hitInfo.collider.Raycast(r, out info, Mathf.Infinity)) {
				
				Vector3 newPos = hitInfo.point + info.normal*0.5f;
				newPos.y = Terrain.activeTerrain.SampleHeight (newPos);
				moveTo (newPos);

			} //else - you clicked yourself


		}/* else if (go.GetComponent<EntityScript> != null) { //interact with some entity
		
*/
	}

	public void setSelected(bool selected) {
		if (selected) {
	//		if (owner == 1) {
				this.GetComponent<MeshRenderer> ().material.color = (Color.green);
	//		} else {
	//			this.GetComponent<MeshRenderer> ().material.color = (Color.red);
	//		}
		} else {
			this.GetComponent<MeshRenderer> ().material.color = (Color.white);
		}
	}
		

	void moveTo(Vector3 position) {
		if (movable != null) {
			movable.setTarget (position);
		}
	}

	public Inventory getInventory() {
		return inventory;
	}

	public string getName() {
	//	if (player == owner) {
			return unitName;
	//	} else {
	//		return owner + "'s " + unitName;
	//	}
	}

}
