using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntController : MonoBehaviour {

	// Use this for initialization
	public GameObject pheromone;
	public float rotateFactor;
	public float pheromoneFactor;
	public float speed;
	public int rotation;

	void Start () {
		InvokeRepeating("RandomRotate", 2.0f, rotateFactor);
		InvokeRepeating("AddPheronomone", 0f, pheromoneFactor);
	}
		
	// Update is called once per frame
	void Update () {
		Vector3 fwd = transform.TransformDirection(Vector3.forward);
		transform.position += fwd * speed;
	}
//		
	void RandomRotate(){
		transform.Rotate (Vector3.up, Random.Range (-rotation, rotation));
	}
	void AddPheronomone(){
		GameObject go = Instantiate (pheromone, transform.position, Quaternion.identity) as GameObject;
	}
		
	void OnTriggerEnter(Collider target) {
		if (target.gameObject.CompareTag ("Pheromone")) {
			if (Random.value > 0.4) {
				Vector3 targetDir = target.transform.position - transform.position;
				float angle = Vector3.Angle (targetDir, transform.forward);

				if (angle < 70) {
					Vector3 direction = (target.transform.position - transform.position).normalized;
					Quaternion rotation = Quaternion.LookRotation (direction);
					transform.rotation = rotation;
				}
			}
		}
	}
}
