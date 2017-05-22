using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public GameObject ant;
	private GameObject[] ants;
	private int count;

	// Use this for initialization
	float time;

	void Start () {
		count = 0;
	}

	void Update(){
		if (count < 50) {
			if (Time.time < time + 1f) {
				time = Time.time;

				GameObject go = Instantiate (ant, Vector3.zero, Quaternion.identity) as GameObject;
				go.transform.Rotate (Vector3.up, Random.Range (0, 360));
				Vector3 fwd = go.transform.TransformDirection (Vector3.forward);
				go.transform.position += fwd * 3;
				count++;
			}
		}
	}
}