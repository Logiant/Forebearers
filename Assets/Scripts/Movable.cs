using UnityEngine;
using System.Collections;

public class Movable : MonoBehaviour {


	float speed = 2.1f; // m/s
	Vector3 targetPosition;
	NavMeshAgent nav;

	float verticalOffset = 0.5f;

	// Use this for initialization
	void Start () {
		targetPosition = transform.position;
		nav = GetComponent<NavMeshAgent> ();
		nav.destination = targetPosition;
		verticalOffset = GetComponent<Collider> ().bounds.extents.y / 2f;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate() {
		if (nav == null) {
			Debug.Log ("No NavMeshAgent Detected!");
			Vector3 distance = (targetPosition - transform.position);

			float dt = Time.fixedDeltaTime;

			if (distance.magnitude < speed * dt) {
				transform.position = targetPosition;
			} else {
				transform.position = transform.position + distance.normalized * speed * dt;
			}
		}
	}


	public void setTarget(Vector3 target) {
		targetPosition = target + new Vector3(0, verticalOffset, 0); //don't sink into the ground
		if (nav != null) {
			nav.destination = targetPosition;
		}

	}

	public void stop() {
		if (nav != null) {
			nav.destination = transform.position;
		}
	}
}
