using UnityEngine;
using System.Collections;


public class UnitScript : MonoBehaviour {

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
		if (selected) {
			transform.Rotate (new Vector3 (0, 30 * Time.deltaTime, 0));
		} else {
			transform.rotation = new Quaternion ();
		}
	}

	public void rightClick(RaycastHit hitInfo) { //called on right click by UIMaster
		GameObject go = hitInfo.collider.gameObject;

		if (go.CompareTag ("Terrain")) { //go to some point, set rally point

			if (movable != null) {
				movable.setTarget (hitInfo.point);
			} //else if (Rally != null) {


			//}
		}/* else if (go.CompareTag ("Building")) { //enter some building
		

		} else if (go.CompareTag("Unit")) { //interact with some unit (enemy, merchant, friend, equipment, etc)
			
			
		} */
	}

	public void setSelected(bool selected) {
		this.selected = selected;

	}
		
}
