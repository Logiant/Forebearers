using UnityEngine;
using System.Collections;

public class EntityScript : MonoBehaviour {

	public string unitName;

	public int owner = PLAYERS.PLAYER_1;

	bool selected;

	//components the unit may or may not have
	Movable movable;

	// Use this for initialization
	void Start () {
		movable = GetComponent<Movable> ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void rightClick(RaycastHit hitInfo) { //called on right click by UIMaster
		if (owner != PLAYERS.PLAYER_1) {
			return;
		}
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
		this.selected = selected;
		if (selected) {
			this.GetComponent<MeshRenderer> ().material.color = (Color.green);
		} else {
			this.GetComponent<MeshRenderer> ().material.color = (Color.white);
		}
	}
		

	void moveTo(Vector3 position) {
		if (movable != null) {
			movable.setTarget (position);
		}
	}

	public string getName(int player) {
		if (player == owner) {
			return unitName;
		} else {
			return "Player " + owner + "'s unit";
		}
	}

}
