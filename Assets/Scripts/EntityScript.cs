using UnityEngine;
using System.Collections;


public class EntityScript : MonoBehaviour {

	public string unitName;

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
		GameObject go = hitInfo.collider.gameObject;

		if (go.CompareTag ("Terrain")) { //go to some point, set rally point

			if (movable != null) {
				movable.setTarget (hitInfo.point);
			} //else if (Rally != null) {


			//}
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
		
}
