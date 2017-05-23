using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PheromoneScript : MonoBehaviour {

	public float lifeTime;

	void Start () {
		Invoke("SelfDestroy", lifeTime);
	}

	void SelfDestroy(){
		Destroy (gameObject);
	}
}
