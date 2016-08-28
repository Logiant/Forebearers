using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Diplomacy : MonoBehaviour {

	Dictionary<int, double> relations;
	//add a RELATIONS script/static var that shows hosue inter-relations
	//

	// Use this for initialization
	void Start () {
		relations = new Dictionary<int, double> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public double getRelationship(Diplomacy other) {
		int uniqueId = other.gameObject.GetInstanceID ();
		double rel = 0.0f;
		if (relations.ContainsKey (uniqueId)) {
			rel = relations[uniqueId];
		} else {
			rel = 0.0f; //TODO replace with value from RELATIONS table
			relations.Add (uniqueId, rel);
		}
		return rel;
	}


}
