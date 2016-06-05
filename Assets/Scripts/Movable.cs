using UnityEngine;
using System.Collections;

public class Movable : MonoBehaviour {


	float speed = 2.1f; // m/s
	Vector3 targetPosition;

	// Use this for initialization
	void Start () {
		targetPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate() {
		Vector3 distance = (targetPosition - transform.position);

		float dt = Time.fixedDeltaTime;

		if (distance.magnitude < speed * dt) {
			transform.position = targetPosition;
		} else {
			transform.position = transform.position + distance.normalized * speed * dt;
		}

	}


	public void setTarget(Vector3 target) {
		targetPosition = target + new Vector3(0, 0.5f, 0); //don't sink into the ground
	}

	public void stop() {
		setTarget (transform.position);
	}
}
