using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PheromoneScript : MonoBehaviour {

	public float lifeTime;

	// Use this for initialization
	void Start () {
		Invoke("SelfDestroy", lifeTime);
	}

	void SelfDestroy(){
		Destroy (gameObject);
	}

//	void OnTriggerEnter(Collider target) {
//		if (target.gameObject.CompareTag ("Pheromone")) {
//			float dist = Vector3.Distance(target.transform.position, transform.position);
//			if (dist < 5f) {
//				if (GetInstanceID() < target.gameObject.GetInstanceID()) {
//					Destroy (gameObject);
//				}
//			}
//			if (GetInstanceID() < target.gameObject.GetInstanceID()) {
//				Destroy (gameObject.GetComponent<SphereCollider>());				
//				Destroy (gameObject.GetComponent<Rigidbody>());				
//
//				SphereCollider colliderTarget = gameObject.GetComponent<SphereCollider> ();
//				colliderTarget.radius +=10;
//			}
//		}
//	}
}
